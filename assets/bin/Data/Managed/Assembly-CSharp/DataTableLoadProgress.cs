using System.Collections.Generic;

internal class DataTableLoadProgress : IProgress
{
	private List<DataLoadRequest> loadings;

	private int total;

	private int endCount;

	public DataTableLoadProgress(List<DataLoadRequest> loadings)
	{
		this.loadings = loadings;
		total = loadings.Count;
	}

	public float GetProgress()
	{
		float num = 0f;
		endCount += loadings.RemoveAll((DataLoadRequest x) => x.isCompleted);
		int count = loadings.Count;
		for (int i = 0; i < count; i++)
		{
			num += loadings[i].progress;
		}
		return (num + (float)endCount) / (float)total;
	}

	public bool IsCompleted()
	{
		return endCount == total;
	}

	public bool IsVisible()
	{
		return !IsCompleted();
	}
}
