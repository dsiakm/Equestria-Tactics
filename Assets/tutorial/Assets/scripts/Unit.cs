using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int tileX, tileY;

	public void Move(){
		transform.position = new Vector3 (tileX, tileY, 0);
	}
}
