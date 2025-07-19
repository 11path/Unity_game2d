using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
   public void Jugar()
    {
        SceneManager.LoadScene("Juego"); // Reemplaza "Juego" por el nombre exacto de tu escena principal
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

}
