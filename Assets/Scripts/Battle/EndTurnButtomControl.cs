using UnityEngine;
using System.Collections;

public class EndTurnButtomControl : MonoBehaviour {

	RoundManager rm;
	void Start () {
		rm = GameObject.Find ("Judge").GetComponent<RoundManager>();
	}

	public void ClickOnEndTurnButtom(){
		rm.EndTurnButtom ();
	}
}
