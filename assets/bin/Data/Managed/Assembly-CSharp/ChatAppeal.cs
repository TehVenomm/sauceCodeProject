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
		if ((Object)rootPosition != (Object)null)
		{
			SetPosition();
		}
	}

	public void View(string text, Transform root, bool isAdjustmentPos)
	{
		rootPosition = root;
		isAdjustment = isAdjustmentPos;
		SetActive(UI.OBJ_TWEEN_ROOT, true);
		SetLabelText(UI.LBL_CHAT, text);
		TweenScale component = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
		component.SetOnFinished(OnFinish);
		component.ResetToBeginning();
		component.PlayForward();
	}

	private void OnFinish()
	{
		SetActive(UI.OBJ_TWEEN_ROOT, false);
		rootPosition = null;
	}

	private void SetPosition()
	{
		Vector3 vector = rootPosition.position;
		if (isAdjustment)
		{
			vector += new Vector3(0f, 2.5f, 0f);
		}
		vector = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(vector);
		vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(vector);
		if (vector.z >= 0f)
		{
			vector.z = 0f;
			GetCtrl(UI.OBJ_TWEEN_ROOT).position = vector;
			SetActive(UI.OBJ_TWEEN_ROOT, true);
		}
		else
		{
			SetActive(UI.OBJ_TWEEN_ROOT, false);
		}
	}
}
