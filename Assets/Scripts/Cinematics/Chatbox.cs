using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour {

	public void SetText(string dialog){
		transform.GetChild (1).GetComponent<Text> ().text = dialog;
	}

	public void SetAvatar(string avatar){
		transform.GetChild (0).GetComponent<RawImage>().texture = Resources.Load (avatar, typeof(Texture)) as Texture;
	}

	public GameObject SetChatbox(string avatar, string dialog){
		GameObject go = Instantiate (Resources.Load("Chatbox", typeof(GameObject)), Vector3.zero,Quaternion.identity) as GameObject;
		SetText (dialog);
		SetAvatar (avatar);
		go.transform.SetParent (GameObject.Find("Canvas").transform,false);

		return go;

	}
	
	public void DeleteChat(){
		Destroy(transform.gameObject);
	}
}
