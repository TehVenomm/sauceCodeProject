using System;
using System.Threading;

public class XorInt
{
	private int key;

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

	public int rawValue
	{
		get;
		private set;
	}

	public int value
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

	public XorInt()
		: this(0)
	{
	}

	public XorInt(int value)
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
			key = random.Next();
		}
	}

	private int Xor(int x)
	{
		return x ^ key;
	}

	public static implicit operator int(XorInt xor)
	{
		return xor?.value ?? 0;
	}

	public static implicit operator XorInt(int val)
	{
		return new XorInt(val);
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
