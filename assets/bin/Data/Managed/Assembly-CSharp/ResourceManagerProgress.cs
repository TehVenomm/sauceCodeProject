using System.Collections.Generic;

internal class ResourceManagerProgress : IProgress
{
	private bool hasProgress;

	public float GetProgress()
	{
		if (IsCompleted())
		{
			return 1f;
		}
		List<ResourceManager.LoadRequest> loadRequests = MonoBehaviourSingleton<ResourceManager>.I.loadRequests;
		int i = 0;
		for (int count = loadRequests.Count; i < count; i++)
		{
			ResourceManager.LoadRequest loadRequest = loadRequests[i];
			if (loadRequest.IsValid() && loadRequests[i].progressObject != null)
			{
				hasProgress = true;
				return loadRequests[i].GetProgress();
			}
		}
		hasProgress = false;
		return 1f;
	}

	public bool IsCompleted()
	{
		return !MonoBehaviourSingleton<ResourceManager>.I.isLoading;
	}

	public bool IsVisible()
	{
		return hasProgress;
	}
}
