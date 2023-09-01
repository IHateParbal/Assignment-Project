using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using Microsoft.Win32;
using System.Diagnostics;

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for Contracts.xaml
    /// </summary>
    public partial class Contracts : Page
    {
        int stringLength = 10;
        string selectedOption;
        string DecryptedFile;
        string selectedOptionDec;
        string encryptedFile;
        private static readonly Random random = new Random();
        private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateRandomString(int length)
        {
            return new string(Enumerable.Repeat(CharSet, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Contracts()
        {
            InitializeComponent();
        }

        private void decryptionmethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedOptionDec = selectedItem.Content.ToString();
        }

        private void Encryptionmethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedOption = selectedItem.Content.ToString();
        }

        private void EncryptFileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeafultpathButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void pathButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DecryptFileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        static void EncryptFile3DES(string inputFile, string outputFile, string userKey)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                // Ensure the key is at least 24 characters
                byte[] keyBytes = Encoding.UTF8.GetBytes(userKey.PadRight(24, '\0').Substring(0, 24));

                tripleDes.Key = keyBytes;
                tripleDes.GenerateIV();

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform encryptor = tripleDes.CreateEncryptor())
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsOutput.Write(tripleDes.IV, 0, tripleDes.IV.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void EncryptFileAES(string inputFile, string outputFile, string userKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Ensure the key is at least 16 characters
                byte[] keyBytes = Encoding.UTF8.GetBytes(userKey.PadRight(16, '\0').Substring(0, 16));

                aesAlg.Key = keyBytes;
                aesAlg.GenerateIV();

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsOutput.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFile3DES(string encryptedFile, string decryptedFile, string userKey)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                // Ensure the key is at least 24 characters
                byte[] keyBytes = Encoding.UTF8.GetBytes(userKey.PadRight(24, '\0').Substring(0, 24));

                byte[] iv = new byte[8];
                using (FileStream fsEncrypted = new FileStream(encryptedFile, FileMode.Open))
                {
                    fsEncrypted.Read(iv, 0, iv.Length);
                }

                tripleDes.Key = keyBytes;
                tripleDes.IV = iv;

                using (FileStream fsEncrypted = new FileStream(encryptedFile, FileMode.Open))
                using (FileStream fsDecrypted = new FileStream(decryptedFile, FileMode.Create))
                using (ICryptoTransform decryptor = tripleDes.CreateDecryptor())
                using (CryptoStream csDecrypt = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    fsEncrypted.Seek(8, SeekOrigin.Begin); // Skip the IV

                    while ((bytesRead = fsEncrypted.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csDecrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFileAES(string encryptedFile, string decryptedFile, string userKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Ensure the key is at least 16 characters
                byte[] keyBytes = Encoding.UTF8.GetBytes(userKey.PadRight(16, '\0').Substring(0, 16));

                byte[] iv = new byte[aesAlg.BlockSize / 8];
                using (FileStream fsEncrypted = new FileStream(encryptedFile, FileMode.Open))
                {
                    fsEncrypted.Read(iv, 0, iv.Length);
                }

                aesAlg.Key = keyBytes;
                aesAlg.IV = iv;

                using (FileStream fsEncrypted = new FileStream(encryptedFile, FileMode.Open))
                using (FileStream fsDecrypted = new FileStream(decryptedFile, FileMode.Create))
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                using (CryptoStream csDecrypt = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    fsEncrypted.Seek(iv.Length, SeekOrigin.Begin); // Skip the IV

                    while ((bytesRead = fsEncrypted.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csDecrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

    }
}
