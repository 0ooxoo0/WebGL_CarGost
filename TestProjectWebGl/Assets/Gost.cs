using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private List<GhostFrame> _frames;
    private float _startTime;
    private int _currentIndex;

    /// <summary>
    /// Инициализация призрака данными
    /// </summary>
    public void Initialize(List<GhostFrame> frames)
    {
        _frames = frames;
        _startTime = Time.time;
        _currentIndex = 0;

        // Устанавливаем начальную позицию
        if (frames.Count > 0)
        {
            transform.position = frames[0].Position;
            transform.rotation = frames[0].Rotation;
        }
    }

    private void Update()
    {
        if (_frames == null || _frames.Count == 0) return;

        // Вычисляем текущее время воспроизведения
        float replayTime = Time.time - _startTime;

        // Ищем следующий подходящий кадр
        while (_currentIndex < _frames.Count - 1 &&
               _frames[_currentIndex + 1].Timestamp <= replayTime)
        {
            _currentIndex++;
        }

        // Проверка на завершение траектории
        if (_currentIndex >= _frames.Count - 1)
        {
            Destroy(gameObject);
            // Можно добавить событие завершения
            return;
        }

        // Интерполяция между кадрами
        GhostFrame current = _frames[_currentIndex];
        GhostFrame next = _frames[_currentIndex + 1];

        float progress = Mathf.InverseLerp(
            current.Timestamp,
        next.Timestamp,
            replayTime
        );

        transform.position = Vector3.Lerp(
            current.Position,
        next.Position,
            progress
        );

        transform.rotation = Quaternion.Slerp(
            current.Rotation,
            next.Rotation,
            progress
        );
    }
}
