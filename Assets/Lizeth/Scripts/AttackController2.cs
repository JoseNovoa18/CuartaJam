using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public Transform destinationPointRound2; // Punto de destino después de ganar la batalla
    public Transform destinationPointRound3; // Punto de destino después de ganar la batalla
    public float movementSpeed = 2f; // Velocidad de movimiento de los zombies
    public bool isGameStartet = false;

    private bool attackZombieObject = true;
    private Coroutine attackCoroutine;
    private bool attacking = false;

    private string zombie, zombieWorker, enemy;
    private int round = 1;
    private int count = 0;

    private bool shouldContinue = true; // Bandera para controlar el bucle

    public MoveSpawnAndCamera moveObjectss;
    public MoveSpawnAndCamera moveZombiesRun;

    private List<Character> characters = new List<Character>(); // Lista de personajes

    private bool canAttack = true;

    private StateGame stateGame;

    private void Start()
    {
        stateGame = FindAnyObjectByType<StateGame>();
    }

    public void OnButtonClick()
    {
        StartGame("Zombie", "ZombieWorker", "Enemy", 1);
    }

    private void OnEnable()
    {
        SelectCharacters.OnCharacterSpawned += HandleCharacterSpawned;
    }

    private void OnDisable()
    {
        SelectCharacters.OnCharacterSpawned -= HandleCharacterSpawned;
    }

    private void Update()
    {
        if (isGameStartet)
        {
            

            if (zombiesObjects.Length > 0 && enemiesObjects.Length < 1) {
                StartCoroutine(WaitToAttack());
                switch (round)
                {
                    case 1:
                        count = 0;
                        StartCoroutine(MoveZombiesToDestination(destinationPointRound2, 2));
                        break;
                    case 2:
                        count = 0;
                        StartCoroutine(MoveZombiesToDestination(destinationPointRound3, 3));
                        break;
                    case 3:
                        win.SetActive(true);
                        isGameStartet = false;
                        round = 1;
                        this.zombie = "Zombie";
                        this.zombieWorker = "ZombieWorker";
                        this.enemy = "Enemy";
                        //SelectCharacters.OnCharacterSpawned-= HandleCharacterSpawned;
                        stateGame.isGameStarted = false;
                        break;
                    default:
                        Console.WriteLine("Opción no reconocida");
                        break;
                }
            }

            if (enemiesObjects.Length > 0 && zombiesObjects.Length < 1)
            {
                if (lost != null)
                {
                    lost.SetActive(true);
                    isGameStartet = false;
                    round = 1;
                    this.zombie = "Zombie";
                    this.zombieWorker = "ZombieWorker";
                    this.enemy = "Enemy";
                    //SelectCharacters.OnCharacterSpawned -= HandleCharacterSpawned;
                    stateGame.isGameStarted = false;
                }
            }
            if (!attacking && enemiesObjects.Length > 0 && zombiesObjects.Length > 0 && canAttack)
            {
                enemiesObjects = CharacterManager.Instance.GetEnemies();
                zombiesObjects = CharacterManager.Instance.GetZombies();
                StartCoroutine(PerformAttacks());
            }


        }
    }

    public void RestartController()
    {
        round = 1;
    }

    private IEnumerator WaitToAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(8f);
        canAttack = true;
    }

    private IEnumerator MoveZombiesToDestination(Transform destinationPoint, int round)
    {

        // Obtener la posición de destino
        Vector3 destination = destinationPoint.position;


        yield return new WaitForSeconds(4f); // Esperar al siguiente frame
        moveObjectss.MoveObjects();
        moveZombiesRun.MoveObjects();

        count++;

        if (round == 2 && count == 1) {
            StartGame("Zombie", "ZombieWorker", "Enemy2", 2);
        }

        if (round == 3 && count == 1)
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
                randomEnemy.hasBrains = 20; 
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

    private IEnumerator PerformAttacks()
    {
        if (enemiesObjects.Length > 0 && zombiesObjects.Length > 0)
        {
            enemiesObjects = CharacterManager.Instance.GetEnemies();
            zombiesObjects = CharacterManager.Instance.GetZombies();

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
                GameObject[] zombies = zombiesObjects.Where(zombie => zombie.GetComponent<Zombie>() != null).ToArray();
                if (zombies.Length > 0)
                {
                    zombiesIndex = Random.Range(0, zombies.Length);
                    zombieObject = zombies[zombiesIndex];
                    zombieCharacter = zombieObject.GetComponent<Character>();
                    zombieCharacter.Attack(enemyObject);
                }
                attackZombieObject = false;
            }
            else
            {
                // Realizar el ataque
                enemyCharacter.Attack(zombieObject);
                attackZombieObject = true;
            }

            yield return new WaitForSeconds(5f); // Esperar antes de continuar con el próximo ataque
            attacking = false;
        }
    }
    /*public void ResetSceneFromMainMenu()
    {
        // Reinicia la escena actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }*/
}