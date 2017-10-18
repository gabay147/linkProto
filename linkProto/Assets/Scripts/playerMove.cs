using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class playerMove : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;
	private BoxCollider2D collider;
	[SerializeField]
	private Vector2 deltaForce;
	private Vector2 lastDir;
	private bool isMoving;
	private bool isAttacking;

	public float speed;

	void Awake() {
		anim = GetComponent<Animator> ();
		collider = GetComponent<BoxCollider2D> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
	}

	void CheckInput() {
		isMoving = false;
		isAttacking = false;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		float a = Input.GetAxisRaw ("Fire1");

		if (h < 0 || h > 0 || v < 0 || v > 0) {
			isMoving = true;

			if (!collider.IsTouchingLayers (-1)) {
				lastDir = rb.velocity;
			}
		}
		if (a < 0 || a > 0) {
			isAttacking = true;
		}

		deltaForce = new Vector2 (h, v);

		CalculateMovement (deltaForce * speed);
	}
		

	void CalculateMovement(Vector2 force) {
		rb.velocity = Vector2.zero;

			rb.AddForce (force, ForceMode2D.Impulse);
		
		SendAnimInfo();
	}

	void SendAnimInfo() {
		anim.SetFloat ("xSpeed", rb.velocity.x);
		anim.SetFloat ("ySpeed", rb.velocity.y);
		anim.SetFloat ("lastX", lastDir.x);
		anim.SetFloat ("lastY", lastDir.y);

		anim.SetBool ("isMoving", isMoving);
		anim.SetBool ("isAttacking", isAttacking);
	}
}
