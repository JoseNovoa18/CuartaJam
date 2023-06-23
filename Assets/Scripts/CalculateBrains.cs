using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CalculateBrains : MonoBehaviour
{
    /*
    public static CalculateBrains Instance { get; private set; }
    public int currentBrains = 150;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateBrainsText();
    }

    private void UpdateBrainsText()
    {
        textMesh.text = currentBrains.ToString();
    }
}
    */
    /*
        public static CalculateBrains Instance { get; private set; }

        public int brains = 100;
        public UnityEvent<int> onBrainsUpdated;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void UpdateBrains(int value)
        {
            brains += value;
            onBrainsUpdated?.Invoke(brains);
        }

    */
}