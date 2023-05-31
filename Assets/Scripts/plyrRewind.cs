using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plyrRewind : MonoBehaviour
{
    public plyrMovement moveScript;
    RaycastHit lookForWall;
    // Start is called before the first frame update
    public void Rewind(float otherPlyrX, float otherPlyrZ)
    {
        if (moveScript.turnNumber > 0)
        {
            var plyrPosition = transform.position;
            var otherPlyrPosition = moveScript.otherPlyr.transform.position;
            switch (moveScript.movementList[moveScript.turnNumber])
            {
                default: break;

                case 0:
                {
                    if (!Physics.Raycast(transform.position, Vector3.back, out lookForWall, 1, moveScript.colLayer) 
                    && !(plyrPosition.x == otherPlyrX && plyrPosition.z - 1 == otherPlyrZ))
                    {
                        plyrPosition.z -= 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;

                case 1:
                {
                    if (!Physics.Raycast(transform.position, Vector3.left, out lookForWall, 1, moveScript.colLayer) && plyrPosition != otherPlyrPosition
                    && !(plyrPosition.x - 1 == otherPlyrX && plyrPosition.z == otherPlyrZ))
                    {
                        plyrPosition.x -= 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;

                case 2:
                {
                    if (!Physics.Raycast(transform.position, Vector3.forward, out lookForWall, 1, moveScript.colLayer) && plyrPosition != otherPlyrPosition
                    && !(plyrPosition.x == otherPlyrX && plyrPosition.z + 1 == otherPlyrZ))
                    {
                        plyrPosition.z += 1;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                    }
                }
                break;

                case 3:
                {
                    otherPlyrPosition.x -= 1;
                    if (!Physics.Raycast(transform.position, Vector3.right, out lookForWall, 1, moveScript.colLayer) && plyrPosition != otherPlyrPosition
                    && !(plyrPosition.x + 1 == otherPlyrX && plyrPosition.z == otherPlyrZ))
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
    }
}
