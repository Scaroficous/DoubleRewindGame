using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class goalCheck : MonoBehaviour
{
    public goalCheck otherGoal;
    public GameObject samePlyr;
    RaycastHit lookUp;
    
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
        return (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1, samePlyr.layer));        
    }

}
