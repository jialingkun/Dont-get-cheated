using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class global_game : MonoBehaviour {
	private player_Status playerStatus;
	private player_Controller playerController;
	private enemy_pattern enemyPattern;
	private cheater_status cheaterStatusLeft;
	private cheater_status cheaterStatusRight;

	public GameObject gameoverPanel;
	public Text finalScore;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("player");
		playerStatus = player.GetComponent<player_Status> ();
		playerController = player.GetComponent<player_Controller> ();
		enemyPattern = this.GetComponent<enemy_pattern> ();
		cheaterStatusLeft = GameObject.Find ("cheater_left").GetComponent<cheater_status> ();
		cheaterStatusRight = GameObject.Find ("cheater_right").GetComponent<cheater_status> ();

		//gameoverPanel = GameObject.Find ("game_over_panel");
		//gameoverPanel.SetActive (false);
		//finalScore = GameObject.Find ("final_score_number").GetComponent<Text>();

	}

	public void gameOver(){
		playerStatus.stop ();
		playerController.stop ();
		enemyPattern.stop ();
		cheaterStatusLeft.stop ();
		cheaterStatusRight.stop ();
		gameoverPanel.SetActive (true);
		finalScore.text = ""+playerStatus.getScore ();
	}

	public void restart(){
		gameoverPanel.SetActive (false);
		playerStatus.restart ();
		playerController.restart ();
		enemyPattern.restart ();
		cheaterStatusLeft.restart ();
		cheaterStatusRight.restart ();
	}

}
