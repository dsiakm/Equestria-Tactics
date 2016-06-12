using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtomControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameObject tooltip;
	Skill skill;
	GameObject go;

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData){
		GameObject.Find ("Judge").SendMessage ("SetActiveSkill", skill);
	}
	#endregion


	public void OnPointerEnter(PointerEventData eventData){
		go = (GameObject)Instantiate (tooltip, new Vector3(240,0,0), Quaternion.identity);
		go.transform.SetParent (GameObject.Find("Canvas").transform, false);
		go.name = "tooltip";

		skill = GetComponent<Skill> (); 
		go.transform.GetChild(0).GetComponent<Text> ().text = 	
			"Cost "+skill.APCost+" AP\n" +
			"Damage: "+skill.damage+" Range: "+skill.range+"\n" +
			"Ignore Line of Sight: "+skill.phaseWall+"\n"+skill.tooltip;
		go.transform.GetChild (1).GetComponent<RawImage> ().texture = skill.skillIcon;
		transform.localScale += new Vector3 (0.2f,0.2f,0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Destroy (GameObject.Find("tooltip"));
		Destroy(go);
		transform.localScale -= new Vector3 (0.2f,0.2f,0.2f);
	}
}
