using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public GameObject[] players;
    public GameObject player1;
    public GameObject player2;
	public int numberOfPlayers;
	public int p1number, p2number = 0;
	public State state;

	void Start()
	{
		Reset ();
	}

	public enum State
	{
		waitingForPlayers,
		waitingForInputs,
		result,
		waitingForReset
	};

    void Update()
    {
		players = GameObject.FindGameObjectsWithTag ("Player");
		numberOfPlayers = players.Length;
		switch (state)
		{
		case State.waitingForPlayers:
			WaitingForPlayers ();
			break;
		case State.waitingForInputs:
			WaitingForInputs ();
			break;
		case State.result:
			Result ();
			break;
		case State.waitingForReset:
			WaitingForReset ();
			break;
		default:
			WaitingForPlayers ();
			break;
		}
    }

	public void Reset()
	{
		player1 = null;
		player2 = null;
		p1number = 0;
		p2number = 0;
		state = State.waitingForPlayers;
	}

	public void WaitingForPlayers()
	{
		if (numberOfPlayers == 1)
		{
			player1 = players [0];
			player1.GetComponent<PlayerController> ().Waiting ();
		}
		if (numberOfPlayers == 2)
		{
			player1 = players [0];
			player2 = players [1];
			state = State.waitingForInputs;
		}
		Debug.Log ("WaitingForPlayers");
	}

	public void WaitingForInputs()
	{
		if (numberOfPlayers != 2)
		{
			Reset ();
			return;
		}

		player1.GetComponent<PlayerController> ().Selecting ();
		player2.GetComponent<PlayerController> ().Selecting ();

		p1number = player1.GetComponent<PlayerController> ().number;
		p2number = player2.GetComponent<PlayerController> ().number;

		if (p1number != 0 && p2number != 0)	state = State.result;
		Debug.Log ("WaitingForPlayersInput");
	}

	public void Result()
	{
		if (numberOfPlayers != 2)
		{
			Reset ();
			return;
		}

		if (p1number == 1 && p2number == 1) Draw();
		if (p1number == 1 && p2number == 2) p2win();
		if (p1number == 1 && p2number == 3) p1win();
		if (p1number == 2 && p2number == 1) p1win();
		if (p1number == 2 && p2number == 2) Draw();
		if (p1number == 2 && p2number == 3) p2win();
		if (p1number == 3 && p2number == 1) p2win();
		if (p1number == 3 && p2number == 2) p1win();
		if (p1number == 3 && p2number == 3) Draw();

		state = State.waitingForReset;

		Debug.Log ("Result");
	}

	public void WaitingForReset()
	{
		p1number = player1.GetComponent<PlayerController> ().number;
		p2number = player2.GetComponent<PlayerController> ().number;
		if (p1number == 0 && p2number == 0) state = State.waitingForInputs;
	}

	public void p1win()
	{
		player1.GetComponent<PlayerController> ().Win ();
		player2.GetComponent<PlayerController> ().Lose ();
	}
	public void p2win()
	{
		player1.GetComponent<PlayerController> ().Lose ();
		player2.GetComponent<PlayerController> ().Win ();
	}
	public void Draw()
	{
		player1.GetComponent<PlayerController> ().Draw ();
		player2.GetComponent<PlayerController> ().Draw ();
	}
}
