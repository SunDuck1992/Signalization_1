using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [HideInInspector] private SpriteRenderer _target;
    [SerializeField] private float _duration;
    [SerializeField] private Color _targetColor;

    private float _runningTime;
    private Color _startColor;
    private float _normalizeRunningTime;

    void Start()
    {
        _target = GetComponent<SpriteRenderer>();
        _startColor = _target.color;
    }

    void Update()
    {
        if(_runningTime <= _duration)
        {
            _runningTime += Time.deltaTime;
            _normalizeRunningTime = _runningTime / _duration;
            _target.color = Color.Lerp(_startColor, _targetColor, _normalizeRunningTime);
        }
    }
}
