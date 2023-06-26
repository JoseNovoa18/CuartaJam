using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected abstract IEnumerator PerformAttack(GameObject target);
    protected abstract IEnumerator Retreat(Vector3 initialPosition);

    public void Attack(GameObject target)
    {
        StartCoroutine(AttackCoroutine(target));
    }

    private IEnumerator AttackCoroutine(GameObject target)
    {
        Debug.Log("LLEGO A CHARACTER");
        yield return StartCoroutine(PerformAttack(target));
        yield return StartCoroutine(Retreat(transform.position));
    }

    private IEnumerator RetreatCoroutine()
    {
        float movementSpeed = 5f; // Ajusta la velocidad de movimiento según tus necesidades
        Vector3 initialPosition = transform.position;

        while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
}