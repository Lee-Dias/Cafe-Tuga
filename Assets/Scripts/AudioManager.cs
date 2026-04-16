
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
        Destroy(ass.gameObject, audioClip.length);
    }
}