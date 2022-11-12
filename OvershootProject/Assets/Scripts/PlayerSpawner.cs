using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public Material mat1;
    public Material mat2;

    public void OnSpawn(PlayerInput input)
    {
        input.gameObject.transform.position = spawnPoints[input.playerIndex].position +Vector3.up*3;
        if (input.playerIndex % 2 == 0)
        {
            input.gameObject.tag = "Team1Player";
            GameManager.instance.Team1.Add(input.gameObject.GetComponent<MyController>());
            input.GetComponent<MyController>().meshRenderer.materials[0] = mat1;
        }
        else
        {
            input.gameObject.tag = "Team2Player";
            GameManager.instance.Team2.Add(input.gameObject.GetComponent<MyController>());
            input.GetComponent<MyController>().meshRenderer.materials[0] = mat2;

        }
    }

}
