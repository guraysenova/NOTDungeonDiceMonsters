using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }

    public PlayerSaveState playerState;
    public SettingsSaveState settingsState;

    string playerSave = "PlayerSave";
    string settingsSave = "SettingsSave";

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
        SceneManager.sceneUnloaded += OnNewScene;
    }

    public void Save()
    {
        PlayerPrefs.SetString(playerSave, Helper.Encrypt(Helper.Serialize(playerState)));
        PlayerPrefs.SetString(settingsSave, Helper.Encrypt(Helper.Serialize(settingsState)));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(playerSave))
        {
            playerState = Helper.Desrialize<PlayerSaveState>(Helper.Decrypt(PlayerPrefs.GetString(playerSave)));
        }
        else
        {
            playerState = new PlayerSaveState();
        }

        if (PlayerPrefs.HasKey(settingsSave))
        {
            settingsState = Helper.Desrialize<SettingsSaveState>(Helper.Decrypt(PlayerPrefs.GetString(settingsSave)));
        }
        else
        {
            settingsState = new SettingsSaveState();
        }

        Save();
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    public void OnSceneChange(Scene currentScene, Scene newScene)
    {
        Save();
    }

    public void OnNewScene(Scene scene)
    {
        Save();
    }
}