﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickableNode : MonoBehaviour, IPointerClickHandler {

	public int gridX, gridY;

	public void setXY(int x, int y){
		gridX = x;
		gridY = y;
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData){
		//Debug.Log ("hOI, distance is:"+Vector2.Distance(new Vector2(1,1),new Vector2(gridX,gridY)));
		Debug.Log ("hOI, x: "+gridX+" y: "+gridY);
		GameObject.Find ("Judge").SendMessage ("ReceiveMove", new Vector2(gridX, gridY));
	}
	#endregion

	public void SetColor(int paint){
		if (paint == 2) {
			GetComponent<Renderer> ().material.SetColor ("_Color", Color.cyan);
		}else if(paint == 3){
			GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}else {
			GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);
		}
	}
}
