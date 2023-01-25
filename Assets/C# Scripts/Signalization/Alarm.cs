using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _growthTime;
    [SerializeField] private AlarmControl _alarmControl;

    private Coroutine _coroutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Awake()
    {
        _audio.volume = 0f;
    }

    private void OnEnable()
    {
        _alarmControl.Entered += ToStart;
        _alarmControl.CameOut += ToStop;
    }

    private void OnDisable()
    {
        _alarmControl.Entered -= ToStart;
        _alarmControl.CameOut -= ToStop;
    }
    
    private void ToStart()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FadeAudioVolumeTo(_maxVolume));
    }

    private void ToStop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FadeAudioVolumeTo(_minVolume));
    }

    private IEnumerator FadeAudioVolumeTo(float targetVolume)
    {
        if (_audio.isPlaying == false)
        {
            _audio.Play();
        }
        
        float stepTime = 1 / _growthTime;

        while (_audio.volume != targetVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, stepTime * Time.deltaTime);
            yield return null;
        }

        if (targetVolume == 0)
        {
            _audio.Stop();
        }
    }
}
