using UnityEngine;

namespace GodComplex.Utility
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = (T) FindObjectOfType(typeof(T));
                }

                return _instance;
            }
        }
    }
}
