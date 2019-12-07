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
		rootPosition = root;
		isAdjustment = isAdjustmentPos;
		StartCoroutine(DoDisplayChatStamp(stampId));
	}

	private IEnumerator DoDisplayChatStamp(int stampId)
	{
		SetActive(UI.OBJ_TWEEN_ROOT, is_visible: false);
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lostamp = loadingQueue.LoadChatStamp(stampId, cache_check: true);
		yield return loadingQueue.Wait();
		if (!(lostamp.loadedObject == null))
		{
			SetTexture(UI.TXT_STAMP, lostamp.loadedObject as Texture2D);
			SetActive(UI.OBJ_TWEEN_ROOT, is_visible: true);
			TweenScale component = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
			component.SetOnFinished(OnFinish);
			component.ResetToBeginning();
			component.PlayForward();
		}
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
			base.transform.position = position;
			SetActive(UI.OBJ_TWEEN_ROOT, is_visible: true);
		}
		else
		{
			SetActive(UI.OBJ_TWEEN_ROOT, is_visible: false);
		}
	}
}
