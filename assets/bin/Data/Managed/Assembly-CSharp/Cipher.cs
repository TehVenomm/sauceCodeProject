// Decompiled with JetBrains decompiler
// Type: Cipher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
public class Cipher
{
  public const string CRYPT_HASH_KEY = "$-5as;hgfm,vgs^*;fd@345-9zds3k5p";
  public const string CRYPT_IV_128 = "8)&#$.Dtsf7%od;.";
  public const string DEFAULT_NETWORKHASH = "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";
  public const string DEFAULT_IV_128 = "yCNBH$$rCNGvC+#f";
  public const int SIGNATURE_LENGTH = 256 /*0x0100*/;
  private const string KEY_FACTORY_ALGORITHM = "RSA";
  private const string SIGNATURE_ALGORITHM = "Sha256WithRSA";
  private const string PUBLIC_KEY_STRING = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy0BC0jB+o9MPpjUJbRKba1z76SaKUeNuE9y5MGFrZbKFPo9dW7Aor8hUdOSk1eSJzuiQktKSEWhvGEfdH0bPb18s53GOHb2rJFA3KcHa58+HItorJUADXbK5mL0TCa4TznxOB/c0gEdZgLZN7aHMDX8Sy32HoVu5Ub0RXUQfrlY+jUEqUXI+Jieg2D2Xgv1qRWTl+RTHJ8oagZk5O5KH+1A6PBG4mJGeWoG7CPpkynvtNo1q3IeIXR/Vwi12InaIAjCfLHsq5LmSzw3rDmUdUZxeO9AnzFHhIA9WVVhjfxgL5QH9OEBdXFva1lr0e6Vaur/TZxl4zRjjg/v45Pp2NQIDAQAB";
  private const string TABLE_PUBLIC_KEY_STRING = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwynqwPzQ33ddKb/oolirybF75lSAodzU7Myxyj7snavvYl15qzbLXRwfK5OqZS1ke7Yc0s1m8EGodkN/m3Xg4/7AKo7GtPSh3VwbTKbTPEn86Es2FG28BDOXUlXf8P4lvCaB/7JAast7JDzZl3jEp3m9ktAOBgg/zeh/W72sAwA4EUuf0MhFalJvpLYjIXD2sM138aKIXIcF8m4nSUAP0ti0iCskjfEUGAfyK9nq/S19RjuWGOI76QnUhn++NNcSl5KMGSf9iXnohuIpFUn/vQkKnLVAbXUhLCxA+LrGYGnk65hiJcCYohdIJqCjKIx3P2XB5a05tr+g24H2KmjAMQIDAQAB";

  public static string EncryptRJ128(string prm_key, string prm_iv, string prm_text_to_encrypt)
  {
    return Cipher.EncryptRJ128Byte(prm_key, prm_iv, Encoding.UTF8.GetBytes(prm_text_to_encrypt));
  }

  public static string EncryptRJ128Byte(string prm_key, string prm_iv, byte[] toEncrypt)
  {
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.Padding = PaddingMode.PKCS7;
    rijndaelManaged.Mode = CipherMode.CBC;
    rijndaelManaged.KeySize = 256 /*0x0100*/;
    rijndaelManaged.BlockSize = 128 /*0x80*/;
    ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(Encoding.UTF8.GetBytes(prm_key), Encoding.UTF8.GetBytes(prm_iv));
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
    cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
    cryptoStream.FlushFinalBlock();
    return Convert.ToBase64String(memoryStream.ToArray());
  }

  public static string DecryptRJ128(string prm_key, string prm_iv, string prm_text_to_decrypt)
  {
    byte[] bytes = Cipher.DecryptRJ128Byte(prm_key, prm_iv, prm_text_to_decrypt);
    return bytes == null ? (string) null : Encoding.UTF8.GetString(bytes);
  }

  public static byte[] DecryptRJ128Byte(string prm_key, string prm_iv, string prm_text_to_decrypt)
  {
    string s = prm_text_to_decrypt;
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.Padding = PaddingMode.PKCS7;
    rijndaelManaged.Mode = CipherMode.CBC;
    rijndaelManaged.KeySize = 256 /*0x0100*/;
    rijndaelManaged.BlockSize = 128 /*0x80*/;
    ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(Encoding.UTF8.GetBytes(prm_key), Encoding.UTF8.GetBytes(prm_iv));
    byte[] buffer = Convert.FromBase64String(s);
    byte[] array = new byte[buffer.Length];
    int newSize = new CryptoStream((Stream) new MemoryStream(buffer), decryptor, CryptoStreamMode.Read).Read(array, 0, array.Length);
    Array.Resize<byte>(ref array, newSize);
    return array;
  }

