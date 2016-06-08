using UnityEngine;
using System.Collections;

public class AppleJackSetter : MonoBehaviour {

	Hero applejack;

	Texture icon;
	int damage, range, APCost;
	bool phaseWall, lazerShot;
	string effect, tooltip;

	void Start () {
		applejack = GetComponent<Hero> ();
		setAppleJack ();
	}
	
	void setAppleJack(){
		createLassoHookSkill ();
	}

	void createLassoHookSkill(){
		icon = Resources.Load ("lassohookicon", typeof(Texture)) as Texture;
		range = 6;
		APCost = 3;
		damage = 2;
		effect = "hook";
		phaseWall = false;
		lazerShot = true;
		tooltip = 	"Applejack throws her lasso at the enemy the way only a cowgirl can! " +
					"She also slaps the enemy when it arives just for kicks!";


		Skill newSkill = new Skill(icon, damage, effect, range, APCost, phaseWall, lazerShot, tooltip);
		applejack.addSkill (newSkill);
	}
}
