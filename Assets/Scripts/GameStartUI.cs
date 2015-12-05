using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStartUI : MonoBehaviour
{
    public GameStartSettingValues settingValues;

    public CPUMode cpuMode = CPUMode.Easy;
    public GameObject selectDifficultyUIs;
    public Dropdown difficultySelectDropDown;

    private GameMode gameMode;

    void Start()
    {
        settingValues.difficulty = CPUMode.Easy;
        SwitchToSinglePlayerMode();
    }

    public void SwitchToSinglePlayerMode()
    {
        gameMode = GameMode.SinglePlayerMode;
        selectDifficultyUIs.SetActive(false);
    }

    public void SwitchToVersusCPUMode()
    {
        gameMode = GameMode.VersusCPUMode;
        selectDifficultyUIs.SetActive(true);
    }

    public void OnSwitchDifficulty()
    {
        var difficulty = difficultySelectDropDown.value;
        Debug.Log("On Switch Difficulty Called. Difficulty: " + difficulty);

        switch(difficulty)
        {
            case 0:
                settingValues.difficulty = CPUMode.Easy;
                break;
            case 1:
                settingValues.difficulty = CPUMode.Normal;
                break;
            case 2:
                settingValues.difficulty = CPUMode.Hard;
                break;
            case 3:
                settingValues.difficulty = CPUMode.Kusotuyo;
                break;
        }
    }

    public void StartGame()
    {
        switch(gameMode)
        {
            case GameMode.SinglePlayerMode:
                Application.LoadLevel("Game");
                break;
            case GameMode.VersusCPUMode:
                Application.LoadLevel("2PGame");
                break;
        }
    }
}
