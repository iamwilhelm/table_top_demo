using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	public GameObject fireballFab;
	public Transform spellSpawn;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			GameObject fireball = Instantiate(fireballFab, spellSpawn.position, spellSpawn.rotation) as GameObject;
			Destroy(fireball, 5);
		}

	}
}
