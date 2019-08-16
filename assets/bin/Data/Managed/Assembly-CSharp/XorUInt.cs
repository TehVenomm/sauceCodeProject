using System;
using System.Threading;

public class XorUInt
{
	private uint key;

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

	public uint rawValue
	{
		get;
		private set;
	}

	public uint value
	{
		get
		{
			return Xor(rawValue);
		}
		set
		{
			rawValue = Xor(value);
		}
	}

	public XorUInt()
		: this(0u)
	{
	}

	public XorUInt(uint value)
	{
		GenerateKey();
		rawValue = Xor(value);
	}

	private void GenerateKey()
	{
		Interlocked.Increment(ref cnt);
		Random random = s_rnds[cnt % 5];
		lock (random)
		{
			key = (uint)random.Next();
		}
	}

	private uint Xor(uint x)
	{
		return x ^ key;
	}

	public static implicit operator uint(XorUInt xor)
	{
		return xor?.value ?? 0;
	}

	public static explicit operator int(XorUInt xor)
	{
		return (int)(xor?.value ?? 0);
	}

	public static implicit operator XorUInt(uint val)
	{
		return new XorUInt(val);
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
}
