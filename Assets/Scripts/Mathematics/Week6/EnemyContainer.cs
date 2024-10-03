using UnityEngine;
public class EnemyContainer : MonoBehaviour
{
    public GameObject Prefab;
    private void Start()
    {
        Invoke("GenerarEnemigos", 4);
    }
    private void GenerarEnemigos()
    {
        Vector3 position = new Vector3(Random.Range(-5.94f, 5.94f), Random.Range(-2.16f,3.1f),transform.position.z);
        Instantiate(Prefab,position,transform.rotation);
        Invoke("GenerarEnemigos", 4);
    }
}
