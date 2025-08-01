using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ������ � ��������������� "��������" ��� �����
/// </summary>
public class GhostSystem : MonoBehaviour
{
    [Header("���������")]
    [Tooltip("��� ����� ���������� ������� (� ��������)")]
    [SerializeField] private float _recordInterval = 0.05f;

    [Header("������")]
    [Tooltip("������ ������ ��� ������")]
    [SerializeField] private Transform _player;
    [Tooltip("������ �������� ��� ���������������")]
    [SerializeField] private Ghost _ghostPrefab;

    // ��������� �������
    private enum State { Idle, Recording, Replaying }
    private State _currentState = State.Idle;

    // ������ ��� ������
    public List<GhostFrame> _recordedFrames = new List<GhostFrame>();
    bool i;
    private float _lastRecordTime;


    private float _recordStartTime;


    public void Clear()
    {
        _recordedFrames.Clear();
    }

    /// <summary>
    /// ������ ������ ����������
    /// </summary>
    public void StartRecording()
    {

        Debug.Log("StartRecording");

        _recordedFrames.Clear();
        _recordStartTime = Time.time;

        RecordFrame(); // �������� ������ ���� �����

        _currentState = State.Recording;
        _lastRecordTime = Time.time;

    }


    /// <summary>
    /// ������ ��������������� ���������� ����������
    /// </summary>
    public void StartReplay()
    {

        if (_recordedFrames.Count == 0)
        {
            Debug.LogWarning("��� ������ ��� ���������������!");
            return;
        }
        Ghost _activeGhost = Instantiate(_ghostPrefab);
        _activeGhost.Initialize(_recordedFrames);

        _currentState = State.Replaying;
    }


    /// <summary>
    /// ���������� � �������� �������
    /// </summary>
    public void ResetSystem()
    {
        _currentState = State.Idle;
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
    /// ������ ����� ������� ������
    /// </summary>
    private void RecordFrame()
    {
        _recordedFrames.Add(new GhostFrame(
            position: _player.position,
            rotation: _player.rotation,
            timestamp: Time.time - _recordStartTime // <=== ����� �������
        ));
    }


    /// <summary>
    /// ��������� �������� ������
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
/// ���� ���� ������ �������/��������
/// </summary>
[System.Serializable]
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

