using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardAnimals : MonoBehaviour
{
    public float speed;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Move to character
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        animator.SetTrigger("Run");

    }
}
