using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown Rdropdown;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        Rdropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResIndex = i;
            }
        }
        Rdropdown.AddOptions(options);
        Rdropdown.value = currentResIndex;
        Rdropdown.RefreshShownValue();
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume",volume);
    }

    public void setFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
