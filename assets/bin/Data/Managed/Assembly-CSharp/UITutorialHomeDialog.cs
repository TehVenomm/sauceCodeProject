using System;
using UnityEngine;

public class UITutorialHomeDialog
{
	private const int oneLine = 0;

	private const int twoLine = 1;

	private const int threeLine = 2;

	[SerializeField]
	private UIPanel[] root;

	[SerializeField]
	private UISprite[] messageLine0;

	[SerializeField]
	private UISprite[] messageLine1;

	[SerializeField]
	private UISprite[] messageLine2;

	[SerializeField]
	private UIPanel lastTutorialPanel;

	[SerializeField]
	private UISprite lastTutorialSprite;

	[SerializeField]
	private UIButton lastTutorialButton;

	private UIAtlas lastTutorialAtlas;

	[SerializeField]
	private UIAtlas[] atlases;

	public GameObject AfterGacha2Tutorial;

	public UITutorialHomeDialog()
		: this()
	{
	}

	public void OpenAfterGacha2()
	{
		AfterGacha2Tutorial.SetActive(true);
		AfterGacha2Tutorial.GetComponent<UIPanel>().alpha = 0f;
		TweenAlpha.Begin(AfterGacha2Tutorial, 0.3f, 1f);
	}

	public void CloseAfterGacha2(Action onClose = null)
	{
		TweenAlpha ta = TweenAlpha.Begin(AfterGacha2Tutorial, 0.3f, 0f);
		if (onClose != null)
		{
			ta.AddOnFinished(delegate
			{
				Object.DestroyImmediate(ta);
				if (onClose != null)
				{
					onClose.Invoke();
				}
			});
		}
	}

	public void Open(int atlasIndex0, string spriteName0)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		root[2].get_gameObject().SetActive(false);
		root[1].get_gameObject().SetActive(false);
		if (!root[0].get_gameObject().get_activeInHierarchy())
		{
			root[0].get_gameObject().SetActive(true);
		}
		messageLine0[0].atlas = atlases[atlasIndex0];
		messageLine0[0].spriteName = spriteName0;
		root[0].alpha = 0f;
		TweenAlpha.Begin(root[0].get_gameObject(), 0.3f, 1f);
	}

	public void Open(int atlasIndex0, string spriteName0, int atlasIndex1, string spriteName1)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		root[2].get_gameObject().SetActive(false);
		if (!root[1].get_gameObject().get_activeInHierarchy())
		{
			root[1].get_gameObject().SetActive(true);
		}
		messageLine0[1].atlas = atlases[atlasIndex0];
		messageLine0[1].spriteName = spriteName0;
		messageLine1[1].atlas = atlases[atlasIndex1];
		messageLine1[1].spriteName = spriteName1;
		root[1].alpha = 0f;
		TweenAlpha.Begin(root[1].get_gameObject(), 0.3f, 1f);
	}

	public void Open(int atlasIndex0, string spriteName0, int atlasIndex1, string spriteName1, int atlasIndex2, string spriteName2)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		root[1].get_gameObject().SetActive(false);
		if (!root[2].get_gameObject().get_activeInHierarchy())
		{
			root[2].get_gameObject().SetActive(true);
		}
		messageLine0[2].atlas = atlases[atlasIndex0];
		messageLine0[2].spriteName = spriteName0;
		messageLine1[2].atlas = atlases[atlasIndex1];
		messageLine1[2].spriteName = spriteName1;
		messageLine2[2].atlas = atlases[atlasIndex2];
		messageLine2[2].spriteName = spriteName2;
		root[2].alpha = 0f;
		TweenAlpha.Begin(root[2].get_gameObject(), 0.3f, 1f);
	}

	public void SetLastTutorialAtlas(UIAtlas atlas)
	{
		lastTutorialAtlas = atlas;
	}

	public void OpenLastTutorial()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		if (lastTutorialAtlas != null)
		{
			lastTutorialPanel.get_gameObject().SetActive(true);
			lastTutorialSprite.get_gameObject().SetActive(true);
			lastTutorialSprite.atlas = lastTutorialAtlas;
			lastTutorialSprite.spriteName = "Tutorial_Matome";
			lastTutorialButton.onClick.Clear();
			TweenAlpha tweenAlpha = TweenAlpha.Begin(lastTutorialSprite.get_gameObject(), 0.3f, 1f);
			tweenAlpha.AddOnFinished(delegate
			{
				lastTutorialButton.onClick.Add(new EventDelegate(CloseLastTutorial));
			});
		}
	}

	public void CloseLastTutorial()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		TweenAlpha ta = TweenAlpha.Begin(lastTutorialSprite.get_gameObject(), 0.3f, 0f);
		ta.AddOnFinished(delegate
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			if (lastTutorialAtlas != null)
			{
				Object.DestroyObject(lastTutorialAtlas);
			}
			Object.DestroyImmediate(ta);
			Object.DestroyImmediate(lastTutorialSprite.get_gameObject());
			lastTutorialSprite = null;
			lastTutorialButton = null;
			lastTutorialAtlas = null;
		});
	}

	public void Close(int lineIndex = 0, Action onClose = null)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		if (AfterGacha2Tutorial.get_gameObject().get_activeSelf())
		{
			CloseAfterGacha2(onClose);
		}
		TweenAlpha ta = TweenAlpha.Begin(root[lineIndex].get_gameObject(), 0.3f, 0f);
		if (onClose != null)
		{
			ta.AddOnFinished(delegate
			{
				Object.DestroyImmediate(ta);
				if (onClose != null)
				{
					onClose.Invoke();
				}
			});
		}
	}
}
