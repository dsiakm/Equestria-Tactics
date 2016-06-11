using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

	Grid grid;

	void Awake(){
		grid = GetComponent<Grid> ();
	}
		
		
	public List<Node> FindPath(Node startNode, Node targetNode){
		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		openSet.Add (startNode);
		while (openSet.Count > 0) {
			Node currentNode = openSet [0];
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet [i];
				}
			}

			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				return RetracePath (startNode, targetNode);
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains (neighbour)) {
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance (neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains (neighbour)) {
						openSet.Add (neighbour);
					}
				}
			}
		}
		return null;
	}

	Vector2 chooseAdjacent(Node startNode, Node targettNode){
		int posx = targettNode.gridX, posy = targettNode.gridY;

		//If is best to aproach by the left...
		if (startNode.gridX < targettNode.gridX) {
			//And the tile is available!
			if (grid.NodeInXY (posx - 1, posy).walkable) {
				posx -= 1;
			}
			//if is not, ill try up, down then the opposite direction right
			else {
				//try up
				if (grid.NodeInXY (posx, posy+1).walkable){
					posy += 1;
				}
				//if impossible try down
				else if(grid.NodeInXY (posx, posy-1).walkable){
					posy -= 1;
				}
				//if impossible try right
				else if(grid.NodeInXY (posx + 1, posy).walkable){
					posx += 1;				
				}
				//if all this fails it means the hero is surrounded, not aproaching is fine rly
			}
		} 
		//if is best to aproach by the right
		else if (startNode.gridX > targettNode.gridX && grid.NodeInXY(posx+1,posy).walkable){
			//And the tile is available!
			if (grid.NodeInXY (posx + 1, posy).walkable) {
				posx += 1;
			}
			//if is not, ill try up, down then the opposite direction left
			else {
				//try up
				if (grid.NodeInXY (posx, posy+1).walkable){
					posy += 1;
				}
				//if impossible try down
				else if(grid.NodeInXY (posx, posy-1).walkable){
					posy -= 1;
				}
				//if impossible try left
				else if(grid.NodeInXY (posx - 1, posy).walkable){
					posx -= 1;				
				}
				//if all this fails it means the hero is surrounded, not aproaching is fine rly
			}
		}
		//if is best to aproach from below
		else if (startNode.gridY < targettNode.gridY && grid.NodeInXY(posx,posy-1).walkable) {
			//And the tile is available!
			if (grid.NodeInXY (posx, posy-1).walkable) {
				posy -= 1;
			}
			//if is not, ill try right, left then the opposite direction up
			else {
				//try right
				if (grid.NodeInXY (posx+1, posy).walkable){
					posx += 1;
				}
				//if impossible try left
				else if(grid.NodeInXY (posx-1, posy).walkable){
					posx -= 1;
				}
				//if impossible try up
				else if(grid.NodeInXY (posx, posy+1).walkable){
					posy += 1;				
				}
				//if all this fails it means the hero is surrounded, not aproaching is fine rly
			}
		} 
		//if is best to aproach from above
		else if (startNode.gridY > targettNode.gridY && grid.NodeInXY(posx,posy+1).walkable){
			//And the tile is available!
			if (grid.NodeInXY (posx, posy+1).walkable) {
				posy += 1;
			}
			//if is not, ill try right, left then the opposite direction down
			else {
				//try right
				if (grid.NodeInXY (posx+1, posy).walkable){
					posx += 1;
				}
				//if impossible try left
				else if(grid.NodeInXY (posx-1, posy).walkable){
					posx -= 1;
				}
				//if impossible try down
				else if(grid.NodeInXY (posx, posy-1).walkable){
					posy -= 1;				
				}
				//if all this fails it means the hero is surrounded, not aproaching is fine rly
			}
		}

		return new Vector2 (posx, posy);
	}

	public List<Node> FindEnemyPath(Node startNode, Node targettNode){
		//target node must be -1 from destiny, and the closest one to the target

		Vector2 posxposy = chooseAdjacent (startNode,targettNode);

		Node targetNode = grid.NodeInXY ((int)posxposy.x,(int)posxposy.y);

		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		openSet.Add (startNode);
		while (openSet.Count > 0) {
			Node currentNode = openSet [0];
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet [i];
				}
			}

			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				return RetracePath (startNode, targetNode);
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains (neighbour)) {
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance (neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains (neighbour)) {
						openSet.Add (neighbour);
					}
				}
			}
		}
		return null;
	}

	List<Node> RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();
		return path;
	}

	int GetDistance (Node nodeA, Node nodeB){
		int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		return dstX + dstY;
	}

	public bool hasLineOfSight(Node startNode, Node targetNode, bool ignoreWall, int castRange){
		Node currentNode; int x=0, y = 0;
		List<Node> los = new List<Node>();


		currentNode = startNode;
		x = currentNode.gridX;
		y = currentNode.gridY;
		while (currentNode != targetNode){
			if (currentNode.gridX < targetNode.gridX) {
				x++;
			} else {
				x--;
			}
			if (currentNode.gridY < targetNode.gridY) {
				y++;
			} else {
				y--;
			}

			currentNode = grid.NodeInXY (x, y);
			los.Add (currentNode);
		}

		if (los.Count > castRange) {
			return false;
		}
		if (ignoreWall) {
			return true;
		}
		for(int i=0; i<los.Count;i++){
			if (!los[i].walkable){
				return false;
			}
		}
		return true;
	}

	public bool isMeele(Node a, Node b){
		int calc = (a.gridX - b.gridX) + (a.gridY - b.gridY);
		//Debug.Log ("Calc = "+calc);
		if(calc == 1 || calc == -1){
			return true;
		}

		return false;
	}
}
