using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable {


	public override void OnRabitHit(HeroRabbit rabit)
	{
		this.CollectedHide();
		LevelController.current.addLife ();
		HeroRabbit.current.playMusicOnLife ();


	}
}
