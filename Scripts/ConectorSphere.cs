using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ConectorSphere : MonoBehaviour {

	public String nombre = "";
	public int needed = 0;
	public int receiving, sending = 0;
	public bool active = false;
	public List<GameObject> conectors = new List<GameObject>();

	// Use this for initialization
	void Start () {

		//Info
		this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
		this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Transmitiendo: " + receiving;
		this.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "X: " + this.transform.position.x;
		this.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = "Y: " + this.transform.position.y;
		this.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<Text>().text = "Z: " + this.transform.position.z;
		this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.GetComponent<Text>().text = "Activo: " + active;

		//Editar

		this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;
		this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<InputField>().text = this.transform.position.x.ToString();
		this.transform.GetChild(1).GetChild(0).GetChild(6).gameObject.GetComponent<InputField>().text = this.transform.position.y.ToString();
		this.transform.GetChild(1).GetChild(0).GetChild(8).gameObject.GetComponent<InputField>().text = this.transform.position.z.ToString();

		this.transform.GetChild(1).GetChild(0).GetChild(9).gameObject.GetComponent<Button>().onClick.AddListener(Aceptar);
		this.transform.GetChild(1).GetChild(0).GetChild(10).gameObject.GetComponent<Button>().onClick.AddListener(Cancelar);
		this.transform.GetChild(1).GetChild(0).GetChild(11).gameObject.GetComponent<Button>().onClick.AddListener(Remover);

	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			this.transform.GetChild(2).gameObject.SetActive(true);
		}
		else if(!active){
			this.transform.GetChild(2).gameObject.SetActive(false);
		}

		receiving = 0;
		foreach(GameObject i in conectors){
			receiving += i.GetComponent<Conector>().transmiting;
		}

		sending = receiving;
		if(receiving > 0){
			active = true;
		}
		this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Transmitiendo: " + receiving;
		this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.GetComponent<Text>().text = "Activo: " + active;
	}


	public void Aceptar(){

		try{
		
			nombre = this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text;
			this.transform.position = new Vector3(float.Parse(this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<InputField>().text), float.Parse(this.transform.GetChild(1).GetChild(0).GetChild(6).gameObject.GetComponent<InputField>().text), float.Parse(this.transform.GetChild(1).GetChild(0).GetChild(8).gameObject.GetComponent<InputField>().text));

			//Info
			this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
			this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Transmitiendo: " + receiving;
			this.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "X: " + this.transform.position.x;
			this.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = "Y: " + this.transform.position.y;
			this.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<Text>().text = "Z: " + this.transform.position.z;
			this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.GetComponent<Text>().text = "Activo: " + active;

			//Editar

			this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;
			this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<InputField>().text = this.transform.position.x.ToString();
			this.transform.GetChild(1).GetChild(0).GetChild(6).gameObject.GetComponent<InputField>().text = this.transform.position.y.ToString();
			this.transform.GetChild(1).GetChild(0).GetChild(8).gameObject.GetComponent<InputField>().text = this.transform.position.z.ToString();


			Controller.isPaused = false;
			this.transform.GetChild(1).GetChild(0).GetChild(12).gameObject.SetActive(false);
			this.transform.GetChild(1).gameObject.SetActive(false);
		}
		catch(FormatException){
			this.transform.GetChild(1).GetChild(0).GetChild(12).gameObject.SetActive(true);
		}
	}

	public void Cancelar(){
		
		//Info
		this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
		this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Transmitiendo: " + receiving;
		this.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "X: " + this.transform.position.x;
		this.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = "Y: " + this.transform.position.y;
		this.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<Text>().text = "Z: " + this.transform.position.z;
		this.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.GetComponent<Text>().text = "Activo: " + active;

		//Editar

		this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;
		this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<InputField>().text = this.transform.position.x.ToString();
		this.transform.GetChild(1).GetChild(0).GetChild(6).gameObject.GetComponent<InputField>().text = this.transform.position.y.ToString();
		this.transform.GetChild(1).GetChild(0).GetChild(8).gameObject.GetComponent<InputField>().text = this.transform.position.z.ToString();


		Controller.isPaused = false;
		this.transform.GetChild(1).GetChild(0).GetChild(12).gameObject.SetActive(false);
		this.transform.GetChild(1).gameObject.SetActive(false);
	}

	public void Remover(){
		Controller.isPaused = false;

		conectors.Clear();

		this.transform.GetChild(1).GetChild(0).GetChild(12).gameObject.SetActive(false);
		this.transform.GetChild(1).gameObject.SetActive(false);
		GameObject.Find("Player").GetComponent<Controller>().spheres.Remove(this.transform.gameObject);
		Destroy(this.transform.gameObject);
	}
}