using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsPanel : MonoBehaviour {
	int currentNumber;
	public int max;
	public UILabel Frlabel;

	// Update is called once per frame
	void FixedUpdate () {
		currentNumber = LevelController.current.getFruits();
		if (currentNumber <= max) {
			write ();
		}
	}

	void write() {
		Frlabel.text = LevelController.current.getFruits().ToString () + "/" + max;
	}
}