using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _health;

    public Material skybox_default;
    public Material skybox_enemyHit;
    // Start is called before the first frame update
    void Start()
    {
        _health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Hurt (int damage)
    {
        _health -= damage;
        Debug.Log(_health);
        StartCoroutine(ChangeSky());
    }

    private IEnumerator ChangeSky()
    {
        RenderSettings.skybox = skybox_enemyHit;
        yield return new WaitForSeconds(1.5f);
        RenderSettings.skybox = skybox_default;
    }
}
