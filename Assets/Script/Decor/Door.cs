using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour {
	public string sceneName;


	void OnTriggerEnter2D(Collider2D collider)
	{   
		HeroRabbit rabit = collider.GetComponent<HeroRabbit>();
		if (rabit != null)
		{
			SceneManager.LoadScene(sceneName);

		}        
	}


}