using UnityEngine;
using System.Collections.Generic;

public class GameBuilder : IBuilder<Game> 
{
    public GameBuilder(GameObject gameObject,
                       ISetting setting,
                       IParticleSpawner particleSpawner,
                       IBlockColorRepository blockColorRepository,
                       List<IGroupPattern> groupPatterns,
                       IBackgroundFactory backgroundFactory,
                       ICameraManager cameraManager,
                       IBlockViewSpawner blockViewSpawner,
                       IFloatingTextRenderer floatingTextRenderer) 
    {
        gameObject.transform.localScale = new Vector3(setting.ScalePerBlock, setting.ScalePerBlock, 1);
        setting.Parent = gameObject.transform;

        setting.ParticleSpawner = particleSpawner;
        setting.BlockColorRepository = blockColorRepository;
        setting.FloatingTextRenderer = floatingTextRenderer;

        _gameObject = gameObject;
        _setting = setting;
        _groupPatterns = groupPatterns;

        _backgroundFactory = backgroundFactory;
        _cameraManager = cameraManager;
        _blockViewSpawner = blockViewSpawner;
    }

    GameObject _gameObject;
    ISetting _setting;
    List<IGroupPattern> _groupPatterns;
    IBackgroundFactory _backgroundFactory;
    ICameraManager _cameraManager;
    IBlockViewSpawner _blockViewSpawner;

    public Game Build()
    {
        Game game = new Game(_setting);

        SetFactories(game);

        return game;
    }

    void SetFactories(Game game)
    {
        game.CameraManager = _cameraManager;
        game.BackgroundFactory = _backgroundFactory;

        IBlockFactory blockFactory = new BlockFactory(_blockViewSpawner);

        IGroupFactory groupFactory = new GroupFactory(blockFactory, _groupPatterns);
        IGroupStock groupStock = new GroupStock(groupFactory);
        groupStock.StockPositions = _setting.StockPositions;
        groupStock.Prepare(_setting);
        game.GroupFactory = groupStock;

        var gridFactory = new GridFactory(_setting, groupStock);
        game.GridFactory = gridFactory;
    }
}
