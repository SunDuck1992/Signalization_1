using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _growthTime;
    [SerializeField] private Detect _detect;

    private Coroutine _coroutine;
    private float _targetVolume;

    private void Awake()
    {
        _audio.volume = 0f;
    }

    private void OnEnable()
    {
        _detect.Entered += StartAlarm;
        _detect.CameOut += StopAlarm;
    }

    private void OnDisable()
    {
        _detect.Entered -= StartAlarm;
        _detect.CameOut -= StopAlarm;
    }
    
    private void StartAlarm()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _targetVolume = 1f;

        _coroutine = StartCoroutine(FadeAudioVolumeTo());
    }

    private void StopAlarm()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _targetVolume = 0f;

        _coroutine = StartCoroutine(FadeAudioVolumeTo());
    }

    private IEnumerator FadeAudioVolumeTo()
    {
        if (_audio.isPlaying == false)
        {
            _audio.Play();
        }
        
        float stepTime = 1 / _growthTime;

        while (_audio.volume != _targetVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, _targetVolume, stepTime * Time.deltaTime);
            yield return null;
        }

        if (_targetVolume == 0)
        {
            _audio.Stop();
        }
    }
}
