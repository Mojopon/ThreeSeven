using UnityEngine;
using System.Collections;

public class BlockView : MonoBehaviour, IBlockView {

    public Vector3 Offset { get; set; }

    public Color Color
    {
        get { return GetComponent<SpriteRenderer>().color; }
        set { GetComponent<SpriteRenderer>().color = value; }
    }

    public Vector3 Position
    {
        get { return transform.localPosition; }
        set { transform.localPosition = value; }
    }
    public Vector3 WorldPosition
    {
        get { return transform.position; }
    }

    public ISetting Setting { get; set; }

    public void Delete()
    {
        isToDelete = true;
        isDeleting = true;
    }

    public void DeleteImmediate()
    {
        if (gameObject != null)
        {
            CreateParticle(Setting.ParticleSpawner);
            Destroy(gameObject);
        }
    }

    #region IMovable Group

    private bool isOnMove = false;
    public bool IsOnMove
    {
        get { return isOnMove; }
    }

    private Vector3 destination;
    public void MoveTo(Vector2 position)
    {
        isOnMove = true;
        destination = new Vector3(position.x, position.y, -1) + Offset;
    }

    public void SetPosition(Vector2 position)
    {
        Position = new Vector3(position.x, position.y, -1) + Offset;
    }

    #endregion

    private bool isToDelete = false;
    public bool IsToDelete { get { return isToDelete; } }

    private bool isDeleting = false;
    public bool IsDeleting { get { return isDeleting; } }

    public void OnUpdate()
    {
        MoveTowardsDestination();
        DeleteEffect();
    }

    void DeleteEffect()
    {
        if (isToDelete)
        {
            if (transform.localScale != Vector3.zero)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Setting.BlockDeleteSpeed * Time.deltaTime);
            }
            else
            {
                if (coroutineStarted == false)
                {
                    StartCoroutine(CompleteDeleting(Setting.WaitAfterDelete, Setting.ParticleSpawner));
                }
            }
        }
    }

    private bool coroutineStarted = false;
    IEnumerator CompleteDeleting(float waitTime, IParticleSpawner particleSpawner)
    {
        coroutineStarted = true;
        CreateParticle(particleSpawner);
        PlaySound(SoundName.Delete);

        float endTime = Time.time + waitTime;
        while(endTime > Time.time)
        {
            yield return null;
        }


        isDeleting = false;
        Destroy(gameObject, 0.5f);
    }

    void CreateParticle(IParticleSpawner particleSpawner)
    {
        particleSpawner.SpawnParticle(Setting, transform.position, Color);
    }

    void MoveTowardsDestination()
    {
        if (isOnMove)
        {
            if (destination != Position)
            {
                if (Vector3.MoveTowards(Position, destination, Setting.BlockFallSpeed * Time.deltaTime) == destination)
                {
                    PlaySound(SoundName.BumpOnTheGround);
                }
                Position = Vector3.MoveTowards(Position, destination, Setting.BlockFallSpeed * Time.deltaTime);
            }
            else
            {
                isOnMove = false;
            }
        }
    }

    public void PlaySound(SoundName soundName)
    {
        if (GetComponent<AudioSource>() == null)
        {
            Debug.Log("No audiosource is attached!");
            return;
        }

        AudioClip clip = SoundRepository.Instance.Get(soundName);

        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().PlayDelayed(0.05f);    
    }
}
