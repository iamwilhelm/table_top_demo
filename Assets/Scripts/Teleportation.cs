using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	public ParticleSystem teleportIndicator;
	public Transform teleportTarget;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name != "Head") return;

		if (collider.gameObject.tag == "Player") {
			GameObject cameraRig = GameObject.Find("/[CameraRig]");
			cameraRig.transform.position = teleportTarget.position;
			cameraRig.transform.rotation = teleportTarget.rotation;
		}

		teleportIndicator.Play();

		GameObject ps = Instantiate(teleportIndicator, collider.transform.position, collider.transform.rotation) as GameObject;
		ps.transform.parent = collider.gameObject.transform;
		Destroy(ps, 5);
	}

	void OnTriggerExit() {
	}

}
