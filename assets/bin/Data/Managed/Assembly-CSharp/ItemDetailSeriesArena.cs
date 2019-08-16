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
		this.StartCoroutine(loadBanner(t));
	}

	private IEnumerator loadBanner(Transform t)
	{
		Network.EventData eventData = (from e in MonoBehaviourSingleton<QuestManager>.I.eventList
		where e.eventTypeEnum == EVENT_TYPE.SERIES_ARENA_POINT_CLEAR
		select e).FirstOrDefault();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, ResourceName.GetEventBanner(eventData.bannerId));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Texture2D bannerTex = obj.loadedObject as Texture2D;
		if (bannerTex != null)
		{
			Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
			SetTexture(t2, bannerTex);
			SetActive(t2, is_visible: true);
		}
	}
}
