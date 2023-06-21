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
        if (CanSpawnAtPosition(randomSpawnPosition))
        {
            Instantiate(prefabToSpawn1, randomSpawnPosition, Quaternion.identity);
        }
    }

    public void SpawnCharacter2()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        if (CanSpawnAtPosition(randomSpawnPosition))
        {
            Instantiate(prefabToSpawn2, randomSpawnPosition, Quaternion.identity);
        }
    }
    public void SpawnCharacter3()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        if (CanSpawnAtPosition(randomSpawnPosition))
        {
            Instantiate(prefabToSpawn3, randomSpawnPosition, Quaternion.identity);
        }
    }
    public void SpawnCharacter4()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        if (CanSpawnAtPosition(randomSpawnPosition))
        {
            Instantiate(prefabToSpawn4, randomSpawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-2f, 3f), 1f, Random.Range(-2f, 3f));
    }

    private bool CanSpawnAtPosition(Vector3 position)
    {
        /*
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f); // Ajusta el radio según el tamaño del objeto

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Respawn"))
            {
                return false; // No se puede spawnear aquí porque ya hay un objeto
            }
        }

        return true; // Es seguro spawnear en esta posición
        */
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f); // Ajusta el radio según el tamaño del objeto

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Respawn"))
            {
                return false; // No se puede spawnear aquí porque ya hay un objeto
            }
        }

        // Buscar una nueva posición si hay colisiones
        int maxAttempts = 10;
        int currentAttempt = 0;
        while (colliders.Length > 0 && currentAttempt < maxAttempts)
        {
            position = GetRandomSpawnPosition();
            colliders = Physics.OverlapSphere(position, 0.5f);
            currentAttempt++;
        }

        return colliders.Length == 0; // Retorna true si no hay colisiones
    }
}
