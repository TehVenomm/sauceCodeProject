using System;
using System.Collections;
using UnityEngine;

public class StampAppeal : UIBehaviour
{
	public enum UI
	{
		OBJ_TWEEN_ROOT,
		SPR_STAMP_BG,
		TXT_STAMP
	}

	private Transform rootPosition;

	private bool isAdjustment;

	private void Update()
	{
		if (rootPosition != null)
		{
			SetPosition();
		}
	}

	public void View(int stampId, Transform root, bool isAdjustmentPos)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		rootPosition = root;
		isAdjustment = isAdjustmentPos;
		this.StartCoroutine(DoDisplayChatStamp(stampId));
	}

	private IEnumerator DoDisplayChatStamp(int stampId)
	{
		SetActive((Enum)UI.OBJ_TWEEN_ROOT, false);
		LoadingQueue lqstamp = new LoadingQueue(this);
		LoadObject lostamp = lqstamp.LoadChatStamp(stampId, true);
		yield return (object)lqstamp.Wait();
		if (!(lostamp.loadedObject == null))
		{
			SetTexture((Enum)UI.TXT_STAMP, lostamp.loadedObject as Texture2D);
			SetActive((Enum)UI.OBJ_TWEEN_ROOT, true);
			TweenScale tween = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
			tween.SetOnFinished(OnFinish);
			tween.ResetToBeginning();
			tween.PlayForward();
		}
	}

	private void OnFinish()
	{
		SetActive((Enum)UI.OBJ_TWEEN_ROOT, false);
		rootPosition = null;
	}

	private void SetPosition()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = rootPosition.get_position();
		if (isAdjustment)
		{
			val += new Vector3(0f, 2.5f, 0f);
		}
		val = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(val);
		val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
		if (val.z >= 0f)
		{
			val.z = 0f;
			this.get_transform().set_position(val);
			SetActive((Enum)UI.OBJ_TWEEN_ROOT, true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_TWEEN_ROOT, false);
		}
	}
}
