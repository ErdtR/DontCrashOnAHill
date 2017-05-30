using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject garagePanel;
    public GameObject locationPanel;
    public Button soundButton;
    public Button languageButton;

    public Sprite onSoundSprite;
    public Sprite offSoundSprite;
    public Sprite enLanguageSprite;
    public Sprite ukrLanguageSprite;

    public Text playText;
    public Text settingTitleText;
    public Text settingSoundText;
    public Text settingLanguageText;
    public Text settingBackText;
    public Text exitQuestionText;
    public Text exitYesText;
    public Text exitNoText;
    public Text garageTitleText;
    public Text garageBikeText;
    public Text garageCarText;
    public Text garageJeepText;
    public Text garageBackText;
    public Text locationTitleText;
    public Text locationFieldText;
    public Text locationForestText;
    public Text locationDesertText;
    public Text locationBackText;

    void Start()
    {
        ApplySettings();
    }

	void Update() {
        if((locationPanel.activeSelf || settingsPanel.activeSelf || garagePanel.activeSelf || exitPanel.activeSelf)
            && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
	}

	public void OnClickPlay()
    {
		locationPanel.SetActive(true);
	}

    public void OnClickSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OnClickGarage()
    {
        garagePanel.SetActive(true);
    }

	public void OnClickExit()
    {
		Application.Quit ();
	}

    public void CloseAllPanels()
    {
        locationPanel.SetActive(false);
        settingsPanel.SetActive(false);
        garagePanel.SetActive(false);
    }

	public void OnDontExit()
    {
		exitPanel.SetActive (false);
	}

	public void SelectLevel(int level)
    {
		SceneManager.LoadScene(level);
	}

    public void ChooseCar(string carType)
    {
        PlayerPrefs.SetString("CarType", carType);
        CloseAllPanels();
    }

    public void ChangeSoundSetting()
    {
        if(PlayerPrefs.GetString("Sound") == "" || PlayerPrefs.GetString("Sound") == "On")
        {
            PlayerPrefs.SetString("Sound", "Off");
        }
        else if(PlayerPrefs.GetString("Sound") == "Off")
        {
            PlayerPrefs.SetString("Sound", "On");
        }

        ApplySettings();
    }

    public void ChangeLanguageSetting()
    {
        if (PlayerPrefs.GetString("Language") == "" || PlayerPrefs.GetString("Language") == "En")
        {
            PlayerPrefs.SetString("Language", "Ukr");
        }
        else if (PlayerPrefs.GetString("Language") == "Ukr")
        {
            PlayerPrefs.SetString("Language", "En");
        }

        ApplySettings();
    }

    private void ApplySettings()
    {
        if (PlayerPrefs.GetString("Sound") == "" || PlayerPrefs.GetString("Sound") == "On")
        {
            AudioListener.volume = 1.0f;
            soundButton.image.sprite = onSoundSprite;
        }
        else if(PlayerPrefs.GetString("Sound") == "Off")
        {
            AudioListener.volume = 0.0f;
            soundButton.image.sprite = offSoundSprite;
        }

        if (PlayerPrefs.GetString("Language") == "" || PlayerPrefs.GetString("Language") == "En")
        {
            languageButton.image.sprite = enLanguageSprite;
            setEng();
        }
        else if (PlayerPrefs.GetString("Language") == "Ukr")
        {
            languageButton.image.sprite = ukrLanguageSprite;
            setUkr();
        }

    }

    private void setEng()
    {
        playText.text = "Play";
        settingTitleText.text = "Settings";
        settingSoundText.text = "Sound";
        settingLanguageText.text = "Language";
        settingBackText.text = "Back";
        exitQuestionText.text = "Do you really want to exit?";
        exitYesText.text = "Yes";
        exitNoText.text = "No";
        garageTitleText.text = "Garage";
        garageBikeText.text = "Bike";
        garageCarText.text = "Car";
        garageJeepText.text = "Jeep";
        garageBackText.text = "Back";
        locationTitleText.text = "Location";
        locationFieldText.text = "Field";
        locationForestText.text = "Forest";
        locationDesertText.text = "Desert";
        locationBackText.text = "Back";
    }

    private void setUkr()
    {
        playText.text = "Грати";
        settingTitleText.text = "Налаштування";
        settingSoundText.text = "Музика";
        settingLanguageText.text = "Мова";
        settingBackText.text = "Повернутись";
        exitQuestionText.text = "Справді хочете вийти з гри?";
        exitYesText.text = "Так";
        exitNoText.text = "Ні";
        garageTitleText.text = "Гараж";
        garageBikeText.text = "Мотоцикл";
        garageCarText.text = "Машина";
        garageJeepText.text = "Джип";
        garageBackText.text = "Повернутись";
        locationTitleText.text = "Локація";
        locationFieldText.text = "Поле";
        locationForestText.text = "Ліс";
        locationDesertText.text = "Пустеля";
        locationBackText.text = "Повернутись";
    }
}
