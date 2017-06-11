using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPanel : MonoBehaviour {

	public Sprite first;
	public Sprite second;
	public Sprite third;
	public bool one = false;
	public bool two = false;
	public bool three = false;

	public static CrystalPanel crystals;

	public UI2DSprite[] gemComponents;


	private void Awake()
	{
		crystals = this;
		gemComponents = new UI2DSprite[3];
		loadComponents();
	}

	private void loadComponents()
	{
		for (int i = 0; i < transform.childCount; ++i)
			gemComponents[i] = transform.GetChild(i).GetComponent<UI2DSprite>();
	}

	public void Crystals(string crystals) {
		if (crystals == "gem-1") {
			gemComponents [0].sprite2D = first;
			one = true;
		} else if (crystals == "gem-2") {
			gemComponents [1].sprite2D = second;
			two = true;
		} else if (crystals == "gem-3") {
			gemComponents [2].sprite2D = third;
			three = true;
		}
	}

}