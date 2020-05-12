using System.IO;
using System.Reflection;

namespace ExtensionSugar.AspNetCore
{
    public static class ResourceExtSugars
    {
        public static string Read(ReadType rdType, string resourceName)
        {
            Assembly assembly = null;
            switch (rdType)
            {
                case ReadType.Calling:
                    assembly = Assembly.GetCallingAssembly();
                    break;
                case ReadType.Entry:
                    assembly = Assembly.GetEntryAssembly();
                    break;
                case ReadType.Executing:
                    assembly = Assembly.GetExecutingAssembly();
                    break;
                default:
                    break;
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string Read(Assembly oAsmp, string resourceName)
        {
            using (Stream stream = oAsmp.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public enum ReadType
    {
        Calling = 0,
        Entry = 1,
        Executing = 2
    }
}
