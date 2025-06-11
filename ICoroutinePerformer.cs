using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutinePerformer
{
    Coroutine StartPerform(IEnumerator coroutineFunction);
    void StopPerform(Coroutine coroutine);
}
