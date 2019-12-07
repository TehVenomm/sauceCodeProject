using System.Collections.Generic;

internal class FirstOpeningProgress : IProgress
{
	private PredownloadProgress predownload;

	private DataTableLoadProgress datatable;

	public float GetProgress()
	{
		if (IsCompleted())
		{
			return 1f;
		}
		return datatable.GetProgress() * 0.2f + predownload.GetProgress() * 0.8f;
	}

	public bool IsCompleted()
	{
		if (datatable.IsCompleted())
		{
			return predownload.IsCompleted();
		}
		return false;
	}

	public FirstOpeningProgress(List<DataLoadRequest> loadings)
	{
		predownload = new PredownloadProgress();
		datatable = new DataTableLoadProgress(loadings);
	}

	public bool IsVisible()
	{
		return !IsCompleted();
	}
}
