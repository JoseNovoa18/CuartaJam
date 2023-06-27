using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController2 : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    public Button startGame;
    private bool attackZombieObject = true;

    private bool shouldContinue = true; // Bandera para controlar el bucle

    private List<Character> characters = new List<Character>(); // Lista de personajes

    public void OnButtonClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        CharacterManager.Instance.AddCharacter<Enemy>();
        CharacterManager.Instance.AddCharacter<Zombie>();

        // Obtener los objetos de los enemigos y zombies
        enemiesObjects = CharacterManager.Instance.GetEnemies();
        zombiesObjects = CharacterManager.Instance.GetZombies();

        

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

    private void HandleObjectDestroyed(GameObject destroyedObject)
    {
        // Si se destruye un objeto, se detiene el bucle
        shouldContinue = false;
        enemiesObjects = CharacterManager.Instance.GetEnemies();
        zombiesObjects = CharacterManager.Instance.GetZombies();
        shouldContinue = true;
        StartCoroutine(WaitAndRestartAttacks());
    }

    private IEnumerator WaitAndRestartAttacks()
    {
        yield return new WaitForSeconds(12f); // Esperar dos segundos

        StartCoroutine(PerformAttacks());
    }

    private IEnumerator PerformAttacks()
    {
        Debug.Log("volvio"); 
        while (shouldContinue && zombiesObjects.Length > 0 && enemiesObjects.Length > 0)
        {
            int zombiesIndex = Random.Range(0, zombiesObjects.Length - 1);
            int enemiesIndex = Random.Range(0, enemiesObjects.Length - 1);

            Character zombieCharacter = zombiesObjects[zombiesIndex].GetComponent<Character>();
            Character enemyCharacter = enemiesObjects[enemiesIndex].GetComponent<Character>();

            GameObject zombieObject = zombieCharacter.gameObject;
            GameObject enemyObject = enemyCharacter.gameObject;

            Debug.Log("zombieObject "+ zombieObject.name);
            Debug.Log("enemyObject " + enemyObject.name);

            if (attackZombieObject)
            {
                // Realizar el ataque
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