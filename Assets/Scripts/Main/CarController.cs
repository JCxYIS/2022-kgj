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

    [Header("Runtime")]
    public int CheckpointPassed = 0;
    public int CollideTimes = 0;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // turn
        if(Input.GetMouseButton(0))
        {
            transform.Rotate(0, 0, RotateSensitivity * Time.deltaTime);
        }
        else if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, 0, -RotateSensitivity * Time.deltaTime);
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
        CollideTimes++;
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
            CheckpointPassed++;
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
