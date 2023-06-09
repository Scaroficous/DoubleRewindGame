using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectPlayer : MonoBehaviour
{
    private RaycastHit plyrClicked;
    private Ray clickRay;
    public LayerMask players;
    

    // Update is called once per frame
    void Update()
    {
        //Sees if the player is clicking on a character, and if so, selecting that character
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickRay, out plyrClicked, Mathf.Infinity, players))
            {
                plyrClicked.transform.GetComponent<plyrMovement>().selected = true;
                //The following line makes the other player not selected
                plyrClicked.transform.GetComponent<plyrMovement>().otherPlyr.GetComponent<plyrMovement>().selected = false;
            }
        }

        //Allows the player to restart the current level/puzzle
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}