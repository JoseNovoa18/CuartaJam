using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class AttackController : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    public Button startGame;

    private Rigidbody rb;

    public GameObject zombieObjectPrefab;
    public Vector3 specificPosition;

    public AudioClip attackSound; // Sonido de ataque
    private AudioSource audioSource; // Referencia al componente AudioSource


    Vector3 initialZombieObjectPosition;
    Vector3 initialEnemyObjectPosition;

    private bool attackZombieObject = true;

    private void StartGame()
    {
        CharacterManager.Instance.AddEnemy();
        enemiesObjects = CharacterManager.Instance.GetEnemies();

        Zombie[] zombies = FindObjectsOfType<Zombie>(); // Obtener todos los objetos de tipo Zombie en la escena

        // Convertir la matriz de zombies a una matriz de GameObjects
        zombiesObjects = System.Array.ConvertAll(zombies, zombie => zombie.gameObject);

        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource del objeto
        audioSource.clip = attackSound; // Asignar el archivo de sonido al componente AudioSource

        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component of the object
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Apply rotation constraint

        StartCoroutine(PerformAttacks());

        // Suscribirse al evento OnObjectDestroyed de cada objeto enemigo
        foreach (var enemyObject in enemiesObjects)
        {
            Health lifeComponent = enemyObject.GetComponent<Health>();
            if (lifeComponent != null)
            {
                lifeComponent.OnObjectDestroyed += RemoveDestroyedObject;
            }
        }

        // Suscribirse al evento OnObjectDestroyed de cada objeto zombie
        foreach (var zombieObject in zombiesObjects)
        {
            Health lifeComponent = zombieObject.GetComponent<Health>();
            if (lifeComponent != null)
            {
                lifeComponent.OnObjectDestroyed += RemoveDestroyedObject;
            }
        }
    }

  

    private void RemoveDestroyedObject(GameObject destroyedObject)
    {
        CharacterManager.Instance.RemoveEnemy(enemiesObjects, destroyedObject);

        // Eliminar el objeto del arreglo enemiesObjects
        if (ArrayContainsGameObject(enemiesObjects, destroyedObject))
        {
            enemiesObjects = RemoveGameObjectFromArray(enemiesObjects, destroyedObject);
        }

        // Eliminar el objeto del arreglo zombiesObjects
        if (ArrayContainsGameObject(zombiesObjects, destroyedObject))
        {
            zombiesObjects = RemoveGameObjectFromArray(zombiesObjects, destroyedObject);
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // Check if the A key is pressed
        {
            GameObject zombieObject = Instantiate(zombieObjectPrefab, specificPosition, Quaternion.identity);
            Rigidbody zombieObjectRigidbody = zombieObject.GetComponent<Rigidbody>();

            if (zombieObjectRigidbody != null)
            {
                zombieObjectRigidbody.isKinematic = true;
            }

        }

    }

    public void OnButtonClick()
    {
        // Acciones que deseas realizar cuando se hace clic en el botón
        Debug.Log("Botón clickeado: " + gameObject.name);
        StartGame();
    }

    private IEnumerator PerformAttacks()
    {
        while (zombiesObjects.Length > 0 && enemiesObjects.Length > 0)
        {
            int zombiesIndex = Random.Range(0, zombiesObjects.Length - 1);
            int enemiesIndex = Random.Range(0, enemiesObjects.Length - 1);
            GameObject zombieObject = zombiesObjects[zombiesIndex];
            GameObject enemyObject = enemiesObjects[enemiesIndex];
            initialZombieObjectPosition = zombieObject.transform.position;
            initialEnemyObjectPosition = enemyObject.transform.position;

            // Check if the next attack is from a good object
            if (attackZombieObject)
            {
                // Disable movement of the bad object
                enemyObject.GetComponent<Rigidbody>().isKinematic = true;

                // Move the good object towards the bad object and perform the attack
                yield return StartCoroutine(Attack(zombieObject, enemyObject));

                // Enable movement of the good object again
                enemyObject.GetComponent<Rigidbody>().isKinematic = false;

                yield return StartCoroutine(Health(enemyObject));

                // Retreat the good object
                yield return StartCoroutine(Retreat(zombieObject, initialZombieObjectPosition));



                attackZombieObject = false;
            }
            else
            {
                // Disable movement of the good object
                zombieObject.GetComponent<Rigidbody>().isKinematic = true;

                // Move the bad object towards the good object and perform the attack
                yield return StartCoroutine(Attack(enemyObject, zombieObject));

                // Enable movement of the bad object again
                zombieObject.GetComponent<Rigidbody>().isKinematic = false;

                yield return StartCoroutine(Health(zombieObject));

                // Retreat the bad object
                yield return StartCoroutine(Retreat(enemyObject, initialEnemyObjectPosition));

                attackZombieObject = true;
            }

            // Wait for a short moment before continuing with the next attack
            yield return new WaitForSeconds(1f);
        }

        attackZombieObject = true;

    }

    private IEnumerator Attack(GameObject attacker, GameObject target)
    {
        Vector3 direction = target.transform.position - attacker.transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(attacker.transform.position, target.transform.position);
        float movementSpeed = 10f; // Adjust the movement speed as needed



        while (distance > 0f)
        {
            attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(attacker.transform.position, target.transform.position);
            // Perform the attack
            yield return null;
        }

        // Reproducir el sonido de ataque
        audioSource.Play();
    }

    private IEnumerator Retreat(GameObject obj, Vector3 initialPosition)
    {
        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades

        while (Vector3.Distance(obj.transform.position, initialPosition) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Health(GameObject obj)
    {
        // Reduce the life of the target object
        Health lifeController = obj.GetComponent<Health>();
        if (lifeController != null)
        {
            lifeController.ReduceHealth(10, obj); // Adjust the amount of life to reduce according to your needs
        }
        yield return null;
    }

}