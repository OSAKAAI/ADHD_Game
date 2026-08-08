using UnityEngine;

public class OptionsUIManager : MonoBehaviour
{
    public void SetVolume(float value)
    {
        AudioManager.instance.SetVolume(value);
    }

    public void ToggleMusic(bool isOn)
    {
        AudioManager.instance.ToggleMusic(isOn);
    }
}