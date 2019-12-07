using System.Collections.Generic;

public class SoulEnergyController
{
	private List<SoulEnergy> collection = new List<SoulEnergy>();

	private Player cacheOwner;

	public void Initialize(Player owner)
	{
		cacheOwner = owner;
	}

	public SoulEnergy Get(float baseValue)
	{
		if (baseValue <= 0f)
		{
			return null;
		}
		SoulEnergy soulEnergy = null;
		int i = 0;
		for (int count = collection.Count; i < count; i++)
		{
			SoulEnergy soulEnergy2 = collection[i];
			if (soulEnergy2.canWork())
			{
				soulEnergy = soulEnergy2;
				break;
			}
		}
		if (soulEnergy == null)
		{
			soulEnergy = new SoulEnergy();
			soulEnergy.Init();
			collection.Add(soulEnergy);
		}
		soulEnergy.Exec(cacheOwner, baseValue);
		return soulEnergy;
	}

	public void Sleep()
	{
		int i = 0;
		for (int count = collection.Count; i < count; i++)
		{
			collection[i].Sleep();
		}
	}

	public void Tap()
	{
		int i = 0;
		for (int count = collection.Count; i < count; i++)
		{
			collection[i].Tap();
		}
	}

	public void Absorbed()
	{
		int i = 0;
		for (int count = collection.Count; i < count; i++)
		{
			collection[i].Absorbed();
		}
	}

	public void Clear()
	{
		int i = 0;
		for (int count = collection.Count; i < count; i++)
		{
			collection[i] = null;
		}
		collection.Clear();
	}
}
