using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWorker : Character
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    protected override IEnumerator Health(GameObject target, Vector3 initialPosition)
    {
        yield break;
    }

    protected override IEnumerator PerformAttack(GameObject target)
    {
        animator.SetTrigger("Attack");
        yield break;
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        yield break;
    }
}
