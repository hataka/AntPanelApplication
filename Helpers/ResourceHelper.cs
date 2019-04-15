using System;
using System.IO;
using System.Reflection;

namespace AntPanelApplication.Helpers
{
    class ResourceHelper
    {
        /// <summary>
        /// Gets the specified resource as an stream
        /// </summary> 
        public static Stream GetStream(String name)
        {
            String prefix = "AntPanelApplication.Resources.";
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(prefix + name);
        }

    }

}