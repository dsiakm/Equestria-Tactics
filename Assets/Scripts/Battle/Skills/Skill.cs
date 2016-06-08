﻿using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	public Texture skillIcon;
	public int damage, range, APCost;
	public bool phaseWall, lazerShot;
	public string effect;
	public Skill(Texture skillIcon, int damage, string effect, int range, int APCost, bool phaseWall, bool lazerShot){
		this.skillIcon = skillIcon;
		this.damage = damage;
		this.effect = effect;
		this.range = range;
		this.APCost = APCost;
		this.phaseWall = phaseWall;
		this.lazerShot = lazerShot;
	}
}