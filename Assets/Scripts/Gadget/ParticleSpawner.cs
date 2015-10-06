using UnityEngine;
using System.Collections;

public class ParticleSpawner : MonoBehaviour, IParticleSpawner {

    public Transform particle;

    public void SpawnParticle(ISetting setting, Vector2 position, Color color)
    {
        Transform spawnedParticle = Instantiate(particle, Vector3.zero, Quaternion.identity) as Transform;
        spawnedParticle.SetParent(setting.Parent);

        spawnedParticle.GetComponent<ParticleSystem>().startColor = color;
        spawnedParticle.position = new Vector3(position.x, position.y, -1);
    }
}
