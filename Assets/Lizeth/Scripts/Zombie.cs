using UnityEngine;
using System.Collections;

public class Zombie : Character
{
    public int damage = 15;
    private AudioManager audioManager;
    private Animator animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();   
    }

    protected override IEnumerator PerformAttack(GameObject target)
    {
        // Obtener la posición inicial del zombie
        Vector3 initialPosition = transform.position;
        yield return new WaitForSeconds(0.5f);

        // Mover al zombie hacia el objetivo y realizar el ataque
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float movementSpeed = 6f; // Ajusta la velocidad de movimiento según tus necesidades


        while (distance > 1f)
        {
            // TODO ANIMACION CORRER
            animator.SetTrigger("");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.transform.position);

            yield return null; // Permitir que el motor de juego actualice la posición del objeto en cada iteración
        }

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(Health(target, initialPosition));
    }

    protected override IEnumerator Health(GameObject target, Vector3 initialPosition)
    {
        if (target == null)
        {
            // El objetivo ya no existe, salimos del método
            yield break;
        }

        // Reduce the life of the target object
        Health lifeController = target.GetComponent<Health>();
        if (lifeController != null)
        {
            // TODO ANIMACION ATACAR
            animator.SetTrigger("");
            lifeController.ReduceHealth(damage, target); // Adjust the amount of life to reduce according to your needs
        }


        yield return StartCoroutine(Retreat(initialPosition));
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades
        float delayBetweenIterations = 0.1f; // Ajusta el tiempo de espera entre iteraciones

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            // TODO ACTIVAR ANIMACION RETROCEDER
            animator.SetTrigger("");
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(delayBetweenIterations);
        }
    }

}
