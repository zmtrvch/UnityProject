using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	string spriteName;

	public override void OnRabitHit(HeroRabbit rabit)
	{
		
		CrystalPanel.crystals.Crystals (name);
		this.CollectedHide();
	}
}
