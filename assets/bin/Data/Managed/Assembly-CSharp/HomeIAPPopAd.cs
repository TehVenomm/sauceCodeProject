using System.Collections;
using UnityEngine;

public class HomeIAPPopAd : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		TEX_MAIN
	}

	private string productId = "";

	public override void Initialize()
	{
		productId = (GameSection.GetEventData() as string);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(productId);
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loTex = loadingQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, pack.popupAdsBanner);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (loTex.loadedObject != null)
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
