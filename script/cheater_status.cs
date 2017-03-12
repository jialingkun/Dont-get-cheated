using UnityEngine;
using System.Collections;

public class cheater_status : MonoBehaviour {
	public enum cheaterPosition{
		LEFT,RIGHT
	}
	//public
	public float progressSpeed;
	public cheaterPosition position;

	//dinamic variable
	private int progress;
	private bool active;
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
	private player_Status player;
	private global_game game;

	// Use this for initialization
	void Start () {
		isGameover = false;
		active = false;
		progress = 0;
		timestamp = 0;
		maxProgress = 100;
		game = GameObject.Find ("game").GetComponent<global_game> ();
		player = GameObject.Find ("player").GetComponent<player_Status> ();

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
				if (active && (
					(position == cheaterPosition.LEFT && player.getLeft () && !player.getRight()) || 
					(position == cheaterPosition.RIGHT && player.getRight () && !player.getLeft()) || 
					(!player.getRight () && !player.getLeft ())
				)) {
					if (progress < maxProgress) {
						progress = progress + 1;
						progressBar.localScale += vectorPieceProgressScale;
					} else {
						game.gameOver ();
					}
				}
				timestamp = Time.time + progressSpeed;
			}

		}
	}

	public void setActive(bool active){
		this.active = active;
	}

	public bool getActive(){
		return active;
	}

	public void stop(){
		isGameover = true;
	}

	public void restart(){
		isGameover = false;
		active = false;
		progress = 0;
		timestamp = 0;
		progressBar.localScale = vectorDefaultProgressScale;
	}
}
