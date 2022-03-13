using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    public Color highlightColor = Color.red;

    private TextMesh textMesh;

    [SerializeField] private SceneController _controller;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnMouseEnter()
    {
        if (textMesh != null)
        {
            textMesh.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        if (textMesh != null)
        {
            textMesh.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
    }

    public void OnMouseUp()
    {
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        _controller.StopGame();
    }
}
