using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableLevel2 : MonoBehaviour
{
    public Button boton;
    public TextMeshProUGUI textoMeshPro;

    private void Start()
    {
        boton.interactable = KeepEnabledLevel2.botonHabilitado;
        textoMeshPro.enabled = KeepEnabledLevel2.textMeshProHabilitado;
    }
}
