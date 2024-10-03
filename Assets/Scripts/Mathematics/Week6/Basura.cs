using UnityEngine;

public class Basura : MonoBehaviour
{
    Rigidbody basura;
    public float velocidad;
    private void Awake()
    {
        basura = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        basura.velocity = new Vector3(basura.velocity.x, basura.velocity.y, velocidad * -1);
    }
}
