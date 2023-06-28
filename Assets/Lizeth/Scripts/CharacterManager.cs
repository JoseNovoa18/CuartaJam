using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; } // Instancia del singleton
    public GameObject[] enemiesObjects; // Arreglo de objetos Enemy
    public GameObject[] zombiesObjects; // Arreglo de objetos Zombie y ZombieWorker

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
        else if (typeof(T) == typeof(Zombie) || typeof(T) == typeof(ZombieWorker))
        {
            int existingZombiesCount = zombiesObjects != null ? zombiesObjects.Length : 0;
            int newCharactersCount = characters != null ? characters.Length : 0;
            int totalZombiesCount = existingZombiesCount + newCharactersCount;

            GameObject[] updatedZombies = new GameObject[totalZombiesCount];
            if (existingZombiesCount > 0)
            {
                zombiesObjects.CopyTo(updatedZombies, 0);
            }
            if (newCharactersCount > 0)
            {
                GameObject[] characterObjects = System.Array.ConvertAll(characters, character => character.gameObject);
                characterObjects.CopyTo(updatedZombies, existingZombiesCount);
            }

            zombiesObjects = updatedZombies;
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
        else if (typeof(T) == typeof(Zombie) || typeof(T) == typeof(ZombieWorker))
        {
            if (ArrayContainsGameObject(zombiesObjects, destroyedObject))
            {
                zombiesObjects = RemoveGameObjectFromArray(zombiesObjects, destroyedObject);
            }
        }
    }

    public GameObject[] GetEnemies()
    {
        return enemiesObjects;
    }

    public GameObject[] GetZombies()
    {
        return zombiesObjects;
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
