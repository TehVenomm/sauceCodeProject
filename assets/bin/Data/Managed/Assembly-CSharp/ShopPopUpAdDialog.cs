using System.Collections;
using UnityEngine;

public class ShopPopUpAdDialog : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		TEX_MAIN
	}

	private string textureName = "";

	public override void Initialize()
	{
		textureName = (GameSection.GetEventData() as string);
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loTex = loadingQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, textureName);
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (loTex.loadedObject != null)
		{
			SetTexture(UI.TEX_MAIN, loTex.loadedObject as Texture);
		}
		PlayTween(UI.OBJ_FRAME, forward: true, null, is_input_block: false);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
