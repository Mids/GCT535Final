using UnityEngine;
using UnityEngine.UI;

public class PitchController : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider tempoSliderUI;
    public Text tempoTextUI;
    public Text titleTextUI;
    public BeatFlow beatUI;

    private void Start()
    {
        titleTextUI.text = audioSource.clip.name;
    }

    public void Update()
    {
        // tempoSliderUI.value += Input.GetAxis("Mouse ScrollWheel");
        tempoSliderUI.value = beatUI.tempo;
    }

    public void OnPitchChanged(float v)
    {
        v = Mathf.Clamp(v, 0.5f, 1.5f);
        audioSource.pitch = v;
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("PitchOnly", 1f / v);
        tempoTextUI.text = $"Tempo: x{v:F2}";
    }
}