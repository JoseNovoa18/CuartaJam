using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWorker : Character
{
    
    protected override IEnumerator Health(GameObject target, Vector3 initialPosition)
    {
        yield break;
    }

    protected override IEnumerator PerformAttack(GameObject target)
    {
        yield break;
    }

    protected override IEnumerator Retreat(Vector3 initialPosition)
    {
        yield break;
    }
}
