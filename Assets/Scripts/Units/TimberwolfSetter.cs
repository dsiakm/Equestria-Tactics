using UnityEngine;
using System.Collections;

public class TimberwolfSetter : MonoBehaviour {

	Enemy timberwolf;
	public Texture avatar;
	int hitPoints, ap, mp, init, damage, range, APCost;
	bool phaseWall, lazerShot;
	string enemyName, effect, tooltip;

	void Awake(){
		timberwolf = GetComponent<Enemy> ();

		setStatus ();
		AddBasicAttack ();
	}

	void setStatus(){
		enemyName = "TimberWolf";
		hitPoints = 50;
		ap = 6;
		mp = 6;
		init = 50;


		timberwolf.initializeEnemy(enemyName, avatar, hitPoints, ap, mp, init);
	}

	void AddBasicAttack(){
		range = 1;
		APCost = 3;
		damage = 3;
		effect = "none";
		phaseWall = false;
		lazerShot = false;

		Skill newSkill = new Skill (null,damage,effect,range,APCost,phaseWall,lazerShot,"");
		timberwolf.addSkill (newSkill);
	}
}
