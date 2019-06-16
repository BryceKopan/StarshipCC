using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySpawner : MonoBehaviour {

    bool gameCanStart = false;
    bool once0 = true;
    bool once1 = true;
    bool once2 = true;
    bool once3 = true;

    bool ready1 = false;
    bool ready2 = false;
    bool ready3 = false;
    bool ready4 = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public AudioClip playerJoinSound;
    public float soundVolume = 1.0f;

    private void Start()
    {
    }

    void Update () {
        if (Input.GetKey("joystick 1 button 0") && once0)
        {
            GameObject player01 = (GameObject)Instantiate(player1, new Vector3(-75.0f, 25.0f, 0), Quaternion.identity);
            once0 = false;
            gameCanStart = true;
            PlayJoinSound();
        }
        if (Input.GetKey("joystick 2 button 0") && once1)
        {
            GameObject player02 = (GameObject)Instantiate(player2, new Vector3(75.0f, 25.0f, 0), Quaternion.identity);
            once1 = false;
            PlayJoinSound();
        }
        if (Input.GetKey("joystick 3 button 0") && once2)
        {
            GameObject player03 = (GameObject)Instantiate(player3, new Vector3(-75.0f, -25.0f, 0), Quaternion.identity);
            once2 = false;
            PlayJoinSound();
        }
        if (Input.GetKey("joystick 4 button 0") && once3)
        {
            GameObject player04 = (GameObject)Instantiate(player4, new Vector3(75.0f, -25.0f, 0), Quaternion.identity);
            once3 = false;
            PlayJoinSound();
        }
        if (Input.GetKey("joystick 1 button 7") && !once0)
        {
            ready1 = true;
        }
        if (Input.GetKey("joystick 2 button 7") && !once1)
        {
            ready2 = true;
        }
        if (Input.GetKey("joystick 3 button 7") && !once2)
        {
            ready3 = true;
        }
        if (Input.GetKey("joystick 4 button 7") && !once3)
        {
            ready4 = true;
        }
        if (gameCanStart && ready1)
        {
            if (once1 || ready2)
            {
                if (once2 || ready3)
                {
                    if (once3 || ready4)
                    {
                        GameObject.Find("Persisting_Spawn").SendMessage("setPlayers", new bool[] { !once0, !once1, !once2, !once3});
                        SceneManager.LoadScene("ConstructedShip0");
                    }
                }
            }
        }
    }

    protected void PlayJoinSound()
    {
        if(playerJoinSound)
        {
            AudioSource.PlayClipAtPoint(playerJoinSound, Camera.main.transform.position, soundVolume);
        }
    }
}
