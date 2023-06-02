using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartTutorial : MonoBehaviour
{
    public GameObject canvasMessage;
    // Update is called once per frame
    void Update()
    {
        //Only use this script on the red player in level 10 - it's for a very specific tutorial case
        if (transform.position.y < 2)
        {
            canvasMessage.SetActive(true);
        }
    }
}
