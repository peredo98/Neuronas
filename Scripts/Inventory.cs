using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject selectedObject;
	public GameObject [] slots = new GameObject[5];
	private int index;

	// Use this for initialization
	void Start () {
		slots[0] = this.transform.GetChild(0).gameObject;
		slots[1] = this.transform.GetChild(1).gameObject;
		slots[2] = this.transform.GetChild(2).gameObject;
		slots[3] = this.transform.GetChild(3).gameObject;
		slots[4] = this.transform.GetChild(4).gameObject;

		selectedObject = slots[0];

		this.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -280, 0);
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1")){
			selectedObject =  slots[0];
			index = 0;
		}
		if(Input.GetKeyDown("2")){
			selectedObject =  slots[1]; 
			index = 1;
		}
		if(Input.GetKeyDown("3")){
			selectedObject =  slots[2];
			index = 2;
		}
		if(Input.GetKeyDown("4")){
			selectedObject =  slots[3];
			index = 3;
		}

		if(Input.GetKeyDown("5")){
			selectedObject =  slots[4];
			index = 4;
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0){
		    index++;
	    	if(index > 4){
		       	index = 0;
		    }
		    selectedObject = slots[index]; 
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
		    index--;
		    if(index < 0){
		     	index = 4;
		    }
		    selectedObject = slots[index]; 
		}  

		foreach (GameObject i in slots){
			i.GetComponent<Image>().color = new Color32(255,255,225,70);
		}

		selectedObject.GetComponent<Image>().color = new Color32(255,255,225,255);
	}
}
