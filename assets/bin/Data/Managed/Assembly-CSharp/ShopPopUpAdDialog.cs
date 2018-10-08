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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		textureName = (GameSection.GetEventData() as string);
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loTex = loadQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, textureName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		if (loTex.loadedObject != null)
		{
			SetTexture((Enum)UI.TEX_MAIN, loTex.loadedObject as Texture);
		}
		PlayTween((Enum)UI.OBJ_FRAME, true, (EventDelegate.Callback)null, false, 0);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}
}
