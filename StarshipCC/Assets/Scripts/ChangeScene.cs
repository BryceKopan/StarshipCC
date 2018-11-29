using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void ChangeTheScene (int button) {
		
        if(button == -1)
        {
            ;
        }
        else if (button == -2)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(button);
        }
    }
}
