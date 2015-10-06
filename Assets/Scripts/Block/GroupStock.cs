using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupStock : IGroupStock
{
    IGroupFactory _groupFactory;
    List<IGroup> _groupStocks;

    Vector3[] _stockPositions;
    public Vector3[] StockPositions
    {
        get
        {
            return _stockPositions;
        }
        set
        {
            _stockPositions = value;
        }
    }

    public GroupStock(IGroupFactory groupFactory)
    {
        _groupFactory = groupFactory;
        _groupStocks = new List<IGroup>();
    }

    public IGroup Create(ISetting setting, IBlockPattern blockPattern, IGroupPattern groupPattern)
    {
        throw new System.NotImplementedException();
    }

    public IGroup Create(ISetting setting)
    {
        IGroup nextGroup = _groupStocks[0];
        nextGroup.Offset = Vector3.zero;
        _groupStocks.Remove(_groupStocks[0]);

        CreateNextStock(setting);

        return nextGroup;
    }

    public void Prepare(ISetting setting)
    {
        if (_stockPositions == null)
        {
            return;
        }

        for (int i = 0; i < _stockPositions.Length; i++)
        {
            CreateNextStock(setting);
        }
    }

    void CreateNextStock(ISetting setting)
    {
        _groupStocks.Add(_groupFactory.Create(setting));

        int i = 0;
        foreach (IGroup group in _groupStocks)
        {
            group.Offset = _stockPositions[i];
            group.SetLocation(new Coord(0, 0));

            i++;
        }

    }
}
