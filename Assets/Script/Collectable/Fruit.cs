using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {
	SpriteRenderer mySprite;
	bool isCollected;

	void Start(){
		int col = PlayerPrefs.GetInt (this.name.ToString (), 0);
		if (col == 1)
			isCollected = true;
		else
			isCollected = false;
		mySprite = GetComponent<SpriteRenderer> ();
		if (isCollected) {
			Color color = mySprite.color;
			color.a = 0.5f;
			mySprite.color = color;
		}
	}

	public override void OnRabitHit(HeroRabbit rabit)
	{
		PlayerPrefs.SetInt (this.name.ToString (), 1);
		LevelController.current.addFruit(1);
		this.CollectedHide();
		rabit.playMusicOnFruit();
	}
}
