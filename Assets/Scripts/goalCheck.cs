using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class goalCheck : MonoBehaviour
{
    public goalCheck otherGoal;
    RaycastHit lookUp;
    public LayerMask layerToCheck;

    public bool CheckForPlayer()
    {
        //See if the relevant player is directly on top of this goal
        return (Physics.Raycast(transform.position, Vector3.up, out lookUp, 1, layerToCheck));        
    }
}
