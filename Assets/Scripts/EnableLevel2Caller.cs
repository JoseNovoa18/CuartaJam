using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLevel2Caller : MonoBehaviour
{
    public void HabilitarBotonEnEscena3()
    {
        KeepEnabledLevel2.botonHabilitado = true;
        KeepEnabledLevel2.textMeshProHabilitado = false;
    }
}
