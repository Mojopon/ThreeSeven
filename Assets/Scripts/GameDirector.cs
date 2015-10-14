using UnityEngine;
using System.Collections.Generic;

public class GameDirector : MonoBehaviour, IGameDirector
{
    public string gameName;

    public CameraManager cameraManager;
    public GameSetting gameSetting;
    public BackgroundFactory backgroundFactory;
    public BlockViewSpawner blockViewSpawner;
    public GroupPatternConverter groupPatternConverter;
    public ParticleSpawner particleSpawner;
    public BlockColorRepository blockColorRepository;
    public GameText scoreText;
    public GameText levelText;
    public SoundRepository soundRepository;
    public FloatingTextRenderer floatingTextRenderer;

    public Game Construct()
    {
        Initialize();

        GameObject playerGame = new GameObject();
        playerGame.name = gameName;
        playerGame.transform.position = gameSetting.GridPosition;

        List<IGroupPattern> groupPatterns = groupPatternConverter.Convert();

        GameBuilder gameBuilder = new GameBuilder(playerGame,
                                                  gameSetting,
                                                  particleSpawner,
                                                  blockColorRepository,
                                                  groupPatterns,
                                                  backgroundFactory,
                                                  cameraManager,
                                                  blockViewSpawner,
                                                  floatingTextRenderer);

        Game game = gameBuilder.Build();

        game.InitializeGrid();

        return game;
    }

    void Initialize()
    {
        gameSetting.Initialize();
        gameSetting.GetGameText(GameTextType.GameMessageCenter).UpdateText("");

        soundRepository.InitializeSingleton();
    }
}
