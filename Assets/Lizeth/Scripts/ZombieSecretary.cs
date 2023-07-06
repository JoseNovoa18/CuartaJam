using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSecretary : MonoBehaviour
{
    public float movementSpeed = 2f; // Velocidad de movimiento de la secretaria
    public float explosionRadius = 5f; // Radio de la explosi�n
    public int damage = 50; // Da�o causado por la explosi�n

    private GameObject[] enemies; // Array de enemigos
    public bool isGameInit = false;

    private CharacterManager characterManager;
    private StateGame stateGame;

    private void Start()
    {
        stateGame = FindAnyObjectByType<StateGame>();
        // Obtener los objetos de los enemigos
        enemies = CharacterManager.Instance.GetEnemies();
        characterManager = FindObjectOfType<CharacterManager>();
    }

    private void Update()
    {
        enemies = CharacterManager.Instance.GetEnemies();
        characterManager = CharacterManager.Instance;

        isGameInit = stateGame.isGameStarted;

        if (enemies.Length > 0 && isGameInit)
        {
            // Mover la secretaria hacia los enemigos
            transform.position = Vector3.MoveTowards(transform.position, enemies[0].transform.position, movementSpeed * Time.deltaTime);
        }
    }

    /*public void OnclickButton()
    {
        Debug.Log("funciona boton fight");
        isGameInit = true;
        if (enemies.Length > 0 && isGameInit)
        {
            while (true)
            {
                Debug.Log("hola muns");
                // Mover la secretaria hacia los enemigos
                transform.position = Vector3.MoveTowards(transform.position, enemies[0].transform.position, movementSpeed * Time.deltaTime);
            }
        }
      
    }*/

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto colisionado es un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Aplicar da�o al enemigo colisionado
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ReduceHealth(damage, other.gameObject);
            }

            // Avisar al CharacterManager que se ha destruido un objeto
            CharacterManager.Instance.RemoveCharacter<Enemy>(other.gameObject);

            // Destruir la secretaria
            Destroy(gameObject);
        }
    }
}
