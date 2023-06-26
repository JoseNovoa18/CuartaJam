using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; } // Instancia del singleton
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zoombiesObjects;


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

    public void AddCharacter<T>() where T : MonoBehaviour
    {
        T[] characters = FindObjectsOfType<T>(); // Obtener todos los objetos del tipo especificado en la escena

        if (typeof(T) == typeof(Enemy))
        {
            enemiesObjects = System.Array.ConvertAll(characters, character => character.gameObject);
        }
        else if (typeof(T) == typeof(Zombie))
        {
            zoombiesObjects = System.Array.ConvertAll(characters, character => character.gameObject);
        }
    }

    public void RemoveCharacter<T>(GameObject destroyedObject) where T : MonoBehaviour
    {
        if (typeof(T) == typeof(Enemy))
        {
            if (ArrayContainsGameObject(enemiesObjects, destroyedObject))
            {
                enemiesObjects = RemoveGameObjectFromArray(enemiesObjects, destroyedObject);
            }
        }
        else if (typeof(T) == typeof(Zombie))
        {
            if (ArrayContainsGameObject(zoombiesObjects, destroyedObject))
            {
                zoombiesObjects = RemoveGameObjectFromArray(zoombiesObjects, destroyedObject);
            }
        }
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
    public GameObject[] GetZoombies()
    {
        return zoombiesObjects;
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
