using TMPro;
using UnityEngine;
using UnityEngine.Events;

/*
 * Attach to a GameObject with a TextMeshProUGUI component
 * Displays a configurable countdown timer
 */

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountdownText : MonoBehaviour
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

    [Header("Timer")]
    [SerializeField] private float totalTime = 60f;
    [SerializeField] private bool countOnAwake;

    [Header("Display")]
    [SerializeField] private DisplayFormat displayFormat = DisplayFormat.MM_SS_2Decimals;

    public UnityEvent OnCountdownEnds;

    private TextMeshProUGUI countdownText;
    private bool counting;
    private float timer;

    private void Awake()
    {
        countdownText = GetComponent<TextMeshProUGUI>();

        ResetTimer();
        counting = countOnAwake;
    }

    private void Update()
    {
        if (!counting)
            return;

        timer -= Time.deltaTime;
        timer = Mathf.Max(0f, timer);

        UpdateText();

        if (timer <= 0f)
        {
            counting = false;
            OnCountdownEnds?.Invoke();
        }
    }

    private void UpdateText()
    {
        countdownText.text = FormatTime(timer);
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

    public void StartCountdown()
    {
        counting = true;
    }

    public void StopCountdown()
    {
        counting = false;
    }

    public void ResetTimer()
    {
        timer = totalTime;
        UpdateText();
    }
}