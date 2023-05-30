using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plyrRewind : MonoBehaviour
{
    public plyrMovement moveScript;
    RaycastHit lookForWall;
    public goalCheck sameGoal;
    // Start is called before the first frame update
    public void Rewind()
    {
        if (moveScript.turnNumber > 0)
        {
            var plyrPosition = transform.position;
            switch (moveScript.movementList[moveScript.turnNumber])
            {
                default: break;
                case 0:
                {
                    if (!Physics.Raycast(transform.position, Vector3.back, out lookForWall, 1))
                    {
                        plyrPosition.z -= 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;
                case 1:
                {
                    if (!Physics.Raycast(transform.position, Vector3.left, out lookForWall, 1))
                    {
                        plyrPosition.x -= 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;
                case 2:
                {
                    if (!Physics.Raycast(transform.position, Vector3.forward, out lookForWall, 1))
                    {
                        plyrPosition.z += 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;
                case 3:
                {
                    if (!Physics.Raycast(transform.position, Vector3.right, out lookForWall, 1))
                    {
                        plyrPosition.x += 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;
            }
            transform.position = plyrPosition;
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

    }

    
}
