using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	public TextMesh scorekeeper;
	private int score;
	private bool validEnter;

	// Use this for initialization
	void Start () {
		resetScore();
		this.validEnter = false;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.attachedRigidbody.velocity.y < 0) {
			this.validEnter = true;
		} else {
			this.validEnter = false;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.attachedRigidbody.velocity.y < 0 && validEnter) {
			scorePoints(1);
			this.validEnter = false;
			scorekeeper.text = getScore().ToString();
		}
	}

	int getScore() {
		return score;
	}

	void scorePoints(int points) {
		score += points;
	}

	void resetScore() {
		score = 0;
	}
}
