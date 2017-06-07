using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

	float timeToWait = 0.06f;
	public int MaxHealth = 2;
	public int health = 1;
	public static HeroRabbit current;
	Transform heroParent = null;
	public float speed = 3;
	Rigidbody2D myBody = null;
	Animator myController = null;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;
	public bool isSuper = false;
	bool colidedBomb = false;
	public float WaitTime = 2f;
	float to_wait = 0f;
	    void Awake()
	  {
	      current = this;
		   to_wait = WaitTime;
		  }


	// Use this for initialization
	void Start()
	{
		myBody = this.GetComponent<Rigidbody2D>();
		myController = this.GetComponent<Animator>();
		//Зберегти стандартний батьківський GameObject
		this.heroParent = this.transform.parent;
	//}


	// Update is called once per frame
	//void Update () {
	LevelController.current.setStartPosition(transform.position);
	}
	public void addHealth(int number)
	{
		this.health += number;
		if (this.health > MaxHealth)
			this.health = MaxHealth;
		this.onHealthChange();
	}

	public void removeHealth(int number)
	{
		this.health -= number;
		if (this.health < 0)
			this.health = 0;
		this.onHealthChange();
	}

	void onHealthChange()
	{
		if (this.health == 1)
		{
			this.transform.localScale = Vector3.one;
		}
		else if (this.health == 2)
		{
			this.transform.localScale = Vector3.one * 2;
		}
		else if (this.health == 0)
		{
			LevelController.current.onRabitDeath(this);
		}
	}

	public void becomeSuper()
	{
		if (!isSuper)
		{
			isSuper = true;
			transform.localScale += new Vector3(0.5F, 0.5f, 0);
		}
	}


	public void colideBomb()
	{
		Animator animator = GetComponent<Animator>();

		if (isSuper)
		{
			isSuper = false;
			transform.localScale += new Vector3(-0.5F, -0.5f, 0);
		}
		else
		{
			colidedBomb = true;
			StartCoroutine (dieAnimation(2.0f));


		


		}
	}

	public void Die(){
		StartCoroutine (dieAnimation(2.0f));
	}
	public IEnumerator dieAnimation (float time){
		Animator animator = GetComponent<Animator>();

		animator.SetBool("die", true);
		yield return new WaitForSeconds (time);
		animator.SetBool("die", false);
		LevelController.current.onRabitDeath(this);

	}
	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			Debug.Log ("No keyboard");
		} else {
			Animator animator = GetComponent<Animator> ();

			if (Mathf.Abs (value) > 0) {
				animator.SetBool ("run", true);
			} else {
				animator.SetBool ("run", false);
			}

			if (Mathf.Abs (value) > 0) {
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}

			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
			if (value < 0) {
				sr.flipX = true;
			} else if (value > 0) {
				sr.flipX = false;
			}
			Vector3 from = transform.position + Vector3.up * 0.3f;
			Vector3 to = transform.position + Vector3.down * 0.1f;
			int layer_id = 1 << LayerMask.NameToLayer ("Ground");
			//Перевіряємо чи проходить лінія через Collider з шаром Ground
			RaycastHit2D hit = Physics2D.Linecast (from, to, layer_id);
			if (hit) {
				isGrounded = true;
				if (hit.transform != null && hit.transform.GetComponent<MovingPlatform> () != null) {
					//Приліпаємо до платформи
					SetNewParent (this.transform, hit.transform);
				}
		
			} else {
				isGrounded = false;
				SetNewParent (this.transform, this.heroParent);
			}

			//Намалювати лінію (для розробника)
			Debug.DrawLine (from, to, Color.red);

			if (Input.GetButtonDown ("Jump") && isGrounded) {
				this.JumpActive = true;
			}
			if (this.JumpActive) {
				//Якщо кнопку ще тримають
				if (Input.GetButton ("Jump")) {
					this.JumpTime += Time.deltaTime;
					if (this.JumpTime < this.MaxJumpTime) {
						Vector2 vel = myBody.velocity;
						vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
						myBody.velocity = vel;
					}
				} else {
					this.JumpActive = false;
					this.JumpTime = 0;
				}
			}

			if (this.isGrounded) {
				animator.SetBool ("jump", false);
			} else {
				animator.SetBool ("jump", true);
			}
		}
	}
	static void SetNewParent(Transform obj, Transform new_parent)
	{
		if (obj.transform.parent != new_parent)
		{
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}
	    
}