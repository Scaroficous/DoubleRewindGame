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
        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
        if (gameObject.layer == redLayer)
        {
            sameGoal = GameObject.Find("redGoal").GetComponent<goalCheck>();
            otherPlyr = GameObject.Find("bluePlyr");
        }
        else
        {
            sameGoal = GameObject.Find("blueGoal").GetComponent<goalCheck>();
            otherPlyr = GameObject.Find("redPlyr");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var plyrPosition = transform.parent.transform.position;

        //See if both the characters are on their goals, and if so, start the timer
        if (selected && sameGoal.CheckForPlayer() && sameGoal.otherGoal.CheckForPlayer())
        {
            selected = false;
            Debug.Log("Level Complete!");
            Instantiate(Resources.Load("Prefabs/endLevelTimer", typeof(GameObject)));
        }

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
                if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 315 
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 360
                || GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 0
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 45)
                {
                    //Debug.Log(GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 315
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 360
                || GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 0
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 45)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 315
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 360
                || GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 0
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 45)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 315
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 360
                || GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 0
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 45)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                    }
                }
            }
        }
        else
        {
            //Make it plain
            GetComponent<Renderer>().material = plainMat;
        }

        LookForGround();

        //See if this player has fallen off the world, and if so, restart the level
        if (transform.parent.transform.position.y < -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        transform.localPosition = new Vector3(0, 0, 0);
    }

    //LookForGround checks if something's below the character, and if there isn't, character falls down one unit
    public void LookForGround()
    {
        if (!Physics.Raycast(transform.parent.transform.position, Vector3.down, out lookForWall, 1, colLayer)
        //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
        && !(transform.parent.transform.position.x == otherPlyr.transform.parent.transform.position.x 
        && transform.parent.transform.position.y == otherPlyr.transform.parent.transform.position.y + 1 
        && transform.parent.transform.position.z == otherPlyr.transform.parent.transform.position.z)) 
        {
            var plyrPosition = transform.parent.transform.position;
            plyrPosition.y--;
            transform.parent.transform.position = plyrPosition;
        }
    }
}
