using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private List<GhostFrame> _frames;
    private float _startTime;
    private int _currentIndex;

    /// <summary>
    /// ������������� �������� �������
    /// </summary>
    public void Initialize(List<GhostFrame> frames)
    {
        _frames = frames;
        _startTime = Time.time;
        _currentIndex = 0;

        // ������������� ��������� �������
        if (frames.Count > 0)
        {
            transform.position = frames[0].Position;
            transform.rotation = frames[0].Rotation;
        }
    }

    private void Update()
    {
        if (_frames == null || _frames.Count == 0) return;

        // ��������� ������� ����� ���������������
        float replayTime = Time.time - _startTime;

        // ���� ��������� ���������� ����
        while (_currentIndex < _frames.Count - 1 &&
               _frames[_currentIndex + 1].Timestamp <= replayTime)
        {
            _currentIndex++;
        }

        // �������� �� ���������� ����������
        if (_currentIndex >= _frames.Count - 1)
        {
            Destroy(gameObject);
            // ����� �������� ������� ����������
            return;
        }

        // ������������ ����� �������
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
