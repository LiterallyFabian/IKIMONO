using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class Settings
{
    [JsonProperty("musicVolume")] private float _musicVolume = 1;
    [JsonProperty("effectsVolume")] private float _effectsVolume = 1;
    [JsonProperty("ambienceVolume")] private float _ambienceVolume = 1;
    [JsonProperty("notificationsOn")] private bool _notificationsToggle = false;

    public float MusicVolume
    {
        get => _musicVolume;
        set => _musicVolume = Mathf.Clamp(value, 0.0001f, 1);
    }

    public float EffectsVolume
    {
        get => _effectsVolume;
        set => _effectsVolume = Mathf.Clamp(value, 0.0001f, 1);
    }
    public float AmbienceVolume
    {
        get => _ambienceVolume;
        set => _ambienceVolume = Mathf.Clamp(value, 0.0001f, 1);
    }

    public bool NotificationsToggle
    {
        get => _notificationsToggle;
        set => _notificationsToggle = value;
    }
}
