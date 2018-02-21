﻿using System.Collections;
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
            if (timeReset > 0.2f)
            {
                timeReset -= 0.01f;
            }
        }
    }

    void fall()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = child.position;
            grid[(int)v.x, (int)v.y] = child;
            if (v.y == 0 || !isFree((int)v.x, (int)v.y - 1)) // Reached lowest point
            {
                // Transfering the blocks to groups
                // Essentially everytime a block should go to a group, I delete it from the Grid,
                // reinstatiate it into one of the Group GameObjects, and replace it on the Grid
                // with a Group.transform.

                foreach(Group group in groups)
                {
                    if((int)group.xpos == (int)v.x)
                    {
                        Transform newobj = Instantiate(child, new Vector3(group.xpos, v.y, 0), Quaternion.identity, group.transform);
                        Destroy(child.gameObject);
                    }
                }
            }
            else if(isFree((int)v.x, (int)v.y - 1)) // Still free falling
            {
                grid[(int)v.x, (int)v.y - 1] = grid[(int)v.x, (int)v.y];
                grid[(int)v.x, (int)v.y] = null;
                child.position += new Vector3(0f, -1, 0);

            }
        }
        
    }

    bool isFree(int x, int y)
    {
        foreach (Group group in groups)
        {
            foreach (Transform child in group.transform)
            {
                if((int)child.position.x == x && (int)child.position.y == y)
                {
                    return false;
                }
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
