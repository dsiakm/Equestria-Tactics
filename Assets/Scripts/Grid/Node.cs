using UnityEngine;
using System.Collections;

public class Node {

	//Usado no algoritmo do pathfinder
	public Node parent;
	public int gCost;
	public int hCost;

	public bool walkable;
	//posição real do vetor no mundo
	public Vector3 worldPosition;

	//posição do node dentro da matrix grid
	public int gridX, gridY;

	public Node (bool walkable, Vector3 worldPosition, int gridX, int gridY){
		this.walkable = walkable;
		this.worldPosition = worldPosition + new Vector3(0,1.1f,0);

		this.gridX = gridX;
		this.gridY = gridY;
	}

	public int fCost{
		get{
			return gCost + hCost;
		}
	}
}
