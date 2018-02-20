using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int w = 4;
    public static int h = 12;
    public static Transform[,] grid = new Transform[w, h];
    public static Group[] groups;

    public Group Group0;
    public Group Group1;
    public Group Group2;
    public Group Group3;

    public GameObject G0;
    public GameObject G1;
    public GameObject G2;
    public GameObject G3;

    public float timeToFall = 1f;
    // Set same as timeToFall
    public float timeReset = 1f;

    // Use this for initialization
    void Start()
    {
        //Initialize groups
        groups = new Group[4] { G0.GetComponent<Group>(), G1.GetComponent<Group>(), G2.GetComponent<Group>(), G3.GetComponent<Group>() };

    }

    // Update is called once per frame
    void Update()
    {
        timeToFall -= Time.deltaTime;
        if(timeToFall < 0)
        {
            fall();
            timeToFall = timeReset;
            if (timeReset > 0.5f)
            {
                timeReset -= 0.1f;
            }
        }
    }

    void fall()
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 1; y < h; y++)
            {
                if (grid[x, y] != null && isFree(grid[x, y]))
                {
                    // Move one towards bottom
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;

                    // Update Block position
                    grid[x, y - 1].position += new Vector3(0f, -1, 0);
                } 
            }
        }
        foreach (Transform child in transform)
        {
            Vector2 v = child.position;
            grid[(int)v.x, (int)v.y] = child;
            if (v.y == 0 || grid[(int)v.x, (int)v.y - 1] != null)
            {
                // Transfering the blocks to groups
                // Essentially everytime a block should go to a group, I delete it from the Grid,
                // reinstatiate it into one of the Group GameObjects, and replace it on the Grid
                // with a Group.transform.

                foreach(Group group in groups)
                {
                    if((int)group.xpos == (int)v.x)
                    {
                        Instantiate(child, new Vector3(group.xpos, v.y, 0), Quaternion.identity, group.transform);
                        Destroy(child.gameObject);
                        grid[(int)v.x, (int)v.y] = group.transform;
                    }
                }
            }
        }
    }

    bool isFree(Transform transform)
    {
        foreach (Group group in groups)
        {
            if (group.containsTransform(transform))
            {
                return false;
            }
        }
        return true;
    }

    public static bool isInGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }
}
