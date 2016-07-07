using UnityEngine;
using System.Collections;

public class ShootableItem : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnParticleCollision(GameObject projectile) {
		Debug.Log(projectile.transform.position);
	}

}
