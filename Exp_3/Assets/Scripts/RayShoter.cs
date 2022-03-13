using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShoter : MonoBehaviour
{
    private Camera _camera;
    private GameObject _explosion;

    [SerializeField] private GameObject explosionPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
                
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        int posX = _camera.pixelHeight / 2 - size / 4;
        int posY = _camera.pixelWidth / 2 - size / 2;
        GUI.Label(new Rect(posY, posX, size, size), "+");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = new Vector3((float)_camera.pixelWidth / 2, (float)_camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(point);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                StartCoroutine(SphereIndicator(hit.point));
            }
            else
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToAim();
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        if (_explosion == null)
        {
            Quaternion _rotation = Quaternion.identity;
            _explosion = Instantiate(explosionPrefab, pos, _rotation);
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(_explosion);
        Destroy(sphere);
    }
}
