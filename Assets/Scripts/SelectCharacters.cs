using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SelectCharacters : MonoBehaviour
{
    private int originalBrains;
    public int characterBrains1 = 10;
    public int characterBrains2 = 15;
    public int characterBrains3 = 20;
    public int characterBrains4 = 25;

    public GameObject prefabToSpawn1;
    public GameObject prefabToSpawn2;
    public GameObject prefabToSpawn3;
    public GameObject prefabToSpawn4;

    public GameObject gameObjectReference;

    public TextMeshProUGUI[] brainsTextArray;

    public static event Action<GameObject> OnCharacterSpawned;

    private List<GameObject> spawnedCharacters1 = new List<GameObject>();
    private List<GameObject> spawnedCharacters2 = new List<GameObject>();
    private List<GameObject> spawnedCharacters3 = new List<GameObject>();
    private List<GameObject> spawnedCharacters4 = new List<GameObject>();

    private void Start()
    {
        originalBrains = CountBrains.Instance.Brainss;
        UpdateBrainsText();
    }
    private void UpdateBrainsText()
    {
        string brainsString = CountBrains.Instance.Brainss.ToString();
        foreach (TextMeshProUGUI textMeshProUGUI in brainsTextArray)
        {
            textMeshProUGUI.text = brainsString;
        }
    }

    public void SpawnCharacter1()
    {
        if (CountBrains.Instance.Brainss >= characterBrains1)
        {
            Vector3 reference = gameObjectReference.transform.position;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-1f, -2f));
            GameObject newCharacter = Instantiate(prefabToSpawn1, reference + randomSpawnPosition, Quaternion.Euler(0f, 180f, 0f));
            spawnedCharacters1.Add(newCharacter);
            CountBrains.Instance.Brainss -= characterBrains1;
            UpdateBrainsText();

            // Disparar el evento para notificar la adici�n del nuevo personaje
            if (OnCharacterSpawned != null)
            {
                OnCharacterSpawned.Invoke(newCharacter);
            }
        }
    }

    public void SpawnCharacter2()
    {
        if (CountBrains.Instance.Brainss >= characterBrains2)
        {
            Vector3 reference = gameObjectReference.transform.position;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(3f, 2f));
            GameObject newCharacter = Instantiate(prefabToSpawn2, reference + randomSpawnPosition, Quaternion.Euler(0f, 180f, 0f));
            spawnedCharacters2.Add(newCharacter);
            CountBrains.Instance.Brainss -= characterBrains2;
            UpdateBrainsText();
        }
    }

    public void SpawnCharacter3()
    {
        if (CountBrains.Instance.Brainss >= characterBrains3)
        {
            Vector3 reference = gameObjectReference.transform.position;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(1f, 0f));
            GameObject newCharacter = Instantiate(prefabToSpawn3, reference + randomSpawnPosition, Quaternion.Euler(0f, 180f, 0f));
            spawnedCharacters3.Add(newCharacter);
            CountBrains.Instance.Brainss -= characterBrains3;
            UpdateBrainsText();
        }
    }

    public void SpawnCharacter4()
    {
        if (CountBrains.Instance.Brainss >= characterBrains4)
        {
            Vector3 reference = gameObjectReference.transform.position;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-3f, -4f));
            GameObject newCharacter = Instantiate(prefabToSpawn4, reference + randomSpawnPosition, Quaternion.Euler(0f, 180f, 0f));
            spawnedCharacters4.Add(newCharacter);
            CountBrains.Instance.Brainss -= characterBrains4;
            UpdateBrainsText();
        }
    }

    public void DestroyCharacter1()
    {
        if (spawnedCharacters1.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters1[spawnedCharacters1.Count - 1];
            spawnedCharacters1.Remove(characterToRemove);
            Destroy(characterToRemove);
            CountBrains.Instance.Brainss += characterBrains1;
            UpdateBrainsText();
        }
    }

    public void DestroyCharacter2()
    {
        if (spawnedCharacters2.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters2[spawnedCharacters2.Count - 1];
            spawnedCharacters2.Remove(characterToRemove);
            Destroy(characterToRemove);
            CountBrains.Instance.Brainss += characterBrains2;
            UpdateBrainsText();
        }
    }

    public void DestroyCharacter3()
    {
        if (spawnedCharacters3.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters3[spawnedCharacters3.Count - 1];
            spawnedCharacters3.Remove(characterToRemove);
            Destroy(characterToRemove);
            CountBrains.Instance.Brainss += characterBrains3;
            UpdateBrainsText();
        }
    }

    public void DestroyCharacter4()
    {
        if (spawnedCharacters4.Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters4[spawnedCharacters4.Count - 1];
            spawnedCharacters4.Remove(characterToRemove);
            Destroy(characterToRemove);
            CountBrains.Instance.Brainss += characterBrains4;
            UpdateBrainsText();
        }
    }

    public void BackToSelectLevelWithoutSaving()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("SelectLevels");
    }
}

// Intento m�s cercano para hacer el codigo m�s corto
/*
public class SelectCharacters : MonoBehaviour
{
    private int originalBrains;
    public int[] characterBrains = { 10, 15, 20, 25 };
    public GameObject[] prefabToSpawn;
    public GameObject gameObjectReference;
    public TextMeshProUGUI[] brainsTextArray;
    private List<GameObject>[] spawnedCharacters = new List<GameObject>[4];

    private void Start()
    {
        originalBrains = CountBrains.Instance.Brainss;
        UpdateBrainsText();
    }

    private void UpdateBrainsText()
    {
        string brainsString = CountBrains.Instance.Brainss.ToString();
        foreach (TextMeshProUGUI textMeshProUGUI in brainsTextArray)
        {
            textMeshProUGUI.text = brainsString;
        }
    }

    private Vector3 GetRandomSpawnPosition(int characterIndex)
    {
        Vector3 reference = gameObjectReference.transform.position;

        switch (characterIndex)
        {
            case 0:
                return reference + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-1f, -2f));
            case 1:
                return reference + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(3f, 2f));
            case 2:
                return reference + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(1f, 0f));
            case 3:
                return reference + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-3f, -4f));
            default:
                return Vector3.zero;
        }
    }

    public void SpawnCharacter(int characterIndex)
    {
        if (CountBrains.Instance.Brainss >= characterBrains[characterIndex])
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition(characterIndex);
            GameObject newCharacter = Instantiate(prefabToSpawn[characterIndex], randomSpawnPosition, Quaternion.Euler(0f, 180f, 0f));
            spawnedCharacters[characterIndex].Add(newCharacter);
            CountBrains.Instance.Brainss -= characterBrains[characterIndex];
            UpdateBrainsText();
        }
    }

    public void DestroyCharacter(int characterIndex)
    {
        if (spawnedCharacters[characterIndex].Count > 0)
        {
            GameObject characterToRemove = spawnedCharacters[characterIndex][spawnedCharacters[characterIndex].Count - 1];
            spawnedCharacters[characterIndex].Remove(characterToRemove);
            Destroy(characterToRemove);
            CountBrains.Instance.Brainss += characterBrains[characterIndex];
            UpdateBrainsText();
        }
    }

    public void RegresarAEscenaPrincipalSinGuardar()
    {
        CountBrains.Instance.RestoreOriginalBrains(); // Restaurar el valor original de cerebros
        SceneManager.LoadScene("EscenaPrincipal");
    }
}
*/