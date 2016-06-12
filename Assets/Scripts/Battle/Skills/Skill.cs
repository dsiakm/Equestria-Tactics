using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	public Texture skillIcon;
	public int damage, range, APCost;
	public bool phaseWall, lazerShot;
	public string effect, tooltip;
	public Skill(Texture skillIcon, int damage, string effect, int range, int APCost, bool phaseWall, bool lazerShot, string tooltip){
		this.skillIcon = skillIcon;
		this.damage = damage;
		this.effect = effect;
		this.range = range;
		this.APCost = APCost;
		this.phaseWall = phaseWall;
		this.lazerShot = lazerShot;
		this.tooltip = tooltip;
	}

	public int DoDamage(){
		return (int) Random.Range (damage/2,damage*2);
	}
}
