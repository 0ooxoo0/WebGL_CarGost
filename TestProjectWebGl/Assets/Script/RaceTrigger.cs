using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    [Tooltip("������ �� ������� ������")]
    [SerializeField] private GhostSystem _recorder;

    [Tooltip("��� ��������� �����?")]
    [SerializeField] private bool _isStartLine = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            // ������ � ����� � ������, �������� ���� => ��������������� ���������� �����
            _recorder.StartReplay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            // ������ �� ������ � ������, ����� ����� ���� => ����� ������
            _recorder.StartRecording();
        }
    }
}
