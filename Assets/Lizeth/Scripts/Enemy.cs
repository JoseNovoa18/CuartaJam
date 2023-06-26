using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    protected override IEnumerator PerformAttack(GameObject target)
    {
        // Lógica de ataque para el enemigo
        Debug.Log("Enemy attacking: " + target.name);

        // Reproducir el sonido de ataque (si se requiere)
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // Obtener la posición inicial del enemigo
        Vector3 initialPosition = transform.position;

        // Mover al enemigo hacia el objetivo y realizar el ataque
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float movementSpeed = 10f; // Ajusta la velocidad de movimiento según tus necesidades

        while (distance > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.transform.position);

            // Realizar el ataque al objetivo (puedes agregar la lógica correspondiente aquí)
            yield return null;
        }

        // Finalizar el ataque
        Debug.Log("Enemy attack finished");

        // Iniciar el retroceso del enemigo hacia su posición inicial
        Retreat(initialPosition);
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        // Lógica de retroceso para el enemigo
        Debug.Log("Enemy retreating");

        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
        }

        // Retroceso completado, volver al estado normal
        Debug.Log("Enemy retreat finished");
        
        yield return null;
    }
}
