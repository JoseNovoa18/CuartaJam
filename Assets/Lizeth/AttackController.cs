using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackController : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    private Rigidbody rb;
    private bool attackInProgress = false; // Control if an atrack is in progress
    //public GameObject zombieObjectPrefab;
    //public Vector3 specificPosition;

    Vector3 initialZombieObjectPosition;
    Vector3 initialEnemyObjectPosition;

    //public GameObject zombieObject;

    private bool attackZombieObject = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component of the object
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Apply rotation constraint
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !attackInProgress) // Check if there is no attack in progress
        {
            StartCoroutine(PerformAttacks());
        }

        if (Input.GetKeyDown(KeyCode.A)) // Check if the A key is pressed
        {
            /*GameObject zombieObject = Instantiate(zombieObjectPrefab, specificPosition, Quaternion.identity);
            Rigidbody zombieObjectRigidbody = zombieObject.GetComponent<Rigidbody>();

            if (zombieObjectRigidbody != null)
            {
                zombieObjectRigidbody.isKinematic = true;
            }*/

        }
    }

    private IEnumerator PerformAttacks()
    {
        attackInProgress = true;

        int zombiesIndex = 0; // Index to iterate through the good objects
        int enemiesIndex = 0; // Index to iterate through the bad objects

        while (zombiesIndex < zombiesObjects.Length && enemiesIndex < enemiesObjects.Length)
        {
            GameObject zombieObject = zombiesObjects[zombiesIndex];
            GameObject enemyObject = zombiesObjects[zombiesIndex];
            initialZombieObjectPosition = zombieObject.transform.position;
            initialEnemyObjectPosition = enemyObject.transform.position;

            // Check if the next attack is from a good object
            if (attackZombieObject)
            {
                // Disable movement of the bad object
                enemyObject.GetComponent<Rigidbody>().isKinematic = true;

                // Move the good object towards the bad object and perform the attack
                yield return StartCoroutine(Attack(zombieObject, enemyObject));

                Debug.Log("Good object attacked!");

                // Enable movement of the good object again
                enemyObject.GetComponent<Rigidbody>().isKinematic = false;

                // Increment the index of good objects
                zombiesIndex++;

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

                Debug.Log("Bad object attacked!");

                // Enable movement of the bad object again
                zombieObject.GetComponent<Rigidbody>().isKinematic = false;

                // Increment the index of bad objects
                enemiesIndex++;

                // Retreat the bad object
                yield return StartCoroutine(Retreat(enemyObject, initialZombieObjectPosition));

                attackZombieObject = true;
            }

            // Wait for a short moment before continuing with the next attack
            yield return new WaitForSeconds(0.5f);
        }

        attackZombieObject = true;
        attackInProgress = false;
    }

    private IEnumerator Attack(GameObject attacker, GameObject target)
    {
        Vector3 direction = target.transform.position - attacker.transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(attacker.transform.position, target.transform.position);
        float movementSpeed = 5f; // Adjust the movement speed as needed

        while (distance > 0f)
        {
            attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(attacker.transform.position, target.transform.position);
            // Perform the attack
            yield return null;
        }
    }

    private IEnumerator Retreat(GameObject obj, Vector3 initialPosition)
    {
        float movementSpeed = 5f; // Adjust the movement speed as needed
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, initialPosition, movementSpeed * Time.deltaTime);
        obj.transform.position = initialPosition;
        yield return null;
    }
}

