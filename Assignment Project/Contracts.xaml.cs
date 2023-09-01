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

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for Contracts.xaml
    /// </summary>
    public partial class Contracts : Page
    {
        int stringLength = 10;
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
            /*string File = "input.txt";
            string encryptedFile = "encrypted.enc";
            string key = "yourSecretKey"; 

            EncryptFile(File, encryptedFile, key); 
            DecryptFile(encryptedFile, File, key);
            EncryptFile3DES(encryptedFile, File, key);
            DecryptFile3DES(encryptedFile, File, key);*/

        }

        private void decryptionmethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string File = InputEncryptedFile.Text;
            string DecryptedFile = DecryptingFile.Text;
            string key = DecryptedKey.Password;
            DecryptFile(DecryptedFile, File, key);
            DecryptFile3DES(DecryptedFile, File, key);
        }

        private void Encryptionmethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string File = InputEncryptedFile.Text;
            string encryptedFile = GenerateRandomString(stringLength);
            string key = DecryptedKey.Password;
            EncryptFile(encryptedFile, File, key);
            EncryptFile3DES(encryptedFile, File, key);
        }

        static void EncryptFile(string inputFile, string outputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV();

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsOutput.Write(aesAlg.IV, 0, aesAlg.IV.Length); // Write IV to the beginning of the file

                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFile(string encryptedFile, string decryptedFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] encryptedBytes = File.ReadAllBytes(encryptedFile);

                byte[] iv = new byte[aesAlg.BlockSize / 8];
                Array.Copy(encryptedBytes, iv, iv.Length);

                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv;

                using (MemoryStream msInput = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                using (FileStream fsOutput = new FileStream(decryptedFile, FileMode.Create))
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (CryptoStream csDecrypt = new CryptoStream(msInput, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsOutput.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void EncryptFile3DES(string inputFile, string outputFile, string key)
        {
            using (TripleDESCryptoServiceProvider desAlg = new TripleDESCryptoServiceProvider())
            {
                desAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                desAlg.GenerateIV();

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform encryptor = desAlg.CreateEncryptor(desAlg.Key, desAlg.IV))
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    fsOutput.Write(desAlg.IV, 0, desAlg.IV.Length); // Write IV to the beginning of the file

                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        csEncrypt.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        static void DecryptFile3DES(string inputFile, string outputFile, string key)
        {
            using (TripleDESCryptoServiceProvider desAlg = new TripleDESCryptoServiceProvider())
            {
                byte[] encryptedBytes = File.ReadAllBytes(inputFile);

                byte[] iv = new byte[desAlg.BlockSize / 8];
                Array.Copy(encryptedBytes, iv, iv.Length);

                desAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                desAlg.IV = iv;

                using (MemoryStream msInput = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (ICryptoTransform decryptor = desAlg.CreateDecryptor(desAlg.Key, desAlg.IV))
                using (CryptoStream csDecrypt = new CryptoStream(msInput, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = csDecrypt.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsOutput.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

    }
}
