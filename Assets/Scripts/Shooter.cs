using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public Transform shootPoint; // Point of origin for the shoot


    public float shootForce = 10f; // Force with which the projectile is shot

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

        if (projectileRigidbody != null)
        {
            Vector3 newPosition = newProjectile.transform.position;
            newPosition.y = 2;
            newProjectile.transform.position = newPosition;

            StartCoroutine(MoveTowardsTarget(newProjectile.transform, shootPoint.position));

        }
    }

    private IEnumerator MoveTowardsTarget(Transform objectToMove, Vector3 target)
    {
        float duration = 1.0f; // Duration of the movement
        float elapsedTime = 0.0f;

        Vector3 initialPosition = objectToMove.position;

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
        //Destroy(objectToMove.gameObject);
    }
}
