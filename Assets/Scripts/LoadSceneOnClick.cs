using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
