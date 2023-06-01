using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plyrMovement : MonoBehaviour
{
    public bool selected;
    public bool justMoved;
    
    public goalCheck sameGoal;
    public GameObject otherPlyr;
    public LayerMask colLayer;
    public List<int> movementList = new List<int>();
    public int turnNumber;

    RaycastHit lookForWall;
    // Start is called before the first frame update
    void Start()
    {
        turnNumber = 0;
        movementList.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        var plyrPosition = transform.position;
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                {
                    plyrPosition.z += 1;
                    transform.position = plyrPosition;
                    movementList.Add(0);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind(transform.position.x, transform.position.y, transform.position.z);
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (!Physics.Raycast(transform.position, Vector3.right, out lookForWall, 1, colLayer))
                {
                    plyrPosition.x += 1;
                    transform.position = plyrPosition;
                    movementList.Add(1);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind(transform.position.x, transform.position.y, transform.position.z);
                }
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (!Physics.Raycast(transform.position, Vector3.back, out lookForWall, 1, colLayer))
                {
                    plyrPosition.z -= 1;
                    transform.position = plyrPosition;
                    movementList.Add(2);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind(transform.position.x, transform.position.y, transform.position.z);
                }
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (!Physics.Raycast(transform.position, Vector3.left, out lookForWall, 1, colLayer))
                {
                    plyrPosition.x -= 1;
                    transform.position = plyrPosition;
                    movementList.Add(3);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind(transform.position.x, transform.position.y, transform.position.z);
                }
            }
            

        }


        if (sameGoal.checkForPlayer() && sameGoal.otherGoal.checkForPlayer())
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
        LookForGround();
    }

    private void LateUpdate()
    {
        
    }

    public void LookForGround()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out lookForWall, 1, colLayer))
        {
            Debug.Log(lookForWall);
            var plyrPosition = transform.position;
            plyrPosition.y -= 1;
            transform.position = plyrPosition;
        }
    }
}
