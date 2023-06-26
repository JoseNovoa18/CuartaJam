using UnityEngine;
using System.Collections;

public class Zombie : Character
{
    protected override IEnumerator PerformAttack(GameObject target)
    {
        Debug.Log("LLEGO AQUI 3");
        // Lógica de ataque para el zombie
        Debug.Log("Zombie attacking: " + target.name);

        // Obtener la posición inicial del zombie
        Vector3 initialPosition = transform.position;

        // Mover al zombie hacia el objetivo y realizar el ataque
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float movementSpeed = 8f; // Ajusta la velocidad de movimiento según tus necesidades

        while (distance > 0f)
        {
            Debug.Log("moviendo");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.transform.position);

            // Realizar el ataque al objetivo (puedes agregar la lógica correspondiente aquí)

            yield return null; // Permitir que el motor de juego actualice la posición del objeto en cada iteración
        }

        // Finalizar el ataque
        Debug.Log("Zombie attack finished");

        // Iniciar el retroceso del zombie hacia su posición inicial
        yield return StartCoroutine(Retreat(initialPosition));
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        // Lógica de retroceso para el zombie
        Debug.Log("Zombie retreating");

        float movementSpeed = 3f; // Ajusta la velocidad de movimiento según tus necesidades
        float delayBetweenIterations = 0.1f; // Ajusta el tiempo de espera entre iteraciones

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(delayBetweenIterations);
        }

        // Retroceso completado, volver al estado normal
        Debug.Log("Zombie retreat finished");
    }

}
