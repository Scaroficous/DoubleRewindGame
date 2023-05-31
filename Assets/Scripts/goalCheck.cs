using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goalCheck : MonoBehaviour
{
    public goalCheck otherGoal;
    //public GameObject samePlyr;
    RaycastHit lookUp;
    public LayerMask layerToCheck;

    public bool checkForPlayer()
    {
        return (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1, layerToCheck));        
    }
}
