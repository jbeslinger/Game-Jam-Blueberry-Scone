using System.Collections.Generic;
using UnityEngine;

public class MazeMemoryBehavior : MonoBehaviour
{
    #region Fields
    public readonly List<Transform> mazeCells = new List<Transform>();

    public GameObject cyanKeyPrefab, magentaKeyPrefab, yellowKeyPrefab;
    #endregion

    #region Methods
    private void Awake()
    {
        for (int index = 0; index < transform.childCount; index++)
            mazeCells.Add(transform.GetChild(index));

        SpawnKeys();
    }

    private void SpawnKeys()
    {
        List<Transform> keySpawners = new List<Transform>();
        foreach (GameObject ks in GameObject.FindGameObjectsWithTag("Key Spawn"))
            keySpawners.Add(ks.transform);

        Transform spawnLocation;
        spawnLocation = keySpawners[Random.Range(0, keySpawners.Count)];
        Instantiate(cyanKeyPrefab, spawnLocation, false);
        keySpawners.Remove(spawnLocation);

        spawnLocation = keySpawners[Random.Range(0, keySpawners.Count)];
        Instantiate(magentaKeyPrefab, spawnLocation, false);
        keySpawners.Remove(spawnLocation);

        spawnLocation = keySpawners[Random.Range(0, keySpawners.Count)];
        Instantiate(yellowKeyPrefab, spawnLocation, false);
        keySpawners.Remove(spawnLocation);
    }
    #endregion
}
