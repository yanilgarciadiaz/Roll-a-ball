using UnityEngine;

public class ObstaculoControlador : MonoBehaviour
{
    public float velocidad = 3f;
    public float distancia = 5f;
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float movimiento = Mathf.PingPong(Time.time * velocidad, distancia);
        transform.position = posicionInicial + new Vector3(movimiento - (distancia / 2f), 0, 0);
    }
}