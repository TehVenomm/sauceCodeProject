internal class PredownloadProgress : IProgress
{
	public float GetProgress()
	{
		if (IsCompleted())
		{
			return 1f;
		}
		MonoBehaviourSingleton<PredownloadManager>.I.GetCount(out int total, out int loaded);
		return (float)loaded / (float)total;
	}

	public bool IsCompleted()
	{
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			return MonoBehaviourSingleton<PredownloadManager>.I.loadedCount >= MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount;
		}
		return true;
	}

	public bool IsVisible()
	{
		return !IsCompleted();
	}
}
