using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Hero : MonoBehaviour {

	public string heroName;
	//to me called by the GUI to display the life bar
	public Texture avatar;
	public List<Skill> skillList = new List<Skill>();
	public Skill activeSkill;
	public Enemy activeTarget;
	public int hitPoints, totalHitPoints, ap, totalAP, mp, totalMP, init, gridPosX, gridPosY;





	public void resetAPMP(){
		ap = totalAP;
		mp = totalMP;
	}

	public void addSkill(Skill skill){
		skillList.Add (skill);
	}
}
