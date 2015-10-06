using UnityEngine;
using System.Collections;

public class NullParticleSpawner : IParticleSpawner 
{
    private static NullParticleSpawner instance;
    public static NullParticleSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NullParticleSpawner();
            }

            return instance;
        }
    }

    private NullParticleSpawner() { }

    public void SpawnParticle(ISetting setting, Vector2 position, Color color)
    {
        
    }
}
