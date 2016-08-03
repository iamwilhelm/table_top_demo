using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Atomic))]
public class Atomic : MonoBehaviour {

	public int atomicNumber = 1;
	public int valenceElectrons = 1;
	public SphereCollider bodyCollider;
	public SphereCollider grabCollider;
	public List<GameObject> bondedAtoms;
	public List<Atomic> nearbyAtoms;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		this.rb = GetComponent<Rigidbody>();
		this.bondedAtoms = new List<GameObject>(8);
		this.nearbyAtoms= new List<Atomic>(10);
	}

	void FixedUpdate () {
		foreach (Atomic atom in this.nearbyAtoms) {
			rb.AddForce(CoulombForce(atom));
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag != "Atom") return;

		Atomic atom = collider.GetComponent<Atomic>();
		this.nearbyAtoms.Add(atom);
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag != "Atom") return;

		Atomic atom = collider.GetComponent<Atomic>();
		this.nearbyAtoms.Remove(atom);
	}

	void MakeBondWith(GameObject atom) {
		// Have we already bonded with this atom?
		GameObject result = bondedAtoms.Find(e => e == GetComponent<Collider>().gameObject);
		if (result != null) return;

		Debug.Log("bonding");
		bondedAtoms.Add(atom);

		//atom.transform.SetParent(transform, true);
	}

	void BreakBondWith(GameObject atom) {
	}

	public Vector3 CoulombForce(Atomic atom) {
		float charges = Charge() * atom.Charge();
		Vector3 dist = transform.position - atom.transform.position;
		float distSqr = dist.sqrMagnitude;

		return (0.001f * charges / distSqr) * dist.normalized;
	}

	public float Charge() {
		return 1.0f;
	}

	public int TotalValenceElectrons() {
		int period = Period();
		return 2 * period * period;
	}

	public int Period() {
		if (atomicNumber >= 1 && atomicNumber <= 2) {
			return 1;
		} else if (atomicNumber >= 3 && atomicNumber <= 10) {
			return 2;
		} else if (atomicNumber >= 11 && atomicNumber <= 18) {
			return 3;
		} else {
			Debug.Log("Can't get period for atomic numbers higher than 18");
			return 0;
		}
	}
}
