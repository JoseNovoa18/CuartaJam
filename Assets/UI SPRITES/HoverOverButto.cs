using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOverButto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject boton;

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        boton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        boton.SetActive(false);
    }

}
