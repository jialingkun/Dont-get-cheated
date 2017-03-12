using UnityEngine;
using System.Collections;

public class player_Controller : MonoBehaviour {
	private Animator animator;
	private player_Status status;
	private bool isGameover;
	// Use this for initialization
	void Start () {
		isGameover = false;
		animator = this.GetComponent<Animator> ();
		status = this.GetComponent<player_Status> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameover) {

			if (Input.GetKey ("left")) {
				status.setLeft (true);
				animator.SetBool ("tekanKiri", true);
			} else {
				status.setLeft (false);
				animator.SetBool ("tekanKiri", false);
			}
			if (Input.GetKey ("right")) {
				status.setRight (true);
				animator.SetBool ("tekanKanan", true);
			} else {
				status.setRight (false);
				animator.SetBool ("tekanKanan", false);
			}

		}

	}

	public void stop(){
		isGameover = true;
	}

	public void restart(){
		isGameover = false;
	}
}
