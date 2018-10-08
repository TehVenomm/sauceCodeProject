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

	private int additionalGroupOffset;

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

	public void Create(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffset = 0, int effective = 0)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_gameObject().get_activeInHierarchy())
		{
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
			if (groupOffset > 0 || flag)
			{
				CreateAdditionalDamage(pos, damage, color, groupOffset, originalDamage, effective);
			}
			else
			{
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
				additionalGroupOffset = 0;
				originalDamage = null;
				int num2 = 0;
				int num3 = 0;
				while (num2 < damageNumList.Count)
				{
					if (num2 != num && damageNumList[num2].enable)
					{
						if (num3 > 10)
						{
							groupOffset = 0;
							additionalGroupOffset = 0;
							break;
						}
						uIDamageNum.get_transform().set_position(uIDamageNum.GetUIPosFromWorld(pos, groupOffset));
						Vector3 localPosition = uIDamageNum.get_transform().get_localPosition();
						Vector3 localPosition2 = damageNumList[num2].get_transform().get_localPosition();
						if (Mathf.Abs(localPosition2.x - localPosition.x) < uIDamageNum.grid.cellWidth * (float)damageNumList[num2].DamageLength && Mathf.Abs(localPosition2.y - localPosition.y) < uIDamageNum.grid.cellHeight - uIDamageNum.grid.cellHeight / 4f)
						{
							groupOffset++;
							additionalGroupOffset++;
							num2 = 0;
							num3++;
							continue;
						}
					}
					num2++;
				}
				originalDamage = uIDamageNum;
				if (!uIDamageNum.Initialize(pos, damage, color, groupOffset))
				{
					return;
				}
			}
		}
	}

	private void CreateAdditionalDamage(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffet, UIDamageNum originalDamage, int effective)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
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
		groupOffet += additionalGroupOffset;
		if (!uIAdditionalDamageNum.Initialize(pos, damage, color, groupOffet, originalDamage, effective))
		{
			return;
		}
	}

	public UIPlayerDamageNum CreatePlayerDamage(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		UIPlayerDamageNum uIPlayerDamageNum = CreatePlayerDamageNum();
		if (uIPlayerDamageNum == null)
		{
			return null;
		}
		if (!uIPlayerDamageNum.Initialize(chara, damage, color, true))
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
		if (!uIPlayerDamageNum.Initialize(chara, status, true))
		{
			return null;
		}
		return uIPlayerDamageNum;
	}

	public UIPlayerDamageNum CreatePlayerRecoverHp(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
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
		if (!uIPlayerDamageNum.Initialize(chara, damage, color, true))
		{
			return null;
		}
		return uIPlayerDamageNum;
	}

	private UIPlayerDamageNum CreatePlayerDamageNum()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
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
		if (!component.Initialize(chara, damage, color, false))
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
