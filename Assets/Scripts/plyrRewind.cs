using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyrRewind : MonoBehaviour
{
    public plyrMovement moveScript;
    public LayerMask justWalls;
    RaycastHit lookForWall;

    //Rewinds - does the opposite of its previous move (in the four cardinal directions)
    public void Rewind(float otherPlyrX, float otherPlyrY, float otherPlyrZ)
    {
        if (moveScript.turnNumber > 0)
        {
            var plyrPosition = transform.parent.transform.position;

            //Checks what the previous move was and does the opposite, and removes that movement
            //0 is forward
            //1 is right
            //2 is back 
            //3 is left

            switch (moveScript.movementList[moveScript.turnNumber])
            {
                default: break;

                case 0:
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);

                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.back, out lookForWall, 1, justWalls)
                    //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
                    && !(plyrPosition.x == otherPlyrX && plyrPosition.y == otherPlyrY && plyrPosition.z - 1 == otherPlyrZ))
                    {
                        plyrPosition.z--;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                        moveScript.animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        moveScript.animator.SetTrigger("Player Bump");
                    }
                }
                break;

                case 1:
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 270, transform.localEulerAngles.z);

                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.left, out lookForWall, 1, justWalls)
                    //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
                    && !(plyrPosition.x - 1 == otherPlyrX && plyrPosition.y == otherPlyrY && plyrPosition.z == otherPlyrZ))
                    {
                        plyrPosition.x--;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                        moveScript.animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        moveScript.animator.SetTrigger("Player Bump");
                    }
                }
                break;

                case 2:
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);

                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.forward, out lookForWall, 1, justWalls)
                    //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
                    && !(plyrPosition.x == otherPlyrX && plyrPosition.y == otherPlyrY && plyrPosition.z + 1 == otherPlyrZ))
                    {
                        plyrPosition.z++;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                        moveScript.animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        moveScript.animator.SetTrigger("Player Bump");
                    }
                }
                break;

                case 3:
                {
                    transform.parent.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90, transform.localEulerAngles.z);

                    if (!Physics.Raycast(transform.parent.transform.position, Vector3.right, out lookForWall, 1, justWalls)
                    //The following line checks the other player's transform, because the physics may not have updated if both characters are moving at the same time, so a raycast won't work
                    && !(plyrPosition.x + 1 == otherPlyrX && plyrPosition.y == otherPlyrY && plyrPosition.z == otherPlyrZ))
                    {
                        plyrPosition.x++;
                        moveScript.movementList.RemoveAt(moveScript.turnNumber);
                        moveScript.turnNumber--;
                        moveScript.animator.SetTrigger("Player Move");
                    }
                    else
                    {
                        moveScript.animator.SetTrigger("Player Bump");
                    }
                }
                break;
            }

            transform.parent.transform.position = plyrPosition;
        }
    }
}
