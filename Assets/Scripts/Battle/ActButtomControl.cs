using UnityEngine;
using System.Collections;

public class ActButtomControl : MonoBehaviour {

	RoundManager rm;
	void Start () {
		rm = GameObject.Find ("Judge").GetComponent<RoundManager>();
	}

	public void ClickOnTheActionButtom(){
		rm.StartHeroAction ();
	}
}
