using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageDisplayControl : MonoBehaviour {

	float timeSpent;

	public void AppearOnEnemyGreatBar(int damage){
		transform.SetParent (GameObject.Find("Canvas").transform,true);
		transform.position = new Vector3 (850,75,0);
		transform.GetChild(0).GetComponent<Text>().text = "-"+damage;
	}

	public void AppearOnHeroLifeBar(string heroName){
		transform.SetParent (GameObject.Find(heroName+"LifeBar").transform,false);
		transform.position = GameObject.Find (heroName + "LifeBar").transform.position;
	}
	public void SetText(int damage){
		transform.GetChild(0).GetComponent<Text>().text = "-"+damage;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, 360 * Time.deltaTime));
		transform.GetChild(0).transform.Rotate (new Vector3 (0, 0, -360 * Time.deltaTime));
		timeSpent += Time.deltaTime;
		if (timeSpent > 2) {
			Destroy (GameObject.Find(transform.name));
		}
	}
}
