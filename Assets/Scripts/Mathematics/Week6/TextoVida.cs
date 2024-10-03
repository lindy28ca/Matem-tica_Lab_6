using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextoVida : MonoBehaviour
{
    TMP_Text textito;
    private void Awake()
    {
        textito = GetComponent<TMP_Text>();
    }
    public void Imprimir(int vida)
    {
        textito.text="Vida: "+vida;
    }
}
