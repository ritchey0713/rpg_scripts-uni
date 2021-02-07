using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {
	// target will be what the camera is attaching to
	public Transform target;

	public Tilemap theMap;

	private Vector3 bottomLeftLimit;
	private Vector3 topRightLimit;

	private float halfHeight;
	private float halfWidth;
    // Start is called before the first frame update
	void Start() {
		theMap.CompressBounds();
		//target = PlayerController.instance.transform;
		// if the controller doesnt exist on frame 1 it never finds the instance to set our target to

		// this will look at all the objects and find the playercontroller object in unity
		// do this to objects that dont exist on frame 1
		target = FindObjectOfType<PlayerController>().transform;
		// gets screen size
		halfHeight = Camera.main.orthographicSize;
		halfWidth = halfHeight * Camera.main.aspect;

		bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
		topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

		// same thing here it tries to fire the set bounds prior to having the instance of a playercontroller
		
		// FindObjectOfType<PlayerController>().SetBounds(theMap.localBounds.min, theMap.localBounds.max);

		// using clone fix in the essentials controller allow us to do this as an alt 
		PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);

	}

	// LateUpdate is called once per frame and after Update()
	void LateUpdate() {
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

		// keep the camera keep inside bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

	}
}
