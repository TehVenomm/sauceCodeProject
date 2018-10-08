using System;
using UnityEngine;

public class UISkillBase : WarpingEffect
{
	private void Start()
	{
		//IL_005c: Expected O, but got Unknown
		base.cacher = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		RenderTargetCacher cacher = base.cacher;
		cacher.onUpdateTexture = (Action<RenderTexture>)Delegate.Combine(cacher.onUpdateTexture, new Action<RenderTexture>(base.OnUpdateTexture));
		try
		{
			atlasMaterial.SetTexture("_SrcTex", base.cacher.GetTexture());
		}
		catch (UnassignedReferenceException val)
		{
			UnassignedReferenceException val2 = val;
		}
	}
}
