using System;
using UnityEngine;

public class UITutorialDialog
{
	private const int oneLine = 0;

	private const int twoLine = 1;

	private const int threeLine = 2;

	private const int threeLineLabel = 3;

	private const int threeLineLabel2 = 4;

	[SerializeField]
	private UIPanel[] root;

	[SerializeField]
	private UISprite[] messageLine0;

	[SerializeField]
	private UISprite[] messageLine1;

	[SerializeField]
	private UISprite[] messageLine2;

	[SerializeField]
	private UIAtlas[] atlases;

	public UILabel lbGreeting;

	public UILabel lbChargeWaypoint;

	public UITutorialDialog()
		: this()
	{
	}

	public void Open(int atlasIndex0, string spriteName0)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		root[1].get_gameObject().SetActive(false);
		root[2].get_gameObject().SetActive(false);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		root[0].get_gameObject().SetActive(false);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		root[0].get_gameObject().SetActive(false);
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

	public void OpenThreeLineLabel()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		if (root[1].get_gameObject().get_activeInHierarchy())
		{
			TweenAlpha.Begin(root[1].get_gameObject(), 0f, 0f);
		}
		if (!root[3].get_gameObject().get_activeInHierarchy())
		{
			root[3].get_gameObject().SetActive(true);
		}
		lbGreeting.supportEncoding = true;
		root[3].alpha = 0f;
		TweenAlpha.Begin(root[3].get_gameObject(), 0.3f, 1f);
	}

	public void HideThreeLineLabel()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		TweenAlpha.Begin(root[3].get_gameObject(), 0.3f, 0f);
	}

	public void OpenThreeLineLabel2()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		if (!root[4].get_gameObject().get_activeInHierarchy())
		{
			root[4].get_gameObject().SetActive(true);
		}
		lbChargeWaypoint.supportEncoding = true;
		root[4].alpha = 1f;
		TweenAlpha.Begin(root[4].get_gameObject(), 3f, 1f).AddOnFinished(delegate
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			root[4].get_gameObject().SetActive(false);
		});
	}

	public bool isThreeLineLabel2Active()
	{
		return root[4].get_isActiveAndEnabled();
	}

	public void HideThreeLineLabel2()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		root[4].get_gameObject().SetActive(false);
	}

	public void Close(int lineIndex = 0, Action onClose = null)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
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
