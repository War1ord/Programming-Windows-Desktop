using Microsoft.Win32;

namespace Models
{
    public struct RegistryKeyValue
    {
        public RegistryValueKind Kind { get; set; }
        public string KeyValueName { get; set; }
        public bool IsMultiString { get { return Kind == RegistryValueKind.MultiString; } }
        public bool IsBinary { get { return Kind == RegistryValueKind.Binary; } }
        public bool IsString { get { return Kind == RegistryValueKind.String; } }
        public string[] MultiString { get; set; }
        public byte[] Binary { get; set; }
        public string String { get; set; }
    }
}