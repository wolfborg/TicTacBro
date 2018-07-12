using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour
{
	public GameObject textThing;

	// Use this for initialization
	private void Start() {
		updateText("Welcome");
		

	}

	// Update is called once per frame
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void updateText(string newText) {
		textThing.GetComponent<Text>().text = newText;
	}

	public void changeScene(string newScene) {
		SceneManager.LoadScene(newScene);
	}
}
