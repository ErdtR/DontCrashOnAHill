using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour {

    /*WheelJoint2D[] wheelJoints;

	private float acceleration = 400f;
	private float deacceleration = -100f;
	public float brakeForce = 3000f;
	private float gravity = 9.8f;
	private float angleCar = 0;
	public bool grounded = false;
	public LayerMask map;
	public Transform bwheel;
	private int coinsInt = 0;
	public Text coinsText;
    public CrashTriggerHandler crashHandler;

	public ClickScript[] ControlCar;*/

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel; 

    public float maxSpeed = 10000f;
    private float acceleration = 400f;

    private CarState state = CarState.Stoping;

	// Use this for initialization
	/*void Start () {

		wheelJoints = gameObject.GetComponents<WheelJoint2D>();
		backWheel = wheelJoints[1].motor;
		frontWheel = wheelJoints[0].motor;
	}*/

	/*void Update()
	{
        if (crashHandler.IsCrashed())
        {
            FinishGame();
        }
		coinsText.text = coinsInt.ToString();
		grounded = Physics2D.OverlapCircle(bwheel.transform.position, 0.50f, map);
	}*/

	/*void FixedUpdate() {

		frontWheel.motorSpeed = backWheel.motorSpeed;

		angleCar = transform.localEulerAngles.z;

		if (angleCar >= 180)
		{
			angleCar = angleCar - 360;
		}

		if (grounded == true)
		{
			if (ControlCar[0].clickedIs == true)
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, maxBackSpeed);
			}
			else if ((backWheel.motorSpeed < 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar < 0))
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, maxSpeed, 0);
			}
			if ((ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0) || (ControlCar[0].clickedIs == false && backWheel.motorSpeed == 0 && angleCar > 0))
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angleCar / 2)) * Time.deltaTime, 0, maxBackSpeed);
			}
		}
		else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed < 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration * Time.deltaTime, maxSpeed, 0);
		}
		else if (ControlCar[0].clickedIs == false && backWheel.motorSpeed > 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxBackSpeed);
		}

		if (ControlCar[1].clickedIs == true && backWheel.motorSpeed > 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);
		}
		else if (ControlCar[1].clickedIs == true && backWheel.motorSpeed < 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxSpeed, 0);
		}

		wheelJoints[1].motor = backWheel;
		wheelJoints[0].motor = frontWheel;
	}*/

	/*void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Coin") {
			coinsInt++;
			Destroy (trigger.gameObject);
		} else if (trigger.gameObject.tag == "Finish") {
            FinishGame();
		}
	}*/

    /*private void FinishGame()
    {
        SceneManager.LoadScene(0);
    }*/

    void FixedUpdate()
    {
        JointMotor2D backMotor = backWheel.motor;
        JointMotor2D frontMotor = frontWheel.motor;

        float motorSpeed = backMotor.motorSpeed;

        if(state == CarState.Stoping)
        {
            if(motorSpeed < 0)
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
        else if(state == CarState.MovingForward)
        {
            motorSpeed = Mathf.Clamp(motorSpeed - acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }
        else if(state == CarState.MovingBackward)
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
}