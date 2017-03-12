using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player_Status : MonoBehaviour {
	//public
	public float progressSpeed;

	//dinamic variable
	private int score;
	private int progress;
	private bool leftActive;
	private bool rightActive;
	private float timestamp;
	private bool isGameover;
	//only initialization variable
	private float maxProgressScale;
	private float pieceProgressScale;
	private Vector3 vectorPieceProgressScale;
	public Vector2 vectorDefaultProgressScale;
	private int maxProgress;
	//other object
	private Transform progressBar;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		isGameover = false;
		leftActive = false;
		rightActive = false;
		progress = 0;
		score = 0;
		timestamp = 0;
		maxProgress = 100;
		scoreText = GameObject.Find ("score_number").GetComponent<Text> ();

		//progress
		progressBar = this.transform.Find("progress_bar");
		maxProgressScale = progressBar.localScale.x;
		pieceProgressScale = maxProgressScale / maxProgress;
		vectorPieceProgressScale = new Vector3 (pieceProgressScale, 0, 0);
		vectorDefaultProgressScale = new Vector2 (0, progressBar.localScale.y);
		progressBar.localScale = vectorDefaultProgressScale;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameover) {

			//loop per second inside if
			if (Time.time >= timestamp) {
				if (!(leftActive && rightActive)) {
					if (progress < maxProgress) {
						progress = progress + 1;
						progressBar.localScale += vectorPieceProgressScale;
					} else {
						score = score + 1;
						scoreText.text = "" + score;
						progress = 0;
						progressBar.localScale = vectorDefaultProgressScale;
					}
				}
				timestamp = Time.time + progressSpeed;
			}

		}
	}

	public void setLeft(bool isActive){
		leftActive = isActive;
	}
	public void setRight(bool isActive){
		rightActive = isActive;
	}
	public bool getLeft(){
		return leftActive;
	}
	public bool getRight(){
		return rightActive;
	}

	public int getScore(){
		return score;
	}

	public void stop(){
		isGameover = true;
	}

	public void restart(){
		isGameover = false;
		leftActive = false;
		rightActive = false;
		progress = 0;
		score = 0;
		scoreText.text = "" + score;
		timestamp = 0;
		progressBar.localScale = vectorDefaultProgressScale;
	}
}
