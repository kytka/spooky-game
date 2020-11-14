using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MouseLook : NetworkBehaviour
{
    public Transform camHolder;
    public GameObject playerCameraPrefab;
    public GameObject playerCamera;

    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            playerCamera = Instantiate(playerCameraPrefab, camHolder);
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority)
        {
            mouseX = Input.GetAxis("Mouse X") * 300f * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * 300f * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 55);
            Quaternion camRotation = Quaternion.Euler(xRotation, 0, 0);
            camHolder.transform.localRotation = camRotation;

            if ((mouseX > 0 && mouseX < 20) || (mouseX < 0 && mouseX > -20))
                yRotation += mouseX;

            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
