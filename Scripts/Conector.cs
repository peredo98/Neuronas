using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Conector : MonoBehaviour {

	public String nombre = "";
	public int transmiting = 0;
	public bool active = false;
	public GameObject origin;
	public GameObject target;

	// Use this for initialization
	void Start () {

		try{
			if(!target.GetComponent<Sphere>().conectors.Contains(this.transform.gameObject)){
				target.GetComponent<Sphere>().conectors.Add(this.transform.gameObject);
			}
		}catch(NullReferenceException){
			if(!target.GetComponent<ConectorSphere>().conectors.Contains(this.transform.gameObject)){
				target.GetComponent<ConectorSphere>().conectors.Add(this.transform.gameObject);
			}
		}
		//Info
		this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
		try{
			this.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Desde: " + origin.GetComponent<Sphere>().nombre;
		}catch(NullReferenceException){
			this.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Desde: " + origin.GetComponent<ConectorSphere>().nombre;
		}
		try{
			this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Hacia: " + target.GetComponent<Sphere>().nombre;
		}catch(NullReferenceException){
			this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Hacia: " + target.GetComponent<ConectorSphere>().nombre;
		}
		this.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = "Transmitiendo: " + transmiting;
		this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<Text>().text = "Activo: " + active;

		//Editar
		this.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;

		this.transform.GetChild(2).GetChild(0).GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(Aceptar);
		this.transform.GetChild(2).GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(Cancelar);
		this.transform.GetChild(2).GetChild(0).GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(Remover);
		
		if(origin.GetComponent<Renderer>().material.name == "blue (Instance)"){
			this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Material) Resources.Load("ConectorBlue", typeof(Material));
		}
		if(origin.GetComponent<Renderer>().material.name == "Red (Instance)"){
			this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Material) Resources.Load("ConectorRed", typeof(Material));
		}
		if(origin.GetComponent<Renderer>().material.name == "yellow (Instance)"){
			this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Material) Resources.Load("ConectorYellow", typeof(Material));
		}
		if(origin.GetComponent<Renderer>().material.name == "New Material (Instance)"){
			this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Material) Resources.Load("ConectorGray", typeof(Material));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(origin == null || target == null){
			Remover();
			return;
		}
		if(active){
			this.transform.GetChild(3).gameObject.SetActive(true);
			try{
			transmiting = origin.GetComponent<Sphere>().sending;
			}
			catch(NullReferenceException){
			transmiting = origin.GetComponent<ConectorSphere>().sending;	
			}		
		}
		else if(!active){
			this.transform.GetChild(3).gameObject.SetActive(false);
			transmiting = 0;
		}
		try{
			if(target.GetComponent<Sphere>().receiving < target.GetComponent<Sphere>().needed){
				target.GetComponent<Sphere>().active = false;
			}
		}
		catch(NullReferenceException){
			if(target.GetComponent<ConectorSphere>().receiving == 0){
				target.GetComponent<ConectorSphere>().active = false;
			}
		}
		try{
			if(origin.GetComponent<Sphere>().active && origin.GetComponent<Sphere>().sending > 0){
				active = true;
			}
			else if(!origin.GetComponent<Sphere>().active){
				active = false;
			}
		}
		catch(NullReferenceException){
			if(origin.GetComponent<ConectorSphere>().active){
				active = true;
			}
			else if(!origin.GetComponent<ConectorSphere>().active){
				active = false;
			}
		}

		this.transform.position = Vector3.Lerp(origin.transform.position, target.transform.position, 0.5f);
		this.transform.LookAt(target.transform);
		this.transform.GetChild(0).localScale = new Vector3(this.transform.GetChild(0).localScale.x, Vector3.Distance(origin.transform.position, target.transform.position)/2 , this.transform.GetChild(0).localScale.z);
		this.transform.GetChild(3).localScale = new Vector3(this.transform.GetChild(3).localScale.x, this.transform.GetChild(3).localScale.y, Vector3.Distance(origin.transform.position, target.transform.position)/2);

		try{
			this.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Desde: " + origin.GetComponent<Sphere>().nombre;
		}catch(NullReferenceException){
			this.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = "Desde: " + origin.GetComponent<ConectorSphere>().nombre;
		}
		try{
			this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Hacia: " + target.GetComponent<Sphere>().nombre;
		}catch(NullReferenceException){
			this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>().text = "Hacia: " + target.GetComponent<ConectorSphere>().nombre;
		}
		this.transform.GetChild(1).GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = "Transmitiendo: " + transmiting;
		this.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.GetComponent<Text>().text = "Activo: " + active;

		Material tempMaterial = this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material;

		tempMaterial.mainTextureScale = new Vector2 (1, 2 * this.transform.GetChild(0).localScale.y);
		this.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = tempMaterial;
	}

	public void Aceptar(){
		nombre = this.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text;

		this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
		this.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;

		Controller.isPaused = false;
		this.transform.GetChild(2).gameObject.SetActive(false);
	}

	public void Cancelar(){
		this.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Nombre: " + nombre;
		this.transform.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<InputField>().text = nombre;

		Controller.isPaused = false;
		this.transform.GetChild(2).gameObject.SetActive(false);
	}

	public void Remover(){
		Controller.isPaused = false;

		try{
			try{
			target.GetComponent<Sphere>().conectors.Remove(this.transform.gameObject);
			target.GetComponent<Sphere>().active = false;
			}catch(NullReferenceException){
				try{
					target.GetComponent<ConectorSphere>().conectors.Remove(this.transform.gameObject);
					target.GetComponent<ConectorSphere>().active = false;	
				}catch(NullReferenceException){}
			}
		}catch(MissingReferenceException){}

		this.transform.GetChild(2).gameObject.SetActive(false);
		GameObject.Find("Player").GetComponent<Controller>().conectors.Remove(this.transform.gameObject);
		Destroy(this.transform.gameObject);
	}
}
