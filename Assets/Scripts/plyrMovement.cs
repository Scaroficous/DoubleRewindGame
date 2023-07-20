using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plyrMovement : MonoBehaviour
{
    public bool selected;
    public bool animationDone = true;
    public Material plainMat;
    public Material glowingMat;
    public Transform handPivot;
    public goalCheck sameGoal;
    public GameObject otherPlyr;
    public LayerMask colLayer;
    public List<int> movementList = new List<int>();
    public List<Object> footstepList = new List<Object>();
    public int turnNumber;
    public Animator animator;

    private bool canvasExists;

    RaycastHit lookForWall;
    // Start is called before the first frame update
    void Start()
    {
        canvasExists = false;
        animationDone = true;
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
        if (selected && animationDone && sameGoal.CheckForPlayer() && sameGoal.otherGoal.CheckForPlayer())
        {
            selected = false;
            Debug.Log("Level Complete!");
            Instantiate(Resources.Load("Prefabs/endLevelTimer", typeof(GameObject)));
        }

        //Turn the clock hand
        handPivot.eulerAngles = new Vector3 (0, 30 * turnNumber, 0);
        

        if (selected)
        {
            //Make it glow
            GetComponent<Renderer>().material = glowingMat;
        }
        else
        {
            //Make it plain
            GetComponent<Renderer>().material = plainMat;
        }

        //handPivot.eulerAngles = rotate;

        if (animationDone)
        {
            LookForGround();

            //See if this player has fallen off the world, and if so, restart the level
            if (transform.parent.transform.position.y < -1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            transform.position = transform.parent.transform.position;

            //Tell the Animator to return to Default State
            animator.SetTrigger("Animation Done");
        }

        if (selected && animationDone)
        {
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
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
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
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
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
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
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
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 90, 0)));
                        }
                        plyrPosition.x--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(3);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 45
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 135)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 180, 0)));
                        }
                        plyrPosition.z++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(0);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 135
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 225)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 270, 0)));
                        }
                        plyrPosition.x++;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(1);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
                else if (GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y >= 225
                && GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localEulerAngles.y < 315)
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, colLayer))
                    {
                        LayerMask redLayer = LayerMask.NameToLayer("RedPlayer");
                        if (gameObject.layer == redLayer)
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/RedFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        else
                        {
                            footstepList.Add(Instantiate(Resources.Load("Prefabs/BlueFootsteps", typeof(GameObject)), new Vector3 (transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.Euler(0, 0, 0)));
                        }
                        plyrPosition.z--;
                        transform.parent.transform.position = plyrPosition;
                        movementList.Add(2);
                        turnNumber++;
                        otherPlyr.GetComponent<plyrRewind>().Rewind(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
                        animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        animator.SetTrigger("Player Bump");
                        if (!canvasExists && (turnNumber >= 3 || otherPlyr.GetComponent<plyrMovement>().turnNumber >= 3))
                        {
                            Instantiate(Resources.Load("Prefabs/Canvas", typeof(GameObject)));
                            canvasExists = true;
                        }
                    }
                }
            }
        }

        //Manage the opacity of the footsteps
        for (var i = 0; i < turnNumber; i++)
        {
            var footstepColor = footstepList[i].GetComponent<MeshRenderer>().material.color;
            if (turnNumber - i == 1)
            {
                footstepColor.a = 1;
            }
            else if (turnNumber - i == 2)
            {
                footstepColor.a = 0.8f;
            }
            else if (turnNumber - i == 3)
            {
                footstepColor.a = 0.6f;
            }
            else if (turnNumber - i == 4)
            {
                footstepColor.a = 0.5f;
            }
            else 
            {
                footstepColor.a = 0;
            }
            footstepList[i].GetComponent<MeshRenderer>().material.color = footstepColor;
        }
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
