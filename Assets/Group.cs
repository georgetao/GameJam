using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {
    private List<Transform> blocks = new List<Transform>(); // list of blocks in this group
    private int lumo = 0; // index of the top of the group. LUMO = Lowest Unoccupied Molecular Orbital
    private Grid grid; // reference to grid object to check block positions
    private int xpos; // between 0 and 4
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void updateBlocks()
    {
        Transform transform = Grid.grid[xpos, lumo];
        if(transform != null)
        {
            if(transform.gameObject.tag == blocks[lumo].gameObject.tag)
            {
                Destroy(transform.gameObject);
                Destroy(blocks[lumo].gameObject);
                blocks.RemoveAt(lumo);
                Grid.grid[xpos, lumo] = null;
                Grid.grid[xpos, lumo + 1] = null;
                lumo-=1;
            }
            else
            {
                blocks.Add(transform); // adds transform of said block to group
                lumo++;
            }
            
        }
    }

    void moveColumn(int delta)
    {
        xpos += delta;
        
    }

    public bool containsTransform(Transform transform)
    {
        return blocks.Contains(transform);
    }

}
