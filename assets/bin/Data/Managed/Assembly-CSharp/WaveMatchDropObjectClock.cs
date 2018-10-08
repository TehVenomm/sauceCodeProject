public class WaveMatchDropObjectClock : WaveMatchDropObject
{
	public override void OnPicked(Self self)
	{
		base.OnPicked(self);
		_AddLimitTime();
	}

	public override void OnReceiveEffect()
	{
		_AddLimitTime();
	}

	private void _AddLimitTime()
	{
		PickedProcess(tableData);
	}

	public static void PickedProcess(WaveMatchDropTable.WaveMatchDropData data)
	{
		MonoBehaviourSingleton<UISimpleAnnounce>.I.Announce(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 2u), string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 3u), data.value));
		MonoBehaviourSingleton<InGameProgress>.I.AddLimitTime((float)data.value);
	}
}
