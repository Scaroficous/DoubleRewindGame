using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartTutorial : MonoBehaviour
{
    bool canvasExists;

    private void Start()
    {
        canvasExists = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Only use ONLY in scenes where if this character is below a certain point the player has to restart
        //It creates a canvas that tells the player how to restart
        if (transform.parent.transform.position.y < 1 && GetComponent<plyrMovement>().turnNumber >= 3 && !canvasExists)
        {
            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
            canvasExists = true;
        }
    }
}
