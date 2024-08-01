namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public class BackspaceKey : Key
    {
        public BackspaceKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            string text = _keyboard.InputField.text;

            if (text.Length > 0)
            {
                _keyboard.InputField.text = text.Remove(text.Length - 1);
            }
        }
    }
}