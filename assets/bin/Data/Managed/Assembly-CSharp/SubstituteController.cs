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

	public void Finalize()
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (effectRoot == null)
		{
			effectRoot = new GameObject();
			effectRoot.get_transform().SetParent(MonoBehaviourSingleton<EffectManager>.I._transform);
			effectRoot.get_transform().set_localPosition(Vector3.get_zero());
			effectRoot.get_transform().set_localScale(Vector3.get_one());
			effectRoot.get_transform().set_localRotation(Quaternion.get_identity());
		}
		effectRoot.SetActive(owner.isActedBattleStart);
		for (int i = 0; i < num; i++)
		{
			if (i < list.Count)
			{
				list[i].Create(effectRoot.get_transform());
				continue;
			}
			SubstituteEffect substituteEffect = new SubstituteEffect();
			substituteEffect.Initialize(effectRoot.get_transform(), i, owner, (i != 0) ? list[i - 1] : null, info);
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
		if (!(effectRoot == null) && !effectRoot.get_activeSelf())
		{
			Update(isLerp: false);
			effectRoot.SetActive(true);
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
