using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("settings")]
    public float MoveSensitivity = 1;
    public float RotateSensitivity = 60;

    [Header("Bindings")]
    [SerializeField] Speedometer _speedometer;
    [SerializeField] Timer _timer;
    [SerializeField] MainStoryController _mainStoryController;

    [Header("Runtime")]
    public int CheckpointPassed = 0;
    public int CollideTimesPerLap = 0;
    int expectedNextInstruction = 1;

    short lastButton = 0;
    bool useOnlyOneButton = true;




    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.CurrentCharacter.name.Contains("00"))
        {
            _mainStoryController.StartInstruction(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_mainStoryController.IsStoryShowing)
        {
            return;
        }

        // turn
        if(Input.GetMouseButton(0))
        {
            transform.Rotate(0, 0, RotateSensitivity * Time.deltaTime);

            if(lastButton != 0)
            {
                useOnlyOneButton = false;
            }
            lastButton = 0;
        }
        else if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, 0, -RotateSensitivity * Time.deltaTime);

            if(lastButton != 1)
            {
                useOnlyOneButton = false;
            }
            lastButton = 1;
        }

        // move
        float speed = _speedometer.Speed;
        transform.Translate(Vector3.left * MoveSensitivity * speed * .01f * Time.deltaTime);
        // print(speed);

        // unlock speed
        // SpeedLock = 100f;
    }


    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        _speedometer.IsSpeedLockActive = true;
        CollideTimesPerLap++;

        PlayerPrefs.SetFloat("STAT_Collide", PlayerPrefs.GetFloat("STAT_Collide", 0) + 1);
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        _speedometer.IsSpeedLockActive = true;
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        _speedometer.IsSpeedLockActive = false;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int checkpointId = int.Parse(other.name.Replace("Checkpoint (", "").Replace(")", ""));

            if(checkpointId == expectedNextInstruction)
            {
                expectedNextInstruction++;

                // LAP!
                if(expectedNextInstruction > 8)
                {
                    expectedNextInstruction = 1;

                    float lapTime = _timer.lapTime;
                    float bestLapTime = PlayerPrefs.GetFloat("STAT_BestLapTime", float.MaxValue);
                    if(lapTime < bestLapTime)
                    {
                        PlayerPrefs.SetFloat("STAT_BestLapTime", lapTime);
                    }
                    if(lapTime <= 15)
                    {
                        GameManager.Instance.AchievementComplete("ACHI_DEJA_VU");
                    }
                    if(useOnlyOneButton)
                    {
                        GameManager.Instance.AchievementComplete("ACHI_08A");
                    }
                    if(CollideTimesPerLap == 0)
                    {
                        GameManager.Instance.AchievementComplete("ACHI_SAFE");
                    }
                    useOnlyOneButton = false;
                    _timer.NewLap();
                }
            }
            else
            {
                Debug.Log($"錯誤的 Checkpoint，應該要是 {expectedNextInstruction} 但是通過 {checkpointId}");
                return;
            }

            CheckpointPassed++;

            // Instruction
            if(GameManager.Instance.CurrentCharacter.name.Contains("00"))
            {                
                if(checkpointId == 5)
                    _mainStoryController.StartInstruction(2);
                else if(checkpointId == 8)
                    _mainStoryController.StartInstruction(3);
            }
        }
        else if(other.CompareTag("Exit"))
        {
            _timer.StopTimer();
            GameManager.Instance.MainGameEnded(CheckpointPassed, _timer.time);
        }
        else if(other.CompareTag("Circulation"))
        {
            _timer.StartTimer();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Circulation"))
        {
            _timer.StopTimer();
        }
    }
}
