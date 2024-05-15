using System.Collections.Generic;
using UnityEngine;

namespace Wave.Engine.Composite
{
    public interface ICompsoiteIdManager
    {
        string GetUniqueId(string fullHash);
    }

    public class CompsoiteIdManager : MonoBehaviour, ICompsoiteIdManager
    {
        private static CompsoiteIdManager _instance;
        public static ICompsoiteIdManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<CompsoiteIdManager>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private Dictionary<string, int> _fullHash
            = new Dictionary<string, int>();

        public string GetUniqueId(string fullHash)
        {
            if (string.IsNullOrEmpty(fullHash))
                throw new System.Exception("Invalid group Id!");

            _fullHash.Remove(fullHash, out int value);
            _fullHash.Add(fullHash, value + 1);

            return $"{value + 1}_{fullHash}";
        }

    }
}
