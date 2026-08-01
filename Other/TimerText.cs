using TMPro;
using UnityEngine;

/*
 * Attach to a GameObject with the TextMeshProUGUI component
 * Display a timer with a TextMeshProUGUI component
 */

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerText : MonoBehaviour
{
    private enum DisplayFormat
    {
        MM_SS,
        MM_SS_1Decimal,
        MM_SS_2Decimals,
        SS,
        SS_1Decimal,
        SS_2Decimals,
        HH_MM_SS
    }

    [SerializeField] private bool countOnAwake;
    [SerializeField] private DisplayFormat displayFormat = DisplayFormat.MM_SS_2Decimals;

    private TextMeshProUGUI timerText;
    private bool counting;
    private float timer;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        counting = countOnAwake;

        ResetTimer();
    }

    private void Update()
    {
        if (!counting)
            return;

        timer += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        timerText.text = FormatTime(timer);
    }

    private string FormatTime(float time)
    {
        switch (displayFormat)
        {
            case DisplayFormat.MM_SS:
            {
                int minutes = Mathf.FloorToInt(time / 60f);
                int seconds = Mathf.FloorToInt(time % 60f);
                return $"{minutes:00}:{seconds:00}";
            }

            case DisplayFormat.MM_SS_1Decimal:
            {
                int minutes = Mathf.FloorToInt(time / 60f);
                float seconds = time % 60f;
                return $"{minutes:00}:{seconds:00.0}";
            }

            case DisplayFormat.MM_SS_2Decimals:
            {
                int minutes = Mathf.FloorToInt(time / 60f);
                float seconds = time % 60f;
                return $"{minutes:00}:{seconds:00.00}";
            }

            case DisplayFormat.SS:
                return Mathf.FloorToInt(time).ToString();

            case DisplayFormat.SS_1Decimal:
                return time.ToString("F1");

            case DisplayFormat.SS_2Decimals:
                return time.ToString("F2");

            case DisplayFormat.HH_MM_SS:
            {
                int hours = Mathf.FloorToInt(time / 3600f);
                int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
                int seconds = Mathf.FloorToInt(time % 60f);

                return $"{hours:00}:{minutes:00}:{seconds:00}";
            }

            default:
                return time.ToString("F2");
        }
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void StopTimer()
    {
        counting = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        UpdateText();
    }
}