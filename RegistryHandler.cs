namespace TrainValley2_Profile_Manager
{
    using Microsoft.Win32;

    public static class RegistryHandler
    {
        private const string PATH = @"Software\Oroboro games\TrainValley2";
        private const string NAME = @"SavedData.Default_h932327797";

        public static byte[] Load()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(PATH))
            {
                if (key != null)
                {
                    return key.GetValue(NAME) as byte[];
                }
            }

            return null;
        }

        public static void Save(byte[] content)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(PATH, true))
            {
                if (content != null)
                {
                    key.SetValue(NAME, content);
                }
                else
                {
                    key.DeleteValue(NAME);
                }
            }
        }
    }
}
