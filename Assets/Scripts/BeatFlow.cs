using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BeatFlow : MonoBehaviour
{
    public AudioSource audioSource;

    public TextAsset beats;

    private float[] _beatTimers;

    private Image _background;

    // Start is called before the first frame update
    private void Start()
    {
        _background = GetComponent<Image>();

        _beatTimers = beats.text.Split(',').Select(p =>
        {
            float.TryParse(p, out var f);
            return f;
        }).ToArray();
    }

    // Update is called once per frame
    private void Update()
    {
        var audioTime = audioSource.time;

        foreach (var beat in _beatTimers)
        {
            var timeToBeat = beat - audioTime;
            if (timeToBeat < 0) continue;
            if (timeToBeat < 0.1f)
            {
                ChangeBackgroundAlpha(1 - timeToBeat * 10);
            }
            else
            {
                ChangeBackgroundAlpha(0f);
            }

            break;
        }
    }

    private void ChangeBackgroundAlpha(float a)
    {
        var color = _background.color;
        color.a = a;
        _background.color = color;
    }
}