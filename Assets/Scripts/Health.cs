using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 20; // The maximum health of the object
    private int currentHealth; // The current health of the object
    public event Action<GameObject> OnObjectDestroyed;
    public event Action<GameObject> OnCollisionDetected;


    private void Start()
    {
        currentHealth = maxHealth; // Set the initial life as the maximum life
    }

    public void ReduceHealth(int amount, GameObject objectDestroy)
    {
        maxHealth -= amount;

        // Check if the object has lost all its life
        if (maxHealth <= 0)
        {
            if (objectDestroy.GetComponent<Enemy>() is Enemy)
            {
                CharacterManager.Instance.RemoveCharacter<Enemy>(objectDestroy);
                Destroy(gameObject);
            }
            // Verificar si el objeto destruido es un Zombie
            else if (objectDestroy.GetComponent<Zombie>() is Zombie)
            {
                CharacterManager.Instance.RemoveCharacter<Zombie>(objectDestroy);
                Destroy(gameObject);
            }

            //GameObject[] enemies = CharacterManager.Instance.GetEnemies();
            //CharacterManager.Instance.RemoveEnemy(enemies, objectDestroy);

            // Trigger the OnObjectDestroyed event
            if (OnObjectDestroyed != null)
            {
                OnObjectDestroyed(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject thisObject = gameObject; // Objeto actual al que se le asigna este script
        GameObject otherObject = collision.collider.gameObject; // Otro objeto que colision� con este objeto

        

        // Verificar si los nombres de los objetos contienen las palabras clave
        if (thisObject.name.Contains("Secretary") && otherObject.name.Contains("Brick"))
        {
            ReduceHealth(1, thisObject);
        }

        if (thisObject.name.Contains("Secretary") && otherObject.name.Contains("Doctor"))
        {
            Debug.Log("colisiono " + thisObject.name + "con " + otherObject.name);
            // Notificar a AttackController sobre la colisi�n
            OnCollisionDetected?.Invoke(otherObject);
        }
    }
}
