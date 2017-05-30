using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Car bike, car, jeep;
    public ClickScript gasPedal;
    public ClickScript breakPedal;

    public GameObject smogPanel;
    public GameObject pausePanel;

    public Text gasText;
    public Text breakText;
    public Text resumeText;
    public Text exitText;
    public Text coinText;

    private Car currentCar;
    private bool isPaused = false;
    private int coins;




	void Start ()
    {
        if(PlayerPrefs.GetString("CarType") == "")
        {
            PlayerPrefs.SetString("CarType", "Car");
            car = Instantiate(car, new Vector3(0, 4, 0), Quaternion.identity);
        }
        else if(PlayerPrefs.GetString("CarType") == "Car")
        {
            car = Instantiate(car, new Vector3(0, 4, 0), Quaternion.identity);
        }
        else if(PlayerPrefs.GetString("CarType") == "Bike")
        {
            car = Instantiate(bike, new Vector3(0, 4, 0), Quaternion.identity);
        }
        else if(PlayerPrefs.GetString("CarType") == "Jeep")
        {
            car = Instantiate(jeep, new Vector3(0, 4, 0), Quaternion.identity);
        }

        coins = PlayerPrefs.GetInt("Coins");
        
        this.gameObject.GetComponent<SmoothCamera>().target = car.transform;
        ApplySettings();
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

        coinText.text = coins.ToString();
	}

    public void pauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            smogPanel.SetActive(true);
            pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            isPaused = false;
            smogPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void exitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private void ApplySettings()
    {

        if (PlayerPrefs.GetString("Language") == "" || PlayerPrefs.GetString("Language") == "En")
        {
            setEng();
        }
        else if (PlayerPrefs.GetString("Language") == "Ukr")
        {
            setUkr();
        }

    }

    private void setEng()
    {
        gasText.text = "Gas";
        breakText.text = "Break";
        resumeText.text = "Resume";
        exitText.text = "Exit";
    }

    private void setUkr()
    {
        gasText.text = "Газ";
        breakText.text = "Гальмо";
        resumeText.text = "Відновити";
        exitText.text = "Вийти";
    }

    public void increaseCoin()
    {
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
    }
}
