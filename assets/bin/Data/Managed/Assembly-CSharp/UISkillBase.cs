using System;
using UnityEngine;

public class UISkillBase : WarpingEffect
{
	private void Start()
	{
		base.cacher = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		RenderTargetCacher cacher = base.cacher;
		cacher.onUpdateTexture = (Action<RenderTexture>)Delegate.Combine(cacher.onUpdateTexture, new Action<RenderTexture>(base.OnUpdateTexture));
		try
		{
			atlasMaterial.SetTexture("_SrcTex", base.cacher.GetTexture());
		}
		catch (UnassignedReferenceException arg)
		{
			Debug.Log("UISkillBase Error:" + arg);
		}
	}
}
