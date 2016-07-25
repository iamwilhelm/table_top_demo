using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	public ParticleSystem availableIndicator;
	public ParticleSystem teleportIndicator;
	public Transform teleportTarget;
	public GameController gameController;

	public bool isOn = true;

	// Use this for initialization
	void Start () {
		if (isOn) {
			TurnOn();
		} else {
			TurnOff();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	}

	public void TurnOn() {
		isOn = true;
		availableIndicator.Play();
	}

	public void TurnOff() {
		isOn = false;
		availableIndicator.Stop();
	}

	void OnTriggerEnter(Collider collider) {
		if (!isOn) return;
		if (collider.gameObject.name != "Head") return;

		if (collider.gameObject.tag == "Player") {
			GameObject cameraRig = GameObject.Find("/[CameraRig]");
			cameraRig.transform.position = teleportTarget.position;
			cameraRig.transform.rotation = teleportTarget.rotation;
		}

		teleportIndicator.Play();

		if (gameController != null) {
			gameController.StartGame();
		}

		/*
		GameObject ps = Instantiate(teleportIndicator, collider.transform.position, collider.transform.rotation) as GameObject;
		ps.transform.parent = collider.gameObject.transform;
		Destroy(ps, 5);
		*/
	}

	void OnTriggerExit(Collider collider) {
	}

}
