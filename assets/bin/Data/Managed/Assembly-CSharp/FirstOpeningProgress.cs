using System.Collections.Generic;

internal class FirstOpeningProgress : IProgress
{
	private PredownloadProgress predownload;

	private DataTableLoadProgress datatable;

	public FirstOpeningProgress(List<DataLoadRequest> loadings)
	{
		predownload = new PredownloadProgress();
		datatable = new DataTableLoadProgress(loadings);
	}

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
		return datatable.IsCompleted() && predownload.IsCompleted();
	}

	public bool IsVisible()
	{
		return !IsCompleted();
	}
}
