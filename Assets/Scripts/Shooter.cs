using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public GameObject[] enemiesObjects; // Array of zombies
    private Animator _animator;
    private AttackController2 attackController;
    private MoveSpawnAndCamera moveSpawnAndCamera;
    private bool gameStarted;
    private bool isMoving = false; 


    public float shootForce = 10f; // Force with which the projectile is shot

    private void Start()
    {
        attackController = FindObjectOfType<AttackController2>();
        moveSpawnAndCamera = FindObjectOfType<MoveSpawnAndCamera>();
        // Convertir la matriz de zombies a una matriz de GameObjects
        enemiesObjects = CharacterManager.Instance.GetEnemies();

        InvokeRepeating("Shoot", 4f, 4f);

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isMoving = moveSpawnAndCamera.isMoving;
        gameStarted = attackController.isGameStartet;
    }

    public void Shoot()
    {
        enemiesObjects = CharacterManager.Instance.GetEnemies();
        

        if (enemiesObjects.Length > 0 && gameStarted && !isMoving)
        {
            // Instantiate the projectile from the prefab and place it at the shoot point
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
            StartCoroutine(MoveTowardsTarget(projectileRigidbody.transform));
            _animator.SetTrigger("Attack");
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

    }
}
