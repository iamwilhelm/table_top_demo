using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoteControllable : MonoBehaviour {

	private GameObject srcObject;
	public int numPoints;

	private List<Vector3> objectPositions;
	private List<Quaternion> objectRotations;
	private int max;
	private int cursor;
	private bool isRecording;
	private bool isPlayback;

	// Use this for initialization
	void Start() {
		objectPositions = new List<Vector3>(numPoints);
		objectRotations = new List<Quaternion>(numPoints);
		cursor = 0;
		max = 0;
		isRecording = false;
		isPlayback = true;
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (IsRecording()) {
			RecordMotion();
		} else {
			PlaybackMotion();
		}
	}

	void RecordMotion() {
		if (srcObject == null) {
			Debug.Log("src Object doesn't exist!");
			return;
		}
		objectPositions.Add(srcObject.transform.position);
		objectRotations.Add(srcObject.transform.rotation);
		max += 1;
	}

	void PlaybackMotion() {
		if (cursor < max && cursor < max) {
			transform.position = objectPositions[cursor];
			transform.rotation = objectRotations[cursor];
			cursor += 1;
		}
	}

	public void Record(GameObject src) {
		srcObject = src;
		isRecording = true;
		isPlayback = false;
		objectPositions.Clear();
		objectRotations.Clear();
		cursor = 0;
		max = 0;
	}

	public void Playback() {
		Debug.Log("Now Playback");
		isRecording = false;
		isPlayback = true;
		cursor = 0;
	}

	public bool IsRecording() {
		return isRecording;
	}

}
