using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	public Camera activeCamera;
	List<Node> path;
	public List<Hero> heroList;
	public GameObject moveButtom;
	public Hero activeHero;
	public Pathfinder pf;
	public Grid grid;
	bool newTurn = true; 
	bool moveHero = false;

	void Start(){
		activeCamera = Camera.current;
		ReceiveListOfHeroes ();
		ReceiveListOfEnemies ();
	}

	void ReceiveListOfHeroes(){
	
	}

	void ReceiveListOfEnemies(){
	
	}
	
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
	//HERO MOVE SECTION
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
		if (active) {
			if (!GameObject.Find ("MoveButtom(Clone)")) {
				GameObject go = (GameObject)Instantiate (moveButtom, moveButtom.transform.position, Quaternion.identity);
				go.transform.SetParent (GameObject.Find("Canvas").transform, false);
			}
		} else {
			Destroy (GameObject.Find ("MoveButtom(Clone)"));
		}
	}

	public void StartMovingHeroButtom(){
		moveHero = true;
	}

	void MoveHero(){
		if (path.Count == 0) {
			moveHero = false;
			path = null;
			activateMoveButton (false);
			return;
		}
		activeHero.gridPosX = path [0].gridX; activeHero.gridPosY = path [0].gridY;
		activeHero.transform.position = Vector3.Lerp (activeHero.transform.position,path[0].worldPosition, 5f * Time.deltaTime);
		if (Vector3.Distance(activeHero.transform.position, path[0].worldPosition)<0.1f){
			AdvancePathing ();
		}
	}

	void AdvancePathing(){

		//Implement teleport to righ pos here
		activeHero.transform.position = path[0].worldPosition;

		path.RemoveAt (0);
	}
	//HERO MOVE SECTION END

	//TURN MANAGEMENT START

	public void EndTurnButtom(){
		if (!HeroTurn()){
			return;
		}
		activeHero.resetAPMP();

		//this put the hero who just had her turn to the end of the list
		heroList.Remove (activeHero);
		heroList.Add (activeHero);
		activeHero = null;
		nextInLine (true);
		newTurn = true;
	}

	void nextInLine(bool wasHero){
		//If it was a hero who ended his turn last time, then a Unit will be made active, otherwise

		//DELETE THE NEGATION, IT WAS FOR A TEST
		if (!wasHero) {
		} else {
			activeHero = heroList[0];
		}
	}

	void updateCamera(){
		if (activeHero != null) {
			//activeCamera.transform.position = new Vector3 (activeHero.transform.position.x, activeHero.transform.position.y, activeCamera.transform.position.z);
		} else {
			//Center to enemy here
		}
	}
	void updateGUI(){
	}
	bool HeroTurn(){
		return true;
	}

	//TURN MANAGEMENT END
}
