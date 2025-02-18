using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int increment)
    {
        gameManager.IncreaseScore(increment);
    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Brick>())
            {
                child.GetComponent<Brick>().GameRestart();
            }
            else if (child.GetComponent<QBox>())
            {
                child.GetComponent<QBox>().GameRestart();
            }
            ;
        }
    }
}