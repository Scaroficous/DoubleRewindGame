using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plyrMovement : MonoBehaviour
{
    public bool selected;
    public bool justSwitched;

    public GameObject otherPlyr;
    RaycastHit lookForWall;
    public List<int> movementList = new List<int>();
    public int turnNumber;
    // Start is called before the first frame update
    void Start()
    {
        turnNumber = 0;
        movementList.Add(0);
        justSwitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            var plyrPosition = transform.position;
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, Vector3.forward, out lookForWall, 1))
                {
                    plyrPosition.z += 1;
                    movementList.Add(0);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind();
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                if (!Physics.Raycast(transform.position, Vector3.right, out lookForWall, 1))
                {
                    plyrPosition.x += 1;
                    movementList.Add(1);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind();
                }
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                if (!Physics.Raycast(transform.position, Vector3.back, out lookForWall, 1))
                {
                    plyrPosition.z -= 1;
                    movementList.Add(2);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind();
                }
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                if (!Physics.Raycast(transform.position, Vector3.left, out lookForWall, 1))
                {
                    plyrPosition.x -= 1;
                    movementList.Add(3);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind();
                }
            }

            transform.position = plyrPosition;

            //Switch characters
            if (Input.GetKeyUp(KeyCode.Space) && !justSwitched)
            {
                selected = false;
                otherPlyr.GetComponent<plyrMovement>().selected = true;
                otherPlyr.GetComponent<plyrMovement>().justSwitched = true;
            }
            justSwitched = false;

        }
    }

    

}
