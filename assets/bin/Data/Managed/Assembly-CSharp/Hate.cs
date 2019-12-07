public class Hate
{
	public enum TYPE
	{
		Distance,
		LifeLowner,
		Damage,
		Heal,
		Skill,
		SpecialDamage,
		Guard,
		MAX_NUM
	}

	public int[] val = new int[7];

	public int turnVal;

	public int cycleLockCount;

	public int totalLockCount;

	public int continuousLockCount;

	public int CalcTotalHate(HateParam param)
	{
		float num = 0f;
		for (int i = 0; i < 7; i++)
		{
			num += (float)val[i] * param.categoryParam[i].importance;
		}
		return (int)num;
	}

	public override string ToString()
	{
		return val + "/" + turnVal + "[" + cycleLockCount + "/" + totalLockCount + "]";
	}
}
