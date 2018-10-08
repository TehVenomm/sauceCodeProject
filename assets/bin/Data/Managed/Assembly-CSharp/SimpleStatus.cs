public class SimpleStatus
{
	public int hp;

	public int[] attacks = new int[7];

	public int[] defences = new int[7];

	public int[] tolerances = new int[6];

	public SimpleStatus()
	{
		Reset();
	}

	public void Reset()
	{
		hp = 0;
		for (int i = 0; i < 7; i++)
		{
			attacks[i] = 0;
			defences[i] = 0;
		}
		for (int j = 0; j < 6; j++)
		{
			tolerances[j] = 0;
		}
	}

	public int GetAttacksSum()
	{
		int num = 0;
		for (int i = 0; i < 7; i++)
		{
			num += attacks[i];
		}
		return num;
	}

	public int GetDefencesSum()
	{
		int num = 0;
		num += defences[0];
		for (int i = 0; i < 6; i++)
		{
			num += tolerances[i];
		}
		return num;
	}
}
