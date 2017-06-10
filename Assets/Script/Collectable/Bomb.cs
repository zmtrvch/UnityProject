using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	public override void OnRabitHit(HeroRabbit rabit)
	{
		this.CollectedHide();
		HeroRabbit.current.colideBomb();
		HeroRabbit.current.playMusicOnBomb ();


	}
}
