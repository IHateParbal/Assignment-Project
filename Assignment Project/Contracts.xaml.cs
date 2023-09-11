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
using System.Diagnostics;

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for Contracts.xaml
    /// </summary>
    public partial class Contracts : Page
    {
        string selectedOptionDec;
        string selectedOption;
        private static readonly Random random = new Random();
        private const string CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

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

        public static string GenerateRandomString(int length)
        {
            return new string(Enumerable.Repeat(CharSet, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void fileSelectionDec_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.thishasbeenencryptedsodontworrylmao)|*.thishasbeenencryptedsodontworrylmao"; 

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                string selectedFileNameDec = System.IO.Path.GetFileName(selectedFilePath);

                InputFileDec.Text = selectedFileNameDec;
            }
        }

        private void DeafultpathButtonDec_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = $@"C:\Users\madgw\source\repos\Assignment Project\Assignment Project\bin\Debug\{GenerateRandomString(8)}.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                
                string Def_selectedFilePathDec = saveFileDialog.FileName;

               
                DeafultPathDec.Text = System.IO.Path.GetFileName(Def_selectedFilePathDec);
            }
        }

        private void EncryptFileButtonDec_Click(object sender, RoutedEventArgs e)
        {
            string InputFile = InputFileDec.Text;
            string key = EncryptedKeyDec.Password;
            string OutputFile =  DeafultPathDec.Text;
            if (selectedOptionDec == "AES")
            {
                DecryptFileAES(InputFile, OutputFile, key);
            }
            else
            {
                DecryptFile3DES(InputFile, OutputFile, key);
            }
        }

        private void fileSelection_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                string selectedFileName = System.IO.Path.GetFileName(selectedFilePath);

                InputFileEnc.Text = selectedFileName;

            }
        }

        private void DeafultpathButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = $@"C:\Users\madgw\source\repos\Assignment Project\Assignment Project\bin\Debug\{GenerateRandomString(8)}.thishasbeenencryptedsodontworrylmao";

            if (saveFileDialog.ShowDialog() == true)
            {

                string selectedFilePath = saveFileDialog.FileName;


                DeafultPathDec.Text = System.IO.Path.GetFileName(selectedFilePath);
            }
        }

        private void EncryptFileButton_Click(object sender, RoutedEventArgs e)
        {
            string InputFile = InputFileEnc.Text;
            string key = EncryptedKey.Password;
            string OutputFile = DeafultPathEnc.Text;
            if (selectedOptionDec == "AES")
            {
                EncryptFileAES(InputFile, OutputFile, key);
            }
            else
            {
                EncryptFile3DES(InputFile, OutputFile, key);
            }
        }
    }
}
