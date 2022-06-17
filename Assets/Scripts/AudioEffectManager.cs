using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectManager : MonoBehaviour
{
    public PlayerWeaponsManager weaponsManager;

    private AudioMixer _mixer;
    private int _snapshotIndex = 1;

    private void Start()
    {
        _mixer = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
    }

    private void Update()
    {
        var weaponIndex = weaponsManager.ActiveWeaponIndex;
        if (weaponIndex < 0 || _snapshotIndex == weaponIndex) return;
        
        _snapshotIndex = weaponIndex;
        switch (weaponIndex)
        {
            case 0:
                _mixer.FindSnapshot("Normal").TransitionTo(0.3f);
                break;
            case 1:
                _mixer.FindSnapshot("LowPass").TransitionTo(0.3f);
                break;
            case 2:
                _mixer.FindSnapshot("Chorus").TransitionTo(0.3f);
                break;
        }
    }
}