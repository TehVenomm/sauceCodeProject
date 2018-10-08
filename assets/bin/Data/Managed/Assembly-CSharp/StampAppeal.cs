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
		if ((Object)rootPosition != (Object)null)
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
		SetActive(UI.OBJ_TWEEN_ROOT, false);
		LoadingQueue lqstamp = new LoadingQueue(this);
		LoadObject lostamp = lqstamp.LoadChatStamp(stampId, true);
		yield return (object)lqstamp.Wait();
		if (!(lostamp.loadedObject == (Object)null))
		{
			SetTexture(UI.TXT_STAMP, lostamp.loadedObject as Texture2D);
			SetActive(UI.OBJ_TWEEN_ROOT, true);
			TweenScale tween = GetCtrl(UI.OBJ_TWEEN_ROOT).GetComponent<TweenScale>();
			tween.SetOnFinished(OnFinish);
			tween.ResetToBeginning();
			tween.PlayForward();
		}
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
			base.transform.position = vector;
			SetActive(UI.OBJ_TWEEN_ROOT, true);
		}
		else
		{
			SetActive(UI.OBJ_TWEEN_ROOT, false);
		}
	}
}
