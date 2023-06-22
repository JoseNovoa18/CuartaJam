using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacters : MonoBehaviour
{
    public GameObject prefabToSpawn1;
    public GameObject prefabToSpawn2;
    public GameObject prefabToSpawn3;
    public GameObject prefabToSpawn4;

    private List<GameObject> spawnedCharacters1 = new List<GameObject>();
    private List<GameObject> spawnedCharacters2 = new List<GameObject>();
    private List<GameObject> spawnedCharacters3 = new List<GameObject>();
    private List<GameObject> spawnedCharacters4 = new List<GameObject>();

    public void SpawnCharacter1()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn1, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters1.Add(newCharacter);
    }

    public void SpawnCharacter2()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn2, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters2.Add(newCharacter);
    }

    public void SpawnCharacter3()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn3, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters3.Add(newCharacter);
    }

    public void SpawnCharacter4()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn4, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters4.Add(newCharacter);
    }

    public void DestroyCharacter1()
    {
        if (spawnedCharacters1.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters1[spawnedCharacters1.Count - 1];
            spawnedCharacters1.Remove(characterToRemove);
            Destroy(characterToRemove);
        }
    }

    public void DestroyCharacter2()
    {
        if (spawnedCharacters2.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters2[spawnedCharacters2.Count - 1];
            spawnedCharacters2.Remove(characterToRemove);
            Destroy(characterToRemove);
        }
    }

    public void DestroyCharacter3()
    {
        if (spawnedCharacters3.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters3[spawnedCharacters3.Count - 1];
            spawnedCharacters3.Remove(characterToRemove);
            Destroy(characterToRemove);
        }
    }

    public void DestroyCharacter4()
    {
        if (spawnedCharacters4.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters4[spawnedCharacters4.Count - 1];
            spawnedCharacters4.Remove(characterToRemove);
            Destroy(characterToRemove);
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-3f, 4f), 0f, Random.Range(-3f, 4f));
    }
}
