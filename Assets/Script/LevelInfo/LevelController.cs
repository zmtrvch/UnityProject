using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	//sounds 
	public AudioClip music = null;
	AudioSource musicSource = null;
	public static int collectedCoins;

	public GameObject winPrefab;
	public GameObject losePrefab;
	public bool isChoseLevel;
	public GameObject settingsPrefab;
	public bool deathSound = false;
	public static bool isLevel1FruitCollected;
	public static bool isLevel1CrysralsCollected;
	public static bool isLevel2FruitCollected;
	public static bool isLevel2CrysralsCollected;
	public static bool isLevel1Complated;
	public static bool isLevel2Complated;
	public MyButton pause;
	int fruitsNumber = 0;
	public int coinsNumber = 0;
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
			//SceneManager.LoadScene ("Levels"); //оновлюемо життя
			createLosePanel();
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
	void createLosePanel(){
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		SettingsPanel popup = obj.GetComponent<SettingsPanel>();
		Time.timeScale = 0;
	}

	public void createWinPanel(){
		isLevel1Complated = true;
		PlayerPrefs.SetInt ("isLevel1Complated", 1);
		PlayerPrefs.Save ();
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		WinPanel win = obj.GetComponent<WinPanel>();
		win.setCoins (this.coinsNumber);
		win.setFruits (this.fruitsNumber,1);
		win.setCrystal ();
		Time.timeScale = 0;
	}

	public void createWinPanel2(){
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		WinPanel win2 = obj.GetComponent<WinPanel>();
		win2.setCoins (this.coinsNumber);
		win2.setFruits (this.fruitsNumber,2);
		///win2.setCrystal (this.crystalPanel.getObtainedCrystal(),2);
		Time.timeScale = 0;
		collectedCoins += coinsNumber;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel2Complated = true;
		PlayerPrefs.SetInt ("isLevel2Complated", 1);
		if (isLevel2CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}
	public void saveStatLevel1(){
		collectedCoins += coinsNumber;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel1Complated = true;
		PlayerPrefs.SetInt ("isLevel1Complated", 1);
		if (isLevel1CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}


	public void saveStatLevel2(){
		collectedCoins += coinsNumber;
		PlayerPrefs.SetInt ("collectedCoins",collectedCoins);
		isLevel2Complated = true;
		PlayerPrefs.SetInt ("isLevel2Complated", 1);
		if (isLevel2CrysralsCollected)
			PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
		PlayerPrefs.Save ();
	}
}


