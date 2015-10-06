using UnityEngine;
using System.Collections.Generic;

public class GameEntity : MonoBehaviour, IControllable {

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

    Game game;
    IControllable currentControl;

    GameObject playerGame;

    List<IGroupPattern> groupPatterns;

    void Awake()
    {
        gameSetting.Initialize();
        gameSetting.GetGameText(GameTextType.GameMessageCenter).UpdateText("");

        currentControl = NullControl.Instance;
        soundRepository.InitializeSingleton();

        CreateNewGame();
    }

    void CreateNewGame()
    {
        playerGame = new GameObject();
        playerGame.name = "PlayerGame";
        playerGame.transform.position = gameSetting.PlayerGridPosition;

        groupPatterns = groupPatternConverter.Convert();

        GameBuilder gameBuilder = new GameBuilder(playerGame,
                                                  gameSetting,
                                                  particleSpawner,
                                                  blockColorRepository,
                                                  groupPatterns,
                                                  backgroundFactory,
                                                  cameraManager,
                                                  blockViewSpawner,
                                                  floatingTextRenderer);

        game = gameBuilder.Build();
        currentControl = game;

        game.StartThreeSeven();
    }

    void Update()
    {
        game.OnUpdate();
    }

    #region IControllable Method Group

    public void OnArrowKeyInput(Direction direction)
    {
        currentControl.OnArrowKeyInput(direction);
    }

    public void OnJumpKeyInput()
    {
        currentControl.OnJumpKeyInput();
    }

    #endregion
}
