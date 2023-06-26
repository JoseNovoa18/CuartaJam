using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackController2 : MonoBehaviour
{
    public GameObject[] enemiesObjects; // Array of zombies
    public GameObject[] zombiesObjects; // Array of enemies
    public Button startGame;

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
            // Obtener los objetos de los enemigos y zombies nuevamente
            //enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");
            //zombiesObjects = GameObject.FindGameObjectsWithTag("Zombie");

            // Seleccionar un objeto de ataque y un objetivo al azar
            int attackerIndex = Random.Range(0, characters.Count);
            int targetIndex = Random.Range(0, characters.Count);

            Character attacker = characters[attackerIndex];
            Character target = characters[targetIndex];

            // Realizar el ataque
            attacker.Attack(target.gameObject);

            yield return new WaitForSeconds(1f); // Esperar antes de continuar con el próximo ataque
        }
    }
}