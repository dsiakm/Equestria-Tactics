using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	public Camera activeCamera;
	List<Node> path;
	public List<Hero> heroList;
	public GameObject moveButtom;
	public GameObject LifeBar;
	GameObject GreatBar;
	public Hero activeHero;
	public Pathfinder pf;
	public Grid grid;
	bool newTurn = true; 
	bool moveHero = false;

	void Start(){
		activeCamera = Camera.current;
		GreatBar = GameObject.Find ("GreatBar");
		ReceiveListOfHeroes ();
		ReceiveListOfEnemies ();
		InitializeLifeBars ();
	}

	void ReceiveListOfHeroes(){
	
	}

	void ReceiveListOfEnemies(){
	
	}

	void InitializeLifeBars (){
		Vector3 pos = new Vector3 (40,-40,0);
		for(int i = 0; i<heroList.Count;i++){
			GameObject go = (GameObject)Instantiate (LifeBar, pos, Quaternion.identity);
			pos = new Vector3 (40, pos.y - 80, 0);
			go.transform.SetParent (GameObject.Find("Canvas").transform, false);
			go.SendMessage ("SetAvatar", heroList[i].avatar);
			go.SendMessage ("SetAP", heroList[i].totalAP);
			go.SendMessage ("SetMP", heroList[i].totalMP);
			go.name = heroList[i].heroName+"LifeBar";

		}
	}

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
		activeHero.mp -= 1;
		activeHero.hitPoints -= 1;
		GameObject.Find (activeHero.heroName+"LifeBar").SendMessage("SetMP", (activeHero.mp));
		GameObject.Find ("GreatBar").SendMessage ("SetMP", activeHero.mp);


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
		for(int i = 0; i<heroList.Count;i++){
			GameObject go = GameObject.Find (heroList[i].heroName+"LifeBar");
			go.SendMessage ("SetAP", heroList[i].totalAP);
			go.SendMessage ("SetMP", heroList[i].totalMP);
			float a = heroList [i].hitPoints; float b = heroList [i].totalHitPoints;
			go.SendMessage ("SetFill", (a/b));

			if (HeroTurn ()) {
				//GreatBar.GetComponent<Canvas> ().enabled = true;
				updateGreatBar ();
			} else {
				//GreatBar.GetComponent<Canvas> ().enabled = false;
			}
		}
	}
	void updateGreatBar(){
		if(!HeroTurn()){
			return;
		}

		GreatBar.SendMessage ("SetAvatar", activeHero.avatar);
		GreatBar.SendMessage ("SetAP", activeHero.ap);
		GreatBar.SendMessage ("SetMP", activeHero.mp);
		float a = activeHero.hitPoints; float b = activeHero.totalHitPoints;
		GreatBar.SendMessage ("SetFill", (a/b));
		GreatBar.SendMessage ("SetSkillBar", activeHero);
	}
	bool HeroTurn(){
		if (activeHero != null) {
			return true;
		}
		return false;
	}

	//TURN MANAGEMENT END

	//CONSTANT ADJUSTMENTS START

	//CONSTANT ADJUSTMENTS END
}
