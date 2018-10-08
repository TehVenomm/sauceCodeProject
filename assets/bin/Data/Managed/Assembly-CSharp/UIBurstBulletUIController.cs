using System.Collections.Generic;
using UnityEngine;

public class UIBurstBulletUIController : MonoBehaviour
{
	public class InitParam
	{
		public int MaxBulletCount;

		public int CurrentRestBulletCount;
	}

	private const int MAX_BULLET_ICON_UI_GENERATE_COUNT = 15;

	private const int BASE_SPRITE_ADD_WIDTH = 28;

	private const int BASE_SPRITE_DEFAULT_WIDTH = 74;

	private const int EFFECT_SPRITE_WIDTH_DIFF = -30;

	private const float DIA_ICON_DEFULT_POS = 16.2f;

	private const float DIA_ICON_ADD_POS = 39.5f;

	private static readonly string BULLET_ICON_PATH = "InternalUI/UI_InGame/Burst/InGameUIBurstBulletIcon";

	[SerializeField]
	private GameObject m_iconRoot;

	[SerializeField]
	private UISprite m_baseSprite;

	[SerializeField]
	private UISprite m_emptyEffect;

	[SerializeField]
	private UISprite m_diamondIcon_L;

	[SerializeField]
	private UISprite m_diamondIcon_R;

	[SerializeField]
	private Transform m_bulletIconRoot;

	private List<UIBurstBulletIconController> m_bulletIcons = new List<UIBurstBulletIconController>(6);

	private int m_currentRestBulletIconCount;

	private int m_currentMaxBulletIconCount;

	private BoxCollider m_boxCol;

	public bool Initialize(InitParam _param)
	{
		m_currentMaxBulletIconCount = _param.MaxBulletCount;
		m_currentRestBulletIconCount = _param.CurrentRestBulletCount;
		for (int i = 0; i < m_currentMaxBulletIconCount; i++)
		{
			if (m_bulletIcons.Count > i || CreateBulletIcon(i))
			{
				if (m_currentMaxBulletIconCount <= i)
				{
					m_bulletIcons[i].SetInvisibleAllIcon();
				}
				else
				{
					m_bulletIcons[i].SetVisibleAllIcon();
				}
				if (m_currentRestBulletIconCount <= i)
				{
					m_bulletIcons[i].SetInvisibleBulletIcon();
				}
				else
				{
					m_bulletIcons[i].SetVisibleBulletIcon();
				}
			}
		}
		SetEmptyAppeal(m_currentRestBulletIconCount == 0);
		InitUISpriteParameter();
		if ((Object)m_boxCol == (Object)null && (Object)m_baseSprite != (Object)null)
		{
			m_boxCol = m_baseSprite.GetComponent<BoxCollider>();
		}
		m_boxCol.size = new Vector3((float)m_baseSprite.width, (float)m_baseSprite.height, 1f);
		m_boxCol.center = new Vector3((float)m_baseSprite.width / 2f, 0f, 0f);
		return true;
	}

	private bool CreateBulletIcon(int index)
	{
		if (index < 0 || 15 <= index)
		{
			return false;
		}
		Transform transform = ResourceUtility.Realizes(Resources.Load(BULLET_ICON_PATH), m_bulletIconRoot, -1);
		if ((Object)transform == (Object)null)
		{
			return false;
		}
		UIBurstBulletIconController component = transform.GetComponent<UIBurstBulletIconController>();
		if ((Object)component == (Object)null)
		{
			return false;
		}
		UIBurstBulletIconController.InitParam initParam = new UIBurstBulletIconController.InitParam();
		initParam.IconIndex = index;
		initParam.DepthOffset = ((!((Object)m_baseSprite == (Object)null)) ? (m_baseSprite.depth + 1) : 0);
		UIBurstBulletIconController.InitParam param = initParam;
		component.Initialize(param);
		m_bulletIcons.Add(component);
		return true;
	}

