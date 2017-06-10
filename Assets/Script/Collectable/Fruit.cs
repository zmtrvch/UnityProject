using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

	public override void OnRabitHit(HeroRabbit rabit)
	{
		LevelController.current.addFruit(1);
		this.CollectedHide();
		rabit.playMusicOnFruit ();
	}
}
