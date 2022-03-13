using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
	public GameObject brick;
    // Start is called before the first frame update
    void Start()
    {
	    createWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createWall()
    {
	    for (int i=0;i<5;i++) {
		    for (int j=0;j<5;j++) {
			    Instantiate(brick, new Vector3(i, j, 0), Quaternion.identity);
		    }
	    }
    }
}
