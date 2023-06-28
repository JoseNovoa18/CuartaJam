using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountBrains : MonoBehaviour
{
    private static CountBrains instance;

    public static CountBrains Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CountBrains>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private int brains = 150;
    private int originalBrains; // Variable para almacenar el valor original de brains

    public int Brainss
    {
        get { return brains; }
        set { brains = value; }
    }

    private void Awake()
    {
        originalBrains = brains; // Guardar el valor original de brains
    }

    public void SaveBrains()
    {
        originalBrains = brains; // Actualizar el valor original de brains
    }

    public void RestoreOriginalBrains()
    {
        brains = originalBrains; // Restaurar el valor original de brains
    }


    /*
    private static CountBrains _instance;
    public static CountBrains Instance { get { return _instance; } }

    public int currentBrains = 150;
    //public TextMeshProUGUI currentBrainsText;
    public TextMeshProUGUI[] currentBrainsTextArray;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        string brainsString = currentBrains.ToString();
        foreach (TextMeshProUGUI textMeshProUGUI in currentBrainsTextArray)
        {
            textMeshProUGUI.text = brainsString;
        }
    /*
        currentBrainsText.text = currentBrains.ToString();
    
    }
    */
}
