using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnabledFightButtom : MonoBehaviour
{
    public SelectCharacters selectCharacters;
         
    public GameObject fightbuttom;
    public GameObject fightText;

    public void Update()
    {
        showFightButtom();
    }

    public void showFightButtom()
    {
        if (selectCharacters.thereIsAtLeastOneCharacter)
        {
            fightbuttom.SetActive(true);
            fightText.SetActive(true);
        }
        else
        {
            fightbuttom.SetActive(false);
            fightText.SetActive(false);
        }
    }
}
