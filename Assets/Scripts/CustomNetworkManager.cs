using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public GameObject specialPlatformPrefab;
    public GameObject specialWallPrefab;
    public Transform[] specialPlatformPlaceholders;
    public Transform[] specialWallPlaceholders;

    public override void OnStartServer()
    {
        base.Start();

        foreach(Transform ph in specialPlatformPlaceholders)
        {
            GameObject specialPlatform = Instantiate(specialPlatformPrefab, ph.transform.position, ph.transform.rotation);
            NetworkServer.Spawn(specialPlatform);
        }

        foreach (Transform ph in specialWallPlaceholders)
        {
            GameObject specialWall = Instantiate(specialWallPrefab, ph.transform.position, ph.transform.rotation);
            NetworkServer.Spawn(specialWall);
        }
    }
}
