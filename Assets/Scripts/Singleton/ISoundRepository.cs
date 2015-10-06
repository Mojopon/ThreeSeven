using UnityEngine;
using System.Collections;

public interface ISoundRepository {

    AudioClip Get(SoundName soundName);
}
