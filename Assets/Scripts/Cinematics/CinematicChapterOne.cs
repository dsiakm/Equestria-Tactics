using UnityEngine;
using System.Collections;

public class CinematicChapterOne : MonoBehaviour {

	public GameObject Applejack;
	GameObject chatbox;
	Animator AJAn;
	float time = 0.0f;
	bool next = false;
	int cinematic = 1;

	void Awake(){
		AJAn = Applejack.GetComponent<Animator> ();
	}

	void Update(){
		if(cinematic == 1){
			BuckSomeApples ();
		}else if(cinematic == 2){
			WalkCloser ();
		}else if(cinematic == 3){
			FirstDialog ();
		}

		if (Input.GetMouseButton (0) == true) {
			next = true;
			chatbox.GetComponent<Chatbox> ().DeleteChat ();
		}

		time += Time.deltaTime;
	}
		
	void BuckSomeApples (){
		if(time < 2){
			return;
		}
		AJAn.SetBool ("buck", true);
		if(time < 4f){
			return;
		}
		AJAn.SetBool ("buck", false);

		//While is a unitary function, unity freezes, do everything inside than resumes.
		//For does not work like that.
		int aux = 666;
		while(aux==666){
			GameObject.Find ("tree4").transform.GetChild (0).GetComponent<Rigidbody> ().useGravity = true;
			GameObject.Find ("tree4").transform.GetChild (1).GetComponent<Rigidbody> ().useGravity = true;
			GameObject.Find ("tree4").transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = true;
			GameObject.Find ("tree4").transform.GetChild (3).GetComponent<Rigidbody> ().useGravity = true;
			GameObject.Find ("tree4").transform.GetChild (4).GetComponent<Rigidbody> ().useGravity = true;
			aux = 0;
		}
		cinematic = 2;
		time = 0;
	}

	Vector3 behindPos = new Vector3(5.83f,0.9f,-1); Vector3 closerPos = new Vector3(8.35f,-0.68f,-1); 
	Vector3 behindScale = new Vector3(3,3,7); Vector3 closerScale = new Vector3(5,5,7); 
	void WalkCloser(){
		//Applejack.transform.localScale = Vector3.Lerp(behind, closer, 4f * Time.deltaTime);
		Applejack.transform.localScale = Vector3.Lerp (behindScale, closerScale,0.4f * time);
		Applejack.transform.position = Vector3.Lerp (behindPos, closerPos,0.4f * time);
		AJAn.SetInteger ("side", 3);
		AJAn.SetBool ("move", true);
		if(Applejack.transform.position == closerPos){
			Applejack.transform.localScale = closerScale;
			Applejack.transform.position = closerPos;
			AJAn.SetBool ("move", false);
			time = 0;
			cinematic = 3;
		}
	}

	void FirstDialog(){
		
	}
}
