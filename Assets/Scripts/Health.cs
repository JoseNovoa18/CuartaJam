using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 40; // The maximum health of the object
    private int currentHealth; // The current health of the object
    public event Action<GameObject> OnObjectDestroyed;
    public event Action<GameObject> OnCollisionDetected;
    public TextMeshProUGUI brainsText;
    //public int damage = 1;
    private int damage = 2;

    public event Action<float> OnHealthPctChanged = delegate { };

    private CountBrains countBrains; // Referencia a la instancia de CountBrains
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth; // Set the initial life as the maximum life

        // Obtener la instancia de CountBrains
        countBrains = CountBrains.Instance;
    }

    public void ReduceHealth(int amount, GameObject objectDestroy)
    {
        currentHealth -= amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        // Check if the object has lost all its life
        if (currentHealth <= 0)
        {
            if(objectDestroy != null && objectDestroy.GetComponent<Enemy>() is Enemy)
{
                if (countBrains != null)
                {
                    Enemy enemy = objectDestroy.GetComponent<Enemy>();
                    CountBrains.Instance.Brainss += enemy.hasBrains;
                    brainsText.text = CountBrains.Instance.Brainss.ToString();
                }

                audioManager.PlaySFX(audioManager.SheepDeath);

                CharacterManager.Instance.RemoveCharacter<Enemy>(objectDestroy);
                Destroy(objectDestroy);
            }

            
            // Verificar si el objeto destruido es un Zombie
            else if (objectDestroy.GetComponent<Zombie>() is Zombie)
            {
                audioManager.PlaySFX(audioManager.Zombie2Death);

                CharacterManager.Instance.RemoveCharacter<Zombie>(objectDestroy);
                Destroy(gameObject);
            }
            else if (objectDestroy.GetComponent<ZombieWorker>() is ZombieWorker)
            {
                CharacterManager.Instance.RemoveCharacter<ZombieWorker>(objectDestroy);
                Destroy(gameObject);
            }

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
        if (thisObject.CompareTag("Enemy") && otherObject.name.Contains("Brick"))
        {
            //ReduceHealth(3, thisObject);
            ReduceHealth(damage, thisObject);
        }

        if (thisObject.CompareTag("Enemy2") && otherObject.name.Contains("Brick"))
        {
            ReduceHealth(damage, thisObject);
        }

        if (thisObject.CompareTag("Enemy3") && otherObject.name.Contains("Brick"))
        {
            ReduceHealth(damage, thisObject);
        }

        if (thisObject.CompareTag("Enemy") && otherObject.name.Contains("Doctor"))
        {
            Debug.Log("Colisión detectada entre " + thisObject.name + " y " + otherObject.name);
            // Notificar a AttackController sobre la colisión
            OnCollisionDetected?.Invoke(otherObject);
        }
    }
}