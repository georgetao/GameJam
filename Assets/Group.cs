using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {
    private List<Transform> blocks = new List<Transform>(); // list of blocks in this group
    private int lumo = 0; // index of the top of the group. LUMO = Lowest Unoccupied Molecular Orbital
    private Grid grid; // reference to grid object to check block positions
    public int xpos; // between 0 and 4

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Grid.grid[xpos, lumo] != null)
        {
            updateBlocks();
        }
	}

    void updateBlocks()
    {
        if (lumo == 0)
        {
            blocks.Add(transform);
            lumo++;
        }
        else 
        {
            int numChildren = transform.childCount;
            if (transform.GetChild(numChildren - 1).gameObject.tag == 
                transform.GetChild(numChildren - 2).gameObject.tag)
            {
                // Destroys the two most recent children (which will be the top two blocks)
                Destroy(transform.GetChild(numChildren - 1).gameObject);
                Destroy(transform.GetChild(numChildren - 2).gameObject);
                blocks.RemoveAt(lumo - 1);
                Grid.grid[xpos, lumo] = null;
                Grid.grid[xpos, lumo - 1] = null;
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
