using UnityEngine;
using System.Collections;

public interface IParticleSpawner 
{
    void SpawnParticle(ISetting setting, Vector2 position, Color color);
}
