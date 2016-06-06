using UnityEngine;
using System.Collections;

public class MoveButtomControl : MonoBehaviour {

	RoundManager rm;
	void Start () {
		rm = GameObject.Find ("Judge").GetComponent<RoundManager>();
	}

	public void ClickOnTheFuckingButtom(){
		rm.StartMovingHeroButtom ();
	}


}
