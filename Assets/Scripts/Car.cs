using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;

    public float maxSpeed = 10000f;
    private float acceleration = 400f;

    private CarState state = CarState.Stoping;
    private Game game;
    public CrashTriggerHandler crashTrigger;

    void Start()
    {
        game = Camera.main.GetComponent<Game>();
    }

    void Update()
    {
        if (crashTrigger.IsCrashed())
        {
            game.exitGame();
        }
    }

    void FixedUpdate()
    {
        JointMotor2D backMotor = backWheel.motor;
        JointMotor2D frontMotor = frontWheel.motor;

        float motorSpeed = backMotor.motorSpeed;

        if (state == CarState.Stoping)
        {
            if (motorSpeed < 0)
            {
                //moving forward
                motorSpeed = Mathf.Clamp(motorSpeed + acceleration * Time.deltaTime, -maxSpeed, 0);
            }
            else
            {
                //moving backward
                motorSpeed = Mathf.Clamp(motorSpeed - acceleration * Time.deltaTime, 0, maxSpeed);
            }
        }
        else if (state == CarState.MovingForward)
        {
            motorSpeed = Mathf.Clamp(motorSpeed - acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }
        else if (state == CarState.MovingBackward)
        {
            motorSpeed = Mathf.Clamp(motorSpeed + acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }

        backMotor.motorSpeed = motorSpeed;
        frontMotor.motorSpeed = motorSpeed;

        backWheel.motor = backMotor;
        frontWheel.motor = frontMotor;
    }

    public void moveForward()
    {
        state = CarState.MovingForward;
    }

    public void moveBackward()
    {
        state = CarState.MovingBackward;
    }

    public void stopMoving()
    {
        state = CarState.Stoping;
    }

    private enum CarState
    {
        MovingForward,
        MovingBackward,
        Stoping
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Coin")
        {
            game.increaseCoin();
            Destroy(trigger.gameObject);
        }
        else if (trigger.gameObject.tag == "Finish")
        {
            game.exitGame();
        }
    }
}