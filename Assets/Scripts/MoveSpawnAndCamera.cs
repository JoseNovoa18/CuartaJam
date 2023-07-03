using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawnAndCamera : MonoBehaviour
{
    public void MoveObjects()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z -= 10;
        transform.position = currentPosition;
    }
    //private MoveObjects moveSpawnZombies;

    /*
    private void Start()
    {
        moveSpawnZombies = GetComponent<MoveObjects>();
    }
    */

    //moveSpawnZombies.MoveObjects();
}
