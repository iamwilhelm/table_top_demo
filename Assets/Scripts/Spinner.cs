using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public float angularVelocity;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, angularVelocity, 0) * Time.deltaTime);
	}
}
