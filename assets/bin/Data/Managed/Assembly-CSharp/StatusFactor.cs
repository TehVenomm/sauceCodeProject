public class StatusFactor
{
	public SimpleStatus baseStatus = new SimpleStatus();

	public float hpRate;

	public float[] atkRate = new float[7];

	public float[] defRate = new float[7];

	public float[] tolRate = new float[6];

	public int constHp;

	public int[] constAtks = new int[7];

	public int[] constDefs = new int[7];

	public int[] constTols = new int[6];

	public void Reset()
	{
		baseStatus.Reset();
		hpRate = 1f;
		constHp = 0;
		for (int i = 0; i < 7; i++)
		{
			atkRate[i] = 1f;
			defRate[i] = 1f;
			constAtks[i] = 0;
			constDefs[i] = 0;
		}
		for (int j = 0; j < 6; j++)
		{
			tolRate[j] = 1f;
			constTols[j] = 0;
		}
	}

	public void CheckMinusRate()
	{
		if (hpRate < 0f)
		{
			hpRate = 0f;
		}
		for (int i = 0; i < 7; i++)
		{
			if (atkRate[i] < 0f)
			{
				atkRate[i] = 0f;
			}
			if (defRate[i] < 0f)
			{
				defRate[i] = 0f;
			}
		}
		for (int j = 0; j < 6; j++)
		{
			if (tolRate[j] < 0f)
			{
				tolRate[j] = 0f;
			}
		}
	}
}
