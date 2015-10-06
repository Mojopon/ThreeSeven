using UnityEngine;
using System.Collections;

public class GridFactory : IGridFactory {

    private GridFactory() { }

    private ISetting _setting;
    private IGroupFactory _groupFactory;
    public GridFactory(ISetting setting, IGroupFactory groupFactory) 
    {
        _setting = setting;
        _groupFactory = groupFactory;
    }

    public IGrid Create()
    {
        var grid = new Grid();
        grid.Initialize(_setting, _groupFactory);

        return grid;
    }
}
