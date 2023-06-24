using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountBrains : MonoBehaviour
{
    private static CountBrains _instance;
    public static CountBrains Instance { get { return _instance; } }

    public int currentBrains = 150;
    //public TextMeshProUGUI currentBrainsText;

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
    /*
    private void Update()
    {
        currentBrainsText.text = currentBrains.ToString();
    }
    */
}
