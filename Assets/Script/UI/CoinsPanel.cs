using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinsPanel : MonoBehaviour {
	public int nullNumber; //кількість нулів
	public UILabel label;
	int current_number;


	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name != "Levels") {
			current_number = LevelController.current.getCoins ();
			writeCoins ();
		} else {
			
			label.text = "000"+LevelController.collectedCoins.ToString();
		}


	}

	int getZeroNumber(int number) {
		int count = (number == 0) ? 1 : 0;
		while (number != 0) {
			count++;
			number /= 10;
		}
		return nullNumber - number;
	}

	void writeCoins() {
		string text = "";
		for (int i = 0; i < getZeroNumber (current_number); ++i) {
			text += "0";
		}
		text += current_number.ToString ();
		label.text = text;
	}
}