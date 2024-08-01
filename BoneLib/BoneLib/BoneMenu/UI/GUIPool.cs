using Il2CppInterop.Runtime.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public class GUIPool : MonoBehaviour
    {
        public GUIPool(System.IntPtr ptr) : base(ptr) { }

        private int _size = 8;

        private GameObject _prefab;

        private List<GameObject> _inactiveObjects = new List<GameObject>();
        private List<GameObject> _activeObjects = new List<GameObject>();

        public void Initialize()
        {
            if (_prefab == null)
            {
                return;
            }

            for (int i = 0; i < _size; i++)
            {
                GameObject clone = Instantiate(_prefab, transform);
                clone.SetActive(false);
                GUIPoolee poolee = clone.AddComponent<GUIPoolee>();
                poolee.SetParent(this);
                _inactiveObjects.Add(clone);
            }
        }
        
        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }

        public GameObject Spawn(Transform parent)
        {
            GameObject clone = GetFirst(_inactiveObjects);

            if (_inactiveObjects.Count == 0)
            {
                Grow(_size);
                return Spawn(parent);
            }

            _activeObjects.Add(clone);
            _inactiveObjects.Remove(clone);
            clone.transform.SetParent(parent);
            clone.SetActive(false);
            return clone;
        }

        public void ReturnAll()
        {
            _activeObjects
            .Select((obj) => obj.GetComponent<GUIPoolee>())
            .ToList()
            .ForEach((element) => element.Return());
        }

        public void OnReturn(GUIPoolee poolee)
        {
            _inactiveObjects.Add(poolee.gameObject);
            _activeObjects.Remove(poolee.gameObject);
            poolee.gameObject.SetActive(false);
            poolee.transform.SetParent(transform);
        }

        private void Grow(int size)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject clone = Instantiate(_prefab, transform);
                clone.SetActive(false);
                GUIPoolee poolee = clone.AddComponent<GUIPoolee>();
                poolee.SetParent(this);
                _inactiveObjects.Add(clone);
            }
        }
        [HideFromIl2Cpp]
        private GameObject GetFirst(List<GameObject> list)
        {
            GameObject found = null;

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].activeInHierarchy)
                {
                    found = list[i];
                    break;
                }
            }

            return found;
        }
    }
}
