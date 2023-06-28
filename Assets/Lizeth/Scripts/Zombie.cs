using UnityEngine;
using System.Collections;

public class Zombie : Character
{
    
    protected override IEnumerator PerformAttack(GameObject target)
    {
        // Obtener la posición inicial del zombie
        Vector3 initialPosition = transform.position;

        // Mover al zombie hacia el objetivo y realizar el ataque
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float movementSpeed = 6f; // Ajusta la velocidad de movimiento según tus necesidades

        yield return new WaitForSeconds(0.5f);
        while (distance > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.transform.position);

            yield return null; // Permitir que el motor de juego actualice la posición del objeto en cada iteración
        }

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Health(target, initialPosition));
    }

    protected override IEnumerator Health(GameObject target, Vector3 initialPosition)
    {
        yield return new WaitForSeconds(1f);
        if (target == null)
        {
            // El objetivo ya no existe, salimos del método
            yield break;
        }

        // Reduce the life of the target object
        Health lifeController = target.GetComponent<Health>();
        if (lifeController != null)
        {
            lifeController.ReduceHealth(10, target); // Adjust the amount of life to reduce according to your needs
        }
        yield return StartCoroutine(Retreat(initialPosition));
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {

        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades
        float delayBetweenIterations = 0.1f; // Ajusta el tiempo de espera entre iteraciones
        yield return new WaitForSeconds(1.5f);

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(delayBetweenIterations);
        }
        
        yield return null;
    }

}
