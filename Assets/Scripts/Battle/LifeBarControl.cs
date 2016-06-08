using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarControl : MonoBehaviour {

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
}
