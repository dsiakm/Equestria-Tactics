using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler {

	public string enemyName;
	public Hero targetHero;
	public List<Node> path;
	public Skill choosenSkill;

	public Texture avatar;
	public List<Skill> skillList = new List<Skill>();
	public AI ai;
	public int hitPoints, totalHitPoints, ap, totalAP, mp, totalMP, init, gridPosX, gridPosY;

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData){
		GameObject.Find ("Judge").SendMessage ("SetActiveTarget", this);
	}
	#endregion

	public void resetAPMP(){
		ap = totalAP;
		mp = totalMP;
	}

	public void addSkill(Skill skill){
		skillList.Add (skill);
	}

	public void initializeEnemy(string enemyName, Texture avatar, int hitPoints, int ap, int mp, int init){
		this.enemyName = enemyName;
		this.hitPoints = hitPoints;
		this.ap = ap;
		this.totalAP = ap;
		this.mp = mp;
		this.totalMP = mp;
		this.init = init;
		this.avatar = avatar;
		ai = GetComponent<AI> ();
	}

	public int AskAI(List<Hero> heroList){
		return ai.Decide (heroList);
	}
}
