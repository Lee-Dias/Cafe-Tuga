using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject m_Source;

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        if (audioClip == null) return;

        // Cria a cópia do AudioSource
        GameObject ass = Instantiate(m_Source, transform.position, Quaternion.identity);

        ass.GetComponent<AudioSource>().volume = volume;
        ass.GetComponent<AudioSource>().clip = audioClip;
        ass.GetComponent<AudioSource>().Play();

        // Inicia a rotina para destruir o objeto após a duração do som
        StartCoroutine(DestroyAfterFinished(ass, audioClip.length));
    }

    private IEnumerator DestroyAfterFinished(GameObject source, float duration)
    {
        // Espera o tempo exato da duração do áudio
        yield return new WaitForSeconds(duration);

        // Destrói o GameObject que foi instanciado
        if (source != null)
        {
            Destroy(source.gameObject);
        }
    }
}