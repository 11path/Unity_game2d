using UnityEngine;
using TMPro;

public class VidaDisplay : MonoBehaviour
{
    public TextMeshProUGUI vidaTMP;

    public void ActualizarVida(int actual, int max)
    {
        vidaTMP.text = "Vida: " + actual + " / " + max;
    }
}
