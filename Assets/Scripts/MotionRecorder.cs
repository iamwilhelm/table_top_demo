using UnityEngine;
using System.Collections;

public class MotionRecorder : MonoBehaviour {

	public SteamVR_TrackedObject srcLeftHand;
	public SteamVR_TrackedObject srcRightHand;
	public SteamVR_TrackedObject srcHead;

	public MotionPlayer remoteLeftHand;
	public MotionPlayer remoteRightHand;
	public MotionPlayer remoteHead;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {
	}

	public void ToggleRecording() {
		ToggleRecordingFor(remoteHead, srcHead);
		ToggleRecordingFor(remoteLeftHand, srcLeftHand);
		ToggleRecordingFor(remoteRightHand, srcRightHand);
	}

	void ToggleRecordingFor(MotionPlayer remote, SteamVR_TrackedObject srcTrackedObj) {
		if (remote.IsRecording()) {
			remote.Playback();
		} else {
			remote.Record(srcTrackedObj.gameObject);
		}
	}

	public void RecordTriggerDown(SteamVR_TrackedObject trackedObj) {
		if (trackedObj == srcLeftHand) {
			remoteLeftHand.RecordTriggerDown();
		} else if (trackedObj == srcRightHand) {
			remoteRightHand.RecordTriggerDown();
		}
	}

	public void RecordTriggerUp(SteamVR_TrackedObject trackedObj) {
		if (trackedObj == srcLeftHand) {
			remoteLeftHand.RecordTriggerUp();
		} else if (trackedObj == srcRightHand) {
			remoteRightHand.RecordTriggerUp();
		}
	}

}
