using UnityEngine;
using System.Collections;

public class ClickableNode : MonoBehaviour {

	public int gridX, gridY;

	public void setXY(int x, int y){
		gridX = x;
		gridY = y;
	}

	void OnMouseDown(){
		Debug.Log ("hOI, Temmie is at x:"+gridX+" y:"+gridY);
		GameObject.Find ("Judge").SendMessage ("ReceiveMove", new Vector2(gridX, gridY));

	}
}
