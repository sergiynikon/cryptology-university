using System.IO;
using Microsoft.Win32;

namespace Cryptology.WPF.Managers
{
    public class FileManager
    {
        public string FileName { get; set; }

        public string GetTextFromFile()
        {
            if (FileName == null)
            {
                SetFileName();
                if (FileName == null)
                {
                    return null;
                }
            }

            var fileText = File.ReadAllText(FileName);

            return fileText;
        }

        public void SaveTextToFile(string content)
        {
            if (FileName == null)
            {
                SetFileName();
                if (FileName == null)
                {
                    return;
                }
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