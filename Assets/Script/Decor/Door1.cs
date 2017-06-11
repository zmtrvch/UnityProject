using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door1 : MonoBehaviour {

	public SpriteRenderer check;
	public Sprite checkSprite;
	public Sprite fruitSprite;
	public Sprite crystalSprite;
	public SpriteRenderer fruitRender;
	public SpriteRenderer crystalRender;

	void Start(){
		if (LevelController.isLevel1Complated ==true)
			check.sprite = checkSprite;
		Debug.Log ("comp");
		if (LevelController.isLevel1CrysralsCollected)
			crystalRender.sprite = crystalSprite;
		if (LevelController.isLevel1FruitCollected) {
			fruitRender.sprite = fruitSprite;	
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level1");
	}

}