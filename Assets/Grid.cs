using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int w = 4;
    public static int h = 12;
    public static Transform[,] grid = new Transform[w, h];
    public static Dictionary<Group, int> groups;

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
        groups = new Dictionary<Group, int>();

    }

    // Update is called once per frame
    void Update()
    {
        timeToFall -= Time.deltaTime;
        if(timeToFall < 0)
        {
            fall();
            timeToFall = timeReset;
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
                if (v.x == 0)
                {
                    Instantiate(child, new Vector3(0f, v.y, 0f), Quaternion.identity, G0.transform);
                    Destroy(child.gameObject);
                    groups[Group0] = 1;
                    grid[0, (int)v.y] = Group0.transform;
                } else if (v.x == 1) {
                    Instantiate(child, new Vector3(1f, v.y, 0f), Quaternion.identity, G1.transform);
                    Destroy(child.gameObject);
                    groups[Group1] = 1;
                    grid[1, (int)v.y] = Group1.transform;
                } else if (v.x == 2) {
                    Instantiate(child, new Vector3(2f, v.y, 0f), Quaternion.identity, G2.transform);
                    Destroy(child.gameObject);
                    groups[Group2] = 1;
                    grid[2, (int)v.y] = Group2.transform;
                } else if (v.x == 3) {
                    Instantiate(child, new Vector3(3f, v.y, 0f), Quaternion.identity, G3.transform);
                    Destroy(child.gameObject);
                    groups[Group3] = 1;
                    grid[3, (int)v.y] = Group3.transform;
                } 
            }
        }
    }

    bool isFree(Transform transform)
    {
        foreach (Group group in groups.Keys)
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
