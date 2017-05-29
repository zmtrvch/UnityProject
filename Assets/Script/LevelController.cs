using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	void Awake() {
		current = this;
	}

	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}
	public void onRabitDeath(HeroRabbit rabit) {
		//При смерті кролика повертаємо на початкову позицію
		rabit.transform.position = this.startingPosition;
	}
	public void addCoins(int number)
	{
		Debug.Log("Coins collected " + number);
	}
	public void addFruit(int number)
	{
		Debug.Log("Fruits collected " + number);
	}
	public void addCrystal(int number)
	{
		Debug.Log("Crystals collected " + number);
	}
}
