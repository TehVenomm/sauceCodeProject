using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RegionMapPortal : MonoBehaviour
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

	public bool IsVisited()
	{
		if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint)entranceId))
		{
			return MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal((uint)exitId);
		}
		return true;
	}

	public bool IsShow()
	{
		if (FieldManager.IsShowPortal((uint)entranceId))
		{
			return FieldManager.IsShowPortal((uint)exitId);
		}
		return false;
	}

	public void Init(RegionMapLocation fromLoc, RegionMapLocation toLoc)
	{
		_from = fromLoc;
		_to = toLoc;
		_transform = base.transform;
		for (int i = 0; i < _transform.childCount; i++)
		{
			Transform child = _transform.GetChild(i);
			if (child.gameObject.name.StartsWith("road"))
			{
				road = child.GetComponent<MeshRenderer>();
			}
			else if (child.gameObject.name.StartsWith("effect"))
			{
				effectRoot = child;
			}
		}
	}

	public void Open()
	{
		road.material.SetTextureOffset("_AlphaTex", new Vector2(-1f, 0f));
	}

	public void Open(Transform effect, Animator animator, bool reverse, float endTime, Action onComplete)
	{
		effect.parent = effectRoot;
		effect.localPosition = Vector3.zero;
		StartCoroutine(DoOpen(effect, animator, reverse, endTime, onComplete));
	}

	private IEnumerator DoOpen(Transform effect, Animator animator, bool reverse, float endTime, Action onComplete)
	{
		if (reverse)
		{
			road.material.SetFloat("_Reverse", 1f);
		}
		while (true)
		{
			yield return null;
			float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			if (normalizedTime > endTime)
			{
				break;
			}
			road.material.SetTextureOffset("_AlphaTex", new Vector2(1f - normalizedTime, 0f));
		}
		animator.enabled = false;
		onComplete?.Invoke();
		float timer = 0f;
		while (true)
		{
			yield return null;
			timer += Time.deltaTime;
			if (!(timer > endTime))
			{
				road.material.SetTextureOffset("_AlphaTex", new Vector2(1f - (timer + endTime), 0f));
				continue;
			}
			break;
		}
	}
}
