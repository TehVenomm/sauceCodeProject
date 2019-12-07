using UnityEngine;

public class TestStone : BreakObject
{
	protected override void Awake()
	{
		base.Awake();
		if (string.IsNullOrEmpty(breakEffectName))
		{
			breakEffectName = "ef_btl_bg_rockbreak_01";
		}
	}

	protected override void Initialize()
	{
		base.Initialize();
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			componentInChildren.gameObject.AddComponent<SphereCollider>().radius = 2.2f;
		}
	}
}
