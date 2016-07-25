﻿using UnityEngine;
using System.Collections;

public class CloneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void ResetPlayback() {
		// find all motion playbacks in children and set to playback
		Component[] motionPlayers = GetComponentsInChildren<MotionPlayer>();
		foreach (MotionPlayer mp in motionPlayers) {
			mp.Playback();
		}
	}

	// Cannot use in Update. Calls Find on Objects
	public void AttachTrackedObject(string partName, SteamVR_TrackedObject trackedObj) {
		if (partName == "LeftHand") {

			GameObject leftHand = transform.Find("LeftCloneHand").gameObject;
			HandController leftHC = leftHand.GetComponent<HandController>();
			if (leftHC == null) { Debug.Log("leftHC is null"); return; }
			leftHC.trackedObj = trackedObj;

		} else if (partName == "RightHand") {

			GameObject rightHand = transform.Find("RightCloneHand").gameObject;
			HandController rightHC = rightHand.GetComponent<HandController>();
			if (rightHC == null) return;
			rightHC.trackedObj = trackedObj;

		}
	}

	// Cannot use in Update. Calls Find on Objects
	public MotionPlayer GetPartMotionPlayer(string partName) {
		if (partName == "LeftHand") {
			GameObject leftHand = transform.Find("LeftCloneHand").gameObject;
			return leftHand.GetComponent<MotionPlayer>();
		} else if (partName == "RightHand") {
			GameObject rightHand = transform.Find("RightCloneHand").gameObject;
			return rightHand.GetComponent<MotionPlayer>();
		} else if (partName == "Head"){
			GameObject head = transform.Find("Head").gameObject;
			return head.GetComponent<MotionPlayer>();
		} else {
			return null;
		}
	}

}