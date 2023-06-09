using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plyrMovement : MonoBehaviour
{
    public bool selected;
    public Material plainMat;
    public Material glowingMat;
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
            //Make it glow
            GetComponent<Renderer>().material = glowingMat; 

            //Make it move in four cardinal directions and store that movement
            //0 is forward
            //1 is right
            //2 is back 
            //3 is left

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!Physics.Raycast(transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                {
                    plyrPosition.z++;
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
                    plyrPosition.x++;
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
                    plyrPosition.z--;
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
                    plyrPosition.x--;
                    transform.position = plyrPosition;
                    movementList.Add(3);
                    turnNumber++;
                    otherPlyr.GetComponent<plyrRewind>().Rewind(transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            //Make it plain
            GetComponent<Renderer>().material = plainMat;
        }

        //See if both the characters are on their goals, and if so, go to the next scene 
        if (sameGoal.CheckForPlayer() && sameGoal.otherGoal.CheckForPlayer())
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

    //LookForGround checks if something's below the character, and if there isn't, character falls down one unit
    public void LookForGround()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out lookForWall, 1, colLayer)
        //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
        && !(transform.position.x == otherPlyr.transform.position.x && transform.position.y == otherPlyr.transform.position.y + 1 && transform.position.z == otherPlyr.transform.position.z)) 
        {
            var plyrPosition = transform.position;
            plyrPosition.y--;
            transform.position = plyrPosition;
        }
    }
}
