using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSecretary : MonoBehaviour
{
    public float movementSpeed = 2f; // Velocidad de movimiento de la secretaria
    public float explosionRadius = 5f; // Radio de la explosión
    public int damage = 50; // Daño causado por la explosión

    private GameObject[] enemies; // Array de enemigos
    public bool isGameInit = false;
    private Animator _animatorSecretary;
    private ExplosionEffect explosionEffect;



    private CharacterManager characterManager;
    private StateGame stateGame;

    private void Start()
    {
        stateGame = FindAnyObjectByType<StateGame>();
        // Obtener los objetos de los enemigos
        enemies = CharacterManager.Instance.GetEnemies();
        characterManager = FindObjectOfType<CharacterManager>();
        explosionEffect = FindObjectOfType<ExplosionEffect>();
        _animatorSecretary = GetComponent<Animator>();

    }

    private void Update()
    {
        enemies = CharacterManager.Instance.GetEnemies();
        characterManager = CharacterManager.Instance;

        isGameInit = stateGame.isGameStarted;

        if (enemies.Length > 0 && isGameInit)
        {
            // Mover la secretaria hacia los enemigos
            _animatorSecretary.SetTrigger("Run");
            transform.position = Vector3.MoveTowards(transform.position, enemies[0].transform.position, movementSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto colisionado es un enemigo
        if (other.CompareTag("Enemy") || other.CompareTag("Enemy2") || other.CompareTag("Enemy3"))
        {
            // Aplicar daño al enemigo colisionado
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ReduceHealth(damage, other.gameObject);

            }

            ExplosionEffect explosionEffect = FindObjectOfType<ExplosionEffect>();
            if (explosionEffect != null)
            {
                explosionEffect.PlayExplosion(transform.position);
            }

            // Avisar al CharacterManager que se ha destruido un objeto
            CharacterManager.Instance.RemoveCharacter<Enemy>(other.gameObject);

            // Destruir la secretaria
            Destroy(gameObject);


        }

       /* IEnumerator DestroySecretary()
        {
            yield return new WaitForSeconds(0);
            Destroy(gameObject);
        } */

    }


}
