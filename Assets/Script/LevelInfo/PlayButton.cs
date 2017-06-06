using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
	public void _onClick()
	{
		SceneManager.LoadScene ("LevelSelect");
		Debug.Log ("Click");
	}
}