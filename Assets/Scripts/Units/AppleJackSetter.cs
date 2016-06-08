using UnityEngine;
using System.Collections;

public class AppleJackSetter : MonoBehaviour {

	Hero applejack;

	Texture icon;
	int damage, range, APCost;
	bool phaseWall, lazerShot;
	string effect;

	void Start () {
		applejack = GetComponent<Hero> ();
		setAppleJack ();
		Debug.Log ("hOI");
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

		Skill newSkill = new Skill(icon, damage, effect, range, APCost, phaseWall, lazerShot);
		applejack.addSkill (newSkill);
	}
}
