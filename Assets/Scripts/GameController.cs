using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MotionRecorder))]
public class GameController : MonoBehaviour {

	public int cloneNumber = 10;
	public int cloneDuration = 30;
	public GameObject clonePrefab;
	public GameObject startLevel;

	private MotionRecorder motionRecorder;
	private List<GameObject> clones;

	// Use this for initialization
	void Start() {
		motionRecorder = GetComponent<MotionRecorder>();
		clones = new List<GameObject>(cloneNumber);
		for (int i = 0; i < cloneNumber; i++) {
			GameObject clone = CreateClone(i);
			this.clones.Add(clone);
		}
		Debug.Log(this.clones[0]);
	}

	void FixedUpdate() {
	}

	public void StartGame() {
		StartCoroutine(SpawnClones());
	}

	// Update is called once per frame
	IEnumerator SpawnClones() {

		for (int i = 0; i < cloneNumber; i++) {
			// attach next clone to motion recorder
			GameObject clone = clones[i];
			motionRecorder.ConnectRemote(clone);

			// reset world positions
			ResetWorld();

			// reset player position
			ResetPlayer();

			// start recording
			motionRecorder.RecordMotion();

			yield return new WaitForSeconds(cloneDuration);

			// stop recording
			motionRecorder.ToggleRecording();
		}
	}

	GameObject CreateClone(int num) {
		return Instantiate(clonePrefab, new Vector3(num, -10, 0), Quaternion.identity) as GameObject;
	}

	void ResetPlayer() {
		GameObject cameraRig = GameObject.Find("/[CameraRig]");
		cameraRig.transform.position = startLevel.transform.position;
		cameraRig.transform.rotation = Quaternion.identity;
	}

	void ResetWorld() {
		foreach (GameObject clone in this.clones) {
			CloneController cloneCtrl = clone.GetComponent<CloneController>();
			cloneCtrl.RewindPlayback();
		}

		foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Dynamic")) {
			Resettable resettableObj = gameObj.GetComponent<Resettable>();
			if (resettableObj == null) continue;
			resettableObj.ResetState();
		}
	}

}
