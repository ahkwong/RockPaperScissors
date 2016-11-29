using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
	[SyncVar] public int number = 0;
	public GameObject playerCanvas;
	public GameObject WinText;
	public GameObject LoseText;
	public GameObject DrawText;
	public GameObject WaitingText;
	public GameObject SelectingText;
    public GameObject Rockbtn;
    public GameObject Paperbtn;
    public GameObject Scissorsbtn;

	void Start()
	{
		if (!isLocalPlayer) return;
		playerCanvas.SetActive (true);
		Debug.Log ("A player has connected");
	}

	void Update()
	{
		if (!isLocalPlayer) return;
	}

    [Command]
    void CmdRockClicked()
    {
		number = 1;
    }

    [Command]
    void CmdPaperClicked()
    {
		number = 2;
    }

    [Command]
    void CmdScissorsClicked()
    {
		number = 3;
    }

    public void Waiting()
    {
        if (!isServer) return;
        RpcWaiting();
    }

    [ClientRpc]
    void RpcWaiting()
    {
		WinText.SetActive (false);
		LoseText.SetActive (false);
		DrawText.SetActive (false);
		WaitingText.SetActive (true);
		SelectingText.SetActive (false);
    }

	public void Selecting()
	{
		if (!isServer) return;
		RpcSelecting();
	}

	[ClientRpc]
	void RpcSelecting()
	{
		WinText.SetActive (false);
		LoseText.SetActive (false);
		DrawText.SetActive (false);
		WaitingText.SetActive (false);
		SelectingText.SetActive (true);
	}

	public void Win()
	{
		if (!isServer) return;
		RpcWin ();
	}

	[ClientRpc]
	void RpcWin()
	{
		StartCoroutine (WinReset());
	}

	IEnumerator WinReset()
	{
		WinText.SetActive (true);
		LoseText.SetActive (false);
		DrawText.SetActive (false);
		WaitingText.SetActive (false);
		SelectingText.SetActive (false);
		yield return new WaitForSeconds (1f);
		number = 0;
	}

	public void Lose()
	{
		if (!isServer) return;
		RpcLose ();
	}

	[ClientRpc]
	void RpcLose()
	{
		StartCoroutine (LoseReset());
	}

	IEnumerator LoseReset()
	{
		WinText.SetActive (false);
		LoseText.SetActive (true);
		DrawText.SetActive (false);
		WaitingText.SetActive (false);
		SelectingText.SetActive (false);
		yield return new WaitForSeconds (1f);
		number = 0;
	}

	public void Draw()
	{
		if (!isServer) return;
		RpcDraw ();
	}

	[ClientRpc]
	void RpcDraw()
	{
		StartCoroutine (DrawReset());
	}

	IEnumerator DrawReset()
	{
		WinText.SetActive (false);
		LoseText.SetActive (false);
		DrawText.SetActive (true);
		WaitingText.SetActive (false);
		SelectingText.SetActive (false);
		yield return new WaitForSeconds (1f);
		number = 0;
	}
		
}
