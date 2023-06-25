using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    private GameObject[] enemiesObjects; // Array of zombies


    public float shootForce = 10f; // Force with which the projectile is shot

    private void Start()
    {
        //Enemy[] enemies = FindObjectsOfType<Enemy>(); // Obtener todos los objetos de tipo Zombie en la escena

        CharacterManager.Instance.AddEnemy();

        // Convertir la matriz de zombies a una matriz de GameObjects
        enemiesObjects = CharacterManager.Instance.GetEnemies();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Check if the S key was pressed
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        enemiesObjects = CharacterManager.Instance.GetEnemies();

        if (enemiesObjects.Length > 0)
        {
            // Instantiate the projectile from the prefab and place it at the shoot point
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();

            StartCoroutine(MoveTowardsTarget(projectileRigidbody.transform));
        }
    }

    private IEnumerator MoveTowardsTarget(Transform objectToMove)
    {
        float duration = 1.0f; // Duration of the movement
        float elapsedTime = 0.0f;

        int zombiesIndex = Random.Range(0, enemiesObjects.Length - 1);

        Vector3 initialPosition = objectToMove.position;
        Vector3 target = enemiesObjects[zombiesIndex].transform.position;

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
        //Debug.Log("le dio!!!! lo mato veci, lo mato");

    }
}
