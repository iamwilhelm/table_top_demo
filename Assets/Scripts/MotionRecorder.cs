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

	public void ConnectRemote(GameObject clone) {
		CloneController cloneCtrl = clone.GetComponent<CloneController>();

		// attach left hand tracked object to clone
		cloneCtrl.AttachTrackedObject("LeftHand", srcLeftHand);
		// attach right hand tracked object to clone
		cloneCtrl.AttachTrackedObject("RightHand", srcRightHand);

		// attach left, right, and head of clone to motion recorder
		remoteLeftHand = cloneCtrl.GetPartMotionPlayer("LeftHand");
		remoteRightHand = cloneCtrl.GetPartMotionPlayer("RightHand");
		remoteHead = cloneCtrl.GetPartMotionPlayer("Head");
	}

	public void ToggleRecording() {
		Debug.Log(remoteHead);
		Debug.Log(srcHead);
		ToggleRecordingFor(remoteHead, srcHead);

		Debug.Log(remoteLeftHand);
		Debug.Log(srcLeftHand);
		ToggleRecordingFor(remoteLeftHand, srcLeftHand);

		Debug.Log(remoteRightHand);
		Debug.Log(srcRightHand);
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
