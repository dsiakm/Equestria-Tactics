using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour {

	public Camera activeCamera;
	List<Node> path;
	public List<Hero> heroList;
	public List<Enemy> enemyList;
	public GameObject moveButtom;
	public GameObject LifeBar;
	GameObject GreatBar;
	public Hero activeHero;
	public Enemy activeEnemy;
	public Pathfinder pf;
	public Grid grid;
	bool newTurn = true; 
	bool moveHero = false;

	//state machine aux variables
	int stateMach;

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
		if (activeEnemy!=null){
			EnemyStateMachine ();
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
		//activeHero.gridPosX = path [0].gridX; activeHero.gridPosY = path [0].gridY;
		activeHero.transform.position = Vector3.Lerp (activeHero.transform.position,path[0].worldPosition, 5f * Time.deltaTime);
		if (Vector3.Distance(activeHero.transform.position, path[0].worldPosition)<0.1f){
			AdvancePathing ();
		}
	}

	void AdvancePathing(){

		//Makes the block the hero was standing walkable
		grid.NodeInXY(activeHero.gridPosX,activeHero.gridPosY).walkable = true;


		activeHero.transform.position = path[0].worldPosition;
		activeHero.gridPosX = path [0].gridX; activeHero.gridPosY = path [0].gridY;
		//Makes the block the hero is standing on right now unwalkable
		grid.NodeInXY(activeHero.gridPosX,activeHero.gridPosY).walkable = false;

		activeHero.mp -= 1;
		GameObject.Find (activeHero.heroName+"LifeBar").SendMessage("SetMP", (activeHero.mp));
		GameObject.Find ("GreatBar").SendMessage ("SetMP", activeHero.mp);


		path.RemoveAt (0);
	}
	//HERO MOVE SECTION END

	//ENEMY TURN SECTION START

	//Ask the Enemy what he will do
	//Act accordly, using the info store in the enemy
	//Ask again
	void EnemyStateMachine(){

		//Ask the AI what to do.
		if(stateMach == 0){
			Debug.Log ("Stato 0");
			stateMach = activeEnemy.AskAI (heroList);
		}
		//Moving state
		else if(stateMach == 1){
			Debug.Log ("Stato 1");
			EnemyMove ();
		}
		else if(stateMach == -1){
			Debug.Log ("ff");
			EndEnemyTurn ();
		}
	}
	void EnemyMove(){
		if (activeEnemy.path == null || activeEnemy.path.Count == 0 || activeEnemy.mp == 0) {
			stateMach = 0;
			activeEnemy.path = null;

			return;
		}
		//moving animation:
		activeEnemy.transform.position = Vector3.Lerp (activeEnemy.transform.position,activeEnemy.path[0].worldPosition, 5f * Time.deltaTime);
		if (Vector3.Distance(activeEnemy.transform.position, activeEnemy.path[0].worldPosition)<0.1f){

			//Make the square the enemy was before walkable
			grid.NodeInXY(activeEnemy.gridPosX,activeEnemy.gridPosY).walkable = true;

			//update enemy pos in the grid
			activeEnemy.gridPosX = activeEnemy.path [0].gridX;
			activeEnemy.gridPosY = activeEnemy.path [0].gridY;

			//Make the grid enemy is on unwalkable.
			grid.NodeInXY(activeEnemy.gridPosX,activeEnemy.gridPosY).walkable = false;

			activeEnemy.transform.position = activeEnemy.path[0].worldPosition;
			activeEnemy.mp -= 1;

			activeEnemy.path.RemoveAt (0);
		}

	}
	void EnemyAct(){
	}
	//ENEMY TURN SECTION END

	//TURN MANAGEMENT START

	public void EndTurnButtom(){
		if (!HeroTurn()){
			return;
		}
		activeHero.resetAPMP();

		//this put the hero who just had her turn to the end of the list
		heroList.Remove (activeHero);
		heroList.Add (activeHero);
		nextInLine (true);
	}

	void nextInLine(bool wasHero){
		newTurn = true;
		//If it was a hero who ended his turn last time, then a Unit will be made active, otherwise
		if (wasHero) {
			activeHero = null;
			activeEnemy = enemyList [0];
			stateMach = 0;
		} else {
			activeHero = heroList[0];
			activeEnemy = null;
			stateMach = 0;
		}
	}

	void EndEnemyTurn(){
		activeEnemy.resetAPMP ();
		enemyList.Remove (activeEnemy);
		enemyList.Add (activeEnemy);
		stateMach = 0;
		nextInLine (false);
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
				GreatBar.GetComponent<RectTransform> ().position = new Vector3 (637, 49.7f,0);
				updateGreatBar ();
			} else {
				GreatBar.gameObject.transform.position = new Vector3(700,700,0);
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
