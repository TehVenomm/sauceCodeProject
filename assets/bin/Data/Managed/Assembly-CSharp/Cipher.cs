using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Cipher
{
	public const string CRYPT_HASH_KEY = "$-5as;hgfm,vgs^*;fd@345-9zds3k5p";

	public const string CRYPT_IV_128 = "8)&#$.Dtsf7%od;.";

	public const string DEFAULT_NETWORKHASH = "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";

	public const string DEFAULT_IV_128 = "yCNBH$$rCNGvC+#f";

	public const int SIGNATURE_LENGTH = 256;

	private const string KEY_FACTORY_ALGORITHM = "RSA";

	private const string SIGNATURE_ALGORITHM = "Sha256WithRSA";

	private const string PUBLIC_KEY_STRING = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy0BC0jB+o9MPpjUJbRKba1z76SaKUeNuE9y5MGFrZbKFPo9dW7Aor8hUdOSk1eSJzuiQktKSEWhvGEfdH0bPb18s53GOHb2rJFA3KcHa58+HItorJUADXbK5mL0TCa4TznxOB/c0gEdZgLZN7aHMDX8Sy32HoVu5Ub0RXUQfrlY+jUEqUXI+Jieg2D2Xgv1qRWTl+RTHJ8oagZk5O5KH+1A6PBG4mJGeWoG7CPpkynvtNo1q3IeIXR/Vwi12InaIAjCfLHsq5LmSzw3rDmUdUZxeO9AnzFHhIA9WVVhjfxgL5QH9OEBdXFva1lr0e6Vaur/TZxl4zRjjg/v45Pp2NQIDAQAB";

	private const string TABLE_PUBLIC_KEY_STRING = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwynqwPzQ33ddKb/oolirybF75lSAodzU7Myxyj7snavvYl15qzbLXRwfK5OqZS1ke7Yc0s1m8EGodkN/m3Xg4/7AKo7GtPSh3VwbTKbTPEn86Es2FG28BDOXUlXf8P4lvCaB/7JAast7JDzZl3jEp3m9ktAOBgg/zeh/W72sAwA4EUuf0MhFalJvpLYjIXD2sM138aKIXIcF8m4nSUAP0ti0iCskjfEUGAfyK9nq/S19RjuWGOI76QnUhn++NNcSl5KMGSf9iXnohuIpFUn/vQkKnLVAbXUhLCxA+LrGYGnk65hiJcCYohdIJqCjKIx3P2XB5a05tr+g24H2KmjAMQIDAQAB";

	public static string EncryptRJ128(string prm_key, string prm_iv, string prm_text_to_encrypt)
	{
		return EncryptRJ128Byte(prm_key, prm_iv, Encoding.UTF8.GetBytes(prm_text_to_encrypt));
	}

	public static string EncryptRJ128Byte(string prm_key, string prm_iv, byte[] toEncrypt)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Padding = PaddingMode.PKCS7;
		rijndaelManaged.Mode = CipherMode.CBC;
		rijndaelManaged.KeySize = 256;
		rijndaelManaged.BlockSize = 128;
		RijndaelManaged rijndaelManaged2 = rijndaelManaged;
		byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(prm_iv);
		ICryptoTransform transform = rijndaelManaged2.CreateEncryptor(bytes, bytes2);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
		cryptoStream.FlushFinalBlock();
		byte[] inArray = memoryStream.ToArray();
		return Convert.ToBase64String(inArray);
	}

	public static string DecryptRJ128(string prm_key, string prm_iv, string prm_text_to_decrypt)
	{
		byte[] array = DecryptRJ128Byte(prm_key, prm_iv, prm_text_to_decrypt);
		if (array == null)
		{
			return null;
		}
		return Encoding.UTF8.GetString(array);
	}

	public static byte[] DecryptRJ128Byte(string prm_key, string prm_iv, string prm_text_to_decrypt)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Padding = PaddingMode.PKCS7;
		rijndaelManaged.Mode = CipherMode.CBC;
		rijndaelManaged.KeySize = 256;
		rijndaelManaged.BlockSize = 128;
		RijndaelManaged rijndaelManaged2 = rijndaelManaged;
		byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(prm_iv);
		ICryptoTransform transform = rijndaelManaged2.CreateDecryptor(bytes, bytes2);
		byte[] array = Convert.FromBase64String(prm_text_to_decrypt);
		byte[] array2 = new byte[array.Length];
		MemoryStream stream = new MemoryStream(array);
		CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
		int newSize = cryptoStream.Read(array2, 0, array2.Length);
		Array.Resize(ref array2, newSize);
		return array2;
	}

	public static bool verifyBytes(Stream signedDataStream, byte[] signature)
	{
		byte[] x509key = Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwynqwPzQ33ddKb/oolirybF75lSAodzU7Myxyj7snavvYl15qzbLXRwfK5OqZS1ke7Yc0s1m8EGodkN/m3Xg4/7AKo7GtPSh3VwbTKbTPEn86Es2FG28BDOXUlXf8P4lvCaB/7JAast7JDzZl3jEp3m9ktAOBgg/zeh/W72sAwA4EUuf0MhFalJvpLYjIXD2sM138aKIXIcF8m4nSUAP0ti0iCskjfEUGAfyK9nq/S19RjuWGOI76QnUhn++NNcSl5KMGSf9iXnohuIpFUn/vQkKnLVAbXUhLCxA+LrGYGnk65hiJcCYohdIJqCjKIx3P2XB5a05tr+g24H2KmjAMQIDAQAB");
		RSACryptoServiceProvider key = DecodeX509PublicKey(x509key);
		RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(key);
		rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA256");
		byte[] rgbHash = SHA256HashStream(signedDataStream);
		bool result = false;
		if (rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, signature))
		{
			result = true;
		}
		return result;
	}

	public static bool verify(string signedData, string base64Signature)
	{
		byte[] rgbSignature = Convert.FromBase64String(base64Signature);
		byte[] x509key = Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy0BC0jB+o9MPpjUJbRKba1z76SaKUeNuE9y5MGFrZbKFPo9dW7Aor8hUdOSk1eSJzuiQktKSEWhvGEfdH0bPb18s53GOHb2rJFA3KcHa58+HItorJUADXbK5mL0TCa4TznxOB/c0gEdZgLZN7aHMDX8Sy32HoVu5Ub0RXUQfrlY+jUEqUXI+Jieg2D2Xgv1qRWTl+RTHJ8oagZk5O5KH+1A6PBG4mJGeWoG7CPpkynvtNo1q3IeIXR/Vwi12InaIAjCfLHsq5LmSzw3rDmUdUZxeO9AnzFHhIA9WVVhjfxgL5QH9OEBdXFva1lr0e6Vaur/TZxl4zRjjg/v45Pp2NQIDAQAB");
		RSACryptoServiceProvider key = DecodeX509PublicKey(x509key);
		RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(key);
		rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA256");
		byte[] rgbHash = SHA256Hash(signedData);
		bool result = false;
		if (rSAPKCS1SignatureDeformatter.VerifySignature(rgbHash, rgbSignature))
		{
			result = true;
		}
		return result;
	}

	public static byte[] SHA256Hash(string text)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		byte[] array = null;
		try
		{
			SHA256 sHA = SHA256.Create();
			return sHA.ComputeHash(bytes);
		}
		catch (Exception)
		{
			Log.Error(LOG.NETWORK, "SHA256.ComputHash failed");
			return null;
		}
	}

	public static byte[] SHA256HashStream(Stream stream)
	{
		byte[] array = null;
		try
		{
			SHA256 sHA = SHA256.Create();
			return sHA.ComputeHash(stream);
		}
		catch (Exception)
		{
			Log.Error(LOG.NETWORK, "SHA256.ComputHash failed");
			return null;
		}
	}

	private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
	{
		byte[] b = new byte[15]
		{
			48,
			13,
			6,
			9,
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			1,
			1,
			5,
			0
		};
		byte[] array = new byte[15];
		MemoryStream input = new MemoryStream(x509key);
		BinaryReader binaryReader = new BinaryReader(input);
		byte b2 = 0;
		ushort num = 0;
		try
		{
			switch (binaryReader.ReadUInt16())
			{
			case 33072:
				binaryReader.ReadByte();
				break;
			case 33328:
				binaryReader.ReadInt16();
				break;
			default:
				return null;
			}
			array = binaryReader.ReadBytes(15);
			if (!CompareBytearrays(array, b))
			{
				return null;
			}
			switch (binaryReader.ReadUInt16())
			{
			case 33027:
				binaryReader.ReadByte();
				break;
			case 33283:
				binaryReader.ReadInt16();
				break;
			default:
				return null;
			}
			if (binaryReader.ReadByte() != 0)
			{
				return null;
			}
			switch (binaryReader.ReadUInt16())
			{
			case 33072:
				binaryReader.ReadByte();
				break;
			case 33328:
				binaryReader.ReadInt16();
				break;
			default:
				return null;
			}
			num = binaryReader.ReadUInt16();
			byte b3 = 0;
			byte b4 = 0;
			switch (num)
			{
			case 33026:
				b3 = binaryReader.ReadByte();
				break;
			case 33282:
				b4 = binaryReader.ReadByte();
				b3 = binaryReader.ReadByte();
				break;
			default:
				return null;
			}
			byte[] value = new byte[4]
			{
				b3,
				b4,
				0,
				0
			};
			int num2 = BitConverter.ToInt32(value, 0);
			byte b5 = binaryReader.ReadByte();
			binaryReader.BaseStream.Seek(-1L, SeekOrigin.Current);
			if (b5 == 0)
			{
				binaryReader.ReadByte();
				num2--;
			}
			byte[] modulus = binaryReader.ReadBytes(num2);
			if (binaryReader.ReadByte() != 2)
			{
				return null;
			}
			int count = binaryReader.ReadByte();
			byte[] exponent = binaryReader.ReadBytes(count);
			RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
			RSAParameters parameters = default(RSAParameters);
			parameters.Modulus = modulus;
			parameters.Exponent = exponent;
			rSACryptoServiceProvider.ImportParameters(parameters);
			return rSACryptoServiceProvider;
		}
		catch (Exception)
		{
			return null;
		}
		finally
		{
			binaryReader.Close();
		}
	}

	private static bool CompareBytearrays(byte[] a, byte[] b)
	{
		if (a.Length != b.Length)
		{
			return false;
		}
		int num = 0;
		foreach (byte b2 in a)
		{
			if (b2 != b[num])
			{
				return false;
			}
			num++;
		}
		return true;
	}
}
