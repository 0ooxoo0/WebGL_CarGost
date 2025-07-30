using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    [Tooltip("—сылка на систему записи")]
    [SerializeField] private GhostSystem _recorder;

    [Tooltip("Ёто стартова€ лини€?")]
    [SerializeField] private bool _isStartLine = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            // ¬ъехал в старт Ч значит, закончил круг => воспроизведение последнего круга
            _recorder.StartReplay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isStartLine)
        {
            // ¬ыехал из старта Ч значит, начал новый круг => нова€ запись
            _recorder.StartRecording();
        }
    }
}
