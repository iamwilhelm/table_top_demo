using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class SplashDamage : MonoBehaviour {

	public float radius;
	public float force;

	private ParticleSystem pSystem;
	private ParticleSystem.Particle[] particles;

	// Use this for initialization
	void Start () {
		if (pSystem == null)
			pSystem = GetComponent<ParticleSystem>();

		if (particles == null || particles.Length < pSystem.maxParticles)
			particles = new ParticleSystem.Particle[pSystem.maxParticles];
	}

	// Update is called once per frame
	void Update () {
	}

	void OnParticleCollision(GameObject hitObject) {
		RaycastHit hitInfo;
		Physics.Raycast(transform.position, transform.forward, out hitInfo);

		Collider[] colliders = Physics.OverlapSphere(hitInfo.point, this.radius);

		foreach (Collider c in colliders) {
			// skip all colliders without a rigidbody, so we don't explode static elements
			if (c.GetComponent<Rigidbody>() == null) continue;
			Debug.Log(c);

			// push all within the splash damage radius out
			c.GetComponent<Rigidbody>().AddExplosionForce(force, hitInfo.point, radius, 0.5f, ForceMode.Impulse);
		}
	}

}
