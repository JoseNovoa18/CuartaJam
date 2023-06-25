using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 20; // The maximum health of the object
    private int currentHealth; // The current health of the object
    public event Action<GameObject> OnObjectDestroyed;

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
            // The object has died, perform additional actions if necessary
            Destroy(gameObject); // Destroy the object

            GameObject[] enemies = CharacterManager.Instance.GetEnemies();
            CharacterManager.Instance.RemoveEnemy(enemies, objectDestroy);

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
        GameObject otherObject = collision.collider.gameObject; // Otro objeto que colisionó con este objeto

        // Verificar si los nombres de los objetos contienen las palabras clave
        if (thisObject.name.Contains("Secretary") && otherObject.name.Contains("Brick"))
        {
            ReduceHealth(1, thisObject);
        }
    }
}
