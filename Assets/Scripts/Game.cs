using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Car bike, car, jeep;
    public ClickScript gasPedal;
    public ClickScript breakPedal;

    private Car currentCar;


	void Start ()
    {
        car = Instantiate(jeep, new Vector3(0, 4, 0), Quaternion.identity);
        this.gameObject.GetComponent<SmoothCamera>().target = car.transform;
    }
	
	void Update ()
    {
        if (gasPedal.clickedIs)
        {
            car.moveForward();
        }
        else if(breakPedal.clickedIs)
        {
            car.moveBackward();
        }
        else
        {
            car.stopMoving();
        }
	}
}
