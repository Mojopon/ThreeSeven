using UnityEngine;
using System.Collections;

public class BlockViewSpawner : MonoBehaviour, IBlockViewSpawner 
{
    public Transform blockPrefab;
    public Transform[] numberPrefabs;


    public IBlockView Spawn(ISetting setting, int number, Coord location)
    {
        Transform blockTransform = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity) as Transform;
        Transform numberTransform = Instantiate(numberPrefabs[number], new Vector3(0, 0, -1), Quaternion.identity) as Transform;
        numberTransform.SetParent(blockTransform);
        blockTransform.SetParent(setting.Parent);
        blockTransform.localScale = new Vector3(1, 1, 1);

        BlockView blockView = blockTransform.GetComponent<BlockView>();
        blockView.Setting = setting;
        blockView.Color = setting.BlockColorRepository.GetColorForTheNumber(number);

        return blockView;
    }
}
