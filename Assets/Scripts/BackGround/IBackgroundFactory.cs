using UnityEngine;
using System.Collections;

public interface IBackgroundFactory 
{
    IBackground Create(ISetting setting);
}
