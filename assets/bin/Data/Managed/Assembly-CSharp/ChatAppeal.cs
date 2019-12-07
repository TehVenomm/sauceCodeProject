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
		SetActive(UI.OBJ_TWEEN_ROOT, is_visible: true);
		SetLabelText(UI.LBL_CHAT, text);
		TweenScale component = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
		component.SetOnFinished(OnFinish);
		component.ResetToBeginning();
		component.PlayForward();
	}

	private void OnFinish()
	{
		SetActive(UI.OBJ_TWEEN_ROOT, is_visible: false);
		rootPosition = null;
	}

	private void SetPosition()
	{
		Vector3 position = rootPosition.position;
		if (isAdjustment)
		{
			position += new Vector3(0f, 2.5f, 0f);
		}
		position = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(position);
		position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		if (position.z >= 0f)
		{
			position.z = 0f;
			GetCtrl(UI.OBJ_TWEEN_ROOT).position = position;
			SetActive(UI.OBJ_TWEEN_ROOT, is_visible: true);
		}
		else
		{
			SetActive(UI.OBJ_TWEEN_ROOT, is_visible: false);
		}
	}
}
