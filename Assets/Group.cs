using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {
    public List<Transform> blocks = new List<Transform>(); // list of blocks in this group
    private Grid grid; // reference to grid object to check block positions
    public int xpos; // between 0 and 4
    int numChildren = 0;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(xpos, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (numChildren != transform.childCount) //If there are more or less children due to new children being instantiated in this group
        {
            updateBlocks();
        }
	}

    public void updateBlocks()
    {
        xpos = (int) transform.position.x; //Keep xpos and transform.position.x insync
        foreach (Transform child in transform){ //Forcefully enforces position and behavior of blocks assigned to this group. Useful when swapping columns
            child.position = new Vector3(xpos, child.position.y, 0);
        }

        numChildren = transform.childCount; // Update child count
        if (numChildren > 1) //Possible annihilation
        {
            Transform trans1 = transform.GetChild(numChildren - 1);
            Transform trans2 = transform.GetChild(numChildren - 2);
            if (trans1.gameObject.tag ==
                trans2.gameObject.tag)
            {
                // Destroys the two most recent children
                Destroy(trans1.gameObject);
                Destroy(trans2.gameObject);
                // Reclaim gridspace
                Grid.grid[xpos, (int) trans1.position.y] = null;
                Grid.grid[xpos, (int) trans2.position.y] = null;
                numChildren = transform.childCount; // Update child count
            }
        }
        
    }

    public bool containsTransform(Transform transform)
    {
        return blocks.Contains(transform);
    }
}
