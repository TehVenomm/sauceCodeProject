using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPanelNGUI : MonoBehaviour
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

	private void Awake()
	{
		aspect = (float)Screen.width / (float)Screen.height;
		FixedNGUIThrowIphoneX component = GetComponent<FixedNGUIThrowIphoneX>();
		resulotion = default(Vector2);
		if (IsIphoneX())
		{
			if (component != null)
			{
				resulotion = GetResolutionFixed(component.LockRatioPosition);
			}
			else
			{
				resulotion = GetResolutionFixed();
			}
		}
		else
		{
			resulotion = GetResolutionFixed();
		}
		float num = resulotion.x / resulotion.y;
		ratioHeight = num / BASERATIO;
		ratio = aspect / BASERATIO;
		if (aspect < BASERATIO)
		{
			if (Root == null)
			{
				Root = GameObject.Find("UI_Root").transform;
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
		if (!(aspect < BASERATIO))
		{
			return;
		}
		if (m_mainChild == null)
		{
			m_mainChild = base.transform;
		}
		findParent();
		if (IsIphoneX())
		{
			detectCameraParent = checkCameraParent(base.gameObject);
			if (detectCameraParent)
			{
				m_parent.localPosition += Vector3.down * GetOffseMoveHeightIfIphoneX(resulotion.y) / 2f;
			}
		}
		if (checkExistComponent() || FixedPanelAction == FixedPanelAction.NONE)
		{
			StartCoroutine(fixedNGUI());
		}
	}

	public static bool checkCameraParent(GameObject obj)
	{
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			if (obj.GetComponent<Camera>() != null)
			{
				return true;
			}
		}
		return false;
	}

	private bool checkExistComponent()
	{
		Transform parent = m_parent;
		while (parent.transform.parent != null && parent.transform.parent != Root && parent.transform.parent.name != "AppMain")
		{
			parent = parent.transform.parent;
			foreach (Transform item in parent)
			{
				if (item != base.transform && item.GetComponent<FixedPanelNGUI>() != null)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void findParent()
	{
		m_parent = base.transform;
		if (m_depthParent > 0)
		{
			for (int i = 0; i < m_depthParent; i++)
			{
				m_parent = m_parent.parent;
			}
		}
	}

	private IEnumerator fixedNGUI()
	{
		Vector3 localScale = Vector3.one * ratio;
		if (!(aspect < BASERATIO))
		{
			yield break;
		}
		lockUI(isLock: false);
		List<Vector3> list = new List<Vector3>();
		foreach (Transform item in m_mainChild)
		{
			list.Add(item.position);
		}
		findListPositionLockTransform();
		bool flag = true;
		switch (FixedPanelAction)
		{
		case FixedPanelAction.NONE:
			flag = false;
			break;
		case FixedPanelAction.FIX_SIZE:
			m_parent.localScale = localScale;
			break;
		}
		if (flag)
		{
			for (int i = 0; i < list.Count; i++)
			{
				Transform child = m_mainChild.GetChild(i);
				UIRect component = child.GetComponent<UIRect>();
				if (component != null && component.isAnchoredHorizontally)
				{
					child.position = list[i];
					continue;
				}
				Vector3 localPosition = m_mainChild.GetChild(i).localPosition;
				m_mainChild.GetChild(i).localPosition = new Vector3(localPosition.x, localPosition.y / ratioHeight, localPosition.z);
			}
		}
		fixLockScale(ratio);
		fixScaleChilds(ratio);
		yield return StartCoroutine(fixedLockTransform(ratio));
		UpdateAnchor();
		FixOffsetHeigh();
		if (anchorCheat != null)
		{
			anchorCheat.SetAnchor((Transform)null);
		}
	}

	private void findListPositionLockTransform()
	{
		if (m_lockPosition != null)
		{
			m_listOldLockPosition = new List<Vector3>();
			for (int i = 0; i < m_lockPosition.Length; i++)
			{
				m_listOldLockPosition.Add(m_lockPosition[i].position);
			}
		}
	}

	private IEnumerator fixedLockTransform(float ratioScreen)
	{
		yield return null;
		if (m_lockPosition != null)
		{
			for (int i = 0; i < m_lockPosition.Length; i++)
			{
				m_lockPosition[i].position = m_listOldLockPosition[i];
			}
		}
	}

	private void fixLockScale(float ratio)
	{
		Transform[] lockScaleUI = m_lockScaleUI;
		for (int i = 0; i < lockScaleUI.Length; i++)
		{
			lockScaleUI[i].localScale /= ratio;
		}
	}

	private void fixScaleChilds(float ratio)
	{
		Transform[] scaleUIChilds = m_scaleUIChilds;
		for (int i = 0; i < scaleUIChilds.Length; i++)
		{
			scaleUIChilds[i].localScale *= ratio;
		}
	}

	private void ChangeTarget(Transform root)
	{
		if (m_lockAnchor == null)
		{
			return;
		}
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

	private void saveBeginStaticUI()
	{
		if (m_unlockStatic == null || m_unlockStatic.Length == 0)
		{
			return;
		}
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

	private void lockUI(bool isLock)
	{
		if (m_beginStaticUI == null)
		{
			return;
		}
		for (int i = 0; i < m_beginStaticUI.Length; i++)
		{
			BeginStaticUI beginStaticUI = m_beginStaticUI[i];
			if (!(beginStaticUI.UIPanel != null))
			{
				continue;
			}
			if (beginStaticUI.UiStatic != null)
			{
				if (isLock)
				{
					beginStaticUI.UiStatic.enabled = true;
					beginStaticUI.UIPanel.widgetsAreStatic = beginStaticUI.BeginStatic;
				}
				else
				{
					beginStaticUI.UiStatic.enabled = false;
					beginStaticUI.UIPanel.widgetsAreStatic = false;
				}
			}
			else if (beginStaticUI.UIRotationStatic != null)
			{
				if (isLock)
				{
					beginStaticUI.UIRotationStatic.enabled = true;
					beginStaticUI.UIPanel.widgetsAreStatic = beginStaticUI.BeginStatic;
				}
				else
				{
					beginStaticUI.UIRotationStatic.enabled = false;
					beginStaticUI.UIPanel.widgetsAreStatic = false;
				}
			}
		}
	}

	public void FixOffsetHeigh()
	{
		if (m_fixOffsetPosition == null)
		{
			return;
		}
		FixOffsetHeigh[] fixOffsetPosition = m_fixOffsetPosition;
		foreach (FixOffsetHeigh fixOffsetHeigh in fixOffsetPosition)
		{
			if (!fixOffsetHeigh.IsOnlyInphoneX || (fixOffsetHeigh.IsOnlyInphoneX && IsIphoneX()))
			{
				fixOffsetHeigh.ObjectMove.localPosition += fixOffsetHeigh.OffsetHeigh * Vector3.up;
			}
			fixOffsetHeigh.ObjectMove.localPosition += fixOffsetHeigh.OffsetWidt * Vector3.right;
		}
	}

	public void UpdateAnchor()
	{
		if (m_updateNewAnchor == null)
		{
			return;
		}
		Transform[] updateNewAnchor = m_updateNewAnchor;
		for (int i = 0; i < updateNewAnchor.Length; i++)
		{
			UIRect component = updateNewAnchor[i].GetComponent<UIRect>();
			if (component != null)
			{
				component.UpdateAnchors();
			}
		}
	}

	public static bool CheckResolutionCanFix()
	{
		float num = (float)Screen.width / (float)Screen.height;
		Vector2 resolutionFixed = GetResolutionFixed();
		_ = resolutionFixed.x / resolutionFixed.y / BASERATIO;
		return num < BASERATIO;
	}

	public static float GetScaleResolution()
	{
		float num = (float)Screen.width / (float)Screen.height;
		Vector2 resolutionFixed = GetResolutionFixed();
		_ = resolutionFixed.x / resolutionFixed.y / BASERATIO;
		return num / BASERATIO;
	}

	public static Vector2 GetResolutionFixed(bool isThrowIphoneX = false)
	{
		float num = Screen.width;
		float num2 = Screen.height;
		float x;
		float num3;
		if (num > num2)
		{
			x = 854f;
			num3 = num2 / num * 854f;
		}
		else
		{
			num3 = 854f;
			x = num / num2 * 854f;
		}
		if (!isThrowIphoneX && IsIphoneX())
		{
			float offseMoveHeightIfIphoneX = GetOffseMoveHeightIfIphoneX(num3);
			num3 -= offseMoveHeightIfIphoneX;
		}
		return new Vector2(x, num3);
	}

	public static float GetOffseMoveHeightIfIphoneX(float screenHeight)
	{
		return 75f * (screenHeight / 2436f);
	}

	public static bool IsIphoneX()
	{
		_ = Screen.width;
		_ = Screen.height;
		return false;
	}
}
