using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject player;
    public Canvas winScreen;
    public float VictoryLength = 100;


    private void Update()
    {
        if(player.transform.position.x >= VictoryLength || player.transform.position.z >= VictoryLength ||
           player.transform.position.x <= -VictoryLength || player.transform.position.z <= -VictoryLength)
        {
            winScreen.gameObject.SetActive(true);
        }
    }
}
