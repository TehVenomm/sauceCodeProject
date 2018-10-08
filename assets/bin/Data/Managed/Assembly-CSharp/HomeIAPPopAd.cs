using System.Collections;
using UnityEngine;

public class HomeIAPPopAd : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		TEX_MAIN
	}

	private string productId = string.Empty;

	public override void Initialize()
	{
		productId = (GameSection.GetEventData() as string);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		GlobalSettingsManager.PackParam.PackInfo pack_info = MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.GetPack(productId);
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loTex = loadQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, pack_info.popupAdsBanner, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (loTex.loadedObject != (Object)null)
		{
			SetTexture(UI.TEX_MAIN, loTex.loadedObject as Texture);
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	private void OnQuery_OK()
	{
		DispatchEvent("CRYSTAL_SHOP", productId);
	}
}
