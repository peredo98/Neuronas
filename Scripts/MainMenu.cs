using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject menu;

	public void crearProyecto(){
		menu.transform.GetChild(0).gameObject.SetActive(false);
		menu.transform.GetChild(1).gameObject.SetActive(true);
		menu.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
	}

	public void newGame(){

		if(menu.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text == ""){
			menu.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
			return;
		}
		
		Pause.gameName = menu.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text;

		SceneManager.LoadScene("NewLevel");
		SceneManager.LoadScene("SaveLoad", LoadSceneMode.Additive);

		Pause.load = 0;
	
	}

	public void abrirProyecto(){
		menu.transform.GetChild(0).gameObject.SetActive(false);
		menu.transform.GetChild(2).gameObject.SetActive(true);

		String pathToUse = Application.persistentDataPath + "/Saved Games/";
		List<SaveGame> list = SaveLoad.GetSaveGames(pathToUse , true);

		foreach(SaveGame i in list){
			GameObject gameSlot = (GameObject)Instantiate(Resources.Load("Juego"), transform.position, transform.rotation);
			gameSlot.transform.SetParent(menu.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0));

			gameSlot.GetComponent<RectTransform>().localPosition = new Vector3(160, -35 - (35 * list.IndexOf(i)), 0);
			gameSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(-40 , 30);
			gameSlot.GetComponent<RectTransform>().offsetMax = new Vector2(-70, gameSlot.GetComponent<RectTransform>().offsetMax.y);

			gameSlot.GetComponentInChildren<Text>().text = i.savegameName;
			gameSlot.GetComponent<Button>().onClick.AddListener(delegate{abrir(i.savegameName);});

			//delete button

			GameObject delete = (GameObject)Instantiate(Resources.Load("Borrar"), transform.position, transform.rotation);
			delete.transform.SetParent(menu.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0));

			delete.GetComponent<RectTransform>().localPosition = new Vector3(160, -35 - (35 * list.IndexOf(i)), 0);
			delete.GetComponent<RectTransform>().sizeDelta = new Vector2(-50 , 30);
			delete.GetComponent<RectTransform>().offsetMin = new Vector2(260, gameSlot.GetComponent<RectTransform>().offsetMin.y);

			delete.GetComponent<Button>().onClick.AddListener(delegate{borrar(i.savegameName);});


		}
		menu.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (35)* (list.Count + 1));
	}


	public void abrir(String filename){
		Pause.gameName = filename;
		Pause.load = 1;

		SceneManager.LoadScene("NewLevel");
		SceneManager.LoadScene("SaveLoad", LoadSceneMode.Additive);
		
		//SceneManager.LoadScene("NewLevel", LoadSceneMode.Additive);
	}

	public void borrar(String filename){
		menu.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
		menu.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "¿Desea borrar el proyecto con el nombre: "+ filename +" ?";
		menu.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
		menu.transform.GetChild(2).GetChild(3).GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate{si(filename);});
	}

	public void si(String filename){
		menu.GetComponent<SaveLoadUtility>().DeleteGame(filename);
		no();
		foreach(Transform child in menu.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0)){
			Destroy(child.gameObject);
		}
		abrirProyecto();
	}

	public void no(){
		menu.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
	}

	public void exit(){
		Application.Quit();
	}

	public void volver(){
		menu.transform.GetChild(0).gameObject.SetActive(true);
		menu.transform.GetChild(1).gameObject.SetActive(false);
		menu.transform.GetChild(2).gameObject.SetActive(false);
	}
}
