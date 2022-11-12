using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public void OnSpawn(PlayerInput input)
    {
        input.gameObject.transform.position = spawnPoints[input.playerIndex].position +Vector3.up*3;
        if (input.playerIndex % 2 == 0)
        {
            input.gameObject.tag = "Team1Player";
            GameManager.instance.Team1.Add(input.gameObject.GetComponent<MyController>());
        }
        else
        {
            input.gameObject.tag = "Team2Player";
            GameManager.instance.Team2.Add(input.gameObject.GetComponent<MyController>());
        }
        input.gameObject.tag = input.playerIndex % 2 == 0 ? "Team1Player": "Team2Player";
    }

}
