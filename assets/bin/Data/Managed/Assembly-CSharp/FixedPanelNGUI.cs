using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPanelNGUI
{
	private class BeginStaticUI
	{
		public UIStaticPanelChanger UiStatic;

		public UIStaticPanelRotateCheck UIRotationStatic;

		public UIPanel UIPanel;

		public bool BeginStatic;
	}

	public const float OffsetTopIphoneX = 75f;

	public static float BASERATIO = 0.5590062f;

	public Transform m_parent;

	public int m_depthParent = 1;

	public FixedPanelAction FixedPanelAction;

	public Transform m_mainChild;

	public Transform[] m_lockPosition;

	public Transform[] m_lockScaleUI;

	public Transform[] m_scaleUIChilds;

	[SerializeField]
	public Transform[] m_updateNewAnchor;

	[SerializeField]
	public Transform[] m_unlockStatic;

	[SerializeField]
	public LockAnchorRoot[] m_lockAnchor;

	[SerializeField]
	public FixOffsetHeigh[] m_fixOffsetPosition;

	private float ratio;

	private float ratioHeight;

	public static Transform Root;

	private float aspect;

	private Vector2 resulotion;

	public bool isLockAnchor;

	private bool detectCameraParent;

	public UIRect anchorCheat;

	private List<Vector3> m_listOldLockPosition;

	private BeginStaticUI[] m_beginStaticUI;

	public FixedPanelNGUI()
		: this()
	{
	}

	private void Awake()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		aspect = (float)Screen.get_width() / (float)Screen.get_height();
		FixedNGUIThrowIphoneX component = this.GetComponent<FixedNGUIThrowIphoneX>();
		resulotion = default(Vector2);
		if (IsIphoneX())
		{
			if (component != null)
			{
				resulotion = GetResolutionFixed(component.LockRatioPosition);
			}
			else
			{
				resulotion = GetResolutionFixed(false);
			}
		}
		else
		{
			resulotion = GetResolutionFixed(false);
		}
		float num = resulotion.x / resulotion.y;
		ratioHeight = num / BASERATIO;
		ratio = aspect / BASERATIO;
		if (aspect < BASERATIO)
		{
			if (Root == null)
			{
				Root = GameObject.Find("UI_Root").get_transform();
			}
			if (Root != null)
			{
				ChangeTarget(Root);
			}
			saveBeginStaticUI();
		}
	}

	private void Start()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		if (aspect < BASERATIO)
		{
			if (m_mainChild == null)
			{
				m_mainChild = this.get_transform();
			}
			findParent();
			if (IsIphoneX())
			{
				detectCameraParent = checkCameraParent(this.get_gameObject());
				if (detectCameraParent)
				{
					Transform parent = m_parent;
					parent.set_localPosition(parent.get_localPosition() + Vector3.get_down() * GetOffseMoveHeightIfIphoneX(resulotion.y) / 2f);
				}
			}
			if (checkExistComponent() || FixedPanelAction == FixedPanelAction.NONE)
			{
				this.StartCoroutine(fixedNGUI());
			}
		}
	}

	public static bool checkCameraParent(GameObject obj)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		while (obj.get_transform().get_parent() != null)
		{
			obj = obj.get_transform().get_parent().get_gameObject();
			if (obj.GetComponent<Camera>() != null)
			{
				return true;
			}
		}
		return false;
	}

	private bool checkExistComponent()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Transform val = m_parent;
		while (val.get_transform().get_parent() != null && val.get_transform().get_parent() != Root && val.get_transform().get_parent().get_name() != "AppMain")
		{
			val = val.get_transform().get_parent();
			foreach (Transform item in val)
			{
				Transform val2 = item;
				if (val2 != this.get_transform() && val2.GetComponent<FixedPanelNGUI>() != null)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void findParent()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		m_parent = this.get_transform();
		if (m_depthParent > 0)
		{
			for (int i = 0; i < m_depthParent; i++)
			{
				m_parent = m_parent.get_parent();
			}
		}
	}

	private IEnumerator fixedNGUI()
	{
		Vector3 localScale = Vector3.get_one() * ratio;
		if (aspect < BASERATIO)
		{
			lockUI(false);
			List<Vector3> oldPosition = new List<Vector3>();
			foreach (Transform item in m_mainChild)
			{
				Transform child2 = item;
				oldPosition.Add(child2.get_position());
			}
			findListPositionLockTransform();
			bool isContinue = true;
			switch (FixedPanelAction)
			{
			case FixedPanelAction.NONE:
				isContinue = false;
				break;
			case FixedPanelAction.FIX_SIZE:
				m_parent.set_localScale(localScale);
				break;
			}
			if (isContinue)
			{
				for (int i = 0; i < oldPosition.Count; i++)
				{
					Transform child = m_mainChild.GetChild(i);
					UIRect uiRect = child.GetComponent<UIRect>();
					if (uiRect != null && uiRect.isAnchoredHorizontally)
					{
						child.set_position(oldPosition[i]);
					}
					else
					{
						Vector3 local = m_mainChild.GetChild(i).get_localPosition();
						m_mainChild.GetChild(i).set_localPosition(new Vector3(local.x, local.y / ratioHeight, local.z));
					}
				}
			}
			fixLockScale(ratio);
			fixScaleChilds(ratio);
			yield return (object)this.StartCoroutine(fixedLockTransform(ratio));
			UpdateAnchor();
			FixOffsetHeigh();
			if (anchorCheat != null)
			{
				anchorCheat.SetAnchor(null);
			}
		}
	}

	private void findListPositionLockTransform()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (m_lockPosition != null)
		{
			m_listOldLockPosition = new List<Vector3>();
			for (int i = 0; i < m_lockPosition.Length; i++)
			{
				m_listOldLockPosition.Add(m_lockPosition[i].get_position());
			}
		}
	}

	private IEnumerator fixedLockTransform(float ratioScreen)
	{
		yield return (object)null;
		if (m_lockPosition != null)
		{
			for (int i = 0; i < m_lockPosition.Length; i++)
			{
				m_lockPosition[i].set_position(m_listOldLockPosition[i]);
			}
		}
	}

	private void fixLockScale(float ratio)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Transform[] lockScaleUI = m_lockScaleUI;
		foreach (Transform val in lockScaleUI)
		{
			Transform obj = val;
			obj.set_localScale(obj.get_localScale() / ratio);
		}
	}

	private void fixScaleChilds(float ratio)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Transform[] scaleUIChilds = m_scaleUIChilds;
		foreach (Transform val in scaleUIChilds)
		{
			Transform obj = val;
			obj.set_localScale(obj.get_localScale() * ratio);
		}
	}

	private void ChangeTarget(Transform root)
	{
		if (m_lockAnchor != null)
		{
			LockAnchorRoot[] lockAnchor = m_lockAnchor;
			foreach (LockAnchorRoot lockAnchorRoot in lockAnchor)
			{
				UIRect component = lockAnchorRoot.Panel.GetComponent<UIRect>();
				if (component != null)
				{
					if (lockAnchorRoot.FullAnchor)
					{
						component.leftAnchor.target = (component.rightAnchor.target = (component.topAnchor.target = (component.bottomAnchor.target = root)));
					}
					else
					{
						component.leftAnchor.target = (component.rightAnchor.target = root);
					}
				}
			}
		}
	}

	private void saveBeginStaticUI()
	{
		if (m_unlockStatic != null && m_unlockStatic.Length > 0)
		{
			m_beginStaticUI = new BeginStaticUI[m_unlockStatic.Length];
			for (int i = 0; i < m_unlockStatic.Length; i++)
			{
				m_beginStaticUI[i] = new BeginStaticUI();
				m_beginStaticUI[i].UiStatic = m_unlockStatic[i].GetComponent<UIStaticPanelChanger>();
				if (m_beginStaticUI[i].UiStatic == null)
				{
					m_beginStaticUI[i].UIRotationStatic = m_unlockStatic[i].GetComponent<UIStaticPanelRotateCheck>();
				}
				m_beginStaticUI[i].UIPanel = m_unlockStatic[i].GetComponent<UIPanel>();
				m_beginStaticUI[i].BeginStatic = m_beginStaticUI[i].UIPanel.widgetsAreStatic;
			}
		}
	}

	private void lockUI(bool isLock)
	{
		if (m_beginStaticUI != null)
		{
			for (int i = 0; i < m_beginStaticUI.Length; i++)
			{
				BeginStaticUI beginStaticUI = m_beginStaticUI[i];
				if (beginStaticUI.UIPanel != null)
				{
					if (beginStaticUI.UiStatic != null)
					{
						if (isLock)
						{
							beginStaticUI.UiStatic.set_enabled(true);
							beginStaticUI.UIPanel.widgetsAreStatic = beginStaticUI.BeginStatic;
						}
						else
						{
							beginStaticUI.UiStatic.set_enabled(false);
							beginStaticUI.UIPanel.widgetsAreStatic = false;
						}
					}
					else if (beginStaticUI.UIRotationStatic != null)
					{
						if (isLock)
						{
							beginStaticUI.UIRotationStatic.set_enabled(true);
							beginStaticUI.UIPanel.widgetsAreStatic = beginStaticUI.BeginStatic;
						}
						else
						{
							beginStaticUI.UIRotationStatic.set_enabled(false);
							beginStaticUI.UIPanel.widgetsAreStatic = false;
						}
					}
				}
			}
		}
	}

	public void FixOffsetHeigh()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (m_fixOffsetPosition != null)
		{
			FixOffsetHeigh[] fixOffsetPosition = m_fixOffsetPosition;
			foreach (FixOffsetHeigh fixOffsetHeigh in fixOffsetPosition)
			{
				if (!fixOffsetHeigh.IsOnlyInphoneX || (fixOffsetHeigh.IsOnlyInphoneX && IsIphoneX()))
				{
					Transform objectMove = fixOffsetHeigh.ObjectMove;
					objectMove.set_localPosition(objectMove.get_localPosition() + fixOffsetHeigh.OffsetHeigh * Vector3.get_up());
				}
			}
		}
	}

	public void UpdateAnchor()
	{
		if (m_updateNewAnchor != null)
		{
			Transform[] updateNewAnchor = m_updateNewAnchor;
			foreach (Transform val in updateNewAnchor)
			{
				UIRect component = val.GetComponent<UIRect>();
				if (component != null)
				{
					component.UpdateAnchors();
				}
			}
		}
	}

	public static bool CheckResolutionCanFix()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)Screen.get_width() / (float)Screen.get_height();
		Vector2 resolutionFixed = GetResolutionFixed(false);
		float num2 = resolutionFixed.x / resolutionFixed.y;
		float num3 = num2 / BASERATIO;
		return num < BASERATIO;
	}

	public static float GetScaleResolution()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)Screen.get_width() / (float)Screen.get_height();
		Vector2 resolutionFixed = GetResolutionFixed(false);
		float num2 = resolutionFixed.x / resolutionFixed.y;
		float num3 = num2 / BASERATIO;
		return num / BASERATIO;
	}

	public static Vector2 GetResolutionFixed(bool isThrowIphoneX = false)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)Screen.get_width();
		float num2 = (float)Screen.get_height();
		float num3;
		float num4;
		if (num > num2)
		{
			num3 = 854f;
			num4 = num2 / num * 854f;
		}
		else
		{
			num4 = 854f;
			num3 = num / num2 * 854f;
		}
		if (!isThrowIphoneX && IsIphoneX())
		{
			float offseMoveHeightIfIphoneX = GetOffseMoveHeightIfIphoneX(num4);
			num4 -= offseMoveHeightIfIphoneX;
		}
		return new Vector2(num3, num4);
	}

	public static float GetOffseMoveHeightIfIphoneX(float screenHeight)
	{
		float num = 75f;
		return num * (screenHeight / 2436f);
	}

	public static bool IsIphoneX()
	{
		float num = (float)Screen.get_width();
		float num2 = (float)Screen.get_height();
		return false;
	}
}
