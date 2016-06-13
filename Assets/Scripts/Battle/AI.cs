using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

	Grid grid;
	Pathfinder pf;
	Enemy enemy;

	void Awake(){
		grid = GameObject.Find ("Plane").GetComponent<Grid>();
		pf = GameObject.Find ("Plane").GetComponent<Pathfinder>();
		enemy = GetComponent<Enemy>();
	}

	public int Decide(List<Hero> heroList){
		if (enemy.enemyName == "TimberWolf") {
			return DecideTimberWolf (heroList);
		} else {
			return 0;
		}
	}

	//TImberwolf gets close and attack, and that is all
	int DecideTimberWolf(List<Hero> heroList){
		//always get the target closest
		enemy.targetHero = null;
		if(enemy.targetHero == null){
			enemy.targetHero = heroList [0];
			for(int i=0; i<heroList.Count;i++){
				//if distance from this one on the list is less than the turrent target, change it
				if(Vector3.Distance(enemy.transform.position, heroList[i].transform.position) < Vector3.Distance(enemy.transform.position, enemy.targetHero.transform.position)){
					enemy.targetHero = heroList [i];
				}
			}
		}

		Node nodeEnemy = grid.NodeInXY (enemy.gridPosX, enemy.gridPosY);
		Node nodeHero = grid.NodeInXY (enemy.targetHero.gridPosX, enemy.targetHero.gridPosY);
		//Debug.Log ("Hero: "+enemy.targetHero.heroName+" is at: "+nodeHero.gridX+" "+nodeHero.gridY
		//	+" Timber MP: "+enemy.mp+ " Timber is at :"+nodeEnemy.gridX+" "+nodeEnemy.gridY);

		//if not in meele range to the closest enemy, walk as close as possible
		if(enemy.mp > 0 && !pf.isMeele(nodeEnemy, nodeHero)){
			enemy.path = pf.FindEnemyPath (nodeEnemy, nodeHero);
			//path is set now tell the Judge to move your piece until your mp runs out!
			return 1;
		}
		//if in meele and enough AP to do a AutoAttack, timberwolf uses a basic attack
		else if( pf.isMeele(nodeEnemy,nodeHero) && enemy.ap >= enemy.skillList[0].APCost){
			enemy.choosenSkill = enemy.skillList [0];
			//return the state of attacking
			return 2;
		}

		//nothing more to do, end my turn
		return -1;
	}
}
