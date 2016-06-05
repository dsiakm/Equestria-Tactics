using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public string heroName;
	//to me called by the GUI to display the life bar
	//public Image avatar;
	public int hitPoints, ap, mp, init, worldPosX, worldPosY, worldPosZ, gridPosX, gridPosY;
	Vector3 worldPos;

	//What grid is this hero on right now?
	public Grid grid;

	void Update(){
		transform.position = worldPos;
	}

	public void setPos(Vector3 pos){
		worldPos = pos;
	}
}
