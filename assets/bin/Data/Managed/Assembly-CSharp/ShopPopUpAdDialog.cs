using System;
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
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loTex = loadQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, textureName);
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		if (loTex.loadedObject != null)
		{
			SetTexture((Enum)UI.TEX_MAIN, loTex.loadedObject as Texture);
		}
		PlayTween((Enum)UI.OBJ_FRAME, forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
