using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake(float shakeTime, float force)
    {
        StartCoroutine(OnShake(shakeTime, force));
    }

    private IEnumerator OnShake(float shakeTime, float force)
    {
        Vector3 pos = transform.position;
        float time = 0.0f;
        while (time < shakeTime)
        {
            transform.localPosition = pos + Random.insideUnitSphere * force;

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pos;
    }
}
