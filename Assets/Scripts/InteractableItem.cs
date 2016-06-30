using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

	private Rigidbody rb;
	private bool currentlyInteracting;
	private HandController attachedHand;
	private Transform interactionPoint;

	private Vector3 posDelta;
	private float velocityFactor = 20000f;

	private Quaternion rotationDelta;
	private float rotationFactor = 400f;

	private float angle;
	private Vector3 axis;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		interactionPoint = new GameObject().transform;

		currentlyInteracting = false;
		velocityFactor /= rb.mass;
		rotationFactor /= rb.mass;

		if (rb == null) {
			Debug.Log("Interactable Item doesn't have a rigidbody");
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (attachedHand && IsInteracting()) {

			posDelta = attachedHand.transform.position - interactionPoint.position;
			this.rb.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

			rotationDelta = attachedHand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
			rotationDelta.ToAngleAxis(out angle, out axis);

			if (angle > 180) {
				angle -= 360;
			}

			rb.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
		}
	}

	public void OnEnterInteraction(HandController hand) {
		attachedHand = hand;

		interactionPoint.position = hand.transform.position;
		interactionPoint.rotation = hand.transform.rotation;
		interactionPoint.SetParent(transform, true);

		currentlyInteracting = true;
	}

	public void OnExitInteraction(HandController hand) {
		if (hand == attachedHand) {
			attachedHand = null;
			currentlyInteracting = false;
		}
	}

	public bool IsInteracting() {
		return currentlyInteracting;
	}
}
