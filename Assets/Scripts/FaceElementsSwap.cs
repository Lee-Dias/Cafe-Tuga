using System.Collections;
using UnityEngine;

public class FaceElementsSwap : MonoBehaviour
{
    [SerializeField] private Renderer leftEye;
    [SerializeField] private Renderer rightEye;
    [SerializeField] private Renderer mouth;

    [SerializeField] private Texture eyesOpen;
    [SerializeField] private Texture eyesClosed;
    [SerializeField] private Texture mouthOpen;
    [SerializeField] private Texture mouthClosed;

    [SerializeField, Range(0.05f, 0.5f)] private float eyesClosedDuration = 0.08f;
    [SerializeField, Range(0.05f, 0.5f)] private float eyesOpenDuration = 3.0f;
    [SerializeField, Range(0.1f, 0.5f)] private float speachSpeed = 0.15f;

    private Coroutine speakingCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetEyes(eyesOpen);
        StartCoroutine(BlinkRoutine());

        SetMouth(mouthClosed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartSpeaking();

        if (Input.GetKeyUp(KeyCode.Space))
            StopSpeaking();
    }

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            SetEyes(eyesClosed);
            yield return new WaitForSeconds(0.08f);
            SetEyes(eyesOpen);
            yield return new WaitForSeconds(3f);
        }
    }

    void SetEyes(Texture tex)
    {
        leftEye.material.mainTexture = tex;
        rightEye.material.mainTexture = tex;
    }

    IEnumerator SpeakRoutine()
    {
        while(true)
        {
            SetMouth(mouthOpen);
            yield return new WaitForSeconds(speachSpeed);
            SetMouth(mouthClosed);
            yield return new WaitForSeconds(speachSpeed);
        }
    }

    void SetMouth(Texture tex)
    {
        mouth.material.mainTexture = tex;
    }

    void StartSpeaking()
    {
        if (speakingCoroutine == null)
            speakingCoroutine = StartCoroutine(SpeakRoutine());
    }

    void StopSpeaking()
    {
        if (speakingCoroutine != null)
        {
            StopCoroutine(speakingCoroutine);
            speakingCoroutine = null;
        }

        SetMouth(mouthClosed);
    }

}
