using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour {

	float min = -600f;
	float max = 600f;
	List<GameObject> spheres;
	float timer;
	TextMesh text;
	float nextActionTime = 0f;
	float period = 0.05f;
	int popped = 0;
	float spawnRate = 4f;
	float r;
	// Use this for initialization
	void Start () {
		spheres = new List<GameObject> ();
		timer = 0.0f;
		text = GameObject.Find ("Text").GetComponent<TextMesh> ();
		InvokeRepeating ("spawnBubble", 0.0f, spawnRate);
	}

	Vector3 getRandomPosition(){
		r = Random.Range (0f, 1f);
		if (r <= 0.25) {
			return new Vector3 (-9f, 0, 0);
		} else if (r <= 0.5) {
			return new Vector3 (9f, 0, 0);
		} else if (r <= 0.75) {
			return new Vector3 (0, 0, -9f);
		} else {
			return new Vector3 (0, 0, 9f);
		}
	}

	float rand(float min, float max){
		return Random.Range (min, max);
	}

	void addAccordingForce(GameObject sphere){
		Vector3 forceVector;
		if (r <= 0.25) {
			forceVector = new Vector3 (rand (50, max), 0, 0);
		} else if (r <= 0.5) {
			forceVector = new Vector3 (-(rand (50, max)), 0, 0);
		} else if (r <= 0.75) {
			forceVector = new Vector3 (0, 0, rand (50, max));
		} else {
			forceVector = new Vector3 (0, 0, -(rand (50, max)));
		}
		sphere.GetComponent<Rigidbody>().AddForce(transform.position + forceVector);
	}

	void spawnBubble(){
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = getRandomPosition ();
		sphere.AddComponent<Rigidbody> ();
		sphere.GetComponent<Rigidbody> ().useGravity = false;
		sphere.GetComponent<Rigidbody> ().detectCollisions = false;
		addAccordingForce (sphere);
		sphere.AddComponent(System.Type.GetType("Controller"));
		spheres.Add (sphere);
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		int seconds = (int) timer % 60;
		text.text = "Timer: " + seconds + "s";

		if (Controller.popped > this.popped) {
			this.popped++;
			CancelInvoke ();
			spawnRate = spawnRate / 1.75f;
			InvokeRepeating ("spawnBubble", 0.0f, spawnRate);
		}


	}
}
