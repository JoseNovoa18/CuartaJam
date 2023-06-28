using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController2 : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    public GameObject win;
    public GameObject lost;
    private bool attackZombieObject = true;
    private bool isGameStartet = false;
    private Coroutine attackCoroutine;

    private bool shouldContinue = true; // Bandera para controlar el bucle

    private List<Character> characters = new List<Character>(); // Lista de personajes

    public void OnButtonClick()
    {
        StartGame();
    }

    private void Update()
    {
        if (isGameStartet)
        {
            SelectCharacters.OnCharacterSpawned += HandleCharacterSpawned;

            if (zombiesObjects.Length > 0 && enemiesObjects.Length < 1) {
                if (win != null)
                {
                    win.SetActive(true);
                }
            }

            if (enemiesObjects.Length > 0 && zombiesObjects.Length < 1)
            {
                if (lost != null)
                {
                    lost.SetActive(true);
                }
            }
        }
    }

    private void StopAttacks()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    private void HandleCharacterSpawned(GameObject newCharacter)
    {
        CharacterManager.Instance.AddCharacter<Enemy>();
        CharacterManager.Instance.AddCharacter<Zombie>();
        CharacterManager.Instance.AddCharacter<ZombieWorker>();

        StartCoroutine(UpdateCharacterLists());
        StartCoroutine(StartAttacksAfterDelay());
    }

    private IEnumerator UpdateCharacterLists()
    {
        yield return null; // Esperar un frame para permitir que los personajes se agreguen correctamente

        enemiesObjects = CharacterManager.Instance.GetEnemies();
        zombiesObjects = CharacterManager.Instance.GetZombies();
        shouldContinue = true;
        StartCoroutine(WaitAndRestartAttacks());
    }

    private IEnumerator StartAttacksAfterDelay()
    {
        yield return new WaitForSeconds(80f); // Esperar dos segundos antes de comenzar los ataques
        attackCoroutine = StartCoroutine(PerformAttacks());
    }

    private void HandleObjectDestroyed(GameObject destroyedObject)
    {
        shouldContinue = false;
        CharacterManager.Instance.RemoveCharacter<Enemy>(destroyedObject);
        CharacterManager.Instance.RemoveCharacter<Zombie>(destroyedObject);

        StartCoroutine(UpdateCharacterLists());
    }

    private void StartGame()
    {
        isGameStartet = true;

        CharacterManager characterManager = CharacterManager.Instance;
        characterManager.AddCharacter<Enemy>();
        characterManager.AddCharacter<Zombie>();
        characterManager.AddCharacter<ZombieWorker>();

        // Obtener los objetos de los enemigos y zombies
        enemiesObjects = CharacterManager.Instance.GetEnemies();
        zombiesObjects = CharacterManager.Instance.GetZombies();

        // Asignar valor aleatorio solo a un enemigo
        if (enemiesObjects.Length > 0)
        {
            int randomEnemyIndex = Random.Range(0, enemiesObjects.Length);
            GameObject randomEnemyObject = enemiesObjects[randomEnemyIndex];
            Enemy randomEnemy = randomEnemyObject.GetComponent<Enemy>();
            if (randomEnemy != null)
            {
                randomEnemy.hasBrains = 10; 
            }
        }

        // Suscribirse al evento OnObjectDestroyed de cada objeto enemigo
        foreach (var enemyObject in enemiesObjects)
        {
            Health lifeComponent = enemyObject.GetComponent<Health>();
            if (lifeComponent != null)
            {
                lifeComponent.OnObjectDestroyed += HandleObjectDestroyed;
            }
        }

        // Suscribirse al evento OnObjectDestroyed de cada objeto zombie
        foreach (var zombieObject in zombiesObjects)
        {
            Health lifeComponent = zombieObject.GetComponent<Health>();
            if (lifeComponent != null)
            {
                lifeComponent.OnObjectDestroyed += HandleObjectDestroyed;
            }
        }

        StartCoroutine(PerformAttacks());
    }

    private IEnumerator WaitAndRestartAttacks()
    {
        yield return new WaitForSeconds(80f); // Esperar dos segundos
        StopAttacks();
        attackCoroutine = StartCoroutine(PerformAttacks());
    }

    private IEnumerator PerformAttacks()
    {
        while (shouldContinue && zombiesObjects.Length > 0 && enemiesObjects.Length > 0)
        {
            int zombiesIndex = Random.Range(0, zombiesObjects.Length);
            GameObject zombieObject = zombiesObjects[zombiesIndex];

            int enemiesIndex = Random.Range(0, enemiesObjects.Length);
            GameObject enemyObject = enemiesObjects[enemiesIndex];
            // Verificar si el objeto zombie aún existe y no ha sido destruido
            if (zombieObject == null || zombieObject.GetComponent<Character>() == null)
            {
                continue;
            }

            // Verificar si el objeto enemigo aún existe y no ha sido destruido
            if (enemyObject == null || enemyObject.GetComponent<Character>() == null)
            {
                continue;
            }

            // Acceder a los componentes de los personajes
            Character zombieCharacter = zombieObject.GetComponent<Character>();
            Character enemyCharacter = enemyObject.GetComponent<Character>();

            if (attackZombieObject)
            {
                // Realizar el ataque
                do
                {                  
                    zombiesIndex = Random.Range(0, zombiesObjects.Length);
                    zombieObject = zombiesObjects[zombiesIndex];
                    zombieCharacter = zombieObject.GetComponent<Character>();
                } while (zombieObject.name.Contains("Worker"));
                zombieCharacter.Attack(enemyObject);
                attackZombieObject = false;
            }
            else
            {
                // Realizar el ataque
                enemyCharacter.Attack(zombieObject);
                attackZombieObject = true;
            }

            yield return new WaitForSeconds(5f); // Esperar antes de continuar con el próximo ataque
        }


    }
}