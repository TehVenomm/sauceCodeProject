using System.Collections;
using UnityEngine;

public class Ookushi_AssetBundle : MonoBehaviour
{
	private IEnumerator Start()
	{
		while (!AppMain.isInitialized)
		{
			yield return null;
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo = loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestRequestItem");
		yield return loadingQueue.Wait();
		ResourceUtility.Instantiate(lo.loadedObject);
	}
}
