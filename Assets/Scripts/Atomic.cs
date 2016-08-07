using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Atomic))]
public class Atomic : MonoBehaviour {

	public int atomicNumber = 1;
	public float electronegativity = 2.20f;

	// for monitoring
	public int totalElectrons;
	public float netCharge;
	public int baseValenceElectrons;
	public int valenceElectrons;
	public int shareableElectrons;
	public int shareableHoles;
	public int valenceOrbitalPositions;

	public SphereCollider bodyCollider;
	public SphereCollider grabCollider;
	public HashSet<Atomic> bondedAtoms;
	public HashSet<Atomic> nearbyAtoms;
	public int bondedAtomsCount;
	public int nearbyAtomsCount;

	private Rigidbody rb;

	public double COULOMB_CONST;
	public float ELECTRON_CHARGE;

	// Use this for initialization
	void Start () {
		this.rb = GetComponent<Rigidbody>();
		this.bondedAtoms = new HashSet<Atomic>();
		this.nearbyAtoms = new HashSet<Atomic>();

		// units in N * nm^2 / e^2
		this.COULOMB_CONST = 0.0000000002306f;
		// units in electron charge
		this.ELECTRON_CHARGE = -1.0f;
	}

	void FixedUpdate () {
		foreach (Atomic atom in this.nearbyAtoms) {
			rb.AddForce(CoulombForce(atom));
		}

		// just checking
		this.totalElectrons = TotalElectrons();
		this.netCharge = NetCharge();
		this.bondedAtomsCount = bondedAtoms.Count;
		this.nearbyAtomsCount = nearbyAtoms.Count;
		this.baseValenceElectrons = BaseValenceElectrons();
		this.valenceElectrons = ValenceElectrons();
		this.shareableElectrons = ShareableElectrons();
		this.shareableHoles = ShareableHoles();
		this.valenceOrbitalPositions = ValenceOrbitalPositions();
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

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "Atom") return;

		//Debug.Log(string.Format("Collision: {0} with {1}", this.gameObject, collision.gameObject));
		Atomic otherAtom = collision.gameObject.GetComponent<Atomic>();
		MakeBondWith(otherAtom, collision);
	}

	void OnJointBreak(float breakForce) {
		// BreakBondWith()
		// trigger particle system
	}

	void MakeBondWith(Atomic otherAtom, Collision collision) {
		// TODO is this a race condition for collision messages?
		// Return if we've already bonded with this atom
		if (this.bondedAtoms.Contains(otherAtom)) return;

		// who wants more electrons based on electronegativity?
		if (this.electronegativity > otherAtom.electronegativity) {
			// this atom wants to get electrons
			// how many electrons can I recv and how many can other give?
			if (this.ShareableHoles() > 0 && otherAtom.ShareableElectrons() > 0) {
				CreateBond(otherAtom, collision);
			}
		} else if (this.electronegativity < otherAtom.electronegativity) {
			// this atom wants to give electrons
			// how many electrons can I give and how many can other get?
			if (this.ShareableElectrons() > 0 && otherAtom.ShareableHoles() > 0) {
				CreateBond(otherAtom, collision);
			}
		} else {
			// both atoms have the same electronegativity
			if (this.ShareableElectrons() > 0 &&
					this.ShareableHoles() > 0 &&
					otherAtom.ShareableElectrons() > 0 &&
					otherAtom.ShareableHoles() > 0) {
				CreateBond(otherAtom, collision);
			}
		}
	}

	void CreateBond(Atomic otherAtom, Collision collision) {
		// How do we know if it's a double or triple bond?
		// add to list of bonded atoms
		Debug.Log(string.Format("{0} bonding with {1}", this, otherAtom));
		this.AddToBondedAtoms(otherAtom);
		otherAtom.AddToBondedAtoms(this);

		// calculate collision force. The amount determines if it's a single, double, or triple bond


		// create spring joint
		SpringJoint bond = gameObject.AddComponent<SpringJoint>();
		bond.autoConfigureConnectedAnchor = false;
		bond.connectedAnchor = new Vector3(0, 0, 0);
		bond.anchor = new Vector3(0, 0, 0);
		bond.connectedBody = collision.rigidbody;
		bond.spring = BondSpringConstant(otherAtom);
		bond.damper = 0.5f;
		bond.minDistance = 0.0105f;
		bond.maxDistance = 0.0112f;

		// FIXME just guessed at what would break a spring. 0.25 of energy to stretch spring 1nm?
		bond.breakForce = BondSpringConstant(otherAtom) / 4.0f;

	}

	// FIXME technically being reached into from other atom.
	void AddToBondedAtoms(Atomic otherAtom) {
		bondedAtoms.Add(otherAtom);
	}

	void BreakBondWith(GameObject atom) {
	}

	public Vector3 CoulombForce(Atomic atom) {
		float charges = NetCharge() * atom.NetCharge();
		Vector3 dist = transform.position - atom.transform.position;
		float distSqr = dist.sqrMagnitude;

		return (0.001f * charges / distSqr) * dist.normalized;
	}

	public float NetCharge() {
		// netcharge should take into account:
		// - any extra electrons picked up to make it an ion.
		// - amount of time electron spends in bonded atom
		return ELECTRON_CHARGE;
	}

	/*
	* Computed from Characteristic frequencies.
	* We only used the stretching vibration frequency here.
	* https://en.wikipedia.org/wiki/Molecular_vibration#Newtonian_mechanics
	*
	*/
	public float BondSpringConstant(Atomic otherAtom) {
		Atomic leading = null;
		Atomic lagging = null;
		if (this.atomicNumber > otherAtom.atomicNumber) {
			leading = this;
			lagging = otherAtom;
		} else {
			leading = otherAtom;
			lagging = this;
		}

		if (leading.atomicNumber == 6) {
		  if (lagging.atomicNumber == 1) {
				// if single bond carbon
				return 167.79f;
				// if double bonded carbon

				// if triple bond carbon

			} else if (lagging.atomicNumber == 6) {
				return 353.75f;
			} else if (lagging.atomicNumber == 8) {
				// double bonded
				return 442.02f;
			}
		} else if (leading.atomicNumber == 7) {
			if (lagging.atomicNumber == 1) {
				// if single bond
				return 232.52f;
			}

		} else if (leading.atomicNumber == 8) {
			if (lagging.atomicNumber == 1) {
				// if single bond
				return 252.14f;
			}
		}

		Debug.Log("Spring Const not in table");
		return 250.0f;
	}

	public int TotalElectrons() {
		return this.atomicNumber + bondedAtoms.Count;
	}

	public int BaseValenceElectrons() {
		int[] orbitals = { 2, 8, 8 };
		int electrons = this.atomicNumber;

		foreach (int orbital in orbitals) {
			if ((electrons - orbital) < 0) {
				return electrons;
			}
			electrons -= orbital;
		}
		Debug.Log("ran out of orbitals to subtract electrons");
		return electrons;
	}

	public int ValenceElectrons() {
		return BaseValenceElectrons() + bondedAtoms.Count;
	}

	public int ShareableElectrons() {
		return BaseValenceElectrons() - bondedAtoms.Count;
	}

	public int ShareableHoles() {
		return ValenceOrbitalPositions() - ValenceElectrons();
	}

	public int ValenceOrbitalPositions() {
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
