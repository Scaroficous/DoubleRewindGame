using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winTimer : MonoBehaviour
{
    public float count;
    public GameObject floor;
    public Material winnerGold;

    // Start is called before the first frame update
    void Start()
    {
        count = 60;

        //Make the floor shiny because the player won
        floor = GameObject.FindGameObjectWithTag("Floor");
        floor.GetComponent<Renderer>().material = winnerGold;
    }

    // Update is called once per frame
    void Update()
    {
        count -= 60 * Time.deltaTime;
        if (count <= 0)
        {
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
