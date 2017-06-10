using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	//sounds 
	public AudioClip music = null;
	AudioSource musicSource = null;

	public GameObject settingsPrefab;
	public bool deathSound = false;
	public MyButton pause;
	int fruitsNumber = 0;
	int coinsNumber = 0;
	int lifesNumber = 3;
	int CrystalColor = -1;

	public static LevelController current;
	void Awake() {
		current = this;
		//sounds
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicSource.loop = true;
		musicSource.Play ();
		this.pause.signalOnClick.AddListener (this.showSettings);
	}


	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}

	public void onRabitDeath(HeroRabbit rabit) {
		//При смерті кролика повертаємо на початкову позицію
		decreaseLifeNumber ();
		rabit.transform.position = this.startingPosition;
		Debug.Log ("Play");
		deathSound = true;




	}
	void showSettings() {
		//Знайти батьківський елемент
		Debug.Log("settings");
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		SettingsPanel popup = obj.GetComponent<SettingsPanel>();
		Time.timeScale = 0;
	} 
	public void setMusicOff(){

		musicSource.Pause ();
	}

	public void setMusicOn(){

		musicSource.Play ();
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
			SceneManager.LoadScene ("Levels"); //оновлюемо життя
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

