using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

	public TileType[] tileTypes;
	int[,] tiles;

	int mapSizeX = 10;
	int mapSizeY = 10;

	void Start() {
		GenerateMapData ();
		//Spawn the prefabs
		GenerateMapVisual();
	}

	void GenerateMapData (){
		int x, y;
		tiles = new int[mapSizeX, mapSizeY];

		//All set to grass
		for (x = 0; x < mapSizeX; x++) {
			for (y = 0; y < mapSizeY; y++) {
				tiles [x, y] = 0;
			}
		}

		//Make a U shape mountain
		tiles[4,4] = 2;
		tiles[5,4] = 2;
		tiles[6,4] = 2;
		tiles[7,4] = 2;
		tiles[8,4] = 2;

		tiles[4,5] = 2;
		tiles[4,6] = 2;
		tiles[8,5] = 2;
		tiles[8,6] = 2;

		//Make Swamp
		for (x = 3; x <= 5; x++) {
			for (y = 0; y < 4; y++) {
				tiles [x, y] = 1;
			}
		}
	}

	void GenerateMapVisual (){
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				GameObject go = (GameObject)Instantiate (tileTypes[tiles[x,y]].tileVisualPrefab, new Vector3(x,y,0), Quaternion.identity);
				ClickableTile ct = go.GetComponent<ClickableTile> ();
				ct.tileX = x;
				ct.tileY = y;
				ct.map = this;
			}
		}
	}

	public void MoveSelectUnitTo(int x, int y){
		selectedUnit.GetComponent<Unit> ().tileX = x;
		selectedUnit.GetComponent<Unit> ().tileY = y;
		selectedUnit.GetComponent<Unit> ().Move ();
	}
}
