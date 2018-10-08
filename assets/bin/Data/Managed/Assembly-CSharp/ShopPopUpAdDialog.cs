using System.Collections;
using UnityEngine;

public class ShopPopUpAdDialog : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		TEX_MAIN
	}

	private string textureName = string.Empty;

	public override void Initialize()
	{
		textureName = (GameSection.GetEventData() as string);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loTex = loadQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, textureName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (loTex.loadedObject != (Object)null)
		{
			SetTexture(UI.TEX_MAIN, loTex.loadedObject as Texture);
		}
		PlayTween(UI.OBJ_FRAME, true, null, false, 0);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
