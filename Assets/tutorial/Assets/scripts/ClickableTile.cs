using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

	public int tileX, tileY;
	public TileMap map;

	void OnMouseUp(){
		Debug.Log ("hOI");
		map.MoveSelectUnitTo (tileX, tileY);
	}
}
