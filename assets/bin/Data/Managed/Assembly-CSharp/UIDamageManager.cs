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
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		float pixelHeight = MonoBehaviourSingleton<InGameCameraManager>.I.GetPixelHeight();
		Vector3 pos2 = new Vector3(pos.x, pos.y + MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset, pos.z);
		Vector3 pos3 = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(pos2);
		if (pos3.y > pixelHeight)
		{
			pos3.y = pixelHeight;
			pos = MonoBehaviourSingleton<InGameCameraManager>.I.ScreenToWorldPoint(pos3);
			pos.y -= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset;
		}
		bool flag = UIDamageNum.DAMAGE_COLOR.BUFF != color && color != UIDamageNum.DAMAGE_COLOR.NONE;
		if (isRegionOnly)
		{
			color = (flag ? UIDamageNum.DAMAGE_COLOR.REGION_ONLY_ELEMENT : ((UIDamageNum.DAMAGE_COLOR.BUFF != color) ? UIDamageNum.DAMAGE_COLOR.REGION_ONLY_NORMAL : UIDamageNum.DAMAGE_COLOR.REGION_ONLY_BUFF));
		}
		if ((groupOffset > 0) | flag)
		{
			CreateAdditionalDamage(pos, damage, color, groupOffset, originalDamage, effective);
			return;
		}
		UIDamageNum uIDamageNum = null;
		int i = 0;
		for (int count = damageNumList.Count; i < count; i++)
		{
			if (!damageNumList[i].enable)
			{
				uIDamageNum = damageNumList[i];
				break;
			}
		}
		if (uIDamageNum == null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(m_damageNumObj);
			if (gameObject == null)
			{
				return;
			}
			Utility.Attach(base._transform, gameObject.transform);
			uIDamageNum = gameObject.GetComponent<UIDamageNum>();
			if (uIDamageNum == null)
			{
				Object.Destroy(gameObject);
				return;
			}
			damageNumList.Add(uIDamageNum);
			_ = damageNumList.Count;
		}
		groupOffset = CalcOffsetPosition(pos, uIDamageNum, groupOffset);
		originalDamage = uIDamageNum;
		uIDamageNum.Initialize(pos, damage, color, groupOffset);
	}

	private void CreateAdditionalDamage(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffset, UIDamageNum originalDamage, int effective)
	{
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
			GameObject gameObject = (GameObject)Object.Instantiate(m_additionalDamageNumObj);
			if (gameObject == null)
			{
				return;
			}
			Utility.Attach(base._transform, gameObject.transform);
			uIAdditionalDamageNum = gameObject.GetComponent<UIAdditionalDamageNum>();
			if (uIAdditionalDamageNum == null)
			{
				Object.Destroy(gameObject);
				return;
			}
			additionalDamageNumList.Add(uIAdditionalDamageNum);
		}
		groupOffset = CalcOffsetPosition(pos, uIAdditionalDamageNum, groupOffset);
		uIAdditionalDamageNum.Initialize(pos, damage, color, groupOffset, originalDamage, effective);
	}

	private int CalcOffsetPosition(Vector3 pos, UIDamageNum damage_num, int orgGroupOffet)
	{
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
		damage_num.transform.position = damage_num.GetUIPosFromWorld(pos, groupOffset);
		Vector3 localPosition = damage_num.transform.localPosition;
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
		if (damageNumLists == null || damage_num == null)
		{
			return false;
		}
		if (!damageNumLists.enable || damage_num == damageNumLists)
		{
			return false;
		}
		Vector3 localPosition = damageNumLists.transform.localPosition;
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
		if (!base.gameObject.activeInHierarchy)
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
			GameObject gameObject = (GameObject)Object.Instantiate(m_playerDamageNumObj);
			if (gameObject == null)
			{
				return null;
			}
			Utility.Attach(base._transform, gameObject.transform);
			uIPlayerDamageNum = gameObject.GetComponent<UIPlayerDamageNum>();
			if (uIPlayerDamageNum == null)
			{
				Object.Destroy(gameObject);
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
		if (!base.gameObject.activeInHierarchy)
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
			GameObject gameObject = (GameObject)Object.Instantiate(m_playerDamageNumObj);
			if (gameObject == null)
			{
				return null;
			}
			Utility.Attach(base._transform, gameObject.transform);
			uIPlayerDamageNum = gameObject.GetComponent<UIPlayerDamageNum>();
			if (uIPlayerDamageNum == null)
			{
				Object.Destroy(gameObject);
				return null;
			}
			playerDamageNumList.Add(uIPlayerDamageNum);
		}
		return uIPlayerDamageNum;
	}

	public UIPlayerDamageNum CreateEnemyRecoverHp(Character chara, int damage, UIPlayerDamageNum.DAMAGE_COLOR color)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return null;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(m_playerDamageNumObj);
		if (gameObject == null)
		{
			return null;
		}
		Utility.Attach(base._transform, gameObject.transform);
		UIPlayerDamageNum component = gameObject.GetComponent<UIPlayerDamageNum>();
		if (component == null)
		{
			Object.Destroy(gameObject);
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
