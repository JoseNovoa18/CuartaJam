using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawnAndCamera : MonoBehaviour
{
    private Vector3 targetPosition;
    public bool isMoving = false;
    public float moveSpeed = 5f;

    public void MoveObjects()
    {
        if (!isMoving)
        {
            targetPosition = transform.position;
            targetPosition.z -= 14.53f;
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Comprueba si el objeto ha llegado cerca de la posición objetivo
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // Establece la posición objetivo directamente para evitar pequeñas fluctuaciones
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
    /*
    public void MoveObjects()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z -= 10;
        transform.position = currentPosition;
    }
    */
    //private MoveObjects moveSpawnZombies;

    /*
    private void Start()
    {
        moveSpawnZombies = GetComponent<MoveObjects>();
    }
    */

    //moveSpawnZombies.MoveObjects();
}
