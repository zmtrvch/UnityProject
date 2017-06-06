using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel : MonoBehaviour {
	public List<UI2DSprite> lifes;
	public Sprite empty_life;
	public Sprite full_life;
	int maxLifesNumber = 3;

	void FixedUpdate () {
		setLives (); //оновлюємо панель
		controlLives (); 
	}

	void controlLives() {
		    
			for (int i =0; i < maxLifesNumber; ++i)
				if (i == LevelController.current.getLifes ())
					lifes [i].sprite2D = empty_life;
		
			} 
	void setLives(){
		if (LevelController.current.getLifes () == maxLifesNumber) {
			for (int i = 0; i < maxLifesNumber; ++i) {
				lifes [i].sprite2D = full_life;

			}
		}
	}
			    
			}
	

