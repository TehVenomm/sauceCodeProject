using UnityEngine;

public class UIStaticPanelRotateCheck : MonoBehaviour
{
	[SerializeField]
	protected UIPanel panel;

	private int updateCount;

	private bool updateAnchors = true;

	protected UIAnchor[] anchors;

	private void Awake()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		anchors = GetComponentsInChildren<UIAnchor>();
		updateCount = 0;
		updateAnchors = true;
		panel.widgetsAreStatic = false;
	}

	private void OnEnable()
	{
		updateCount = 0;
		updateAnchors = true;
		panel.widgetsAreStatic = false;
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	protected virtual void Update()
	{
		if (!panel.widgetsAreStatic)
		{
			updateCount++;
			if (updateCount >= 3)
			{
				if (updateAnchors)
				{
					int i = 0;
					for (int num = anchors.Length; i < num; i++)
					{
						anchors[i].enabled = true;
					}
					GetComponentsInChildren(true, Temporary.uiRectList);
					int j = 0;
					for (int count = Temporary.uiRectList.Count; j < count; j++)
					{
						Temporary.uiRectList[j].UpdateAnchors();
					}
					Temporary.uiRectList.Clear();
					updateAnchors = false;
				}
				else
				{
					panel.widgetsAreStatic = true;
				}
			}
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		updateCount = 0;
		panel.widgetsAreStatic = false;
		panel.ForceUpDate();
	}
}
