using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Servcies
{
    public class Win32IoService : IIoService
    {
        private string _lastFileName = null;
        public string Read()
        {
            var ofd = new OpenFileDialog
            {
                Filter = "JSON files (.json)|*.json|All Files|*.*",
                DefaultExt = ".json",
                FileName =_lastFileName != null ? Path.GetFileName(_lastFileName) : "file.json",
                InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory,
            };

            var res = ofd.ShowDialog();
            if (res == true)
            {
                try
                {
                    string result = null;
                    _lastFileName = ofd.FileName;
                    using(var reader = new StreamReader(ofd.FileName))
                    {
                        result = reader.ReadToEnd();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            return null;
        }
    }
}
