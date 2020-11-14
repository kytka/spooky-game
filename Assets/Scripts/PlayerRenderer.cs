using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRenderer : NetworkBehaviour
{
    public GameObject[] meshObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            if(meshObjects.Length > 0)
            {
                foreach(GameObject mesh in meshObjects)
                {
                    SkinnedMeshRenderer renderer = mesh.GetComponent<SkinnedMeshRenderer>();
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
