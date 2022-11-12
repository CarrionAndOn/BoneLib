﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

namespace BoneLib.BoneMenu.UI
{
    /// <summary>
    /// The UI manager's purpose is to tie the internal menu together with the UI.
    /// It can show/hide pages, go back to pages, and add/remove elements.
    /// </summary>
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIManager : MonoBehaviour
    {
        public UIManager(System.IntPtr ptr) : base(ptr) { }

        public static UIManager Instance { get; private set; }

        public UIPool PagePool { get; private set; }
        public UIPool CategoryPool { get; private set; }
        public UIPool FunctionPool { get; private set; }
        public UIPool ValuePool { get; private set; }
        public UIPool DropdownPool { get; private set; }
        public UIPool TogglePool { get; private set; }

        public UIPage MainPage;

        private GameObject pagePool;
        private GameObject categoryPool;
        private GameObject functionPool;
        private GameObject valuePool;
        private GameObject dropdownPool;
        private GameObject togglePool;

        private void Awake()
        {
            Instance = this;

            gameObject.AddComponent<Canvas>();
            gameObject.AddComponent<GraphicRaycaster>();

            SetupPools();

            MainPage = PagePool.Spawn(DataManager.UI.PanelView.transform, false).GetComponent<UIPage>();
            DataManager.UI.Init();
        }

        private void Update()
        {
            MainPage.transform.position = DataManager.UI.PanelView.transform.position;
            MainPage.transform.rotation = DataManager.UI.PanelView.transform.rotation;
        }

        private void OnEnable()
        {
            MenuManager.OnCategoryCreated += OnCategoryCreated;
            MenuManager.OnCategorySelected += OnCategorySelected;
        }

        private void OnDisable()
        {
            MenuManager.OnCategoryCreated -= OnCategoryCreated;
            MenuManager.OnCategorySelected -= OnCategorySelected;
        }

        public void OnCategoryCreated(MenuCategory category)
        {
            if (category == null)
            {
                return;
            }
        }

        public void OnCategorySelected(MenuCategory category)
        {
            if (category == null)
            {
                return;
            } 

            MelonLoader.MelonLogger.Msg("Invoked");

            MainPage.AssignElement(category);
            MainPage.Draw();
            MainPage.gameObject.SetActive(true);
        }

        private void SetupPools()
        {
            pagePool = new GameObject("Page Pool");
            categoryPool = new GameObject("Category Pool");
            functionPool = new GameObject("Function Pool");
            valuePool = new GameObject("Value Pool");
            dropdownPool = new GameObject("Dropdown Pool");
            togglePool = new GameObject("Toggle Pool");

            pagePool.transform.SetParent(gameObject.transform);
            categoryPool.transform.SetParent(gameObject.transform);
            functionPool.transform.SetParent(gameObject.transform);
            valuePool.transform.SetParent(gameObject.transform);
            dropdownPool.transform.SetParent(gameObject.transform);
            togglePool.transform.SetParent(gameObject.transform);

            PagePool = pagePool?.AddComponent<UIPool>();
            CategoryPool = categoryPool?.AddComponent<UIPool>();
            FunctionPool = functionPool?.AddComponent<UIPool>();
            ValuePool = valuePool?.AddComponent<UIPool>();
            DropdownPool = dropdownPool?.AddComponent<UIPool>();
            TogglePool = togglePool?.AddComponent<UIPool>();

            PagePool.SetCount(2);
            CategoryPool.SetCount(6);
            FunctionPool.SetCount(6);
            ValuePool.SetCount(6);
            DropdownPool.SetCount(6);
            TogglePool.SetCount(6);

            PagePool.SetPrefab(DataManager.UI.PagePrefab);
            CategoryPool.SetPrefab(DataManager.UI.CategoryPrefab);
            FunctionPool.SetPrefab(DataManager.UI.FunctionPrefab);
            ValuePool.SetPrefab(DataManager.UI.ValuePrefab);
            DropdownPool.SetPrefab(DataManager.UI.DropdownPrefab);
            TogglePool.SetPrefab(DataManager.UI.TogglePrefab);

            PagePool.Populate(PagePool.Count);
            CategoryPool.Populate(CategoryPool.Count);
            FunctionPool.Populate(FunctionPool.Count);
            ValuePool.Populate(ValuePool.Count);
            DropdownPool.Populate(ValuePool.Count);
            TogglePool.Populate(ValuePool.Count);
        }
    }
}
