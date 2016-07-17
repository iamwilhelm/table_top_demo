using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Collectable : MonoBehaviour {

	public ParticleSystem dissolve;

	private Rigidbody rb;
	private MeshRenderer meshRen;
	private float collectedTime = -1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		meshRen = GetComponent<MeshRenderer>();
		rb.isKinematic = true;
		rb.useGravity = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (collectedTime == -1) return;
		if (Time.fixedTime > collectedTime + 2.0f) {
			gameObject.SetActive(false);
		} else if (Time.fixedTime > collectedTime + 0.8f) {
			meshRen.enabled = false;
		}
	}

	void OnTriggerEnter(Collider collider) {
		collectedTime = Time.fixedTime;
		rb.isKinematic = false;
		rb.useGravity = true;
		Vector3 popForce = new Vector3(0, 4 * rb.mass, 0);
		rb.AddForce(popForce, ForceMode.Impulse);
		dissolve.Play();
	}

}
