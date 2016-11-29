using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Singleplayer : MonoBehaviour {

    private bool isReady;
    private int random;
    public Text win, lose, draw;
    
    void Awake () {
        isReady = true;
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        draw.gameObject.SetActive(false);
        random = Random.Range(1, 4);
        Debug.Log(random);
    }

    public void Rock()
    {
        if (isReady == true && random == 1) StartCoroutine(YouWin());
        if (isReady == true && random == 2) StartCoroutine(YouLose());
        if (isReady == true && random == 3) StartCoroutine(YouDraw());
    }

    public void Paper()
    {
        if (isReady == true && random == 1) StartCoroutine(YouWin());
        if (isReady == true && random == 2) StartCoroutine(YouLose());
        if (isReady == true && random == 3) StartCoroutine(YouDraw());
    }

    public void Scissors()
    {
        if (isReady == true && random == 1) StartCoroutine(YouWin());
        if (isReady == true && random == 2) StartCoroutine(YouLose());
        if (isReady == true && random == 3) StartCoroutine(YouDraw());
    }

    IEnumerator YouWin()
    {
        isReady = false;
        win.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }

    IEnumerator YouLose()
    {
        isReady = false;
        lose.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }

    IEnumerator YouDraw()
    {
        isReady = false;
        draw.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Awake();
    }

	public void MenuOn()
	{
		SceneManager.LoadScene(0);
	}
}
