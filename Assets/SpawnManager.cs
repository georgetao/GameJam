using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject[] blocks;
    public GameObject GM;

    public float timeToSpawn = 6f;
    // Set this the same as timeToSpawn
    public float timeReset = 6f; 

    public void spawnNext()
    {
        // Which block gets spawned
        int i = Random.Range(0, blocks.Length);
        int i1 = Random.Range(0, blocks.Length);

        // Position of block spawn
        float j = Random.Range(0, 4);
        float j1 = Random.Range(0, 4);

        // Makes sure the two blocks aren't in the same column
        while (j1 == j)
        {
            j1 = Random.Range(0, 4);
        }

        // Create blocks
        Instantiate(blocks[i], new Vector3(j, 8f, 0f), Quaternion.identity, GM.transform);
        Instantiate(blocks[i1], new Vector3(j1, 8f, 0f), Quaternion.identity, GM.transform);
        
    }
	// Use this for initialization
	void Start () {
        Debug.Log("STARTSPAWN");
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn < 0)
        {
            Debug.Log("SPAWN");
            timeToSpawn = timeReset;
            spawnNext();
        }
	}
}
