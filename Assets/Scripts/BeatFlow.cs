using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BeatFlow : MonoBehaviour
{
    public AudioSource audioSource;

    public TextAsset beats;

    private float[] _beatTimers;

    private Image _background;

    public GameObject barPrefab;

    public float width = 1f;

    public float height = 30f;

    private RectTransform[] bars = new RectTransform[10];

    [Range(0.75f, 1.25f)]
    public float tempo = 1f;

    private int _goodStack = 0;
    private int _badStack = 0;

    // Start is called before the first frame update
    private void Start()
    {
        _background = GetComponent<Image>();

        _beatTimers = beats.text.Split(',').Select(p =>
        {
            float.TryParse(p, out var f);
            return f;
        }).ToArray();


        for (int i = 0; i < bars.Length; i++)
        {
            bars[i] = Instantiate(barPrefab, transform).GetComponent<RectTransform>();
            bars[i].localPosition = new Vector2(i * width - bars.Length * width / 2, 0);


            bars[i].sizeDelta = new Vector2(width, height);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        MoveBeatBar();

        ChangeBackgroundAlpha();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            BeatChecker();
        }
    }

    private void MoveBeatBar()
    {
        var audioTime = audioSource.time;
        var barIndex = 0;
        foreach (var beat in _beatTimers)
        {
            if (audioTime > beat) continue;

            if (barIndex == bars.Length) break;

            var timeToBeat = beat - audioTime;

            bars[barIndex++].localPosition = new Vector2(timeToBeat * 100f - 100f, 0);
        }
    }

    private void ChangeBackgroundAlpha()
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

    private void BeatChecker()
    {
        var audioTime = audioSource.time;
        var closestBeat = float.MaxValue;

        foreach (var beat in _beatTimers)
        {
            var timeToBeat = Mathf.Abs(beat - audioTime);
            if (timeToBeat > closestBeat)
            {
                break;
            }

            closestBeat = timeToBeat;
        }

        if (closestBeat < 0.1f)
        {
            print("good");
            _goodStack++;
            _badStack = 0;
        }
        else
        {
            print("bad");
            _goodStack = 0;
            _badStack++;
        }

        if (_goodStack > 2 && tempo < 1.25f)
        {
            tempo += 0.05f;
            _goodStack = 0;
        }

        if (_badStack > 2 && tempo > 0.75f)
        {
            tempo -= 0.05f;
            _badStack = 0;
        }
    }
}