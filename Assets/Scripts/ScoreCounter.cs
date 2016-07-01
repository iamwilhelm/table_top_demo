using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	private int score;

	// Use this for initialization
	void Start () {
		resetScore();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log("entered hoop");
	}

	void OnTriggerExit(Collider collider) {
		scorePoints(1);
		Debug.Log("Score is now: " + getScore());
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
