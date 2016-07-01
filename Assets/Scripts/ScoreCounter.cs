using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	public TextMesh scorekeeper;
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
		scorekeeper.text = getScore().ToString();
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
