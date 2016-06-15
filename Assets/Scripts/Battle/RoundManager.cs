using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

	public Camera activeCamera;
	List<Node> path;
	public List<Hero> heroList;
	public List<Enemy> enemyList;
	public GameObject moveButtom;
	public GameObject LifeBar, EnemyPanel, Damage;
	GameObject GreatBar;
	public Hero activeHero;
	public Enemy activeEnemy;
	public Pathfinder pf;
	public Grid grid;
	bool newTurn = true; 
	bool moveHero = false, updateAnin=true, actHero;

	//state machine aux variables
	int stateMach;

	void Start(){
		activeCamera = Camera.main;
		GreatBar = GameObject.Find ("GreatBar");
		ReceiveListOfHeroes ();
		ReceiveListOfEnemies ();
		InitializeLifeBars ();
		SpawnEnemyPanel ();
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

	void SpawnEnemyPanel(){
		for (int i = 0; i<=enemyList.Count;i++){
			Destroy ( GameObject.Find( "EnemyPanel" + i ) );
		}
		Vector3 pos = new Vector3 (-33,128,0);
		for(int i = 0; i<enemyList.Count; i++){
			GameObject go = (GameObject)Instantiate (EnemyPanel, pos, Quaternion.identity);
			pos = new Vector3 (-21, pos.y + 40, 0);
			if (i > 0) {
				go.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
			}
			go.transform.SetParent (GameObject.Find("Canvas").transform, false);
			go.transform.GetChild (0).GetComponent<RawImage>().texture = enemyList[i].avatar;
			go.name = "EnemyPanel" + i;
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
		if (actHero) {
			if(activeHero.activeSkill.effect == "hook"){
				HookProcedure ();
			}
		}
		if (activeEnemy!=null){
			EnemyStateMachine ();
		}
		paintTheWay (path);
	}

	//HERO ACTION SECTION START

	public void StartHeroAction(){
		//Does the Hero have a activeSkill and a activeTarget?
		if(activeHero.activeSkill == null || activeHero.activeTarget == null){
			return;
		}
		//Does the Hero have enough AP do execute the action?
		if(activeHero.activeSkill.APCost > activeHero.ap){
			return;
		}

		Node heroNode = grid.NodeInXY (activeHero.gridPosX,activeHero.gridPosY);
		Node enemyNode = grid.NodeInXY (activeHero.activeTarget.gridPosX, activeHero.activeTarget.gridPosY);
		if (pf.hasLineOfSight (heroNode, enemyNode, activeHero.activeSkill)) {
			actHero = true;
			activeHero.ap -= activeHero.activeSkill.APCost;
			int damage = activeHero.activeSkill.DoDamage ();
			activeHero.activeTarget.hitPoints -= damage;
			GameObject go = (GameObject)Instantiate (Damage, Vector3.zero,Quaternion.identity);
			go.SendMessage ("AppearOnEnemyGreatBar",damage);
			updateGUI ();
		}
	}

	public void SetActiveSkill(Skill skill){
		activeHero.activeSkill = skill;
	}

	public void SetActiveTarget(Enemy enemy){
		activeHero.activeTarget = enemy;
		updateGUI ();
	}



	//HERO ACTION SECTION ENDS

	//HERO MANY MANY PROCEDURES STARTS
			void HookProcedure(){
				int x=activeHero.gridPosX, y=activeHero.gridPosY;

				if (activeHero.gridPosX < activeHero.activeTarget.gridPosX) {
					x++;
				} else if (activeHero.gridPosX > activeHero.activeTarget.gridPosX) {
					x--;
				} else if (activeHero.gridPosY < activeHero.activeTarget.gridPosY) {
					y++;
				} else if (activeHero.gridPosY > activeHero.activeTarget.gridPosY){
					y--;
				}

				Node destNode = grid.NodeInXY (x,y);

				activeHero.activeTarget.transform.position = Vector3.Lerp (activeHero.activeTarget.transform.position, destNode.worldPosition, 5f * Time.deltaTime);

				//Once is close enough to finish the procedure
				if (Vector3.Distance(activeHero.activeTarget.transform.position, destNode.worldPosition)<0.1f){

					//The node the enemy was is now walkable
					Node nodeEnemyWas = grid.NodeInXY (activeHero.activeTarget.gridPosX,activeHero.activeTarget.gridPosY);
					nodeEnemyWas.walkable = true;

					//get node enemy is in
					//this node is now unwalkable
					destNode.walkable = false;
					//update enemy pos in the grid
					activeHero.activeTarget.gridPosX = destNode.gridX; activeHero.activeTarget.gridPosY = destNode.gridY; 
					//finish the acting
					actHero = false;

					Debug.Log ("x: "+destNode.gridX+" y: "+destNode.gridY);

				}

			}

	//HERO MANY MANY PROCEDURES FINALLY ENDS

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
		activeHero.GetComponent<Animator> ().SetBool ("move", true);
	}

	void MoveHero(){
		if (path.Count == 0) {
			moveHero = false;
			path = null;
			activateMoveButton (false);
			activeHero.GetComponent<Animator> ().SetBool ("move", false);
			activeHero.GetComponent<SpriteRenderer> ().flipX = false;
			return;
		}
		//find with animation to call
		updateAnimator();
		activeHero.transform.position = Vector3.Lerp (activeHero.transform.position,path[0].worldPosition, 4f * Time.deltaTime);
		if (Vector3.Distance(activeHero.transform.position, path[0].worldPosition)<0.1f){
			AdvancePathing ();
		}
	}

	void updateAnimator(){
		if (updateAnin) {

			if( activeHero.gridPosX < path[0].gridX ){
				activeHero.GetComponent<Animator> ().SetInteger ("side",1);
				activeHero.GetComponent<SpriteRenderer> ().flipX = true;
			}else if( activeHero.gridPosX > path[0].gridX ){
				activeHero.GetComponent<Animator> ().SetInteger ("side",1);
				activeHero.GetComponent<SpriteRenderer> ().flipX = false;
			}else if( activeHero.gridPosY < path[0].gridY ){
				activeHero.GetComponent<Animator> ().SetInteger ("side",2);
				activeHero.GetComponent<SpriteRenderer> ().flipX = false;
			}else if ( activeHero.gridPosY > path[0].gridY ){
				activeHero.GetComponent<Animator> ().SetInteger ("side",3);
				activeHero.GetComponent<SpriteRenderer> ().flipX = false;
			}

			updateAnin = false;
		}
	}

	void AdvancePathing(){
		updateAnin = true;
		//activeHero.GetComponent<SpriteRenderer> ().flipX = false;

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
		else if(stateMach == 2){
			Debug.Log ("Stato 2");
			EnemyAct ();
		}
		//those stateMach is for the many procedures an enemy Attack can cause. Hook, hookshot and what not.
		else if(stateMach == -1){
			Debug.Log ("ff");
			EndEnemyTurn ();
		}
	}
	void EnemyMove(){
		if (activeEnemy.path == null || activeEnemy.path.Count == 0 || activeEnemy.mp == 0) {
			activeEnemy.mp = 0;
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
		if (activeEnemy.choosenSkill.effect == "none"){
			stateMach = 0;
		}
		activeEnemy.ap -= activeEnemy.choosenSkill.APCost;
		int damage = activeEnemy.choosenSkill.DoDamage ();
		activeEnemy.targetHero.hitPoints -= damage;
		GameObject go = (GameObject)Instantiate (Damage, Vector3.zero,Quaternion.identity);
		go.SendMessage ("AppearOnHeroLifeBar",activeEnemy.targetHero.heroName);
		go.SendMessage ("SetText", damage);
		updateGUI ();
	}
	//ENEMY TURN SECTION END

	//TURN MANAGEMENT START

	void WhoDied(){
		for(int i=0;i<heroList.Count;i++){
			if(heroList[i].hitPoints <=0) {

				grid.NodeInXY (heroList [i].gridPosX, heroList [i].gridPosY).walkable = true;

				Destroy (GameObject.Find(heroList [i].heroName));
				heroList.RemoveAt (i);
			}
		}
		for(int i=0;i<enemyList.Count;i++){
			if(enemyList[i].hitPoints<=0){

				grid.NodeInXY (enemyList [i].gridPosX, enemyList [i].gridPosY).walkable = true;

				Destroy (GameObject.Find(enemyList [i].name));
				enemyList.RemoveAt (i);
			}
		}
	}

	void EndBattle(){
		if (enemyList.Count == 0){
			Debug.Log ("Victory!");
		}
		else if(heroList.Count == 0){
			Debug.Log ("Defeated!");
		}
	}

	public void EndTurnButtom(){
		if (!HeroTurn()){
			return;
		}
		activeHero.resetAPMP();

		//this put the hero who just had her turn to the end of the list
		heroList.Remove (activeHero);
		heroList.Add (activeHero);

		WhoDied ();

		nextInLine (true);
	}

	void nextInLine(bool wasHero){
		newTurn = true;
		//If it was a hero who ended his turn last time, then a Unit will be made active, otherwise
		if (wasHero) {
			activeHero = null;
			if (enemyList.Count == 0)
				EndBattle ();
			activeEnemy = enemyList [0];
			stateMach = 0;
		} else {
			if (heroList.Count == 0)
				EndBattle ();
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

		WhoDied ();
		EndBattle ();

		nextInLine (false);
	}

	void updateCamera(){
		if (activeHero != null) {
			activeCamera.GetComponent<BattleCamera> ().setLookAt (activeHero.transform);
		} else {
			activeCamera.GetComponent<BattleCamera> ().setLookAt (activeEnemy.transform);
		}
	}
	void updateGUI(){
		for(int i = 0; i<heroList.Count;i++){
			GameObject go = GameObject.Find (heroList[i].heroName+"LifeBar");
			go.SendMessage ("SetAP", heroList[i].ap);
			go.SendMessage ("SetMP", heroList[i].ap);
			float a = heroList [i].hitPoints; float b = heroList [i].totalHitPoints;
			go.SendMessage ("SetFill", (a/b));
		}
		SpawnEnemyPanel ();

		if (HeroTurn ()) {
			GreatBar.GetComponent<RectTransform> ().position = new Vector3 (637, 49.7f,0);
			updateGreatBar ();
		} else {
			GreatBar.gameObject.transform.position = new Vector3(700,700,0);
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
		if (activeHero.activeTarget != null)
			GreatBar.SendMessage ("SetTarget", activeHero.activeTarget.avatar);
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
