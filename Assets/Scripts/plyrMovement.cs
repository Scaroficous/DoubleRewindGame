using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plyrMovement : MonoBehaviour
{
    public bool selected;
    //public GameObject otherPlyr;
    public plyrMovement otherPlayerMovement;
    RaycastHit lookForWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, transform.forward, out lookForWall, 1))
                {

                }
            }

            //Switch characters
            if (Input.GetKeyUp(KeyCode.Space))
            {
                selected = false;
                otherPlayerMovement.selected = true;
            }
        }
    }

    

}
