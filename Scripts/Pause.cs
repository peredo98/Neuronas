using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public static int load = 0;

	public GameObject Player;

	public static String gameName;

	public int skybox;

	private bool isPaused = false;
	// Use this for initialization
	void Start () {
		if(skybox == 0){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundBlanco");
		}
		if(skybox == 1){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundGris");
		}
		if(skybox == 2){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundNegro");
		}
		if(skybox == 3){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundSky");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(load == 2){
			Load();
			Debug.Log("Cargando");
			Pause.load++;
		}

		try{
			SceneManager.MergeScenes(SceneManager.GetSceneByName("SaveLoad"), SceneManager.GetSceneByName("NewLevel"));
			GameObject.Find("SaveLoad").GetComponent<SaveLoadUtility>().quickSaveName = gameName;
			Pause.load++;
		}catch(ArgumentException){
			
		}
		
		if(Input.GetKeyDown("escape") && !isPaused){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			this.transform.GetChild(0).gameObject.SetActive(true);
			Player.GetComponent<Controller>().enabled = false;
			Player.transform.GetChild(0).gameObject.GetComponent<Mouse>().enabled = false;
			isPaused = true;
		}
		else if(Input.GetKeyDown("escape") && isPaused){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Ok();
			this.transform.GetChild(0).gameObject.SetActive(false);
			Player.GetComponent<Controller>().enabled = true;
			Player.GetComponent<Controller>().enabled = true;
			Player.transform.GetChild(0).gameObject.GetComponent<Mouse>().enabled = true;
			isPaused = false;
		}
	}

	public void Resume(){
		this.transform.GetChild(0).gameObject.SetActive(false);
		Player.GetComponent<Controller>().enabled = true;
		Player.GetComponent<Controller>().enabled = true;
		Player.transform.GetChild(0).gameObject.GetComponent<Mouse>().enabled = true;
		isPaused = false;
	}

	public void GoToMenu(){
		this.transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(true);
	}

	public void GuardarYSalir(){
		GameObject.Find("SaveLoad").GetComponent<SaveLoadUtility>().SaveGame(gameName);
		SceneManager.LoadScene("MainMenu");
	}

	public void CancelarSalir(){
		this.transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(false);
	}

	public void SalirSinGuardar(){
		SceneManager.LoadScene("MainMenu");
	}

	public void Load(){
		GameObject.Find("SaveLoad").GetComponent<SaveLoadUtility>().LoadGame(gameName);
		SceneManager.LoadScene("SaveLoad", LoadSceneMode.Additive);
	}

	public void Controles(){
		this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
	}

	public void Volver(){
		this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
	}

	public void Guardar(){
		GameObject.Find("SaveLoad").GetComponent<SaveLoadUtility>().SaveGame(gameName);
		this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
	}

	public void Ok(){
		this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(false);
	}

	public void Ajustes(){
		this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
	}

	public void Selector(){
		if(this.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.GetComponent<Dropdown>().value == 0){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundBlanco");
			skybox = 0;
		}
		if(this.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.GetComponent<Dropdown>().value == 1){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundGris");
			skybox = 1;
		}
		if(this.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.GetComponent<Dropdown>().value == 2){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundNegro");
			skybox = 2;
		}
		if(this.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.GetComponent<Dropdown>().value == 3){
			RenderSettings.skybox = (Material) Resources.Load("BackgroundSky");
			skybox = 3;
		}
	}
}
