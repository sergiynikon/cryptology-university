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
using AppDomain = System.AppDomain;

namespace Cryptology.WPF
{
    /// <summary>
    /// Interaction logic for EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        private readonly CipherProvider _cipherProvider;
        public EncryptionWindow()
        {
            InitializeComponent();
            _cipherProvider = new CipherProvider(new DefaultCipher());
            LoadCipherNames();
            CipherProviderComboBox.SelectedItem = nameof(DefaultCipher);
            CipherProviderComboBox.ItemsSource = CipherProviderNames;
        }

        public ObservableCollection<string> CipherProviderNames { get; set; }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (EncryptTextBox.Text == string.Empty)
            {
                return;
            }

            _cipherProvider.Key = KeyTextBox.Text;
            _cipherProvider.Message = EncryptTextBox.Text;
            DecryptTextBox.Text = _cipherProvider.Encrypt();
            EncryptTextBox.Text = string.Empty;
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (DecryptTextBox.Text == string.Empty)
            {
                return;
            }

            _cipherProvider.Key = KeyTextBox.Text;
            _cipherProvider.Message = DecryptTextBox.Text;
            EncryptTextBox.Text = _cipherProvider.Decrypt();
            DecryptTextBox.Text = string.Empty;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            EncryptTextBox.Text = string.Empty;
            DecryptTextBox.Text = string.Empty;
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
    }
}
