using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectCharacters : MonoBehaviour
{
    [SerializeField]private int brains;
    public int characterBrains1 = 10;
    public int characterBrains2 = 15;
    public int characterBrains3 = 20;
    public int characterBrains4 = 25;

    public GameObject prefabToSpawn1;
    public GameObject prefabToSpawn2;
    public GameObject prefabToSpawn3;
    public GameObject prefabToSpawn4;

    public TextMeshProUGUI[] brainsTextArray;

    private List<GameObject> spawnedCharacters1 = new List<GameObject>();
    private List<GameObject> spawnedCharacters2 = new List<GameObject>();
    private List<GameObject> spawnedCharacters3 = new List<GameObject>();
    private List<GameObject> spawnedCharacters4 = new List<GameObject>();

    private void Start()
    {
        brains = CountBrains.Instance.currentBrains;
        UpdateBrainsText();
    }
    private void UpdateBrainsText()
    {
        string brainsString = brains.ToString();
        foreach (TextMeshProUGUI textMeshProUGUI in brainsTextArray)
        {
            textMeshProUGUI.text = brainsString;
        }
    }
    public void SpawnCharacter1()
    {
        if (brains >= characterBrains1)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject newCharacter = Instantiate(prefabToSpawn1, randomSpawnPosition, Quaternion.identity);
            spawnedCharacters1.Add(newCharacter);
            brains -= characterBrains1;
            UpdateBrainsText();
        }
    }

    public void SpawnCharacter2()
    {
        if (brains >= characterBrains2)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject newCharacter = Instantiate(prefabToSpawn2, randomSpawnPosition, Quaternion.identity);
            spawnedCharacters2.Add(newCharacter);
            brains -= characterBrains2;
            UpdateBrainsText();
        }
    }

    public void SpawnCharacter3()
    {
        if (brains >= characterBrains3)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            GameObject newCharacter = Instantiate(prefabToSpawn3, randomSpawnPosition, Quaternion.identity);
            spawnedCharacters3.Add(newCharacter);
            brains -= characterBrains3;
            UpdateBrainsText();
        }
    }

    public void SpawnCharacter4()
    {
        if (brains >= characterBrains4)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-0.4f, -3.5f), 1.65f, Random.Range(-3.4f, 1.8f));
            GameObject newCharacter = Instantiate(prefabToSpawn4, randomSpawnPosition, Quaternion.identity);
            spawnedCharacters4.Add(newCharacter);
            brains -= characterBrains4;
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
            brains += characterBrains1;
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
            brains += characterBrains2;
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
            brains += characterBrains3;
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
            brains += characterBrains4;
            UpdateBrainsText();
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-0.4f, -3.5f), 0.15f, Random.Range(-3.4f, 1.8f));
    }
}




/*
private int brains;
public int characterBrains1 = 10;
public int characterBrains2 = 15;
public int characterBrains3 = 20;
public int characterBrains4 = 25;

public GameObject prefabToSpawn1;
public GameObject prefabToSpawn2;
public GameObject prefabToSpawn3;
public GameObject prefabToSpawn4;

private List<GameObject> spawnedCharacters1 = new List<GameObject>();
private List<GameObject> spawnedCharacters2 = new List<GameObject>();
private List<GameObject> spawnedCharacters3 = new List<GameObject>();
private List<GameObject> spawnedCharacters4 = new List<GameObject>();

private CalculateBrains calculateBrains;

private void Start()
{
    calculateBrains = CalculateBrains.Instance;
    brains = calculateBrains.brains;
}

public void SpawnCharacter1()
{
    if (brains >= characterBrains1)
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn1, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters1.Add(newCharacter);
        calculateBrains.UpdateBrains(-characterBrains1);
    }
}

public void SpawnCharacter2()
{
    if (brains >= characterBrains2)
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn2, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters2.Add(newCharacter);
        calculateBrains.UpdateBrains(-characterBrains2);
    }
}

public void SpawnCharacter3()
{
    if (brains >= characterBrains3)
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        GameObject newCharacter = Instantiate(prefabToSpawn3, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters3.Add(newCharacter);
        calculateBrains.UpdateBrains(-characterBrains3);
    }
}

public void SpawnCharacter4()
{
    if (brains >= characterBrains4)
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-0.4f, -3.5f), 1.65f, Random.Range(-3.4f, 1.8f));
        GameObject newCharacter = Instantiate(prefabToSpawn4, randomSpawnPosition, Quaternion.identity);
        spawnedCharacters4.Add(newCharacter);
        calculateBrains.UpdateBrains(-characterBrains4);
    }
}

public void DestroyCharacter1()
{
    if (spawnedCharacters1.Count > 0)
    {
        GameObject characterToRemove = spawnedCharacters1[spawnedCharacters1.Count - 1];
        spawnedCharacters1.Remove(characterToRemove);
        Destroy(characterToRemove);
        calculateBrains.UpdateBrains(characterBrains1);
    }
}

public void DestroyCharacter2()
{
    if (spawnedCharacters2.Count > 0)
    {
        GameObject characterToRemove = spawnedCharacters2[spawnedCharacters2.Count - 1];
        spawnedCharacters2.Remove(characterToRemove);
        Destroy(characterToRemove);
        calculateBrains.UpdateBrains(characterBrains2);
    }
}

public void DestroyCharacter3()
{
    if (spawnedCharacters3.Count > 0)
    {
        GameObject characterToRemove = spawnedCharacters3[spawnedCharacters3.Count - 1];
        spawnedCharacters3.Remove(characterToRemove);
        Destroy(characterToRemove);
        calculateBrains.UpdateBrains(characterBrains3);
    }
}
public void DestroyCharacter4()
{
    if (spawnedCharacters4.Count > 0)
    {
        GameObject characterToRemove = spawnedCharacters4[spawnedCharacters4.Count - 1];
        spawnedCharacters4.Remove(characterToRemove);
        Destroy(characterToRemove);
        calculateBrains.UpdateBrains(characterBrains4);
    }
}
private Vector3 GetRandomSpawnPosition()
{
    return new Vector3(Random.Range(-0.4f, -3.5f), 0.15f, Random.Range(-3.4f, 1.8f));
}    
}
*/