using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class HandController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device controller;
	HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
	private InteractableItem closestItem;
	private InteractableItem interactingItem;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (trackedObj == null) return;

		try {
			controller = SteamVR_Controller.Input((int)trackedObj.index);
		} catch(System.IndexOutOfRangeException err) {
			// happens when the controllers aren't turned on
			return;
		}
		if (controller == null) return;

		// when using the trigger
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			controller.TriggerHapticPulse(2000);
			pickUp();
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
			controller.TriggerHapticPulse(2000);
			letGo();
		}

		// when using the trackpad
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
		}

		// when using the grip
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
			controller.TriggerHapticPulse(2000);
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
			controller.TriggerHapticPulse(2000);
		}
	}

	public void pickUp() {
		float minDistance = float.MaxValue;
		float distance;

		closestItem = null;
		foreach (InteractableItem item in objectsHoveringOver) {
			distance = (item.transform.position - transform.position).sqrMagnitude;
			if (distance < minDistance) {
				minDistance = distance;
				closestItem = item;
			}
		}
		interactingItem = closestItem;

		if (interactingItem) {
			makeVisible(false);

			if (interactingItem.IsInteracting()) {
				interactingItem.OnExitInteraction(this);
			}
			interactingItem.OnEnterInteraction(this);
		}
	}

	private void makeVisible(bool isVisible) {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.detectCollisions = isVisible;

		Component[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in meshRenderers) {
			meshRenderer.enabled = isVisible;
		}
	}

	public void letGo() {
		makeVisible(true);

		if (interactingItem != null) {
			interactingItem.OnExitInteraction(this);
		}
	}

	public Rigidbody getRigidbody() {
		return GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider collider) {
		InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
		if (collidedItem) {
			objectsHoveringOver.Add(collidedItem);
		}
	}

	void OnTriggerExit(Collider collider) {
		InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
		if (collidedItem) {
			objectsHoveringOver.Remove(collidedItem);
		}
	}
}
