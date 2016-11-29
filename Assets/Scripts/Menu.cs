using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void SingleplayerOn()
    {
        SceneManager.LoadScene(1);
    }

    public void MultiplayerOn()
    {
        SceneManager.LoadScene(2);
    }
}
