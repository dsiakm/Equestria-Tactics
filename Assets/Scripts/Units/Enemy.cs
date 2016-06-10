using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public string enemyName;
	public Hero targetHero;
	public List<Node> path;
	public Skill choosenSkill;

	public Texture avatar;
	public List<Skill> skillList = new List<Skill>();
	public AI ai;
	public int hitPoints, totalHitPoints, ap, totalAP, mp, totalMP, init, gridPosX, gridPosY;

	public void resetAPMP(){
		ap = totalAP;
		mp = totalMP;
	}

	public void addSkill(Skill skill){
		skillList.Add (skill);
	}

	public void initializeEnemy(string enemyName, int hitPoints, int ap, int mp, int init){
		this.enemyName = enemyName;
		this.hitPoints = hitPoints;
		this.ap = ap;
		this.totalAP = ap;
		this.mp = mp;
		this.totalMP = mp;
		this.init = init;
		ai = GetComponent<AI> ();
	}

	public int AskAI(List<Hero> heroList){
		return ai.Decide (heroList);
	}
}
