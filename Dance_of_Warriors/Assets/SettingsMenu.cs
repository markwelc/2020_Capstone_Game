using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private CameraLook camScript;

    [Header("Audio Mixers")]
    public AudioMixer backgroundSoundMixer;
    public AudioMixer soundFXMixer;

    
    [Header("Sliders")]
    public Slider bgSoundSlider;
    public Slider fxSoundSlider;
    public Slider xLookSlider;
    public Slider yLookSlider;

    private string keyBG = "Volume_BG";
    private string keyFX = "Volume_FX";
    private string keyXSensitivity = "Sensitivity_X";
    private string keyYSensitivity = "Sensitivity_Y";

    private float defaultBGVolume = -20f;
    private float defaultFXVolume = -20f;
    private float defaultXLookScale = 0.75f;
    private float defaultYLookScale = 0.75f;

    private void Start()
    {
        // Using preferences due to ease of storing between scenes
        // they may even store after closing and reopening application not sure tho
        // they do store when exiting and re entering play mode

        // Set defaults as have the changed the vals before? if so get the pref value stored
        // otherwise set as default
        if (PlayerPrefs.HasKey(keyBG))
            defaultBGVolume = PlayerPrefs.GetFloat(keyBG);

        if (PlayerPrefs.HasKey(keyFX))
            defaultFXVolume = PlayerPrefs.GetFloat(keyFX);

        if (PlayerPrefs.HasKey(keyXSensitivity))
            defaultXLookScale = PlayerPrefs.GetFloat(keyXSensitivity);

        if (PlayerPrefs.HasKey(keyYSensitivity))
            defaultYLookScale = PlayerPrefs.GetFloat(keyYSensitivity);

        // Set default slider variables
        bgSoundSlider.value = defaultBGVolume;
        fxSoundSlider.value = defaultFXVolume;
        xLookSlider.value = defaultXLookScale;
        yLookSlider.value = defaultYLookScale;

        // Now actually set the init values
        if (backgroundSoundMixer == null || soundFXMixer == null)
        {
            Debug.LogError("ERROR: Sound mixers must be assigned in inspector!");
        }
        else
        {
            backgroundSoundMixer.SetFloat("Volume BG", defaultBGVolume);
            soundFXMixer.SetFloat("Volume Sound FX", defaultFXVolume);
        }

        if(camScript != null) {
            camScript.setXLookScale(defaultXLookScale);
            camScript.setYLookScale(defaultYLookScale);
        }
    }
    public void setBackgroundVolume(float volume)
    {
        // update player prefs
        PlayerPrefs.SetFloat(keyBG, volume);

        // update music volume
        backgroundSoundMixer.SetFloat("Volume BG", volume);
    }

    public void setSoundFXVolume(float volume)
    {
        // update player prefs
        PlayerPrefs.SetFloat(keyFX, volume);
        
        // update sound fx volume
        soundFXMixer.SetFloat("Volume Sound FX", volume);
    }

    public void setLookSensitivityHorizontal(float sensitivityX)
    {
        // Store new sensitivity value to be used in
        PlayerPrefs.SetFloat(keyXSensitivity, sensitivityX);

        // update sensitivity
        if(camScript != null)
            camScript.setXLookScale(sensitivityX);
    }

    public void setLookSensitivityVertical(float sensitivityY)
    {
        // store new pref value
        PlayerPrefs.SetFloat(keyYSensitivity, sensitivityY);

        // update sensitivity horizontal
        if(camScript != null)
            camScript.setYLookScale(defaultYLookScale);
    }

}
