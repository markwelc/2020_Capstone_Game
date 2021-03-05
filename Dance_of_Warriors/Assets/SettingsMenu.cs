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

    private float defaultBGVolume = 0.75f;
    private float defaultFXVolume = 0.75f;
    private float defaultXLookScale = 0.75f;
    private float defaultYLookScale = 0.75f;

    [Header("Sample Sounds")]
    public AudioSource bgExample;
    public AudioSource fxExample;

    private bool calledBG, calledFX;

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
            backgroundSoundMixer.SetFloat("Volume BG", convertToLogarithmic(defaultBGVolume));
            soundFXMixer.SetFloat("Volume Sound FX", convertToLogarithmic(defaultFXVolume));
        }


        // Check if cam script is null because it will be in main menu scene
        if(camScript != null) {
            camScript.setXLookScale(defaultXLookScale);
            camScript.setYLookScale(defaultYLookScale);
        }

        calledFX = false;
        calledBG = false;
    }
    public void setBackgroundVolume(float volume)
    {
        // update player prefs
        PlayerPrefs.SetFloat(keyBG, volume);

        // update music volume
        backgroundSoundMixer.SetFloat("Volume BG", convertToLogarithmic(volume));

        // want to be sure not to call with slider initialization
        if (calledBG)
            StartCoroutine(playSampleSound(volume, keyBG));
        else
            calledBG = true;
    }

    public void setSoundFXVolume(float volume)
    {
        // update player prefs
        PlayerPrefs.SetFloat(keyFX, volume);
        
        // update sound fx volume
        soundFXMixer.SetFloat("Volume Sound FX", convertToLogarithmic(volume));
        
        // Want to be sure not to call with slider initzialization
        if (calledFX)
            StartCoroutine(playSampleSound(volume, keyFX));
        else
            calledFX = true;
    }

    /**
     * Play a sample to show how the expected volume sounds
     */
    IEnumerator playSampleSound(float inVolume, string key)
    {
        // need to do realtime since time scale is 0
        // 0.2 seems to work nicely
        yield return new WaitForSecondsRealtime(0.2f);
        // Play sample sound for volume level
        // but dont want to do it every small change or else it will 
        // be like bebeepebebbeope bepe beep ya know
        
        // Play sound effect if it has been at the same spot for 0.2 seconds
        if (inVolume == PlayerPrefs.GetFloat(key))
        {
            // Check key to be sure we play on right one
            if (key == keyBG)
            {   
                bgExample.PlayOneShot(bgExample.clip);
            }
            else if (key == keyFX)
            {
                fxExample.PlayOneShot(fxExample.clip);
            }
        }
    }

    /**
     * Mixer values work logarithmic not linear
     * this allows us to convert our '0' to 1 slider val to logarithmic
     */
    private float convertToLogarithmic(float val)
    {
        // turns in value -80 to 0 but on logarithmic scale
        return Mathf.Log10(val) * 20;
    }

    /**
     * Updates player prefs
     * set look sensitity in horizontal(x-axis)
     */
    public void setLookSensitivityHorizontal(float sensitivityX)
    {
        // Store new sensitivity value to be used in
        PlayerPrefs.SetFloat(keyXSensitivity, sensitivityX);

        // could just access player prefs val in camscript
        // but then it would have to be called everyframe
        if (camScript != null)
            camScript.setXLookScale(sensitivityX);
    }

    /**
     * Updates player prefs
     * set look sensitity in vertical(y-axis)
     */
    public void setLookSensitivityVertical(float sensitivityY)
    {
        // store new pref value
        PlayerPrefs.SetFloat(keyYSensitivity, sensitivityY);
        
        // could just access player prefs val in camscript
        // but then it would have to be called everyframe
        if(camScript != null)
            camScript.setYLookScale(sensitivityY);
    }

}
