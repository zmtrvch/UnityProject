using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {
	public override void OnRabitHit(HeroRabbit rabit)
	{
		LevelController.current.addCrystal(1);
		this.CollectedHide();
	}
}
