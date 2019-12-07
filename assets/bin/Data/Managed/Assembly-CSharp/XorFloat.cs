using System;
using System.Threading;

public class XorFloat
{
	private byte[] key;

	private byte[] rawValue = new byte[4];

	private byte[] tmpValue = new byte[4];

	private const int gens = 5;

	private static Random[] s_rnds = new Random[5]
	{
		new Random(),
		new Random(),
		new Random(),
		new Random(),
		new Random()
	};

	private static int cnt = 0;

	public float value
	{
		get
		{
			return BitConverter.ToSingle(XorAndSet(rawValue, tmpValue), 0);
		}
		set
		{
			rawValue = XorAndSet(BitConverter.GetBytes(value), rawValue);
		}
	}

	public XorFloat()
		: this(0f)
	{
	}

	public XorFloat(float value)
	{
		GenerateKey();
		XorAndSet(BitConverter.GetBytes(value), rawValue);
	}

	private void GenerateKey()
	{
		byte[] buffer = new byte[4];
		Interlocked.Increment(ref cnt);
		Random random = s_rnds[cnt % 5];
		lock (random)
		{
			random.NextBytes(buffer);
		}
		key = buffer;
	}

	private byte[] XorAndSet(byte[] buf, byte[] outBuf)
	{
		for (int i = 0; i < 4; i++)
		{
			outBuf[i] = (byte)(buf[i] ^ key[i]);
		}
		return outBuf;
	}

	public static implicit operator float(XorFloat xor)
	{
		return xor?.value ?? 0f;
	}

	public static implicit operator XorFloat(float val)
	{
		return new XorFloat(val);
	}

	public override string ToString()
	{
		return value.ToString();
	}

	public string ToString(string format)
	{
		return value.ToString(format);
	}

	public string ToString(IFormatProvider provider)
	{
		return value.ToString(provider);
	}

	public string ToString(string format, IFormatProvider provider)
	{
		return value.ToString(format, provider);
	}

	public static XorFloat operator ++(XorFloat value)
	{
		return (float)value + 1f;
	}
}
