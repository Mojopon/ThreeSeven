using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupStock : IGroupStock
{
    IGroupFactory _groupFactory;
    List<IGroup> _groupStocks;
    List<GameObject> _groupHolder;
    StockDisplayConfig[] _stockDisplayConfig;
    public StockDisplayConfig[] StockDisplayConfig
    {
        get
        {
            return _stockDisplayConfig;
        }
        set
        {
            _stockDisplayConfig = value;
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
        var parent = nextGroup.Parent;
        if (parent != null)
        {
            var childCount = parent.childCount;
            for(int i = 0; i < childCount; i++)
            {
                var child = parent.GetChild(0);
                child.SetParent(setting.Parent);
                child.localScale = setting.Parent.localScale;
            }
            parent.gameObject.AddComponent<DestroyGameObject>();
        }
        else
        {
            nextGroup.Offset = Vector3.zero;
        }
        _groupStocks.Remove(_groupStocks[0]);
        
        CreateNextStock(setting);

        return nextGroup;
    }

    public void Prepare(ISetting setting)
    {
        if (_stockDisplayConfig == null)
        {
            return;
        }

        for (int i = 0; i < _stockDisplayConfig.Length; i++)
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
            group.Parent.position = _stockDisplayConfig[i].position;
            if (_stockDisplayConfig[i].scale != Vector3.zero)
            {
                group.Parent.localScale = _stockDisplayConfig[i].scale;
            }
            group.SetLocation(new Coord(0, 0));
            
            i++;
        }

    }
}
