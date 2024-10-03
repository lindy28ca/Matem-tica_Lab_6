using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class TextoPuntos : MonoBehaviour
{
    TMP_Text textito;
    int puntitos = 0;
    bool corutina = true;
    private void Awake()
    {
        textito=GetComponent<TMP_Text>();
    }
    private void Start()
    {
        StartCoroutine(AumentarPuntosConstante());
    }
    private IEnumerator AumentarPuntosConstante()
    {
        while (corutina==true)
        {
            puntitos++;
            textito.text = "Puntos: " + puntitos;
            yield return new WaitForSeconds(1f);
        }
    }
}
