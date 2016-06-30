using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		// when using the trigger
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log("trigger and there is object");
			controller.TriggerHapticPulse(2000);

			float minDistance = float.MaxValue;
			float distance;
			foreach (InteractableItem item in objectsHoveringOver) {
				distance = (item.transform.position - transform.position).sqrMagnitude;
				if (distance < minDistance) {
					minDistance = distance;
					closestItem = item;
				}
			}
			interactingItem = closestItem;

			if (interactingItem) {
				if (interactingItem.IsInteracting()) {
					interactingItem.OnExitInteraction(this);
				}
				interactingItem.OnEnterInteraction(this);
			}
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
			controller.TriggerHapticPulse(2000);

			if (interactingItem != null) {
				interactingItem.OnExitInteraction(this);
			}
		}

		// when using the grip
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
			controller.TriggerHapticPulse(2000);
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
			controller.TriggerHapticPulse(2000);
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
