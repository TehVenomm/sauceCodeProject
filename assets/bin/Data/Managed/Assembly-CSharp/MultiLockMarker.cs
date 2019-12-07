using System.Collections.Generic;
using UnityEngine;

public class MultiLockMarker : MonoBehaviour
{
	public const int kLayerMask = 16;

	[SerializeField]
	private Animator animator;

	private Transform _transform;

	private List<int> _lockOrder = new List<int>();

	private float _lockInterval;

	private InGameSettingsManager.Player.ArrowActionInfo info;

	public List<int> lockOrder => _lockOrder;

	private void Update()
	{
		if (_lockInterval >= 0f)
		{
			_lockInterval -= Time.deltaTime;
		}
	}

	public void Init()
	{
		_transform = base.transform;
		_lockOrder.Clear();
		_lockInterval = 0f;
		info = (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() ? MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo : null);
	}

	private bool CanLock()
	{
		if (info == null)
		{
			return false;
		}
		if (_lockOrder.Count >= info.soulLockRegionMax)
		{
			return false;
		}
		if (_lockInterval > 0f)
		{
			return false;
		}
		return true;
	}

	public bool Lock(int sumLockNum, bool isBoost)
	{
		if (!CanLock())
		{
			return false;
		}
		_lockOrder.Add(sumLockNum + 1);
		_lockInterval = (isBoost ? info.soulBoostLockRegionInterval : info.soulLockRegionInterval);
		animator.SetInteger("state", _lockOrder.Count);
		EffectManager.GetEffect("ef_btl_wsk2_bow_lock_02", _transform);
		return true;
	}

	public void Hide()
	{
		if (_lockOrder.Count <= 0)
		{
			animator.SetInteger("state", -1);
		}
	}

	public void Reset()
	{
		_lockOrder.Clear();
		_lockInterval = 0f;
		animator.SetInteger("state", 0);
	}

	public void EndBoost(bool isHide)
	{
		int soulArrowNormalLockNum = MonoBehaviourSingleton<StageObjectManager>.I.self.GetSoulArrowNormalLockNum();
		for (int i = 0; i < _lockOrder.Count; i++)
		{
			if (_lockOrder[i] > soulArrowNormalLockNum)
			{
				_lockOrder.RemoveRange(i, _lockOrder.Count - i);
				break;
			}
		}
		if (_lockOrder.Count > 0)
		{
			animator.SetInteger("state", _lockOrder.Count);
		}
		else
		{
			animator.SetInteger("state", isHide ? (-1) : 0);
		}
	}
}
