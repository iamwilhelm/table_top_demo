using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	public ParticleSystem teleportIndicator;
	public Transform teleportTarget;

	private float teleportTime = -1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (teleportTime == -1) return;
		if (Time.fixedTime < teleportTime + 1) return;

		GameObject cameraRig = GameObject.Find("/[CameraRig]");
		cameraRig.transform.position = teleportTarget.position;
		cameraRig.transform.rotation = teleportTarget.rotation;

		teleportTime = -1;
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log("enter transporter");
		if (collider.gameObject.name != "Head") return;
		teleportIndicator.Play();

		teleportTime = Time.fixedTime;
	}

	void OnTriggerExit() {
	}

}
