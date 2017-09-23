using System.Collections.Generic;
using Microsoft.Win32;
using Models;

namespace Business.Managers
{
    public static class Registry
    {
        public static RegistryKeyValue Get(string regKey, string name)
        {
            var value = new RegistryKeyValue();
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(regKey);
            if (key != null)
            {
                value.Kind = key.GetValueKind(name);
                value.KeyValueName = name;
                switch (value.Kind)
                {
                    case RegistryValueKind.MultiString:
                        value.MultiString = (string[])key.GetValue(name);
                        break;
                    case RegistryValueKind.Binary:
                        value.Binary = (byte[])key.GetValue(name);
                        break;
                    default:
                        value.String = key.GetValue(name).ToString();
                        break;
                }
                key.Close();
            }
            return value;
        }
        public static List<RegistryKeyValue> GetAll(string regKeyDomain)
        {
            var value = new List<RegistryKeyValue>();
            RegistryKey keyDomain = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(regKeyDomain);

            if (keyDomain != null)
            {
                var names = keyDomain.GetValueNames();
                foreach (var name in names)
                {
                    var item = new RegistryKeyValue();
                    item.Kind = keyDomain.GetValueKind(name);
                    item.KeyValueName = name;
                    switch (item.Kind)
                    {
                        case RegistryValueKind.MultiString:
                            item.MultiString = (string[])keyDomain.GetValue(name);
                            break;
                        case RegistryValueKind.Binary:
                            item.Binary = (byte[])keyDomain.GetValue(name);
                            break;
                        default:
                            item.String = keyDomain.GetValue(name).ToString();
                            break;
                    }
                    keyDomain.Close();
                    value.Add(item);
                }
            }

            return value;
        }

        public static Result Save(string regKey, string name, string value)
        {
            return SaveKey(regKey, name, value, RegistryValueKind.String);
        }
        public static Result Save(string regKey, string name, string[] value)
        {
            return SaveKey(regKey, name, value, RegistryValueKind.MultiString);
        }
        public static Result Save(string regKey, string name, byte[] value)
        {
            return SaveKey(regKey, name, value, RegistryValueKind.Binary);
        }
        public static Result Save(string regKey, string name, object value, RegistryValueKind kind)
        {
            return SaveKey(regKey, name, value, kind);
        }

        #region Helpers

        private static Result SaveKey(string regKey, string name, object value, RegistryValueKind kind)
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(regKey);
            if (key != null)
            {
                // blank or empty is for default
                key.SetValue(name, value, kind);
                key.Close();
            }
            return new Models.Result();
        }

        #endregion
    }
}
