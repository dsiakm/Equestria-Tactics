using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Hero : MonoBehaviour {

	public string heroName;
	//to me called by the GUI to display the life bar
	public Texture avatar;
	public List<Skill> skillList = new List<Skill>();
	public int hitPoints, ap, totalAP, mp, totalMP, init, worldPosX, worldPosY, worldPosZ, gridPosX, gridPosY;

	//What grid is this hero on right now?
	public Grid grid;

	void Update(){
	}

	public void resetAPMP(){
		ap = totalAP;
		mp = totalMP;
	}

	public void addSkill(Skill skill){
		skillList.Add (skill);
	}
}
