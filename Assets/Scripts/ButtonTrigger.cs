using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {

	public float max = 0.16f;
	public float min = 0.11f;

	private bool buttonOn = false;
	private Transform buttonSurfaceTf;

	// Use this for initialization
	void Start () {
		buttonSurfaceTf = transform.Find("Surface");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (buttonOn == true) {
			buttonSurfaceTf.position = new Vector3(0, min, 0);
		} else {
			buttonSurfaceTf.position = new Vector3(0, max, 0);
		}
	}

	void OnTriggerEnter() {
		buttonOn = true;
	}

	void OnTriggerExit() {
		buttonOn = false;
	}
}
