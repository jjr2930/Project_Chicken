using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class ScurePlayerPref
{
    static byte[] _secretKey;
    static byte[] _secretIV;
    static bool _bIsInit;
    static Aes _aes;
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

    static private string Encryt(string value)
    {
        string result = "";

        byte[] bytes = StringToByte(value);

        


        return result;
    }

    static private void Init()
    {
        //initialize in here
        //set scret key and iv

        //this time using test code
        _aes = Aes.Create();
    }


    static private void Decrypt()
    {

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
