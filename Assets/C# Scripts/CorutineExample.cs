using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorutineExample : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _duration;

    private float _targetVolume = 1f;
    private float _runningTime;
    private float _volumeScale;

    private void Start()
    {
        _audio.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audio.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _runningTime += Time.deltaTime;
        _volumeScale = _runningTime / _duration;

        //if (_audio.volume == 1f)
        //{
        //    _targetVolume = 0f;
        //    _runningTime = 0;
        //}
        if (_audio.volume == 0f)
        {
            _targetVolume = 1f;
            _runningTime = 0;
        }

        _audio.volume = Mathf.MoveTowards(_audio.volume, _targetVolume, _volumeScale);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (_audio.volume == 1f)
        //{
        //    _targetVolume = 0f;
        //    _runningTime = 0;
        //}
        //_audio.volume = Mathf.MoveTowards(_audio.volume, _targetVolume, _volumeScale);
        StartCoroutine(FadeAudioVolumeTo(0, 3));
        //_audio.Stop();
    }

    private IEnumerator LowVolume()
    {
        for (int i = 0; i < 100; i++)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _targetVolume, _volumeScale);
            yield return null;
        }
    }

    private IEnumerator FadeAudioVolumeTo(float targetVolume, float fadeTime = 1f)
    {
        // если громкость будет не нулевой, то нужно включить звук, если он ещё не играет
        if (targetVolume > 0 && _audio.isPlaying == false)
        {
            _audio.Play();
        }

        float volume = _audio.volume;
        float target = targetVolume;

        // скорость изменения громкости за кадр
        float stepTime = 1f / fadeTime;

        // если плавность затухания громкости не задана
        //if (fadeTime <= 0)
        //    volume = targetVolume;

        // изменение громкости от текущей до целевой
        while (volume != target)
        {
            volume = Mathf.MoveTowards(volume, target, stepTime * Time.deltaTime);
            _audio.volume = volume;
            yield return null;
        }

        // если целевая громкость была нулевой, то есть смысл потом выключить звук
        if (target == 0)
        {
            _audio.Stop();
        }
    }
}
