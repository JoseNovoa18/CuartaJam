using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCharacter : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private Vector3 initialPosition;
    private GameObject characterInstance;

    public GameObject characterPrefab; // El prefab del personaje

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        initialPosition = transform.position;

        // Instanciar el personaje en la escena
        characterInstance = Instantiate(characterPrefab, initialPosition, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && characterInstance != null)
        {
            characterInstance.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
            characterInstance.transform.position = new Vector3(characterInstance.transform.position.x, 0f, characterInstance.transform.position.z);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // Agrega aquí la lógica adicional que desees ejecutar al soltar el personaje.

        // Destruir la instancia del personaje al soltarlo
        if (characterInstance != null)
        {
            Destroy(characterInstance);
        }
    }
}
