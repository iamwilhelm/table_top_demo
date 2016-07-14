using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	public GameObject srcHead;
	public GameObject srcLeftHand;
	public GameObject srcRightHand;

	public GameObject tgtHead;
	public GameObject tgtLeftHand;
	public GameObject tgtRightHand;

	private RemoteControllable remoteHead;
	private RemoteControllable remoteLeftHand;
	private RemoteControllable remoteRightHand;

	// Use this for initialization
	void Start () {
		remoteHead = tgtHead.GetComponent<RemoteControllable>();
		remoteLeftHand = tgtLeftHand.GetComponent<RemoteControllable>();
		remoteRightHand = tgtRightHand.GetComponent<RemoteControllable>();
	}

	// Update is called once per frame
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		if (controller == null) return;

		// when using the trackpad
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			ToggleRecording(remoteHead, srcHead);
			ToggleRecording(remoteLeftHand, srcLeftHand);
			ToggleRecording(remoteRightHand, srcRightHand);
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
		}
	}

	public void ToggleRecording(RemoteControllable remote, GameObject srcGameObject) {
		Debug.Log(remote);

		if (remote.IsRecording()) {
			remote.Playback();
		} else {
			remote.Record(srcGameObject);
		}
	}

}
