using UnityEngine;
using System.Collections;

public class ShootableItem : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		this.rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnParticleCollision(GameObject particle) {
		Debug.Log("hitting shootable item");
		this.rb.AddExplosionForce(10000.0f, particle.transform.position, 1.0f, 10000.0f);
	}

}
