using System.Collections.Generic;
using UnityEngine;

public class SubstituteController
{
	private Player owner;

	private List<SubstituteEffect> list = new List<SubstituteEffect>();

	private InGameSettingsManager.BuffParamInfo info;

	private GameObject effectRoot;

	public void Initialize(Player p)
	{
		owner = p;
		list.Clear();
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			info = MonoBehaviourSingleton<InGameSettingsManager>.I.buff;
		}
	}

	public void TryFinalize()
	{
		owner = null;
		End();
		list.Clear();
		info = null;
		if (effectRoot != null)
		{
			Object.Destroy(effectRoot);
		}
		effectRoot = null;
	}

	public void Create(int num)
	{
		if (effectRoot == null)
		{
			effectRoot = new GameObject();
			effectRoot.transform.SetParent(MonoBehaviourSingleton<EffectManager>.I._transform);
			effectRoot.transform.localPosition = Vector3.zero;
			effectRoot.transform.localScale = Vector3.one;
			effectRoot.transform.localRotation = Quaternion.identity;
		}
		effectRoot.SetActive(owner.isActedBattleStart);
		for (int i = 0; i < num; i++)
		{
			if (i < list.Count)
			{
				list[i].Create(effectRoot.transform);
				continue;
			}
			SubstituteEffect substituteEffect = new SubstituteEffect();
			substituteEffect.Initialize(effectRoot.transform, i, owner, (i == 0) ? null : list[i - 1], info);
			list.Add(substituteEffect);
		}
	}

	public void Sub()
	{
		for (int num = list.Count - 1; num >= 0; num--)
		{
			SubstituteEffect substituteEffect = list[num];
			if (substituteEffect.IsEnable())
			{
				substituteEffect.End();
				break;
			}
		}
		int enableNum = GetEnableNum();
		if (enableNum != 0 && owner.playerSender != null)
		{
			owner.playerSender.OnSyncSubstitute(enableNum);
		}
	}

	public void End()
	{
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			list[i].End();
		}
	}

	public void Update(bool isLerp = true)
	{
		if (!list.IsNullOrEmpty())
		{
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				list[i].Update(isLerp);
			}
		}
	}

	public void ActiveEffectRoot()
	{
		if (!(effectRoot == null) && !effectRoot.activeSelf)
		{
			Update(isLerp: false);
			effectRoot.SetActive(value: true);
		}
	}

	private int GetEnableNum()
	{
		int num = 0;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			if (list[i].IsEnable())
			{
				num++;
			}
		}
		return num;
	}

	public void Sync(int num)
	{
		ActiveEffectRoot();
		int enableNum = GetEnableNum();
		if (enableNum == num)
		{
			return;
		}
		if (enableNum < num)
		{
			Create(num);
			return;
		}
		int i = 0;
		for (int num2 = enableNum - num; i < num2; i++)
		{
			Sub();
		}
	}
}
