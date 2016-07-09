using UnityEngine;
using System.Collections;

public class RayDamage : MonoBehaviour {

	public float force;
	public float radius;
	public GameObject effectParticleSystem;

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
			// don't blow away the player's hand
			if (c.gameObject.CompareTag("Player")) continue;
			Debug.Log(c);

			c.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);

			// add lingering particle effect
			if (effectParticleSystem != null) {
				GameObject effect = Instantiate(effectParticleSystem, c.transform.position, c.transform.rotation) as GameObject;
				effect.transform.parent = c.gameObject.transform;
				Destroy(effect, 10);
			}

			// TODO abstract the effect of spell out of the ray damage
			Levitatable levitation = c.GetComponent<Levitatable>();
			if (levitation != null) {
				levitation.activate(!levitation.isActive());
			}
		}
	}
}
