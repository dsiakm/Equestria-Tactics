using UnityEngine;
using System.Collections;

public class Raycaster : MonoBehaviour {
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray toMouse = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit rhInfo;
			bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
			Debug.Log (rhInfo.collider.name);
		}
	}
}
