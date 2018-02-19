using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public static int w = 4;
    public static int h = 12;
    public static Transform[,] grid = new Transform[w, h];
    public static Dictionary<Group, int> groups;
    // Use this for initialization
    void Start () {
        groups = new Dictionary<Group, int>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void fall()
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (grid[x, y] != null && isFree(grid[x,y]))
                {
                    // Move one towards bottom
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;

                    // Update Block position
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
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
