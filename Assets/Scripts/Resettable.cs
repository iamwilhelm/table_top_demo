using UnityEngine;
using System.Collections;

public class Resettable : MonoBehaviour {

	public Vector3 position;
	public Quaternion rotation;


	// Use this for initialization
	void Start () {
		this.position = transform.position;
		this.rotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
	}

	public void ResetState() {
		transform.position = this.position;
		transform.rotation = this.rotation;

		Collectable coll = GetComponent<Collectable>();
		if (coll != null) {
			coll.ResetState();
		}
	}
}
