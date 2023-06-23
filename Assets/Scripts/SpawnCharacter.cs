using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject prefabToSpawn1;
    public GameObject prefabToSpawn2;
    public GameObject prefabToSpawn3;
    public GameObject prefabToSpawn4;

    public void SpawnCharacter1()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        Instantiate(prefabToSpawn1, randomSpawnPosition, Quaternion.identity);
    }

    public void SpawnCharacter2()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        Instantiate(prefabToSpawn2, randomSpawnPosition, Quaternion.identity);
    }

    public void SpawnCharacter3()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        Instantiate(prefabToSpawn3, randomSpawnPosition, Quaternion.identity);
    }

    public void SpawnCharacter4()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        Instantiate(prefabToSpawn4, randomSpawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-3f, 1f), 2f, Random.Range(-2f, 3f));
    }
}
