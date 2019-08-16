using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayProcessor : MonoBehaviour
{
	[Serializable]
	public class EffectSetting
	{
		[Tooltip("名前")]
		public string name;

		[Tooltip("エフェクト名")]
		public string effectName;

		[Tooltip("ノ\u30fcド名")]
		public string nodeName;

		[Tooltip("オフセット座標")]
		public Vector3 position = Vector3.get_zero();

		[Tooltip("回転")]
		public Vector3 rotation = Vector3.get_zero();

		[Tooltip("スケ\u30fcル")]
		public float scale = 1f;

		public EffectSetting Clone()
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			EffectSetting effectSetting = new EffectSetting();
			effectSetting.name = name;
			effectSetting.effectName = effectName;
			effectSetting.nodeName = nodeName;
			effectSetting.position = position;
			effectSetting.rotation = rotation;
			effectSetting.scale = scale;
			return effectSetting;
		}
	}

	public EffectSetting[] effectSettings;

	public EffectPlayProcessor()
		: this()
	{
	}

	public bool IsContainSetting(string setting_name)
	{
		if (string.IsNullOrEmpty(setting_name))
		{
			return false;
		}
		int i = 0;
		for (int num = effectSettings.Length; i < num; i++)
		{
			if (effectSettings[i].name == setting_name)
			{
				return true;
			}
		}
		return false;
	}

	public List<EffectSetting> GetSettings(string setting_name)
	{
		if (string.IsNullOrEmpty(setting_name))
		{
			return null;
		}
		List<EffectSetting> list = new List<EffectSetting>();
		int i = 0;
		for (int num = effectSettings.Length; i < num; i++)
		{
			if (effectSettings[i].name == setting_name)
			{
				list.Add(effectSettings[i]);
			}
		}
		if (list.Count <= 0)
		{
			return null;
		}
		return list;
	}

	public List<Transform> PlayEffect(string setting_name, Transform owner_node = null)
	{
		return PlayEffect(GetSettings(setting_name), owner_node);
	}

	public List<Transform> PlayEffect(List<EffectSetting> settings, Transform owner_node = null)
	{
		if (settings == null)
		{
			return null;
		}
		List<Transform> list = new List<Transform>();
		int i = 0;
		for (int count = settings.Count; i < count; i++)
		{
			Transform val = PlayEffect(settings[i], owner_node);
			if (val != null)
			{
				list.Add(val);
			}
		}
		if (list.Count <= 0)
		{
			return null;
		}
		return list;
	}

	public Transform PlayEffect(EffectSetting setting, Transform owner_node = null)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (setting == null)
		{
			return null;
		}
		if (string.IsNullOrEmpty(setting.effectName))
		{
			return null;
		}
		if (owner_node == null)
		{
			owner_node = this.get_transform();
		}
		Transform val = (!string.IsNullOrEmpty(setting.nodeName)) ? Utility.Find(owner_node, setting.nodeName) : owner_node;
		if (val == null)
		{
			val = owner_node;
		}
		Transform effect = EffectManager.GetEffect(setting.effectName, val);
		if (effect != null)
		{
			effect.set_localPosition(setting.position);
			effect.set_localRotation(Quaternion.Euler(setting.rotation));
			float num = setting.scale;
			if (num == 0f)
			{
				num = 1f;
			}
			effect.set_localScale(Vector3.get_one() * num);
		}
		return effect;
	}
}
