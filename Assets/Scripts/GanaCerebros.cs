using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GanaCerebros : MonoBehaviour
{
    public TextMeshProUGUI brainsText;
    private int originalBrains;

    private void Start()
    {
        originalBrains = CountBrains.Instance.Brainss;
        UpdateBrainsText();
    }

    private void UpdateBrainsText()
    {
        brainsText.text = CountBrains.Instance.Brainss.ToString();
    }

    public void AddBrains()
    {
        CountBrains.Instance.Brainss += 15;
        UpdateBrainsText();
    }
}
