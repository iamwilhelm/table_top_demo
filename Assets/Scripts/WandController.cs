using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;
	public Transform spellSpawn;

	public GameObject IncendioSpell;
	public GameObject IncendioSpellIndicator;

	public GameObject StupefySpell;
	public GameObject StupefySpellIndicator;

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
			castSpell();
		}

		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			toggleSpell();
		}
	}

	void castSpell() {
			if (activeSpell == null) {
				return;
			} else if (activeSpell == IncendioSpell) {
				GameObject spellProjectile = Instantiate(activeSpell, spellSpawn.position, spellSpawn.rotation) as GameObject;
				Destroy(spellProjectile, 5);
			} else if (activeSpell == StupefySpell) {
				GameObject spellProjectile = Instantiate(activeSpell, spellSpawn.position, spellSpawn.rotation) as GameObject;
				RayDamage raydamage = spellProjectile.GetComponent<RayDamage>();
				raydamage.OnTrigger(this);
				Destroy(spellProjectile, 5);
			}

	}

	void toggleSpell() {
		if (activeSpell == IncendioSpell) {
			Debug.Log("toggle to stupefy");
			switchSpell("Stupefy");
		} else {
			Debug.Log("toggle to incendio");
			switchSpell("Incendio");
		}
		activateSpell();
	}

	void activateSpell() {
		Destroy(indicator);
		indicator = Instantiate(activeSpellIndicator, spellSpawn.position, spellSpawn.rotation) as GameObject;
		indicator.transform.parent = transform;
	}

	void switchSpell(string spellName) {
		if (spellName == "Incendio") {
			activeSpell = IncendioSpell;
			activeSpellIndicator = IncendioSpellIndicator;
		} else if (spellName == "Stupefy") {
			activeSpell = StupefySpell;
			activeSpellIndicator = StupefySpellIndicator;
		}
	}
}
