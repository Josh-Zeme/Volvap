using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Drool _Drool;
    [SerializeField] private Aberration _Aberration;
    [SerializeField] private SpriteRenderer _MinuteHand;
    [SerializeField] private SpriteRenderer _HourHand;

    private float _BaseTimeSpeed = 1;
    private float _TimeSpeed = 1;
    private float _TotalTime = 0;
    private float _CurrentTime = 0;

    public bool IsWaiting = false;

    private float _TargetTime = 0;
    // things i need.
    // forced timeskips (buy hours)
    // reversing time (Timespeed but negative)
    // dropping the dials down the bottom (SUPER FAST and swinging back and forward)


    protected void FixedUpdate()
    {
        _TotalTime += Time.deltaTime * _TimeSpeed;
        _CurrentTime = _TotalTime % GameSettings.DayDuration;

        if((_TargetTime < _TotalTime && _TimeSpeed > 0) || (_TargetTime > _TotalTime && _TimeSpeed < 0))
        {
            IsWaiting = false;
            _TimeSpeed = _BaseTimeSpeed;
        }

        // Get Hour
        _HourHand.transform.rotation = Quaternion.Euler(0, 0, -GetHour() * GameSettings.HoursToDegrees);
        _MinuteHand.transform.rotation = Quaternion.Euler(0, 0, -GetMinutes() * GameSettings.MinutesToDegrees);
    }

    public void UpdateSpeed(float speed)
    {
        _TimeSpeed = speed;
    }

    private float GetHour()
    {
        return _CurrentTime * GameSettings.HoursInDay / GameSettings.DayDuration;
    }

    private float GetMinutes()
    {
        return (_CurrentTime * GameSettings.HoursInDay * GameSettings.MinutesInHour / GameSettings.DayDuration) % GameSettings.MinutesInHour;
    }

    public void AddTargetTime(int minutes, int hours, int timeSpeed)
    {
        _TargetTime = _TotalTime + minutes + (GameSettings.MinutesInHour * hours);
        _TimeSpeed = timeSpeed;
        IsWaiting = true;
    }

    public void TriggerAberration(float flickerLength, List<float> flickerIntervals)
    {
        if (_Aberration != null)
        {
            _Aberration.TriggerFlicker(flickerLength, flickerIntervals);
        }
    }

    public void TriggerDrool()
    {
        if (_Drool != null)
        {
            _Drool.TriggerDrool();
        }
    }
}
