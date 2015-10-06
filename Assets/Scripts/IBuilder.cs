using UnityEngine;
using System.Collections;

public interface IBuilder<T> {

    T Build();
}
