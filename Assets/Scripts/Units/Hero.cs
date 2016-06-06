using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

	public string heroName;
	//to me called by the GUI to display the life bar
	//public Image avatar;
	public int hitPoints, ap, totalAP, mp, totalMP, init, worldPosX, worldPosY, worldPosZ, gridPosX, gridPosY;
	Vector3 worldPos;

	//What grid is this hero on right now?
	public Grid grid;

	void Update(){
	}

	public void setPos(Vector3 pos){
		worldPos = pos;
	}

	public void resetAPMP(){
		ap = totalAP;
		mp = totalMP;
	}
}
