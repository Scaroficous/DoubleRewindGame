using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartTutorial : MonoBehaviour
{
    public GameObject canvasMessage;
    // Update is called once per frame
    void Update()
    {
        //Only use ONLY in scenes where if this character is below a certain point the player has to restart
        //It turns on a canvas that tells the player how to restart
        if (transform.position.y < 1)
        {
            canvasMessage.SetActive(true);
        }
    }
}
