using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GreatBarControl : MonoBehaviour {

	public GameObject skillPrefab;

	public void SetAvatar(Texture avatar){
		transform.GetChild (0).GetComponent<RawImage> ().texture = avatar;
	}
	public void SetAP(int AP){
		transform.GetChild (2).GetChild (0).GetComponent<Text> ().text = "" + AP;
	}
	public void SetMP(int MP){
		transform.GetChild (3).GetChild (0).GetComponent<Text> ().text = "" + MP;
	}
	public void SetFill(float i){
		transform.GetChild (1).GetChild (0).GetComponent<Image> ().fillAmount = i;
	}
	public void SetSkillBar(Hero hero){
		Destroy (GameObject.Find("s1"));
		Destroy (GameObject.Find("s2"));
		Destroy (GameObject.Find("s3"));
		Destroy (GameObject.Find("s4"));
		Destroy (GameObject.Find("s5"));
		Destroy (GameObject.Find("s6"));
		for(int i=0;i<hero.skillList.Count;i++){
			GameObject skillButtom = (GameObject)Instantiate (skillPrefab, PosSkill(i), Quaternion.identity);
			skillButtom.name = "s" + (i+1);
			skillButtom.transform.SetParent (GameObject.Find("SkillBar").transform, false);
			skillButtom.GetComponent<Skill> ().APCost = hero.skillList [i].APCost;
			skillButtom.GetComponent<Skill> ().range = hero.skillList [i].range;
			skillButtom.GetComponent<Skill> ().damage = hero.skillList [i].damage;
			skillButtom.GetComponent<Skill> ().effect = hero.skillList [i].effect;
			skillButtom.GetComponent<Skill> ().lazerShot = hero.skillList [i].lazerShot;
			skillButtom.GetComponent<Skill> ().phaseWall = hero.skillList [i].phaseWall;
			skillButtom.GetComponent<Skill> ().skillIcon = hero.skillList [i].skillIcon;
			skillButtom.GetComponent<RawImage> ().texture = hero.skillList [i].skillIcon;
			skillButtom.GetComponent<Skill> ().tooltip = hero.skillList [i].tooltip;
		}	
	}

	Vector3 PosSkill(int i){
		if (i == 0) {
			return new Vector3 (-33, 26, 0);
		} else if (i == 1) {
			return new Vector3 (0, 26, 0);
		} else if (i == 2) {
			return new Vector3 (33, 26, 0);
		} else if (i == 3) {
			return new Vector3 (-33, -26, 0);
		} else if (i == 4) {
			return new Vector3 (0, -26, 0);
		} else {
			return new Vector3 (33, -26, 0);
		}
	}
}
