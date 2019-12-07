using Network;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemDetailSeriesArena : UIBehaviour
{
	protected enum UI
	{
		TEX_EVENT_BANNER
	}

	public void SetUpItem(Transform t)
	{
		StartCoroutine(loadBanner(t));
	}

	private IEnumerator loadBanner(Transform t)
	{
		Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I.eventList.Where((Network.EventData e) => e.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR).FirstOrDefault();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, ResourceName.GetEventBanner(eventData.bannerId));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Texture2D texture2D = obj.loadedObject as Texture2D;
		if (texture2D != null)
		{
			Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
			SetTexture(t2, texture2D);
			SetActive(t2, is_visible: true);
		}
	}
}
