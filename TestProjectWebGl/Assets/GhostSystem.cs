using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Система записи и воспроизведения "призрака" для гонок
/// </summary>
public class GhostRecorder : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Как часто записывать позицию (в секундах)")]
    [SerializeField] private float _recordInterval = 0.05f;

    [Header("Ссылки")]
    [Tooltip("Объект игрока для записи")]
    [SerializeField] private Transform _player;
    [Tooltip("Префаб призрака для воспроизведения")]
    [SerializeField] private Ghost _ghostPrefab;

    // Состояние системы
    private enum State { Idle, Recording, Replaying }
    private State _currentState = State.Idle;

    // Данные для записи
    private List<GhostFrame> _recordedFrames = new List<GhostFrame>();
    private float _lastRecordTime;

    // Экземпляр призрака
    private Ghost _activeGhost;

    /// <summary>
    /// Начать запись траектории
    /// </summary>
    public void StartRecording()
    {
        // Сбрасываем предыдущие данные
        _recordedFrames.Clear();

        // Сохраняем начальную позицию
        RecordFrame();

        _currentState = State.Recording;
        _lastRecordTime = Time.time;
    }

    /// <summary>
    /// Начать воспроизведение записанной траектории
    /// </summary>
    public void StartReplay()
    {
        // Проверка на наличие записанных данных
        if (_recordedFrames.Count == 0)
        {
            Debug.LogWarning("Нет данных для воспроизведения!");
            return;
        }

        // Создаем экземпляр призрака
        _activeGhost = Instantiate(_ghostPrefab);
        _activeGhost.Initialize(_recordedFrames);

        _currentState = State.Replaying;
    }

    /// <summary>
    /// Остановить и сбросить систему
    /// </summary>
    public void ResetSystem()
    {
        _currentState = State.Idle;

        if (_activeGhost != null)
        {
            Destroy(_activeGhost.gameObject);
            _activeGhost = null;
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Recording:
                HandleRecording();
                break;
        }
    }

    /// <summary>
    /// Запись кадра позиции игрока
    /// </summary>
    private void RecordFrame()
    {
        _recordedFrames.Add(new GhostFrame(
            position: _player.position,
            rotation: _player.rotation,
            timestamp: Time.time
        ));
    }

    /// <summary>
    /// Обработка процесса записи
    /// </summary>
    private void HandleRecording()
    {
        if (Time.time - _lastRecordTime >= _recordInterval)
        {
            RecordFrame();
            _lastRecordTime = Time.time;
        }
    }
}

/// <summary>
/// Один кадр записи позиции/вращения
/// </summary>
public struct GhostFrame
{
    public readonly Vector3 Position;
    public readonly Quaternion Rotation;
    public readonly float Timestamp;

    public GhostFrame(Vector3 position, Quaternion rotation, float timestamp)
    {
        Position = position;
        Rotation = rotation;
        Timestamp = timestamp;
    }
}

