using UnityEngine;
using UnityEngine.UI;

public class Visualizer : MonoBehaviour
{
    public GameObject barPrefab;

    public Transform barsParent;

    public AudioSource audioSource;

    private const int numOfSamples = 32;

    private RectTransform[] bars = new RectTransform[numOfSamples];

    public float[] samples = new float[numOfSamples];

    private float width = 128f / numOfSamples;
    private float height = 200f;

    private void OnEnable()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i] = Instantiate(barPrefab, barsParent).GetComponent<RectTransform>();
            bars[i].localPosition = new Vector2(i * width - bars.Length * width / 2, 0);

            bars[i].GetComponent<Image>().color =
                new Vector4(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);

            bars[i].sizeDelta = new Vector2(width, 1f);
        }
    }

    private void Update()
    {
        //스펙트럼 가지고오는 함수
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);

        for (int i = 0; i < numOfSamples; i++)
            bars[i].sizeDelta = new Vector2(width, samples[i] * height);
    }
}