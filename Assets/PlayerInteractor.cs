using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractor : NetworkBehaviour
{
    public Transform camHolder;

    void Update()
    {
        if (hasAuthority)
        {
            Debug.DrawRay(camHolder.position, camHolder.transform.forward, Color.blue);
            RaycastHit hit;
            if(Physics.Raycast(camHolder.position, camHolder.transform.forward, out hit, 3))
            {
                string tag = hit.transform.tag;
                switch (tag)
                {
                    case "Doors":
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject doors = hit.transform.gameObject;
                            CmdUseDoorsClick(doors);
                        }
                        break;
                }
            }
        }   
    }

    [Command]
    void CmdUseDoorsHold(GameObject doors, float input)
    {
        DoorsController doorsController = doors.GetComponent<DoorsController>();
        doorsController.UpdateRotation(input); 
    }

    [Command]
    void CmdUseDoorsClick(GameObject doors)
    {
        DoorsController doorsController = doors.GetComponent<DoorsController>();
        doorsController.SetRotation();
    }
}
