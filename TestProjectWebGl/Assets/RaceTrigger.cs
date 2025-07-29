using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    [Tooltip("—сылка на систему записи")]
    [SerializeField] private GhostRecorder _recorder;

    [Tooltip("Ёто стартова€ лини€?")]
    [SerializeField] private bool _isStartLine = true;

    private bool _isFirstLap = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            if (_isFirstLap)
            {
                // ѕервый проезд старта - начало записи
                _recorder.StartRecording();
                _isFirstLap = false;
            }
            else
            {
                // ѕоследующие проезды старта - воспроизведение
                _recorder.StartReplay();
            }
        }
    }
}