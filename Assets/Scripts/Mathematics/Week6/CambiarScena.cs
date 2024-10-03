using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarScena : MonoBehaviour
{
    public string nombre;
    public void Scena()
    {
        SceneManager.LoadScene(nombre);
    }
}
