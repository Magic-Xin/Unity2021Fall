using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public float speedMax = 15.0f;
    public float speedTime = 2.0f;

    public Material skybox_default;
    public Material skybox_playerHit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToHit()
    {
        // Debug.Log("Target hit");
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        
        StartCoroutine(Die());
    }

    public void ReactToAim()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            //StartCoroutine(Fast(behavior));
        }
        
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        RenderSettings.skybox = skybox_playerHit;
        yield return new WaitForSeconds(1.5f);
        RenderSettings.skybox = skybox_default;
        Destroy(this.gameObject);
    }

    private IEnumerator Fast(WanderingAI behavior)
    {
        if (behavior.speed < speedMax)
        {
            behavior.speed++;
        }
        yield return new WaitForSeconds(speedTime);
        if (behavior.speed > behavior.basic_speed)
        {
            behavior.speed--;
        }
    }
}
