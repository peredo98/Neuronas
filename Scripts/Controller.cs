using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Controller : MonoBehaviour {

	public float speed = 10.0F;

	public Inventory inventory;

	private int rojo, azul, amarillo, contradorConector, gray = 0;

	public List<GameObject> spheres = new List<GameObject>();
	public List<GameObject> conectors = new List<GameObject>();

	public static bool isPaused = false;

	private GameObject origin = null;
	private GameObject target = null;

	private bool isNearby = false;

	public Vector2 mouseData;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		float jump = Input.GetAxis("Jump") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;
		jump *= Time.deltaTime;

		transform.Translate(straffe, jump, translation);
		if(Input.GetKeyDown("escape") && Cursor.lockState == CursorLockMode.None){
			Cursor.lockState = CursorLockMode.Locked;
		}
		if(Input.GetKeyDown("escape") && Cursor.lockState == CursorLockMode.Locked){
			Cursor.lockState = CursorLockMode.None;
		}

		if (Input.GetMouseButtonDown(0) && !isPaused){
			if(inventory.selectedObject == inventory.slots[0]){
				foreach(GameObject i in spheres){
					if(Vector3.Distance(i.transform.position, (transform.position + transform.forward * 1)) < 1){
						Debug.Log("close");
						isNearby = true;
					}
				}
				if(!isNearby){
					rojo++;
					GameObject sphere = (GameObject)Instantiate(Resources.Load("redSphere"),  transform.position + transform.forward * 1, transform.rotation);
					sphere.GetComponent<Sphere>().nombre = "Rojo " + rojo;
					spheres.Add(sphere);
				}
				isNearby = false;
			}
			if(inventory.selectedObject == inventory.slots[1]){
				foreach(GameObject i in spheres){
					if(Vector3.Distance(i.transform.position, (transform.position + transform.forward * 1)) < 1){
						Debug.Log("close");
						isNearby = true;
					}
				}
				if(!isNearby){
					azul++;
					GameObject sphere = (GameObject)Instantiate(Resources.Load("blueSphere"),  transform.position + transform.forward * 1, transform.rotation);
					sphere.GetComponent<Sphere>().nombre = "Azul " + azul;
					spheres.Add(sphere);
				}
				isNearby = false;		
			}
			if(inventory.selectedObject == inventory.slots[2]){
				foreach(GameObject i in spheres){
					if(Vector3.Distance(i.transform.position, (transform.position + transform.forward * 1)) < 1){
						Debug.Log("close");
						isNearby = true;
					}
				}
				if(!isNearby){
					amarillo++;
					GameObject sphere = (GameObject)Instantiate(Resources.Load("yellowSphere"),  transform.position + transform.forward * 1, transform.rotation);
					sphere.GetComponent<Sphere>().nombre = "Amarillo " + amarillo;
					spheres.Add(sphere);
				}
				isNearby = false;				
			}
			if(inventory.selectedObject == inventory.slots[4]){
				foreach(GameObject i in spheres){
					if(Vector3.Distance(i.transform.position, (transform.position + transform.forward * 1)) < 1){
						Debug.Log("close");
						isNearby = true;
					}
				}
				if(!isNearby){
					gray++;
					GameObject sphere = (GameObject)Instantiate(Resources.Load("conectorSphere"),  transform.position + transform.forward * 1, transform.rotation);
					sphere.GetComponent<ConectorSphere>().nombre = "Bola conectora " + gray;
					spheres.Add(sphere);
				}
				isNearby = false;				
			}
		}
		if(inventory.selectedObject == inventory.slots[3]){
			if (Input.GetMouseButtonDown(0) && !isPaused){
				if(Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane)), transform.GetChild(0).forward, out hit)){
					foreach(GameObject i in spheres){
						if(hit.collider.gameObject == i){
							origin = hit.collider.gameObject;
						}
					}
				}
			}
			if (Input.GetMouseButtonUp(0) && !isPaused){
				if(Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane)), transform.GetChild(0).forward, out hit)){
					foreach(GameObject i in spheres){
						if(hit.collider.gameObject == i){
							target = hit.collider.gameObject;
						}
					}
				}
				try{
					if(target != origin){
						foreach(GameObject i in conectors){
							if((i.GetComponent<Conector>().target == target && i.GetComponent<Conector>().origin == origin) || (i.GetComponent<Conector>().target == origin && i.GetComponent<Conector>().origin == target)){
								Debug.Log("already placed");
								isNearby = true;
							}
						}
						if(!isNearby){
							GameObject conector = (GameObject)Instantiate(Resources.Load("Conector"),  Vector3.Lerp(origin.transform.position, target.transform.position, 0.5f), transform.rotation);
							contradorConector++;
							conector.GetComponent<Conector>().origin = origin;
							conector.GetComponent<Conector>().target = target;
							conector.GetComponent<Conector>().nombre = "Conector " + contradorConector;
							conector.transform.LookAt(target.transform);
							conector.transform.GetChild(0).localScale = new Vector3(conector.transform.GetChild(0).localScale.x, Vector3.Distance(origin.transform.position, target.transform.position)/2 ,conector.transform.GetChild(0).localScale.z);
							conector.transform.GetChild(3).localScale = new Vector3(conector.transform.GetChild(3).localScale.x, conector.transform.GetChild(3).localScale.y, Vector3.Distance(origin.transform.position, target.transform.position)/2);
							conectors.Add(conector);
							target = null;
							origin = null;
						}
						isNearby = false;
					}		
				}
				catch(NullReferenceException){
					target = null;
					origin = null;
				}
			}
			
		}
		if(Physics.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane)), transform.GetChild(0).forward, out hit)){
		
			//for spheres
			foreach(GameObject i in spheres){
				i.transform.GetChild(0).gameObject.SetActive(false);
				if(i == hit.collider.gameObject){
					hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
					if(Input.GetMouseButtonDown(1)){
						hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
						Controller.isPaused = true;
					}
				}
			}
			//for conectors
			foreach(GameObject i in conectors){
				i.transform.GetChild(1).gameObject.SetActive(false);
				try{
					if(i == hit.collider.gameObject.transform.parent.gameObject){
						hit.collider.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
						if(Input.GetMouseButtonDown(1)){
							hit.collider.gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
							Controller.isPaused = true;
						}
					}
				}
				catch(NullReferenceException){}
			}
			
		}
		else{
			foreach(GameObject i in spheres){
				i.transform.GetChild(0).gameObject.SetActive(false);
			}
			foreach(GameObject i in conectors){
				i.transform.GetChild(1).gameObject.SetActive(false);
			}
		}
		
	}

}
