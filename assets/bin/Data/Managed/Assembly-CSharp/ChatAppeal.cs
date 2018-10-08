using System;
using UnityEngine;

public class ChatAppeal : UIBehaviour
{
	public enum UI
	{
		OBJ_TWEEN_ROOT,
		SPR_CHAT_BG,
		LBL_CHAT
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

	public void View(string text, Transform root, bool isAdjustmentPos)
	{
		rootPosition = root;
		isAdjustment = isAdjustmentPos;
		SetActive((Enum)UI.OBJ_TWEEN_ROOT, true);
		SetLabelText((Enum)UI.LBL_CHAT, text);
		TweenScale component = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
		component.SetOnFinished(OnFinish);
		component.ResetToBeginning();
		component.PlayForward();
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
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
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
			GetCtrl(UI.OBJ_TWEEN_ROOT).set_position(val);
			SetActive((Enum)UI.OBJ_TWEEN_ROOT, true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_TWEEN_ROOT, false);
		}
	}
}
