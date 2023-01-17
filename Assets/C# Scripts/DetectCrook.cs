using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCrook : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    private Coroutine _coroutine;

    private void Start()
    {
        _audio.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FadeAudioVolumeTo(1, 3));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(_coroutine);
        StartCoroutine(FadeAudioVolumeTo(0, 3));
    }

    private IEnumerator FadeAudioVolumeTo(float targetVolume, float fadeTime = 1f)
    {
        if (targetVolume > 0 && _audio.isPlaying == false)
        {
            _audio.Play();
        }

        float volume = _audio.volume;
        float target = targetVolume;
        float stepTime = 1f / fadeTime;

        while (volume != target)
        {
            volume = Mathf.MoveTowards(volume, target, stepTime * Time.deltaTime);
            _audio.volume = volume;
            yield return null;
        }

        if (target == 0)
        {
            _audio.Stop();
        }
    }
}
