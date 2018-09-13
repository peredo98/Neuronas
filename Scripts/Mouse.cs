using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mouse : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	GameObject character;


	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
		mouseLook = this.transform.parent.gameObject.GetComponent<Controller>().mouseData;
	}
	
	// Update is called once per frame
	void Update () {

		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;

		this.transform.parent.gameObject.GetComponent<Controller>().mouseData = mouseLook;

		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

		if(Controller.isPaused){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Time.timeScale = 0f;
			sensitivity = 0f;
		}

		else if(!Controller.isPaused){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1f;
			sensitivity = 5.0f;
		}
	}
}
