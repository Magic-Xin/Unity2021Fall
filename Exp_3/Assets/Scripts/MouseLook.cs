using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Rigidbody body;
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public GameObject characterCamera;

    public float sensitivityH = 9.0f;
    public float sensitivityV = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    public float _rotationX = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityH, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityV;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float _rotationY = transform.localEulerAngles.y;
            characterCamera.transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityV;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float _rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityH;
            transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            characterCamera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
    }
}
