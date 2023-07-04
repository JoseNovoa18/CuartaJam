using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class Enemy : Character
{
    public int hasBrains = 0;
    private Animator animator;
    private AudioManager audioManager;


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
        // Obtener la posición inicial del enemigo
        Vector3 initialPosition = transform.position;

        // Mover al enemigo hacia el objetivo y realizar el ataque
        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector3.Distance(transform.position, target.transform.position);
        float movementSpeed = 6f; // Ajusta la velocidad de movimiento según tus necesidades

        while (distance > 1f)
        {
            // TODO ANIMACION CORRER
            _animator.SetTrigger("Run");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.transform.position);

            // Realizar el ataque al objetivo (puedes agregar la lógica correspondiente aquí)
            _animator.SetTrigger("Attack");
            audioManager.PlayAttack(audioManager.Attack);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
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
            //animator.SetTrigger("");
             
            lifeController.ReduceHealth(10, target); // Adjust the amount of life to reduce according to your needs
        }

        yield return StartCoroutine(Retreat(initialPosition));
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        _animator.SetTrigger("Run");
        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades
        float delayBetweenIterations = 0.1f; // Ajusta el tiempo de espera entre iteraciones

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            // TODO ANIMACION ATACAR
            //animator.SetTrigger("");
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(delayBetweenIterations);
        }

        yield return null;

        _animator.SetTrigger("Idle");
    }

    
}
