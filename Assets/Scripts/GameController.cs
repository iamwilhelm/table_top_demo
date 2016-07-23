using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MotionRecorder))]
public class GameController : MonoBehaviour {

	public int cloneNumber = 10;
	public int cloneDuration = 30;
	public GameObject clonePrefab;
	public GameObject startLevel;
	public bool startGame = false;

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
		if (startGame == true) {
			startGame = false;
			StartCoroutine(SpawnClones());
		}
	}

	// Update is called once per frame
	IEnumerator SpawnClones() {

		for (int i = 0; i < cloneNumber; i++) {
			Debug.Log(this.clones[0]);
			GameObject clone = clones[i];

			// attach next clone to motion recorder
			motionRecorder.ConnectRemote(clone);

			// reset player position
			Debug.Log("reset player");
			ResetPlayer();

			// reset world positions
			Debug.Log("reset world positions");
			ResetWorld();

			// start recording
			Debug.Log("start recording");
			motionRecorder.ToggleRecording();

			Debug.Log("ticking down");
			yield return new WaitForSeconds(cloneDuration);

			// stop recording
			Debug.Log("stop recording");
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
		Debug.Log("reset world");
		foreach (GameObject gameObj in this.clones) {
			CloneController cloneCtrl = gameObj.GetComponent<CloneController>();
			cloneCtrl.ResetPlayback();
		}
	}

}