  public static bool verifyBytes(Stream signedDataStream, byte[] signature)
  {
    RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter((AsymmetricAlgorithm) Cipher.DecodeX509PublicKey(Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwynqwPzQ33ddKb/oolirybF75lSAodzU7Myxyj7snavvYl15qzbLXRwfK5OqZS1ke7Yc0s1m8EGodkN/m3Xg4/7AKo7GtPSh3VwbTKbTPEn86Es2FG28BDOXUlXf8P4lvCaB/7JAast7JDzZl3jEp3m9ktAOBgg/zeh/W72sAwA4EUuf0MhFalJvpLYjIXD2sM138aKIXIcF8m4nSUAP0ti0iCskjfEUGAfyK9nq/S19RjuWGOI76QnUhn++NNcSl5KMGSf9iXnohuIpFUn/vQkKnLVAbXUhLCxA+LrGYGnk65hiJcCYohdIJqCjKIx3P2XB5a05tr+g24H2KmjAMQIDAQAB")));
    signatureDeformatter.SetHashAlgorithm("SHA256");
    byte[] rgbHash = Cipher.SHA256HashStream(signedDataStream);
    bool flag = false;
    if (signatureDeformatter.VerifySignature(rgbHash, signature))
      flag = true;
    return flag;
  }

  public static bool verify(string signedData, string base64Signature)
  {
    byte[] rgbSignature = Convert.FromBase64String(base64Signature);
    RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter((AsymmetricAlgorithm) Cipher.DecodeX509PublicKey(Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy0BC0jB+o9MPpjUJbRKba1z76SaKUeNuE9y5MGFrZbKFPo9dW7Aor8hUdOSk1eSJzuiQktKSEWhvGEfdH0bPb18s53GOHb2rJFA3KcHa58+HItorJUADXbK5mL0TCa4TznxOB/c0gEdZgLZN7aHMDX8Sy32HoVu5Ub0RXUQfrlY+jUEqUXI+Jieg2D2Xgv1qRWTl+RTHJ8oagZk5O5KH+1A6PBG4mJGeWoG7CPpkynvtNo1q3IeIXR/Vwi12InaIAjCfLHsq5LmSzw3rDmUdUZxeO9AnzFHhIA9WVVhjfxgL5QH9OEBdXFva1lr0e6Vaur/TZxl4zRjjg/v45Pp2NQIDAQAB")));
    signatureDeformatter.SetHashAlgorithm("SHA256");
    byte[] rgbHash = Cipher.SHA256Hash(signedData);
    bool flag = false;
    if (signatureDeformatter.VerifySignature(rgbHash, rgbSignature))
      flag = true;
    return flag;
  }

  public static byte[] SHA256Hash(string text)
  {
    byte[] bytes = Encoding.UTF8.GetBytes(text);
    try
    {
      return SHA256.Create().ComputeHash(bytes);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.NETWORK, "SHA256.ComputHash failed");
      return (byte[]) null;
    }
  }

  public static byte[] SHA256HashStream(Stream stream)
  {
    try
    {
      return SHA256.Create().ComputeHash(stream);
    }
    catch (Exception ex)
    {
      Log.Error(LOG.NETWORK, "SHA256.ComputHash failed");
      return (byte[]) null;
    }
  }

  private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
  {
    byte[] b = new byte[15]
    {
      (byte) 48 /*0x30*/,
      (byte) 13,
      (byte) 6,
      (byte) 9,
      (byte) 42,
      (byte) 134,
      (byte) 72,
      (byte) 134,
      (byte) 247,
      (byte) 13,
      (byte) 1,
      (byte) 1,
      (byte) 1,
      (byte) 5,
      (byte) 0
    };
    byte[] numArray1 = new byte[15];
    BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(x509key));
    try
    {
      switch (binaryReader.ReadUInt16())
      {
        case 33072:
          int num1 = (int) binaryReader.ReadByte();
          break;
        case 33328:
          int num2 = (int) binaryReader.ReadInt16();
          break;
        default:
          return (RSACryptoServiceProvider) null;
      }
      if (!Cipher.CompareBytearrays(binaryReader.ReadBytes(15), b))
        return (RSACryptoServiceProvider) null;
      switch (binaryReader.ReadUInt16())
      {
        case 33027:
          int num3 = (int) binaryReader.ReadByte();
          break;
        case 33283:
          int num4 = (int) binaryReader.ReadInt16();
          break;
        default:
          return (RSACryptoServiceProvider) null;
      }
      if (binaryReader.ReadByte() != (byte) 0)
        return (RSACryptoServiceProvider) null;
      switch (binaryReader.ReadUInt16())
      {
        case 33072:
          int num5 = (int) binaryReader.ReadByte();
          break;
        case 33328:
          int num6 = (int) binaryReader.ReadInt16();
          break;
        default:
          return (RSACryptoServiceProvider) null;
      }
      ushort num7 = binaryReader.ReadUInt16();
      byte num8 = 0;
      byte num9;
      switch (num7)
      {
        case 33026:
          num9 = binaryReader.ReadByte();
          break;
        case 33282:
          num8 = binaryReader.ReadByte();
          num9 = binaryReader.ReadByte();
          break;
        default:
          return (RSACryptoServiceProvider) null;
      }
      int int32 = BitConverter.ToInt32(new byte[4]
      {
        num9,
        num8,
        (byte) 0,
        (byte) 0
      }, 0);
      byte num10 = binaryReader.ReadByte();
      binaryReader.BaseStream.Seek(-1L, SeekOrigin.Current);
      if (num10 == (byte) 0)
      {
        int num11 = (int) binaryReader.ReadByte();
        --int32;
      }
      byte[] numArray2 = binaryReader.ReadBytes(int32);
      if (binaryReader.ReadByte() != (byte) 2)
        return (RSACryptoServiceProvider) null;
      int count = (int) binaryReader.ReadByte();
      byte[] numArray3 = binaryReader.ReadBytes(count);
      RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
      cryptoServiceProvider.ImportParameters(new RSAParameters()
      {
        Modulus = numArray2,
        Exponent = numArray3
      });
      return cryptoServiceProvider;
    }
    catch (Exception ex)
    {
      return (RSACryptoServiceProvider) null;
    }
    finally
    {
      binaryReader.Close();
    }
  }

  private static bool CompareBytearrays(byte[] a, byte[] b)
  {
    if (a.Length != b.Length)
      return false;
    int index = 0;
    foreach (int num in a)
    {
      if (num != (int) b[index])
        return false;
      ++index;
    }
    return true;
  }
}
