using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera TPVCamera;
    [SerializeField] private Camera FPVCamera;
    
    [SerializeField] private GameObject player;

    private FPSInput fpsInput;
    private MouseLook mouseLook;
    private RelativeMovement relativeMovement;
    // Start is called before the first frame update
    void Start()
    {
        fpsInput = player.GetComponent<FPSInput>();
        mouseLook = player.GetComponent<MouseLook>();
        relativeMovement = player.GetComponent<RelativeMovement>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (TPVCamera.gameObject.activeSelf)
            {
                FPVCamera.gameObject.SetActive(true);
                TPVCamera.gameObject.SetActive(false);
                fpsInput.enabled = true;
                mouseLook.enabled = true;
                relativeMovement.enabled = false;
            }
            else
            {
                TPVCamera.gameObject.SetActive(true);
                FPVCamera.gameObject.SetActive(false);
                fpsInput.enabled = false;
                mouseLook.enabled = false;
                relativeMovement.enabled = true;
            }
        }
    }
}
