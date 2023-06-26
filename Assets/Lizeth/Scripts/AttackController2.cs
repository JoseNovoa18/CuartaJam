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

    private List<Character> characters = new List<Character>(); // Lista de personajes


    public void OnButtonClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        // Obtener los objetos de los enemigos y zombies
        enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");
        zombiesObjects = GameObject.FindGameObjectsWithTag("Zombie");

        // Crear instancias de los personajes y agregarlos a la lista
        foreach (var enemyObject in enemiesObjects)
        {
            Character enemy = enemyObject.GetComponent<Character>();
            if (enemy != null)
            {
                characters.Add(enemy);
            }
        }

        foreach (var zombieObject in zombiesObjects)
        {
            Character zombie = zombieObject.GetComponent<Character>();
            if (zombie != null)
            {
                characters.Add(zombie);
            }
        }

        StartCoroutine(PerformAttacks());
    }

    private IEnumerator PerformAttacks()
    {
        while (zombiesObjects.Length > 0 && enemiesObjects.Length > 0)
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

            yield return new WaitForSeconds(8f); // Esperar antes de continuar con el próximo ataque
        }
    }
}