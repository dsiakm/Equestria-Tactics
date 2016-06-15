using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	Camera cam;

	void Awake(){
		cam = Camera.main;
	}

	void Update () {
		transform.LookAt (cam.transform);
		//transform.position =   new Vector3 (0,1.1f,0);
	}
}
