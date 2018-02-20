using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float yPos = -0.31f;
    private float xPos = 2;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(xPos, yPos, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            transform.position = new Vector3(Mathf.Clamp(xPos-1,1f,3f), yPos, 0);
            xPos = transform.position.x;
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position = new Vector3(Mathf.Clamp(xPos + 1, 1f, 3f), yPos, 0);
            xPos = transform.position.x;
        }
        if (Input.GetKeyDown("space"))
        {
            float leftCol = xPos - 1f;
            float rightCol = xPos;
            //Store right column in the astral plane
            Transform leftGroup = null;
            Transform rightGroup = null;
            foreach(Group group in Grid.groups)
            {
                if(group.xpos == leftCol)
                {
                    leftGroup = group.transform;
                }
                else if(group.xpos == rightCol)
                {
                    rightGroup = group.transform;
                }
            }
            
            rightGroup.position += new Vector3(-1, 0, 0);
            leftGroup.position += new Vector3(1, 0, 0);

            

            rightGroup.GetComponent<Group>().updateBlocks();
            leftGroup.GetComponent<Group>().updateBlocks();
        }

    }
}
