using System.Collections.Generic;

public class BrainBlackboard : Singleton<BrainBlackboard>
{
	private List<OpponentMemory.RecordData> records;

	public void Clear()
	{
		if (records != null)
		{
			records.Clear();
		}
	}
}
