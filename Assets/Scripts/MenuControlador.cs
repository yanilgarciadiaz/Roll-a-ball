using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlador : MonoBehaviour
{
    // Función para ir a la escena del juego (Nivel 1)
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }

    // Función para ir a la escena de opciones
    public void IrAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    // Función para regresar al Menú
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // cierra la aplicación por completo
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}