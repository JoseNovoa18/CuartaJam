using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public GameObject[] enemiesObjects; // Array of zombies


    public float shootForce = 10f; // Force with which the projectile is shot

    private void Start()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // Obtener todos los objetos de tipo Zombie en la escena

        // Convertir la matriz de zombies a una matriz de GameObjects
        enemiesObjects = System.Array.ConvertAll(enemies, enemy => enemy.gameObject);

      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Check if the S key was pressed
        {
            Debug.Log("Shooting");
            Shoot();
        }
    }

    public void Shoot()
    {
        // Instantiate the projectile from the prefab and place it at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

        StartCoroutine(MoveTowardsTarget(newProjectile.transform));
    }

    private IEnumerator MoveTowardsTarget(Transform objectToMove)
    {
        float duration = 1.0f; // Duration of the movement
        float elapsedTime = 0.0f;

        Vector3 initialPosition = objectToMove.position;
        Vector3 target = enemiesObjects[0].transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate linear interpolation between the initial position and the target
            float t = elapsedTime / duration;
            Vector3 newPosition = Vector3.Lerp(initialPosition, target, t);

            // Update the position of the object
            objectToMove.position = newPosition;

            yield return null;
        }

        // Once the target is reached, you can perform other actions or destroy the object if necessary
        Destroy(objectToMove.gameObject);
    }
}
