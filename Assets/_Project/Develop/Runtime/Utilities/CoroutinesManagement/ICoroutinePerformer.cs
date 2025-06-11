using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}