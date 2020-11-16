using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerUserInterface : NetworkBehaviour
{
    public GameObject interfacePrefab;
    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            if (isClient)
            {
                ui = Instantiate(interfacePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}
