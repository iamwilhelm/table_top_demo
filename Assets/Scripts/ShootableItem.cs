using UnityEngine;
using System.Collections;

public class ShootableItem : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnParticleCollision(GameObject projectile) {
		Debug.Log(projectile.transform.position);
	}

}
