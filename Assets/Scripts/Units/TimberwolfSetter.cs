using UnityEngine;
using System.Collections;

public class TimberwolfSetter : MonoBehaviour {

	Enemy timberwolf;

	int hitPoints, ap, mp, init, damage, range, APCost;
	bool phaseWall, lazerShot;
	string enemyName, enemyAI, effect, tooltip;

	void Awake(){
		timberwolf = GetComponent<Enemy> ();
		setStatus ();
	}

	void setStatus(){
		enemyName = "TimberWolf";
		enemyAI = "charge";
		hitPoints = 50;
		ap = 6;
		mp = 6;
		init = 50;


		timberwolf.initializeEnemy(enemyName, hitPoints, ap, mp, init);
	}
}
