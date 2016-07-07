using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class SplashDamage : MonoBehaviour {

	public float radius;
	public float force;

	private ParticleSystem pSystem;
	private ParticleSystem.Particle[] particles;
	private Vector3 lastKnownPoint;

	// Use this for initialization
	void Start () {
		if (pSystem == null)
			pSystem = GetComponent<ParticleSystem>();

		if (particles == null || particles.Length < pSystem.maxParticles)
			particles = new ParticleSystem.Particle[pSystem.maxParticles];
	}

	// Update is called once per frame
	void FixedUpdate () {
		int numParticlesAlive = pSystem.GetParticles(particles);
		if (numParticlesAlive > 0) {
			lastKnownPoint = transform.TransformPoint(particles[0].position);
		}
	}

	void OnParticleCollision(GameObject hitObject) {
		// TODO raycasting only works for static environments
		// RaycastHit hitInfo;
		// Physics.Raycast(transform.position, transform.forward, out hitInfo);

		Collider[] colliders = Physics.OverlapSphere(lastKnownPoint, this.radius);

		foreach (Collider c in colliders) {
			// skip all colliders without a rigidbody, so we don't explode static elements
			if (c.GetComponent<Rigidbody>() == null) continue;
			Debug.Log(c);

			// push all within the splash damage radius out
			c.GetComponent<Rigidbody>().AddExplosionForce(force, lastKnownPoint, radius, 0.5f, ForceMode.Impulse);
		}
	}

}
