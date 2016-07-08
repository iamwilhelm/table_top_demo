using UnityEngine;
using System.Collections;

public class RayDamage : MonoBehaviour {

	public float force;
	public float radius;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {
	}

	public void OnTrigger(WandController wand) {
		RaycastHit hitInfo;
		bool isIntersect = Physics.Raycast(transform.position, transform.forward, out hitInfo);
		if (isIntersect == false) return;

		Debug.Log(hitInfo.point);

		Collider[] colliders = Physics.OverlapSphere(hitInfo.point, this.radius);

		foreach (Collider c in colliders) {
			if (c.GetComponent<Rigidbody>() == null) continue;
			Debug.Log(c);

			c.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
		}
	}
}
