using UnityEngine;
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
		Debug.Log ("hOI, Temmie is at x:"+gridX+" y:"+gridY);
		GameObject.Find ("Judge").SendMessage ("ReceiveMove", new Vector2(gridX, gridY));
	}
	#endregion
}
