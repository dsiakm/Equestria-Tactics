using UnityEngine;
using System.Collections;

public class BattleCamera : MonoBehaviour {

	const float yAngleMin = 0f, yAngleMax = 90f;

	public Transform lookAt;
	public Transform camTransform;

	Camera cam;

	public float distance = 10.0f;
	public float currentX=45f, currentY=45f, offSetX, offSetY;

	void Awake(){
		camTransform = transform;
		cam = Camera.main;

	}

	public void setLookAt(Transform look){
		lookAt = look;
		//currentX = 45f; currentY = 45f;
	}

	void Update(){
		if (Input.GetMouseButton(1)){
			currentY += Input.GetAxis ("Mouse X");
			currentX -= Input.GetAxis ("Mouse Y");

			currentX = Mathf.Clamp(currentX, yAngleMin,yAngleMax);
		}
		cam.orthographicSize += 5*Input.GetAxis ("Mouse ScrollWheel");
		if(cam.orthographicSize < 0.1f){
			cam.orthographicSize = 0.1f;
		}
		if(cam.orthographicSize > 15f){
			cam.orthographicSize = 15f;
		}
	}

	void LateUpdate(){
		Vector3 dir = new Vector3 (0,0,-distance);
		Quaternion rotation = Quaternion.Euler (currentX,currentY,0);
		camTransform.position = lookAt.position + rotation * dir;
		//camTransform.position = new Vector3 (camTransform.position.x+offSetX,camTransform.position.y+offSetY,camTransform.position.z);
		camTransform.LookAt (lookAt.position);
	}
}
