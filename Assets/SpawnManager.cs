using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject[] blocks;

    public void spawnNext()
    {
        // Which block gets spawned
        int i = Random.Range(0, blocks.Length);
        int i1 = Random.Range(0, blocks.Length);

        // Position of block spawn
        float j = Random.Range(-2, 2);
        float j1 = Random.Range(-2, 2);

        // Makes sure the two blocks aren't in the same column
        while (j1 == j)
        {
            j1 = Random.Range(-2, 2);
        }

        Instantiate(blocks[i], new Vector3(j, 4f, 0f), Quaternion.identity);
        Instantiate(blocks[i1], new Vector3(j1, 4f, 0f), Quaternion.identity);
        GameGrid.grid[(int)j, 4] = blocks[i].transform;
        GameGrid.grid[(int)j1, 4] = blocks[i1].transform;
        
    }
	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
