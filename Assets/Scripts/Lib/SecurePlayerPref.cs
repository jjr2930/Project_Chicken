using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class SecurePlayerPref
{
    static byte[] _secretKey = null;
    static byte[] _secretIV = null;
    static bool _bIsInit = false;
    static Aes _aes = null;

    static public void SetInt(string key, int value)
    {
        PlayerPrefs.SetString(key, Encryt(value.ToString()));   
    }

    static public void SetBool(string key, bool value)
    {
        PlayerPrefs.SetString(key, Encryt(value.ToString()));
    }

    static public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, Encryt(value));
    }

    static public int GetInt(string key)
    {
        string cipherText = PlayerPrefs.GetString(key);

        string plainText = Decrypt(cipherText);

        return int.Parse(plainText);
    }

    static public bool GetBool(string key)
    {
        string cipherText = PlayerPrefs.GetString(key);

        string plainText = Decrypt(cipherText);

        return bool.Parse(plainText);
    }

    static public string GetString(string key)
    {
        string cipherText = PlayerPrefs.GetString(key);

        string plainText = Decrypt(cipherText);

        return plainText;
    }

    static public void Save()
    {
        PlayerPrefs.Save();
    }

    static private void Init()
    {
        if (_bIsInit)
        {
            return;
        }
        //initialize in here
        //set scret key and iv

        //this time using test code
        _aes = Aes.Create();
        _aes.KeySize = 256;
        _aes.IV = _secretIV;
        _aes.Key = _secretKey;
        _aes.Mode = CipherMode.CBC;
        _aes.Padding = PaddingMode.PKCS7;

        _bIsInit = true;

    }
    static private string Encryt(string plainText)
    {
        string result = "";

        byte[] bytes = StringToByte(plainText);

        // Create a decrytor to perform the stream transform.
        ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);
        // Create the streams used for encryption.
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(bytes);
                }
                bytes = msEncrypt.ToArray();
            }
        }

        result = ByteToString(bytes);
        return result;
    }

    static private string Decrypt(string cipherText)
    {
        string result = "";

        byte[] bytes = StringToByte(cipherText);

        var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

        using (MemoryStream msDecrypt = new MemoryStream(bytes))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    result = srDecrypt.ReadToEnd();
                }
            }
        }

        return result;
    }


    static byte[] StringToByte(string value)
    {
        byte[] result = Encoding.UTF8.GetBytes(value);
        return result;
    }

    static string ByteToString(byte[] value)
    {
        string result = Encoding.UTF8.GetString(value);
        return result;
    }
}
