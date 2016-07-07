using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;
	public Transform spellSpawn;

	public GameObject IncendioSpell;
	public GameObject IncendioSpellIndicator;

	private GameObject activeSpell;
	private GameObject activeSpellIndicator;

	private GameObject indicator;

	// Use this for initialization
	void Start () {
		switchSpell("Incendio");
		activateSpell();
	}

	// Update is called once per frame
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			if (activeSpell == null) {
				return;
			}

			GameObject spellProjectile = Instantiate(activeSpell, spellSpawn.position, spellSpawn.rotation) as GameObject;
			Destroy(spellProjectile, 5);
		}

		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
		}
	}

	void activateSpell() {
		indicator = Instantiate(activeSpellIndicator, spellSpawn.position, spellSpawn.rotation) as GameObject;
		indicator.transform.parent = transform;
	}

	void switchSpell(string spellName) {
		activeSpell = IncendioSpell;
		activeSpellIndicator = IncendioSpellIndicator;
	}
}
