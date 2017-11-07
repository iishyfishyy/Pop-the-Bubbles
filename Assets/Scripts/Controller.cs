using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	static TextMesh poppedText;
	static TextMesh missedText;
	public static int popped = 0;
	static int missed = 0;
	bool onplane = false;
	GameObject plane;
	Vector3 center;
	Material mat;
	float time = 10;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material = Resources.Load("Blue", typeof(Material)) as Material;
		gameObject.GetComponent<Collider> ().material = (PhysicMaterial)Resources.Load ("Physic", typeof(PhysicMaterial)) as PhysicMaterial;
		gameObject.GetComponent<Rigidbody> ().drag = 0f;
		gameObject.GetComponent<Rigidbody> ().angularDrag = 0f;
		if (poppedText == null || missedText == null) {
			poppedText = GameObject.Find ("Popped").GetComponent<TextMesh> ();
			missedText = GameObject.Find ("Missed").GetComponent<TextMesh> ();
			poppedText.text += "0";
			missedText.text += "0";
		}
	}

	void OnCollisionEnter(){

	}

	bool inBounds(){
		if (gameObject.transform.position.x < 5f && gameObject.transform.position.x > -5f) {
			return true;
		}
		return false;
	}

	void OnMouseDown(){
		Destroy (gameObject);
		popped++;
		poppedText.text = "Bubbles popped: " + popped;
	}

	// Update is called once per frame
	void Update () {
		if (!onplane && inBounds()) {
			gameObject.GetComponent<Rigidbody> ().detectCollisions = true;
			onplane = true;
		}
		time -= Time.deltaTime;
		if (time < 0) {
			Destroy (gameObject);
			missed++;
			missedText.text = "Bubbles missed: " + missed;
		}

		if (transform.position.y > 1f)
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);

		if (missed > 10)
			Application.Quit ();
	}
}
