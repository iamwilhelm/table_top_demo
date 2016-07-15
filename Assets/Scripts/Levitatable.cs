using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Levitatable : MonoBehaviour {

	public float multiplier;
	public bool active;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		activate(active);
	}

	// FIXME this is not working
	void FixedUpdate () {
		if (active == false) return;

	  RaycastHit hitInfo;
		bool isIntersect = Physics.Raycast(transform.position,
																			 Vector3.down,
																			 out hitInfo);
		if (!isIntersect) return;
		//Debug.Log(hitInfo.distance);

		float amp = multiplier * rb.mass / Mathf.Pow( Mathf.Max(hitInfo.distance, 0.2f), 2);
		Vector3 forceVec = amp * Vector3.up;
		//Debug.Log(forceVec);

		rb.AddForce(forceVec);
	}

	public void activate(bool state) {
		active = state;
	}

	public bool isActive() {
		return active;
	}

}
