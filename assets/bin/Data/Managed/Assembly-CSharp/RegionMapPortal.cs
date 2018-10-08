using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RegionMapPortal
{
	private Transform _transform;

	[SerializeField]
	private int _entranceId;

	[SerializeField]
	private int _exitId;

	[SerializeField]
	private RegionMapLocation _from;

	[SerializeField]
	private RegionMapLocation _to;

	[SerializeField]
	private MeshRenderer road;

	[SerializeField]
	private Transform effectRoot;

	public int entranceId => _entranceId;

	public int exitId => _exitId;

	public RegionMapLocation fromLocation => _from;

	public RegionMapLocation toLocation => _to;

	public RegionMapPortal()
		: this()
	{
	}

	public bool IsVisited()
	{
		return MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint)entranceId) || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint)exitId);
	}

	public bool IsShow()
	{
		return FieldManager.IsShowPortal((uint)entranceId) && FieldManager.IsShowPortal((uint)exitId);
	}

	public void Init(RegionMapLocation fromLoc, RegionMapLocation toLoc)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		_from = fromLoc;
		_to = toLoc;
		_transform = this.get_transform();
		for (int i = 0; i < _transform.get_childCount(); i++)
		{
			Transform val = _transform.GetChild(i);
			if (val.get_gameObject().get_name().StartsWith("road"))
			{
				road = val.GetComponent<MeshRenderer>();
			}
			else if (val.get_gameObject().get_name().StartsWith("effect"))
			{
				effectRoot = val;
			}
		}
	}

	public void Open()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		road.get_material().SetTextureOffset("_AlphaTex", new Vector2(-1f, 0f));
	}

	public void Open(Transform effect, Animator animator, bool reverse, float endTime, Action onComplete)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		effect.set_parent(effectRoot);
		effect.set_localPosition(Vector3.get_zero());
		this.StartCoroutine(DoOpen(effect, animator, reverse, endTime, onComplete));
	}

	private IEnumerator DoOpen(Transform effect, Animator animator, bool reverse, float endTime, Action onComplete)
	{
		if (reverse)
		{
			road.get_material().SetFloat("_Reverse", 1f);
		}
		while (true)
		{
			yield return (object)null;
			AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
			float t = currentAnimatorStateInfo.get_normalizedTime();
			if (t > endTime)
			{
				break;
			}
			road.get_material().SetTextureOffset("_AlphaTex", new Vector2(1f - t, 0f));
		}
		animator.set_enabled(false);
		onComplete?.Invoke();
		float timer = 0f;
		while (true)
		{
			yield return (object)null;
			timer += Time.get_deltaTime();
			if (timer > endTime)
			{
				break;
			}
			road.get_material().SetTextureOffset("_AlphaTex", new Vector2(1f - (timer + endTime), 0f));
		}
	}
}
