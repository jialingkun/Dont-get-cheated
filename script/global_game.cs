using UnityEngine;
using System.Collections;

public class global_game : MonoBehaviour {
	private player_Status playerStatus;
	private player_Controller playerController;
	private enemy_pattern enemyPattern;
	private cheater_status cheaterStatusLeft;
	private cheater_status cheaterStatusRight;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("player");
		playerStatus = player.GetComponent<player_Status> ();
		playerController = player.GetComponent<player_Controller> ();
		enemyPattern = this.GetComponent<enemy_pattern> ();
		cheaterStatusLeft = GameObject.Find ("cheater_left").GetComponent<cheater_status> ();
		cheaterStatusRight = GameObject.Find ("cheater_right").GetComponent<cheater_status> ();
	}

	public void gameOver(){
		playerStatus.stop ();
		playerController.stop ();
		enemyPattern.stop ();
		cheaterStatusLeft.stop ();
		cheaterStatusRight.stop ();
	}

}
