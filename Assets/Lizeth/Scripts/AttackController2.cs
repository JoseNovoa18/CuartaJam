using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AttackController2 : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    public GameObject win;
    public GameObject lost;

    public Transform destinationPointRound2; // Punto de destino despu�s de ganar la batalla
    public Transform destinationPointRound3; // Punto de destino despu�s de ganar la batalla
    public float movementSpeed = 2f; // Velocidad de movimiento de los zombies

    private bool attackZombieObject = true;
    private bool isGameStartet = false;
    private Coroutine attackCoroutine;
    private bool attacking = false;

    private string zombie, zombieWorker, enemy;
    private int round = 1;

    private bool shouldContinue = true; // Bandera para controlar el bucle

    private List<Character> characters = new List<Character>(); // Lista de personajes

    public void OnButtonClick()
    {
        StartGame("Zombie", "ZombieWorker", "Enemy", 1);
    }

    private void Update()
    {
        if (isGameStartet)
        {
            SelectCharacters.OnCharacterSpawned += HandleCharacterSpawned;

            if (zombiesObjects.Length > 0 && enemiesObjects.Length < 1) {

                switch (round)
                {
                    case 1:
                        StartCoroutine(MoveZombiesToDestination(destinationPointRound2, 2));
                        break;
                    case 2:
                        StartCoroutine(MoveZombiesToDestination(destinationPointRound3, 3));
                        break;
                    case 3:
                        win.SetActive(true);
                        break;
                    default:
                        Console.WriteLine("Opci�n no reconocida");
                        break;
                }
            }

            if (enemiesObjects.Length > 0 && zombiesObjects.Length < 1)
            {
                if (lost != null)
                {
                    lost.SetActive(true);
                }
            }

            if (!attacking && enemiesObjects.Length > 0 && zombiesObjects.Length > 0)
            {
                enemiesObjects = CharacterManager.Instance.GetEnemies();
                zombiesObjects = CharacterManager.Instance.GetZombies();

                StartCoroutine(PerformAttacks());
            }

        }
    }

    private IEnumerator MoveZombiesToDestination(Transform destinationPoint, int round)
    {
        // Obtener la posici�n de destino
        Vector3 destination = destinationPoint.position;

        while (Vector3.Distance(zombiesObjects[0].transform.position, destination) > 0.1f)
        {
            // Mover cada zombie hacia la posici�n de destino
            foreach (var zombieObject in zombiesObjects)
            {
                if (zombieObject != null)
                {
                    // Generar un valor aleatorio para el desplazamiento en el eje X
                    float randomOffsetX = Random.Range(-2f, 2f);
                    Vector3 destinationWithOffset = destination + new Vector3(randomOffsetX, 0f, 0f);

                    zombieObject.transform.position = Vector3.MoveTowards(zombieObject.transform.position, destinationWithOffset, movementSpeed * Time.deltaTime);
                }
            }

            yield return null; // Esperar al siguiente frame
        }

        if (round  == 2) {
            StartGame("Zombie", "ZombieWorker", "Enemy2", 2);
        }

        if (round == 3)
        {
            StartGame("Zombie", "ZombieWorker", "Enemy3", 3);
        }

    }


    private void HandleCharacterSpawned(GameObject newCharacter)
    {
        CharacterManager.Instance.AddCharacter<Enemy>(this.zombie, this.zombieWorker, this.enemy);
        CharacterManager.Instance.AddCharacter<Zombie>(this.zombie, this.zombieWorker, this.enemy);
        CharacterManager.Instance.AddCharacter<ZombieWorker>(this.zombie, this.zombieWorker, this.enemy);

        StartCoroutine(UpdateCharacterLists());
        //StartCoroutine(StartAttacksAfterDelay());
    }

    private IEnumerator UpdateCharacterLists()
    {
        yield return null; // Esperar un frame para permitir que los personajes se agreguen correctamente

        enemiesObjects = CharacterManager.Instance.GetEnemies();
        zombiesObjects = CharacterManager.Instance.GetZombies();
        //shouldContinue = true;
        //StartCoroutine(WaitAndRestartAttacks());
    }

    /*private IEnumerator StartAttacksAfterDelay()
    {
        yield return new WaitForSeconds(80f); // Esperar dos segundos antes de comenzar los ataques
        //attackCoroutine = StartCoroutine(PerformAttacks());
    }*/

    private void HandleObjectDestroyed(GameObject destroyedObject)
    {
        shouldContinue = false;
        CharacterManager.Instance.RemoveCharacter<Enemy>(destroyedObject);
        CharacterManager.Instance.RemoveCharacter<Zombie>(destroyedObject);

        StartCoroutine(UpdateCharacterLists());
    }

    private void StartGame(string zombie, string zombieWorker, string enemy, int round)
    {
        isGameStartet = true;
        this.zombie = zombie;
        this.zombieWorker = zombieWorker;
        this.enemy = enemy;

        CharacterManager characterManager = CharacterManager.Instance;
        characterManager.AddCharacter<Enemy>(this.zombie, this.zombieWorker, this.enemy);
        characterManager.AddCharacter<Zombie>(this.zombie, this.zombieWorker, this.enemy);
        characterManager.AddCharacter<ZombieWorker>(this.zombie, this.zombieWorker, this.enemy);

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

        if (round == 2)
        {
            this.round = 2;
        }

        if (round == 3)
        {
            this.round = 3;
        }


        //StartCoroutine(PerformAttacks());
    }

    /*private IEnumerator WaitAndRestartAttacks()
    {
        yield return new WaitForSeconds(80f); // Esperar dos segundos
        StopAttacks();
        //attackCoroutine = StartCoroutine(PerformAttacks());
    }*/

    private IEnumerator PerformAttacks()
    {
        attacking = true;
        int zombiesIndex = Random.Range(0, zombiesObjects.Length);
        GameObject zombieObject = zombiesObjects[zombiesIndex];

        int enemiesIndex = Random.Range(0, enemiesObjects.Length);
        GameObject enemyObject = enemiesObjects[enemiesIndex];

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
            } while (zombieObject.name.Contains("Worker") && zombiesObjects.Length > 1);
            zombieCharacter.Attack(enemyObject);
            attackZombieObject = false;
        }
        else
        {
            // Realizar el ataque
            enemyCharacter.Attack(zombieObject);
            attackZombieObject = true;
        }

        yield return new WaitForSeconds(5f); // Esperar antes de continuar con el pr�ximo ataque
        attacking = false;

        /*while (shouldContinue && zombiesObjects.Length > 0 && enemiesObjects.Length > 0)
        {
            int zombiesIndex = Random.Range(0, zombiesObjects.Length);
            GameObject zombieObject = zombiesObjects[zombiesIndex];

            int enemiesIndex = Random.Range(0, enemiesObjects.Length);
            GameObject enemyObject = enemiesObjects[enemiesIndex];
            // Verificar si el objeto zombie a�n existe y no ha sido destruido
            if (zombieObject == null || zombieObject.GetComponent<Character>() == null)
            {
                continue;
            }

            // Verificar si el objeto enemigo a�n existe y no ha sido destruido
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
                } while (zombieObject.name.Contains("Worker") && zombiesObjects.Length > 1);
                zombieCharacter.Attack(enemyObject);
                attackZombieObject = false;
            }
            else
            {
                // Realizar el ataque
                enemyCharacter.Attack(zombieObject);
                attackZombieObject = true;
            }

            yield return new WaitForSeconds(5f); // Esperar antes de continuar con el pr�ximo ataque
        }*/

    }

    public void ResetSceneFromMainMenu()
    {
        // Reinicia la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}