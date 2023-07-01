using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected abstract IEnumerator PerformAttack(GameObject target);
    protected abstract IEnumerator Retreat(Vector3 initialPosition);
    protected abstract IEnumerator Health(GameObject target, Vector3 initialPosition);

    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack(GameObject target)
    {
        StartCoroutine(AttackCoroutine(target));
    }

    private IEnumerator AttackCoroutine(GameObject target)
    {
        yield return StartCoroutine(PerformAttack(target));
        yield return StartCoroutine(Retreat(transform.position));
    }
}