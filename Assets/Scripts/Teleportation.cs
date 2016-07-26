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
			ParticleSystem arrivalIndicator = cameraRig.GetComponentInChildren<ParticleSystem>();
			arrivalIndicator.Play();
			TeleportPlayer(cameraRig);
		} else if (collider.gameObject.tag == "Clone") {
			Debug.Log("In clone");
			CloneController cloneCtrl = collider.gameObject.GetComponentInParent<CloneController>();
			cloneCtrl.PlayArrivalIndicator();
		}

		// play teleportation on player/clone
		teleportIndicator.time = 2;
		teleportIndicator.Play();

		if (gameController != null) {
			gameController.StartGame();
		}
	}

	void TeleportPlayer(GameObject cameraRig) {
		// move the camera rig
		cameraRig.transform.position = teleportTarget.position;
		cameraRig.transform.rotation = teleportTarget.rotation;
	}

}
