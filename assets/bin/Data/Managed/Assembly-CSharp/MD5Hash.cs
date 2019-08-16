using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class MD5Hash : IDataTableRequestHash
{
	public static readonly MD5Hash invalidHash = new MD5Hash();

	private static MD5 md5Calc = MD5.Create();

	private uint u32_0;

	private uint u32_1;

	private uint u32_2;

	private uint u32_3;

	public bool isValid => u32_0 != 0 || u32_1 != 0 || u32_2 != 0 || u32_3 != 0;

	public uint this[int key]
	{
		get
		{
			switch (key)
			{
			case 0:
				return u32_0;
			case 1:
				return u32_1;
			case 2:
				return u32_2;
			case 3:
				return u32_3;
			default:
				throw new KeyNotFoundException();
			}
		}
		private set
		{
			switch (key)
			{
			case 0:
				u32_0 = value;
				break;
			case 1:
				u32_1 = value;
				break;
			case 2:
				u32_2 = value;
				break;
			case 3:
				u32_3 = value;
				break;
			default:
				throw new KeyNotFoundException();
			}
		}
	}

	public MD5Hash(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
	{
		this.u32_0 = u32_0;
		this.u32_1 = u32_1;
		this.u32_2 = u32_2;
		this.u32_3 = u32_3;
	}

	public MD5Hash()
	{
	}

	private MD5Hash(byte[] hash)
	{
		u32_0 |= (uint)(hash[0] << 24);
		u32_0 |= (uint)(hash[1] << 16);
		u32_0 |= (uint)(hash[2] << 8);
		u32_0 |= (uint)(hash[3] << 0);
		u32_1 |= (uint)(hash[4] << 24);
		u32_1 |= (uint)(hash[5] << 16);
		u32_1 |= (uint)(hash[6] << 8);
		u32_1 |= (uint)(hash[7] << 0);
		u32_2 |= (uint)(hash[8] << 24);
		u32_2 |= (uint)(hash[9] << 16);
		u32_2 |= (uint)(hash[10] << 8);
		u32_2 |= (uint)(hash[11] << 0);
		u32_3 |= (uint)(hash[12] << 24);
		u32_3 |= (uint)(hash[13] << 16);
		u32_3 |= (uint)(hash[14] << 8);
		u32_3 |= (uint)(hash[15] << 0);
	}

	public static MD5Hash Parse(string hashString)
	{
		MD5Hash mD5Hash = new MD5Hash();
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				string value = hashString.Substring(j * 2 + i * 8, 2);
				byte b = Convert.ToByte(value, 16);
				MD5Hash mD5Hash2;
				int key;
				(mD5Hash2 = mD5Hash)[key = i] = (uint)((int)mD5Hash2[key] | (b << 24 - j * 8));
			}
		}
		return mD5Hash;
	}

	public static MD5Hash Calc(byte[] data)
	{
		md5Calc.Initialize();
		byte[] hash = md5Calc.ComputeHash(data);
		return new MD5Hash(hash);
	}

	public static MD5Hash Calc(string s)
	{
		md5Calc.Initialize();
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		byte[] hash = md5Calc.ComputeHash(bytes);
		return new MD5Hash(hash);
	}

	public override string ToString()
	{
		return u32_0.ToString("x8") + u32_1.ToString("x8") + u32_2.ToString("x8") + u32_3.ToString("x8");
	}

	public override bool Equals(object obj)
	{
		MD5Hash mD5Hash = obj as MD5Hash;
		if (mD5Hash == null)
		{
			return false;
		}
		return this == mD5Hash;
	}

	public override int GetHashCode()
	{
		return (int)(u32_0 ^ u32_1 ^ u32_2 ^ u32_3);
	}

	public uint GetUIntHashCode()
	{
		return u32_0 ^ u32_1 ^ u32_2 ^ u32_3;
	}

	public static bool operator ==(MD5Hash a, MD5Hash b)
	{
		return a.u32_0 == b.u32_0 && a.u32_1 == b.u32_1 && a.u32_2 == b.u32_2 && a.u32_3 == b.u32_3;
	}

	public static bool operator !=(MD5Hash a, MD5Hash b)
	{
		return a.u32_0 != b.u32_0 || a.u32_1 != b.u32_1 || a.u32_2 != b.u32_2 || a.u32_3 == b.u32_3;
	}
}
