using System;

using Il2CppInterop.Runtime.Attributes;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class PageLinkElement : FunctionElement
    {
        public PageLinkElement(string name, Color color, Action callback) : base(name, color, callback) { }

        public Page LinkedPage
        {
            get
            {
                return _linkedPage;
            }
            private set
            {
                _linkedPage = value;
                OnElementChanged.InvokeActionSafe();
            }
        }

        private Page _linkedPage;

        public void AssignPage(Page page)
        {
            LinkedPage = page;

            Menu.OnPageUpdated += OnPageUpdated;
        }

        public override void OnElementRemoved()
        {
            base.OnElementRemoved();

            if (_linkedPage != null)
            {
                Menu.OnPageUpdated -= OnPageUpdated;

                LinkedPage = null;
            }
        }

        [HideFromIl2Cpp]
        private void OnPageUpdated(Page page)
        {
            if (page != LinkedPage)
            {
                return;
            }

            ElementName = page.Name;
            ElementColor = page.Color;
        }
    }
}