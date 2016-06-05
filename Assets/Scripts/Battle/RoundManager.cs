using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	List<Node> path;
	List<Hero> heroList;
	public Hero activeHero;
	public Pathfinder pf;
	public Grid grid;
	bool newTurn = true, moveHero = false;
	
	// Update is called once per frame
	void Update () {
		if (newTurn) {
			updateCamera ();
			updateGUI ();
			newTurn = false;
		}
		if (moveHero) {
			MoveHero ();		
		}
		paintTheWay (path);
	}

	void updateCamera(){
	}
	void updateGUI(){
	}
	bool HeroTurn(){
		return true;
	}

	void ReceiveMove(Vector2 xy){
		if (!HeroTurn()) {
			return;
		}
		Node receivedNode = grid.NodeInXY ((int)xy.x, (int)xy.y);
		Node heroNode = grid.NodeInXY(activeHero.gridPosX, activeHero.gridPosY);
		path = pf.FindPath (heroNode, receivedNode);
		if (path != null) {
			if (path.Count <= activeHero.mp) {
				activateMoveButton (true);
			} else {
				activateMoveButton (false);
			}
		} else {
			activateMoveButton (false);
		}
	}
	void paintTheWay(List<Node> path){

	}
	void activateMoveButton(bool active){
		
	}

	void MoveHero(){
		
	}
}
