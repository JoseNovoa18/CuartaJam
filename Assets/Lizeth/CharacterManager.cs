using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; } // Instancia del singleton
    public GameObject[] enemiesObjects; // Array of zombies


    private void Awake()
    {
        // Verificar si ya existe una instancia del singleton
        if (Instance != null && Instance != this)
        {
            // Si ya hay una instancia, destruir este objeto
            Destroy(gameObject);
            return;
        }

        // Si no hay una instancia, asignar esta instancia al singleton
        Instance = this;

        // Mantener este objeto persistente entre las escenas
        DontDestroyOnLoad(gameObject);
    }

    public void AddEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // Obtener todos los objetos de tipo Zombie en la escena

        // Convertir la matriz de zombies a una matriz de GameObjects
        enemiesObjects = System.Array.ConvertAll(enemies, enemy => enemy.gameObject);
    }

    public void RemoveEnemy(GameObject[] enemiesObject, GameObject destroyedObject)
    {
        // Eliminar el objeto del arreglo enemiesObjects
        if (ArrayContainsGameObject(enemiesObjects, destroyedObject))
        {
            enemiesObjects = RemoveGameObjectFromArray(enemiesObjects, destroyedObject);
        }
    }

    public GameObject[] GetEnemies()
    {
        return enemiesObjects;
    }
    private bool ArrayContainsGameObject(GameObject[] array, GameObject gameObject)
    {
        foreach (var obj in array)
        {
            if (obj == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private GameObject[] RemoveGameObjectFromArray(GameObject[] array, GameObject gameObject)
    {
        List<GameObject> list = new List<GameObject>(array);
        list.Remove(gameObject);
        return list.ToArray();
    }
}
