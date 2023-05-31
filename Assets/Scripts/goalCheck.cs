using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goalCheck : MonoBehaviour
{
    public goalCheck otherGoal;
    //public GameObject samePlyr;
    RaycastHit lookUp;
    public LayerMask layerToCheck;

    
    /* void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Something's in me.");
        if (collision.gameObject == samePlyr)
        {
            Debug.Log("Player entered goal.");
            inGoal = true;
            if (otherGoal.inGoal)
            {
                Debug.Log("Level Complete!");
                if (SceneManager.GetActiveScene().buildIndex - 1 == SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
    */
    
    /*
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == samePlyr)
        {
            inGoal = false;
        }
    }
    */

    public bool checkForPlayer()
    {
        Debug.Log("Check for player running");
        if (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1, layerToCheck))
        {
            Debug.Log("On a thing wow");
        }
        else
        {
            Debug.Log("I missed!");
        }
        
        return (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1, layerToCheck));        
    }

    /*
    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1))
        {
            Debug.Log("On a thing wow");
        }
    }
    */
}
