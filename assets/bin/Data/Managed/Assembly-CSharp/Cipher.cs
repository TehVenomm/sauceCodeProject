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
		RijndaelManaged obj = new RijndaelManaged
		{
			Padding = PaddingMode.PKCS7,
			Mode = CipherMode.CBC,
			KeySize = 256,
			BlockSize = 128
		};
		byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(prm_iv);
		ICryptoTransform transform = obj.CreateEncryptor(bytes, bytes2);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
		cryptoStream.FlushFinalBlock();
		return Convert.ToBase64String(memoryStream.ToArray());
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
		RijndaelManaged obj = new RijndaelManaged
		{
			Padding = PaddingMode.PKCS7,
			Mode = CipherMode.CBC,
			KeySize = 256,
			BlockSize = 128
		};
		byte[] bytes = Encoding.UTF8.GetBytes(prm_key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(prm_iv);
		ICryptoTransform transform = obj.CreateDecryptor(bytes, bytes2);
		byte[] array = Convert.FromBase64String(prm_text_to_decrypt);
		byte[] array2 = new byte[array.Length];
		int newSize = new CryptoStream(new MemoryStream(array), transform, CryptoStreamMode.Read).Read(array2, 0, array2.Length);
		Array.Resize(ref array2, newSize);
		return array2;
	}

	public static bool verifyBytes(Stream signedDataStream, byte[] signature)
	{
		RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(DecodeX509PublicKey(Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwynqwPzQ33ddKb/oolirybF75lSAodzU7Myxyj7snavvYl15qzbLXRwfK5OqZS1ke7Yc0s1m8EGodkN/m3Xg4/7AKo7GtPSh3VwbTKbTPEn86Es2FG28BDOXUlXf8P4lvCaB/7JAast7JDzZl3jEp3m9ktAOBgg/zeh/W72sAwA4EUuf0MhFalJvpLYjIXD2sM138aKIXIcF8m4nSUAP0ti0iCskjfEUGAfyK9nq/S19RjuWGOI76QnUhn++NNcSl5KMGSf9iXnohuIpFUn/vQkKnLVAbXUhLCxA+LrGYGnk65hiJcCYohdIJqCjKIx3P2XB5a05tr+g24H2KmjAMQIDAQAB")));
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
		RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(DecodeX509PublicKey(Convert.FromBase64String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy0BC0jB+o9MPpjUJbRKba1z76SaKUeNuE9y5MGFrZbKFPo9dW7Aor8hUdOSk1eSJzuiQktKSEWhvGEfdH0bPb18s53GOHb2rJFA3KcHa58+HItorJUADXbK5mL0TCa4TznxOB/c0gEdZgLZN7aHMDX8Sy32HoVu5Ub0RXUQfrlY+jUEqUXI+Jieg2D2Xgv1qRWTl+RTHJ8oagZk5O5KH+1A6PBG4mJGeWoG7CPpkynvtNo1q3IeIXR/Vwi12InaIAjCfLHsq5LmSzw3rDmUdUZxeO9AnzFHhIA9WVVhjfxgL5QH9OEBdXFva1lr0e6Vaur/TZxl4zRjjg/v45Pp2NQIDAQAB")));
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
			return SHA256.Create().ComputeHash(bytes);
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
			return SHA256.Create().ComputeHash(stream);
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
		_ = new byte[15];
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(x509key));
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
			if (!CompareBytearrays(binaryReader.ReadBytes(15), b))
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
			byte b2 = 0;
			byte b3 = 0;
			switch (num)
			{
			case 33026:
				b2 = binaryReader.ReadByte();
				break;
			case 33282:
				b3 = binaryReader.ReadByte();
				b2 = binaryReader.ReadByte();
				break;
			default:
				return null;
			}
			int num2 = BitConverter.ToInt32(new byte[4]
			{
				b2,
				b3,
				0,
				0
			}, 0);
			byte b4 = binaryReader.ReadByte();
			binaryReader.BaseStream.Seek(-1L, SeekOrigin.Current);
			if (b4 == 0)
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
			rSACryptoServiceProvider.ImportParameters(new RSAParameters
			{
				Modulus = modulus,
				Exponent = exponent
			});
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
		for (int i = 0; i < a.Length; i++)
		{
			if (a[i] != b[num])
			{
				return false;
			}
			num++;
		}
		return true;
	}
}
