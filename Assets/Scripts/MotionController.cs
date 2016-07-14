using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	public MotionRecorder motionRecorder;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		if (controller == null) return;

		// when using the trigger
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			motionRecorder.RecordTriggerDown(trackedObj);
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
			motionRecorder.RecordTriggerUp(trackedObj);
		}

		// when using the trackpad
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			motionRecorder.ToggleRecording();
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
		}
	}


}
