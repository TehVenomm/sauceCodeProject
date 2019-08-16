using System.Collections.Generic;
using UnityEngine;

public class UIDamageManager : MonoBehaviourSingleton<UIDamageManager>
{
	private static readonly Vector3 OFFSET_RECOVER_NUM = new Vector3(0f, 0.7f, 0f);

	protected List<UIDamageNum> damageNumList = new List<UIDamageNum>();

	protected List<UIAdditionalDamageNum> additionalDamageNumList = new List<UIAdditionalDamageNum>();

	protected List<UIPlayerDamageNum> playerDamageNumList = new List<UIPlayerDamageNum>();

	protected List<UIPlayerDamageNum> playerRecoverNumList = new List<UIPlayerDamageNum>();

	protected List<UIPlayerDamageNum> enemyRecoverNumList = new List<UIPlayerDamageNum>();

	private UIDamageNum originalDamage;

	private UIPlayerDamageNum currentRecoverNum;

	private Object m_playerDamageNumObj;

	private Object m_damageNumObj;

	private Object m_additionalDamageNumObj;

	public void RegisterDamageNumResources(Object damageNumObj, Object playerDamageNumObj, Object additionalDamageNumObj)
	{
		m_damageNumObj = damageNumObj;
		m_playerDamageNumObj = playerDamageNumObj;
		m_additionalDamageNumObj = additionalDamageNumObj;
	}

