﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc1 : MonoBehaviour {
	bool sound = true;
	//sounds 
	AudioSource attackSource = null;
	public AudioClip attackSound = null;

	public float speed = 1.0f;
	public Vector3 moveBy = Vector3.one;


	public enum Mode
	{
		GoToA,
		GoToB,
		Attack,
		Die
	}

	Rigidbody2D myBody = null;

	Vector3 pointA;
	Vector3 pointB;
	public Mode currentMode = Mode.GoToB;


	void Start()
	{
		//sounds
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;


		this.myBody = this.GetComponent<Rigidbody2D>();
		this.pointA = this.transform.position;

		moveBy.y = 0;
		moveBy.z = 0;
		this.pointB = pointA + moveBy;

	}



	void FixedUpdate()
	{
		setMode();
		run();
		StartCoroutine(die());

	}

	public void  setSoundOff(){
		sound = false;
	}
	public void setSoundOn(){
		sound = true;
	}
	private void setMode()
	{
		Vector3 rabit_pos = HeroRabbit.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.Die) return;
		else if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
			&& rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
		{
			currentMode = Mode.Attack;
		}
		else if (currentMode == Mode.GoToA)
		{
			if (isArrived(my_pos, pointA))
			{
				currentMode = Mode.GoToB;
			}
		}
		else if (currentMode == Mode.GoToB)
		{
			if (isArrived(my_pos, pointB))
			{
				currentMode = Mode.GoToA;
			}
		}
		else currentMode = Mode.GoToB;
	}

	public void playAttackMusic(){
		if(sound)
		attackSource.Play ();
	}



	private IEnumerator attack(HeroRabbit rabit)
	{ 
		Animator animator = GetComponent<Animator>();
		animator.SetBool("attack", true);
		playAttackMusic ();
		HeroRabbit.current.Die ();
		yield return new WaitForSeconds(1.0f);



		animator.SetBool("attack", false);       
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (currentMode != Mode.Die)
		{
			HeroRabbit rabit = collision.gameObject.GetComponent<HeroRabbit>();
			if (rabit != null)
			{
				Vector3 rabit_pos = HeroRabbit.current.transform.position;
				Vector3 my_pos = this.transform.position;
				currentMode = Mode.Attack;

				if (currentMode == Mode.Attack && Mathf.Abs(rabit_pos.y - my_pos.y) < 1.0f)
				{
					StartCoroutine(attack(rabit));
				}
				else if (currentMode == Mode.Attack && Mathf.Abs(rabit_pos.y - my_pos.y) > 1.0f)
				{
					currentMode = Mode.Die;
				}

			}
		}
	}

	private void run()
	{

		//[-1, 1]
		float value = this.getDirection();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Animator animator = GetComponent<Animator>();

		if (value < 0)
		{
			sr.flipX = false;

		}
		else if (value > 0)
		{
			sr.flipX = true;
		}
		if (Mathf.Abs(value) > 0)
		{
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		if (Mathf.Abs(value) > 0)
		{
			animator.SetBool("run", true);
		}
		else
		{
			animator.SetBool("run", false);
		}
	}


	private float getDirection()
	{
		Vector3 rabit_pos = HeroRabbit.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.Attack)
		{
			//Move towards rabit
			if (my_pos.x - rabit_pos.x < -1)
			{
				return 1;
			}
			else if (my_pos.x - rabit_pos.x > 1)
			{
				return -1;
			}
			else return 0;
		}

		else  if (currentMode == Mode.GoToA)
		{
			return -1; 
		}
		else if (currentMode == Mode.GoToB)
		{
			return 1; 
		}
		return 0; 
	}

	private IEnumerator die()
	{
		if (currentMode == Mode.Die)
		{
			Animator animator = GetComponent<Animator>();
			animator.SetBool("die", true);
			this.GetComponent<BoxCollider2D>().isTrigger = true;

			if (myBody != null) Destroy(myBody);

			yield return new WaitForSeconds(3.0f);

			animator.SetBool("die", false);
			Destroy(this.gameObject);
		}
	}

	private bool isArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.2f;
	}

}