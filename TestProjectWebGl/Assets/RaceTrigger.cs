using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    [Tooltip("������ �� ������� ������")]
    [SerializeField] private GhostRecorder _recorder;

    [Tooltip("��� ��������� �����?")]
    [SerializeField] private bool _isStartLine = true;

    private bool _isFirstLap = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            if (_isFirstLap)
            {
                // ������ ������ ������ - ������ ������
                _recorder.StartRecording();
                _isFirstLap = false;
            }
            else
            {
                // ����������� ������� ������ - ���������������
                _recorder.StartReplay();
            }
        }
    }
}