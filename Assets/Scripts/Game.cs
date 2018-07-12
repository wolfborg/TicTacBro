using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	public List<Button> squareButtons;
	public List<Text> squares;
	public Text yourScoreText, dumbAIScoreText, victoryText;
	public GameObject victoryScreen;
	int yourScore, dumbAIScore;
	bool isPlaying, firstTurn;

	// Use this for initialization
	void Start () {
		yourScoreText.text = yourScore.ToString();
		dumbAIScoreText.text = dumbAIScore.ToString();
		firstTurn = true;
		resetGame();
	}

	public void buttonClick(int square) {
		if (squares[square].text == "") {
			squares[square].text = "X";
			victoryCheck(0);
			computersTurn();
		}
	}

	void computersTurn() {
		if (isPlaying) {
			smartMove();
		}
	}

	void smartMove() {
		//int count = 0;
		foreach(Text t in squares) {
			if (t.text == "") {
				t.text = "O";
				if (isVictory()) {
					victoryCheck(1);
					return;
				}
				t.text = "X";
				if (isVictory()) {
					t.text = "O";
					victoryCheck(1);
					//if (count > squares.Count - 1) {
					//}
					return;
				}
				t.text = "";
			}

			//count++;
		}
		dumbMove();
	}

	void dumbMove() {
		int rand = Random.Range(0, 9);
		if (squares[rand].text == "") {
			squares[rand].text = "O";
			victoryCheck(1);
		} else {
			dumbMove();
		}
	}

	void victoryCheck(int player) {
		for(int i=0; i<squares.Count; i++) {
			if (squares[i].text == "") {
				break;
			}
			if (i == squares.Count-1) {
				victory(2);
			}
		}

		if (isVictory()) {
			victory(player);
		}
	}

	bool isVictory() {
		if (//horizonal
			checkMatch(0, 1, 2) ||
			checkMatch(3, 4, 5) ||
			checkMatch(6, 7, 8) ||
			//vertical
			checkMatch(0, 3, 6) ||
			checkMatch(1, 4, 7) ||
			checkMatch(2, 5, 8) ||
			//diagonal
			checkMatch(0, 4, 8) ||
			checkMatch(2, 4, 6)) {
			return true;
		}

		return false;
	}

	bool checkMatch(int s1, int s2, int s3) {
		string s1t = squares[s1].text;
		string s2t = squares[s2].text;
		string s3t = squares[s3].text;
		return (s1t != "" && s2t != "" && s3t != "" && 
				s1t == s2t && s2t == s3t && s1t == s3t);
	}

	void victory(int state) {
		isPlaying = false;
		foreach(Button squareButton in squareButtons) {
			squareButton.enabled = false;
		}
		victoryScreen.SetActive(true);
		if (state == 0) {
			victoryText.text = "WIN!";
			yourScore++;
			yourScoreText.text = yourScore.ToString();
		} else if (state == 1) {
			victoryText.text = "LOSE!";
			dumbAIScore++;
			dumbAIScoreText.text = dumbAIScore.ToString();
		} else if (state == 2) {
			victoryText.text = "DRAW!";
		}
		switchFirstTurn();
	}

	public void resetGame() {
		isPlaying = true;

		foreach(Button squareButton in squareButtons) {
			squareButton.enabled = true;
		}

		for (int i = 0; i < squares.Count; i++) {
			squares[i].text = "";
		}
		victoryScreen.SetActive(false);
		if (!firstTurn) {
			computersTurn();
		}
	}

	void switchFirstTurn() {
		if (firstTurn) {
			firstTurn = false;
		} else {
			firstTurn = true;
		}
	}
}