	private void InitUISpriteParameter()
	{
		if (!((Object)m_baseSprite == (Object)null))
		{
			int num = m_baseSprite.depth + 2 * (m_currentMaxBulletIconCount + 1) + 1;
			m_diamondIcon_L.depth = num;
			m_diamondIcon_R.depth = num;
			m_emptyEffect.depth = num + 1;
			int num2 = 74 + 28 * m_currentMaxBulletIconCount;
			m_baseSprite.width = num2;
			m_emptyEffect.width = num2 + -30;
			m_diamondIcon_R.transform.localPosition = Vector3.right * (16.2f + 39.5f * (float)m_currentMaxBulletIconCount);
		}
	}

	public bool ReloadAction()
	{
		bool result = false;
		if (!IsEnableReload())
		{
			return result;
		}
		result = SetVisibleSprite(m_currentRestBulletIconCount);
		if (result)
		{
			m_currentRestBulletIconCount++;
			if (!IsEmpty())
			{
				SetEmptyAppeal(false);
			}
		}
		return result;
	}

	public bool ConsumeBulletAction()
	{
		bool result = false;
		if (m_currentRestBulletIconCount <= 0)
		{
			return result;
		}
		m_currentRestBulletIconCount--;
		result = SetInvisibleSprite(m_currentRestBulletIconCount);
		if (!result)
		{
			m_currentRestBulletIconCount++;
		}
		else if (IsEmpty())
		{
			SetEmptyAppeal(true);
		}
		return result;
	}

	public bool FullBurstAction()
	{
		bool result = false;
		if (m_currentRestBulletIconCount <= 0)
		{
			return result;
		}
		result = true;
		for (int i = 0; i < m_currentRestBulletIconCount; i++)
		{
			SetInvisibleSprite(i);
		}
		m_currentRestBulletIconCount = 0;
		SetEmptyAppeal(true);
		return result;
	}

	public bool SetActivateIconRoot()
	{
		return SwitchActivateIconRoot(true);
	}

	public bool SetDeactivateIconRoot()
	{
		return SwitchActivateIconRoot(false);
	}

	private bool SwitchActivateIconRoot(bool _isActivate)
	{
		if ((Object)m_iconRoot == (Object)null || m_iconRoot.activeSelf == _isActivate)
		{
			return false;
		}
		m_iconRoot.SetActive(_isActivate);
		return true;
	}

	private bool SetVisibleSprite(int _targetIconIndex)
	{
		return SwitchSpriteVisible(_targetIconIndex, true);
	}

	private bool SetInvisibleSprite(int _targetIconIndex)
	{
		return SwitchSpriteVisible(_targetIconIndex, false);
	}

	private bool SwitchSpriteVisible(int _targetIconIndex, bool _isVisible)
	{
		if (!IsValidSpriteTarget(_targetIconIndex))
		{
			return false;
		}
		if (_isVisible)
		{
			return m_bulletIcons[_targetIconIndex].SetVisibleBulletIcon();
		}
		return m_bulletIcons[_targetIconIndex].SetInvisibleBulletIcon();
	}

	private bool SetEmptyAppeal(bool _isValid)
	{
		if ((Object)m_emptyEffect == (Object)null || m_emptyEffect.enabled == _isValid)
		{
			return false;
		}
		m_emptyEffect.enabled = _isValid;
		return true;
	}

	private bool IsValidSpriteTarget(int _targetIconIndex)
	{
		if (_targetIconIndex < 0 || m_bulletIcons.Count <= _targetIconIndex)
		{
			return false;
		}
		if ((Object)m_bulletIcons[_targetIconIndex] == (Object)null)
		{
			return false;
		}
		return true;
	}

	public void TryManualReload()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			Player targetPlayer = MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer;
			if (!((Object)targetPlayer == (Object)null) && targetPlayer.thsCtrl != null)
			{
				targetPlayer.thsCtrl.TryFirstReloadAction();
			}
		}
	}

	public bool IsEmpty()
	{
		return m_currentMaxBulletIconCount > 0 && m_currentRestBulletIconCount <= 0;
	}

	public bool IsEnableReload()
	{
		return m_currentMaxBulletIconCount > 0 && m_currentRestBulletIconCount < m_currentMaxBulletIconCount;
	}
}
