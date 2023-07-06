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
    }

    
    public void AddCharacter<T>(string zombie, string zombieWorker, string enemy) where T : MonoBehaviour
    {
        GameObject[] characters = null;

        if (typeof(T) == typeof(Enemy))
        {
            characters = GameObject.FindGameObjectsWithTag(enemy);
            enemiesObjects = characters;
        }
        else if (typeof(T) == typeof(Zombie) || typeof(T) == typeof(ZombieWorker))
        {
            characters = GameObject.FindGameObjectsWithTag(zombie);
            List<GameObject> tempList = new List<GameObject>(characters);

            // Tambi�n agregar los personajes ZombieWorker
            GameObject[] zombieWorkers = GameObject.FindGameObjectsWithTag(zombieWorker);
            tempList.AddRange(zombieWorkers);

            zombiesObjects = tempList.ToArray();
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
