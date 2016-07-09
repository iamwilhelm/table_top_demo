using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Levitatable : MonoBehaviour {

	public bool active;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		activate(active);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (active == false) return;

	  RaycastHit hitInfo;
		bool isIntersect = Physics.Raycast(transform.position, Vector3.down, out hitInfo);
		if (!isIntersect) return;

		Debug.Log(hitInfo.distance);
		Vector3 forceVec = Vector3.up * rb.mass / Mathf.Pow(hitInfo.distance, 2);

		Debug.Log(forceVec);
		rb.AddForce(forceVec);
	}

	public void activate(bool state) {
		active = state;
	}

	public bool isActive() {
		return active;
	}

}
