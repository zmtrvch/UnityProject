using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	int fruitsNumber = 0;
	int coinsNumber = 0;
	int lifesNumber = 3;
	int CrystalColor = -1;

	public static LevelController current;



	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}
	public void onRabitDeath(HeroRabbit rabit) {
		//При смерті кролика повертаємо на початкову позицію
		decreaseLifeNumber ();
		rabit.transform.position = this.startingPosition;

	}
	public void addCoins(int number)
	{
		Debug.Log("Coins collected " + number);
		coinsNumber++;
	}
	public void addFruit(int number)
	{
		Debug.Log("Fruits collected " + number);
		fruitsNumber++;

	}
	public void addCrystal(int number)
	{
		Debug.Log("Crystals collected " + number);
	}
	public int getFruits() {
		return fruitsNumber;
	}

	public int getCoins() {
		return coinsNumber;
	}

	public int getLifes() {
		return lifesNumber;
	}

	void decreaseLifeNumber() {
		if (lifesNumber <= 0) {
			lifesNumber = 3; //оновлюемо життя
		} else {
			lifesNumber--;
		}
	}

	public void setCrystalColor(int color) {
		CrystalColor = color;
	}

	public int getCurCrystalColor() {
		return CrystalColor;
	}
}

