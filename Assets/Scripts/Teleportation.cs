using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	public ParticleSystem teleportIndicator;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log("enter transporter");
		if (collider.gameObject.name != "Head") return;
		teleportIndicator.Play();
	}

	void OnTriggerExit() {
	}

}
