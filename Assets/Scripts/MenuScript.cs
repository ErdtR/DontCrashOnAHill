using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	public GameObject levelChanger;
	public GameObject exitPanel;

	void Update() {
		if (levelChanger.activeSelf == true && Input.GetKeyDown (KeyCode.Escape)) {
			levelChanger.SetActive (false);
		} else if (exitPanel.activeSelf == false && Input.GetKeyDown (KeyCode.Escape)) {
			exitPanel.SetActive (true);
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			exitPanel.SetActive (false);
		}
	}

	public void OnClickStart() {
		levelChanger.SetActive (true);
	}

	public void OnClickExit() {
		Application.Quit ();
	}

	public void OnDontExit() {
		exitPanel.SetActive (false);
	}

	public void SelectLevel(){
		SceneManager.LoadScene (1);
	}
}
