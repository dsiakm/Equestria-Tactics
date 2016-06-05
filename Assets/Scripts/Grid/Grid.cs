using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public GameObject grassPrefab;

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	public Node NodeInXY (int x, int y){
		return grid [x, y];
	}
		
	/*
	void OnDrawGizmos (){
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.25f, gridWorldSize.y));

		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube (n.worldPosition, new Vector3(1,0.25f,1) * (nodeDiameter - 0.1f));
			}
		}
	}
	*/

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node>();

		//vizinho em cima
		if (checkNode(node.gridX, node.gridY+1)){
			neighbours.Add(grid[node.gridX, node.gridY+1]);
		}
		//vizinho em baixo
		if (checkNode(node.gridX, node.gridY-1)){
			neighbours.Add(grid[node.gridX, node.gridY-1]);
		}
		//vizinho direita
		if (checkNode(node.gridX+1, node.gridY)){
			neighbours.Add(grid[node.gridX+1, node.gridY]);
		}
		//vizinho esquerda
		if (checkNode(node.gridX-1, node.gridY)){
			neighbours.Add(grid[node.gridX-1, node.gridY]);
		}

		return neighbours;
	}

	bool checkNode (int x, int y){
		if (x >=0 && x < gridSizeX && y >= 0 && y < gridSizeY){
			return true;
		}
		else return false;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid [x, y];

	}

	void CreateGrid(){
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;


		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPoint,x,y);
				GameObject go = (GameObject)Instantiate (grassPrefab, worldPoint, Quaternion.identity);
				go.GetComponent<ClickableNode> ().setXY (x, y);
			}
		}
	}
}