	public void Create(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffset = 0, int effective = 0, bool isRegionOnly = false)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			return;
		}
		float pixelHeight = MonoBehaviourSingleton<InGameCameraManager>.I.GetPixelHeight();
		Vector3 pos2 = default(Vector3);
		pos2._002Ector(pos.x, pos.y + MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset, pos.z);
		Vector3 pos3 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(pos2);
		if (pos3.y > pixelHeight)
		{
			pos3.y = pixelHeight;
			pos = MonoBehaviourSingleton<InGameCameraManager>.I.ScreenToWorldPoint(pos3);
			pos.y -= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset;
		}
		bool flag = color != UIDamageNum.DAMAGE_COLOR.BUFF && UIDamageNum.DAMAGE_COLOR.NONE != color;
		if (isRegionOnly)
		{
			color = (flag ? UIDamageNum.DAMAGE_COLOR.REGION_ONLY_ELEMENT : ((color != UIDamageNum.DAMAGE_COLOR.BUFF) ? UIDamageNum.DAMAGE_COLOR.REGION_ONLY_NORMAL : UIDamageNum.DAMAGE_COLOR.REGION_ONLY_BUFF));
		}
		if (groupOffset > 0 || flag)
		{
			CreateAdditionalDamage(pos, damage, color, groupOffset, originalDamage, effective);
			return;
		}
		UIDamageNum uIDamageNum = null;
		int num = 0;
		int i = 0;
		for (int count = damageNumList.Count; i < count; i++)
		{
			if (!damageNumList[i].enable)
			{
				uIDamageNum = damageNumList[i];
				num = i;
				break;
			}
		}
		if (uIDamageNum == null)
		{
			GameObject val = Object.Instantiate(m_damageNumObj);
			if (val == null)
			{
				return;
			}
			Utility.Attach(base._transform, val.get_transform());
			uIDamageNum = val.GetComponent<UIDamageNum>();
			if (uIDamageNum == null)
			{
				Object.Destroy(val);
				return;
			}
			damageNumList.Add(uIDamageNum);
			num = damageNumList.Count - 1;
		}
		groupOffset = CalcOffsetPosition(pos, uIDamageNum, groupOffset);
		originalDamage = uIDamageNum;
		if (uIDamageNum.Initialize(pos, damage, color, groupOffset))
		{
		}
	}

	private void CreateAdditionalDamage(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffset, UIDamageNum originalDamage, int effective)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		UIAdditionalDamageNum uIAdditionalDamageNum = null;
		int i = 0;
		for (int count = additionalDamageNumList.Count; i < count; i++)
		{
			if (!additionalDamageNumList[i].enable)
			{
				uIAdditionalDamageNum = additionalDamageNumList[i];
				break;
			}
		}
		if (uIAdditionalDamageNum == null)
		{
			GameObject val = Object.Instantiate(m_additionalDamageNumObj);
			if (val == null)
			{
				return;
			}
			Utility.Attach(base._transform, val.get_transform());
			uIAdditionalDamageNum = val.GetComponent<UIAdditionalDamageNum>();
			if (uIAdditionalDamageNum == null)
			{
				Object.Destroy(val);
				return;
			}
			additionalDamageNumList.Add(uIAdditionalDamageNum);
		}
		groupOffset = CalcOffsetPosition(pos, uIAdditionalDamageNum, groupOffset);
		if (uIAdditionalDamageNum.Initialize(pos, damage, color, groupOffset, originalDamage, effective))
		{
		}
	}

	private int CalcOffsetPosition(Vector3 pos, UIDamageNum damage_num, int orgGroupOffet)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			if (!isLabelOverlap(pos, damage_num, orgGroupOffet + i))
			{
				return orgGroupOffet + i;
			}
		}
		return orgGroupOffet;
	}

	private bool isLabelOverlap(Vector3 pos, UIDamageNum damage_num, int groupOffset)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		damage_num.get_transform().set_position(damage_num.GetUIPosFromWorld(pos, groupOffset));
		Vector3 localPosition = damage_num.get_transform().get_localPosition();
		for (int i = 0; i < damageNumList.Count; i++)
		{
			if (_isLabelOverlap(localPosition, damage_num, damageNumList[i]))
			{
				return true;
			}
		}
		for (int j = 0; j < additionalDamageNumList.Count; j++)
		{
			if (_isLabelOverlap(localPosition, damage_num, additionalDamageNumList[j]))
			{
				return true;
			}
		}
		return false;
	}

	private bool _isLabelOverlap(Vector3 nextPosition, UIDamageNum damage_num, UIDamageNum damageNumLists)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (damageNumLists == null || damage_num == null)
		{
			return false;
		}
		if (!damageNumLists.enable || damage_num == damageNumLists)
		{
			return false;
		}
		Vector3 localPosition = damageNumLists.get_transform().get_localPosition();
		float num = Mathf.Abs(localPosition.x - nextPosition.x);
		float num2 = Mathf.Abs(localPosition.y - nextPosition.y);
		float num3 = 16f * (float)damageNumLists.DamageLength;
		if (num < num3 && num2 < 15f)
		{
			return true;
		}
		return false;
	}

	public UIPlayerDamageNum CreatePlayerDamage(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		UIPlayerDamageNum uIPlayerDamageNum = CreatePlayerDamageNum();
		if (uIPlayerDamageNum == null)
		{
			return null;
		}
		if (!uIPlayerDamageNum.Initialize(chara, damage, color))
		{
			return null;
		}
		return uIPlayerDamageNum;
	}

	public UIPlayerDamageNum CreatePlayerDamage(Character chara, AttackedHitStatus status)
	{
		UIPlayerDamageNum uIPlayerDamageNum = CreatePlayerDamageNum();
		if (uIPlayerDamageNum == null)
		{
			return null;
		}
		if (!uIPlayerDamageNum.Initialize(chara, status))
		{
			return null;
		}
		return uIPlayerDamageNum;
	}

	public UIPlayerDamageNum CreatePlayerRecoverHp(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			return null;
		}
		UIPlayerDamageNum uIPlayerDamageNum = null;
		for (int i = 0; i < playerRecoverNumList.Count; i++)
		{
			if (!playerRecoverNumList[i].enable)
			{
				uIPlayerDamageNum = playerRecoverNumList[i];
				break;
			}
		}
		if (uIPlayerDamageNum == null)
		{
			GameObject val = Object.Instantiate(m_playerDamageNumObj);
			if (val == null)
			{
				return null;
			}
			Utility.Attach(base._transform, val.get_transform());
			uIPlayerDamageNum = val.GetComponent<UIPlayerDamageNum>();
			if (uIPlayerDamageNum == null)
			{
				Object.Destroy(val);
				return null;
			}
			uIPlayerDamageNum.offset = OFFSET_RECOVER_NUM;
			playerRecoverNumList.Add(uIPlayerDamageNum);
		}
		if (!uIPlayerDamageNum.Initialize(chara, damage, color))
		{
			return null;
		}
		return uIPlayerDamageNum;
	}

	private UIPlayerDamageNum CreatePlayerDamageNum()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			return null;
		}
		UIPlayerDamageNum uIPlayerDamageNum = null;
		int i = 0;
		for (int count = playerDamageNumList.Count; i < count; i++)
		{
			if (!playerDamageNumList[i].enable)
			{
				uIPlayerDamageNum = playerDamageNumList[i];
				break;
			}
		}
		if (uIPlayerDamageNum == null)
		{
			GameObject val = Object.Instantiate(m_playerDamageNumObj);
			if (val == null)
			{
				return null;
			}
			Utility.Attach(base._transform, val.get_transform());
			uIPlayerDamageNum = val.GetComponent<UIPlayerDamageNum>();
			if (uIPlayerDamageNum == null)
			{
				Object.Destroy(val);
				return null;
			}
			playerDamageNumList.Add(uIPlayerDamageNum);
		}
		return uIPlayerDamageNum;
	}

	public UIPlayerDamageNum CreateEnemyRecoverHp(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			return null;
		}
		GameObject val = Object.Instantiate(m_playerDamageNumObj);
		if (val == null)
		{
			return null;
		}
		Utility.Attach(base._transform, val.get_transform());
		UIPlayerDamageNum component = val.GetComponent<UIPlayerDamageNum>();
		if (component == null)
		{
			Object.Destroy(val);
			return null;
		}
		enemyRecoverNumList.Add(component);
		if (!component.Initialize(chara, damage, color, isAutoPlay: false))
		{
			return null;
		}
		component.EnableAutoDelete();
		return component;
	}

	private void LateUpdate()
	{
		EnemyRecoverHpUIProc();
	}

	private void EnemyRecoverHpUIProc()
	{
		if (currentRecoverNum != null)
		{
			if (currentRecoverNum.AlphaRate <= 0.8f || !currentRecoverNum.enable)
			{
				currentRecoverNum = null;
			}
		}
		else if (enemyRecoverNumList.Count > 0)
		{
			currentRecoverNum = enemyRecoverNumList[0];
			currentRecoverNum.Play();
			enemyRecoverNumList.RemoveAt(0);
		}
	}
}
