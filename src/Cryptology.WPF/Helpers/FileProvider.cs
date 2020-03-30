using Microsoft.Win32;
using System.IO;

namespace Cryptology.WPF.Helpers
{
    public class FileProvider
    {
        public string FileName { get; set; }

        public string GetTextFromFile()
        {
            if (FileName == null)
            {
                SetFileName();
            }

            var fileText = File.ReadAllText(FileName);

            return fileText;
        }

        public void SaveTextToFile(string content)
        {
            if (FileName == null)
            {
                SetFileName();
            }

            File.WriteAllText(FileName, content);
        }
        public void SetFileName()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }
    }
}