using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemy_pattern : MonoBehaviour {
	public enum cheatState{
		LEFT_CHEAT,RIGHT_CHEAT,BOTH_CHEAT,NO_CHEAT
	}

	public float interval;
	public Pattern[] pattern;
	public Level[] level;

	//pattern structure
	private List<int> listRandomPatternIndex = new List<int>();
	private Session currentSession;
	private Pattern currentPattern;

	//other variable
	private int currentSessionIndex;
	private int currentSessionMaxIndex;
	private float timestamp;
	private float countdown;
	private bool isTransition;
	private Session transitionSession;
	private bool isGameover;
	private Pattern emptyPattern;

	//direct retrieve external object
	private player_Status player;
	private Animator cheaterLeftAnimator;
	private Animator cheaterRightAnimator;
	private cheater_status cheaterLeftStatus;
	private cheater_status cheaterRightStatus;
	//undirect retrieve external object
	private int playerScore;


	[System.Serializable]
	public class Session{
		public cheatState eventPattern;
		public float duration;

		public Session(cheatState eventPattern, float duration){
			this.eventPattern = eventPattern;
			this.duration = duration;
		}
	}

	[System.Serializable]
	public class Pattern{
		public Session[] session;

		//for empty pattern only
		public Pattern(bool isEmpty, int duration){
			if(isEmpty){
				session = new Session[1];
				session[0]=new Session (cheatState.NO_CHEAT,duration);
			}
		}

	}

	[System.Serializable]
	public class Level{
		public int minimum;
		public int maximum;
		public int[] patternIndex;

	}

	void Start () {
		isGameover = false;
		player = GameObject.Find ("player").GetComponent<player_Status> ();

		GameObject cheaterLeft = GameObject.Find ("cheater_left");
		cheaterLeftAnimator = cheaterLeft.GetComponent<Animator> ();
		cheaterLeftStatus = cheaterLeft.GetComponent<cheater_status> ();

		GameObject cheaterRight = GameObject.Find ("cheater_right");
		cheaterRightAnimator = cheaterRight.GetComponent<Animator> ();
		cheaterRightStatus = cheaterRight.GetComponent<cheater_status> ();

		transitionSession = new Session (cheatState.NO_CHEAT,1);
		emptyPattern = new Pattern (true, 2);

		nextPattern ();

	}

	void Update () {
		if (!isGameover) {

			//loop if every second
			if (Time.time >= timestamp) {
				//eksekusi
				countdown--;
				if (countdown <= 0) {
					nextSession ();
				}
				timestamp = Time.time + interval;
			}

		}
	}

	private void nextPattern(){
		playerScore = player.getScore();

 		//generate list of level that fit requirement
		listRandomPatternIndex.Clear();
		foreach (var item in level) {
			if (item.maximum < 0) {
				if (playerScore >= item.minimum) {
					foreach (var num in item.patternIndex) {
						listRandomPatternIndex.Add (num);
					}
				}
			} else {
				if (playerScore >= item.minimum && playerScore <= item.maximum) {
					foreach (var num in item.patternIndex) {
						listRandomPatternIndex.Add (num);
					}
				}
			}
		}

		//pick pattern from list
		int rand = Random.Range (0, listRandomPatternIndex.Count);
		currentPattern = pattern [listRandomPatternIndex [rand]];
		currentSessionMaxIndex = currentPattern.session.Length-1;
		currentSessionIndex = 0;
		currentSession = currentPattern.session [currentSessionIndex];
		countdown = currentSession.duration;
		isTransition = false;
		activeSession ();

	}

	private void nextSession(){
		if (isTransition) {
			isTransition = false;
			if (currentSessionIndex < currentSessionMaxIndex) {
				currentSessionIndex++;
				currentSession = currentPattern.session [currentSessionIndex];
				countdown = currentSession.duration;
				activeSession ();
			} else {
				nextPattern ();
			}
		} else {
			isTransition = true;
			currentSession = transitionSession;
			countdown = 1;
			activeSession ();
		}
	}

	private void activeSession(){
		switch (currentSession.eventPattern) {
		case cheatState.LEFT_CHEAT:
			cheaterLeftAnimator.SetBool ("active", true);
			cheaterLeftStatus.setActive (true);
			break;
		case cheatState.RIGHT_CHEAT:
			cheaterRightAnimator.SetBool ("active", true);
			cheaterRightStatus.setActive (true);
			break;
		case cheatState.BOTH_CHEAT:
			cheaterLeftAnimator.SetBool ("active", true);
			cheaterRightAnimator.SetBool ("active", true);
			cheaterLeftStatus.setActive (true);
			cheaterRightStatus.setActive (true);
			break;
		case cheatState.NO_CHEAT:
			cheaterLeftAnimator.SetBool ("active", false);
			cheaterRightAnimator.SetBool ("active", false);
			cheaterLeftStatus.setActive (false);
			cheaterRightStatus.setActive (false);
			break;
		}

	}

	public void stop(){
		isGameover = true;
	}

	public void restart(){
		isGameover = false;
		currentPattern = emptyPattern;
		currentSessionMaxIndex = currentPattern.session.Length-1;
		currentSessionIndex = 0;
		currentSession = currentPattern.session [currentSessionIndex];
		countdown = currentSession.duration;
		isTransition = false;
		activeSession ();
	}


}
