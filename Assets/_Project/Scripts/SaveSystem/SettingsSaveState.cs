using System;

[Serializable]
public class SettingsSaveState
{
    [UnityEngine.SerializeField]
    private bool isSoundOn , isVibrationOn;

    public SettingsSaveState()
    {
        isSoundOn = true;
        isVibrationOn = true;
    }

    public bool IsSoundOn
    {
        set
        {
            isSoundOn = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return isSoundOn;
        }
    }

    public bool IsVibrationOn
    {
        set
        {
            isVibrationOn = value;
            SaveManager.Instance.Save();
        }
        get
        {
            return isVibrationOn;
        }
    }
}