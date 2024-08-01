using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public class GUIPoolee : MonoBehaviour
    {
        public GUIPoolee(System.IntPtr ptr) : base(ptr) { }

        private GUIPool _parent;

        public void SetParent(GUIPool parent)
        {
            _parent = parent;
        }

        public void Return()
        {
            _parent.OnReturn(this);
        }
    }
}

