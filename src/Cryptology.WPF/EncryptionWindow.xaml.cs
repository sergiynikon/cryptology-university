using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cryptology.Domain;
using Cryptology.Domain.Abstract;
using Cryptology.Domain.Ciphers;
using Cryptology.WPF.Helpers;
using AppDomain = System.AppDomain;

namespace Cryptology.WPF
{
    /// <summary>
    /// Interaction logic for EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        private readonly CipherProvider _cipherProvider;
        private readonly FileProvider _decryptedTextFileProvider = new FileProvider();
        private readonly FileProvider _encryptedTextFileProvider = new FileProvider();

        public EncryptionWindow()
        {
            InitializeComponent();
            LoadCipherNames();
            _cipherProvider = CipherProvider.CreateDefault();
            CipherProviderComboBox.SelectedItem = CipherProvider.DefaultCipherName;
            CipherProviderComboBox.ItemsSource = CipherProviderNames;
        }

        public ObservableCollection<string> CipherProviderNames { get; set; }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (DecryptedTextTextBox.Text == string.Empty)
            {
                return;
            }

            _cipherProvider.Key = KeyTextBox.Text;
            _cipherProvider.Message = DecryptedTextTextBox.Text;
            try
            {
                EncryptedTextTextBox.Text = _cipherProvider.Encrypt();
                DecryptedTextTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (EncryptedTextTextBox.Text == string.Empty)
            {
                return;
            }

            _cipherProvider.Key = KeyTextBox.Text;
            _cipherProvider.Message = EncryptedTextTextBox.Text;
            try
            {
                DecryptedTextTextBox.Text = _cipherProvider.Decrypt();
                EncryptedTextTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DecryptedTextTextBox.Text = string.Empty;
            EncryptedTextTextBox.Text = string.Empty;
            KeyTextBox.Text = string.Empty;
        }

        private void CipherProviderComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cipherName = e.AddedItems[0].ToString();
            SetCipher(cipherName);
        }

        private void LoadCipherNames()
        {
            var cipherNames = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ICipher).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => t.Name)
                .ToList();

            CipherProviderNames = new ObservableCollection<string>(cipherNames);
        }

        private void SetCipher(string cipherName)
        {
            var objectToInstantiate = $"Cryptology.Domain.Ciphers.{cipherName}, Cryptology.Domain";
            var objectType = Type.GetType(objectToInstantiate);
            var instantiatedObject = Activator.CreateInstance(objectType) as ICipher;
            _cipherProvider.SetCipher(instantiatedObject);
        }

        private void OpenFileDecryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            var textFromFile = _decryptedTextFileProvider.GetTextFromFile();
            DecryptedTextTextBox.Text = textFromFile;
        }

        private void OpenFileEncryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            var textFromFile = _encryptedTextFileProvider.GetTextFromFile();
            EncryptedTextTextBox.Text = textFromFile;
        }

        private void SaveToFileDecryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            var textToSave = DecryptedTextTextBox.Text;
            _decryptedTextFileProvider.SaveTextToFile(textToSave);
            DecryptedTextTextBox.Text = string.Empty;
        }

        private void SaveToFileEncryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            var textToSave = EncryptedTextTextBox.Text;
            _encryptedTextFileProvider.SaveTextToFile(textToSave);
            EncryptedTextTextBox.Text = string.Empty;
        }

        private void SetFileDecryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            _decryptedTextFileProvider.SetFileName();
        }

        private void SetFileEncryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            _encryptedTextFileProvider.SetFileName();
        }
    }
}
