using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.Updater
{
    public class NonMonoBehUpdater : MonoBehaviour
    {
        private List<IUpdatable> _updatables = new();

        private void Update()
        {
            foreach (IUpdatable updatable in _updatables)
                updatable.Update();
        }

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }     
    }
}
