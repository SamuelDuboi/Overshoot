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
    }

}
