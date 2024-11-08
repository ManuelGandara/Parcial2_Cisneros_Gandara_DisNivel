using System.Collections;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    public Light lightOB;
    public AudioSource lightSound;
    public float minTime = 0.1f;
    public float maxTime = 1.0f;

    private Coroutine flickerCoroutine;

    void Start()
    {
        // Comienza la corrutina para el parpadeo de la luz
        flickerCoroutine = StartCoroutine(FlickerLight());

        // Configura el audio para que se reproduzca en bucle de fondo
        if (lightSound != null)
        {
            lightSound.loop = true;
            lightSound.Play();
        }
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Alterna el estado de la luz
            lightOB.enabled = !lightOB.enabled;

            // Espera un tiempo aleatorio antes de cambiar el estado nuevamente
            float flickerTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(flickerTime);
        }
    }

    void OnDisable()
    {
        // Detiene la corrutina si el objeto se desactiva
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
        }

        // Detiene el audio cuando el objeto se desactiva
        if (lightSound != null)
        {
            lightSound.Stop();
        }
    }
}


