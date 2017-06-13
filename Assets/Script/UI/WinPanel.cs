using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinPanel : MonoBehaviour {
	public int numberOfFruit;

	public static bool isSound=true;
	public AudioClip music;
	AudioSource musicSource;
	public UI2DSprite gem;
	public MyButton closeButton;
	public MyButton blackBackground;
	public MyButton nextButton;
	public MyButton replayButton;
	public UILabel fruits;
	public UILabel coins;
	public UI2DSprite crystal1;
	public UI2DSprite crystal2;
	public UI2DSprite crystal3;
	public Sprite crystalNotGet;
	public Sprite crystalSprite1;
	public Sprite crystalSprite2;
	public Sprite crystalSprite3;

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music; 
		musicSource.loop = false; 
		if(isSound)
			musicSource.Play ();
		closeButton.signalOnClick.AddListener (this.close);
		blackBackground.signalOnClick.AddListener (this.close);
		nextButton.signalOnClick.AddListener (this.next);
		replayButton.signalOnClick.AddListener (this.replay);
	}

	// Update is called once per frame
	void close () {
		SceneManager.LoadScene ("Levels");
		Destroy (this.gameObject);
		Time.timeScale = 1;
	}

	void next(){
		SceneManager.LoadScene ("Levels");
		Time.timeScale = 1;

	}
	void replay(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}

	public void setCoins(int coins){
		
		this.coins.text = LevelController.current.getCoins ().ToString ();



	}

	public void setFruits(int fruits,int level){
		this.fruits.text = LevelController.current.getFruits().ToString();
		if (this.numberOfFruit == fruits) {
						LevelController.isLevel1FruitCollected = true;
						if(level==1)
						PlayerPrefs.SetInt ("isLevel1FruitCollected",1);
						if(level==2)
								PlayerPrefs.SetInt ("isLevel2FruitCollected",1);
						PlayerPrefs.Save ();
				}


	}

	public void setCrystal(){
		if (CrystalPanel.crystals.one == true) {
			crystal1.sprite2D = crystalSprite1;
		}
		if (CrystalPanel.crystals.two == true) {
			crystal2.sprite2D = crystalSprite2;
		}
		if (CrystalPanel.crystals.three == true) {
			crystal3.sprite2D = crystalSprite3;
		}
		if(CrystalPanel.crystals.two == true&&CrystalPanel.crystals.one == true&&CrystalPanel.crystals.three == true) {
						LevelController.isLevel1CrysralsCollected = true;
			if (SceneManager.GetActiveScene().name == "Level1") {
								PlayerPrefs.SetInt ("isLevel1CrysralsCollected", 1);
						}
			if (SceneManager.GetActiveScene().name == "Level2") {	
							PlayerPrefs.SetInt ("isLevel2CrysralsCollected", 1);
							}
						PlayerPrefs.Save ();
					}

	}


}