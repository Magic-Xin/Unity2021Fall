using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("hit");
            player.Hurt(damage);
        }

        StartCoroutine(Scale());
    }

    private IEnumerator Scale()
    {
        while (transform.localScale.x > 0.0f)
        {
            transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(this.gameObject);
    }
}
