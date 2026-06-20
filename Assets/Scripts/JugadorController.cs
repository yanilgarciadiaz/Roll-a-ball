using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class JugadorController : MonoBehaviour
{
    //Declarlo la variable de tipo RigidBody que luego asociaremos a nuestro Jugador
    private Rigidbody rb;
    //Declaro la variable pública velocidad para poder modificarla desde la Inspector window
    public float velocidad;
    //Inicializo el contador de coleccionables recogidos
    private int contador;
    //Inicializo variables para los textos
    public Text TextoContador, TextoGanar;

    // Timer 60segs
    public Text TextoTiempo;
    public float tiempoRestante = 60f;
    private bool juegoTerminado = false;
    private int totalColeccionables;

    // Use this for initialization
    void Start()
    {
        //Capturo esa variable al iniciar el juego
        rb = GetComponent<Rigidbody>();
        totalColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length;

        //Inicio el contador a 0
        contador = 0;
        //Actualizo el texto del contador por pimera vez
        setTextoContador();
        //Inicio el texto de ganar a vacío
        TextoGanar.text = "";
    }
   
    // Para que se sincronice con los frames de física del motor
    void FixedUpdate()
    {
        if (juegoTerminado)
            return;

        //Estas variables nos capturan el movimiento en horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
       
        //Un vector 3 es un trío de posiciones en el espacio XYZ, en este caso el que corresponde al movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        //Asigno ese movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento * velocidad);
    }

    void Update()
    {
        if (juegoTerminado)
            return;

        tiempoRestante -= Time.deltaTime;

        TextoTiempo.text = "Tiempo: " + Mathf.Ceil(tiempoRestante);

        if (tiempoRestante <= 0)
        {
            juegoTerminado = true;
            TextoGanar.text = "¡Perdiste!";
            Invoke("ReiniciarNivel", 3f);
        }
    }

    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            //Incremento el contador en uno (también se peude hacer como contador++)
            contador = contador + 1;
            //Actualizo elt exto del contador
            setTextoContador();
        }
    }

    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador()
    {
        TextoContador.text = "Contador: " + contador.ToString();
        if (contador >= totalColeccionables)
        {
            juegoTerminado = true;
            TextoGanar.text = "¡Ganaste!";

            string escenaActual = SceneManager.GetActiveScene().name;
            if (escenaActual == "Nivel6") {
                Invoke("RegresarAlMenu", 5f);
            }
            else {
                Invoke("SiguienteNivel", 2.5f);
            }
        }
    }

    void RegresarAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void SiguienteNivel()
    {
        int siguienteIndice = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(siguienteIndice);
    }

    void ReiniciarNivel()
    {
        string nivelActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nivelActual);
    }
}
