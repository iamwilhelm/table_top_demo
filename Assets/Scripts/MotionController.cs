using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	public GameObject remoteObject;
	private RemoteControllable remote;

	// Use this for initialization
	void Start () {
		if (remoteObject != null) {
			remote = remoteObject.GetComponent<RemoteControllable>();
			Debug.Log("has remote object");
			Debug.Log(remoteObject);
		} else {
			Debug.Log("no remote object");
		}
	}

	// Update is called once per frame
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		// when using the trackpad
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			Debug.Log("clicked touchpad");
			Debug.Log(remote);
			if (remote == null) return;

			if (remote.IsRecording()) {
				Debug.Log("playback");
				remote.Playback();
			} else {
				Debug.Log("recording");
				remote.Record(this.gameObject);
			}
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
		}
	}

}
