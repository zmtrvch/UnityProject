﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : MonoBehaviour {

	bool sound = true;
	public float speed = 1.0f;
	public Vector3 moveBy = Vector3.one;
	public float seen = 10.0f;
	public float carrotPeriod = 3.0f;

	public enum Mode
	{
		GoToA,
		GoToB,
		StandartAttack,
		CarrotAttack,
		Die
	}

	Rigidbody2D myBody = null;

	Vector3 pointA;
	Vector3 pointB;
	public Mode currentMode = Mode.GoToB;
	//sounds
	AudioSource attackSource = null;
	public AudioClip attackSound = null;


	void Start()
	{
		//sounds
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;


		this.myBody = this.GetComponent<Rigidbody2D>();
		this.pointA = this.transform.position;

		this.myBody = this.GetComponent<Rigidbody2D>();
		this.pointA = this.transform.position;
		this.timeBefore = this.carrotPeriod;
		moveBy.y = 0;
		moveBy.z = 0;
		this.pointB = pointA + moveBy;

	}



	void FixedUpdate()
	{
		setMode();
		run();
		carrotAttack();
		StartCoroutine(Die());

	}

	private float timeBefore;
	private void carrotAttack()
	{
		if (currentMode == Mode.CarrotAttack && timeBefore >= carrotPeriod)
		{
			StartCoroutine(throwCarrot());
			timeBefore = 0;
		}
		else timeBefore += Time.deltaTime;
	}
	public void  setSoundOff(){
		sound = false;
	}
	public void setSoundOn(){
		sound = true;
	}
	private IEnumerator throwCarrot()
	{

		Animator animator = GetComponent<Animator>();

		playAttackMusic ();
		animator.SetBool("attack", true);

		launchCarrot(getDirection());
		yield return new WaitForSeconds(0.8f);

		animator.SetBool("attack", false);
	}

	public GameObject prefabCarrot;
	void launchCarrot(float direction)
	{
		if (direction != 0)
		{
			GameObject obj = GameObject.Instantiate(this.prefabCarrot);
			//Розміщуємо в просторі
			obj.transform.position = this.transform.position;
			obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
			//Запускаємо в рух
			Carrot carrot = obj.GetComponent<Carrot>();
			carrot.launch(direction);
		}
	}


	public void playAttackMusic(){
		if(sound)
		attackSource.Play ();
	}
	private void setMode()
	{
		Vector3 rabit_pos = HeroRabbit.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.Die) return;
		else if (Mathf.Abs(rabit_pos.x - my_pos.x) < seen)
		{
			currentMode = Mode.CarrotAttack;
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


	private IEnumerator attack(HeroRabbit rabit)
	{
		Animator animator = GetComponent<Animator>();
		playAttackMusic ();
		animator.SetBool("attack", true);
		rabit.removeHealth(1);
		yield return new WaitForSeconds(0.8f);
		playAttackMusic ();
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
				currentMode = Mode.StandartAttack;

				if (currentMode == Mode.StandartAttack && Mathf.Abs(rabit_pos.y - my_pos.y) < 1.0f)
				{
					StartCoroutine(attack(rabit));
				}
				else if (currentMode == Mode.StandartAttack && Mathf.Abs(rabit_pos.y - my_pos.y) > 1.0f)
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
		if (currentMode != Mode.CarrotAttack)
		{
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
		} else animator.SetBool("run", false);
	}


	private float getDirection()
	{
		Vector3 rabit_pos = HeroRabbit.current.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.StandartAttack || currentMode == Mode.CarrotAttack)
		{
			
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

		else if (currentMode == Mode.GoToA)
		{
			return -1;
		}
		else if (currentMode == Mode.GoToB)
		{
			return 1;
		}
		return 0;
	}

	private IEnumerator Die()
	{
		if (currentMode == Mode.Die)
		{
			Animator animator = GetComponent<Animator>();
			animator.SetBool("die", true);
			this.GetComponent<BoxCollider2D>().isTrigger = true;

			if (myBody != null) Destroy(myBody);

			yield return new WaitForSeconds(2.0f);

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