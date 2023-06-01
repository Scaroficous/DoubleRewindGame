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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickRay, out plyrClicked, Mathf.Infinity, players))
            {
                plyrClicked.transform.GetComponent<plyrMovement>().selected = true;
                plyrClicked.transform.GetComponent<plyrMovement>().otherPlyr.GetComponent<plyrMovement>().selected = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}