using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            playerInstance = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            NetworkServer.Spawn(playerInstance, connectionToClient);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
