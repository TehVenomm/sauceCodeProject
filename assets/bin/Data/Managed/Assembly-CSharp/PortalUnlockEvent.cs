using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalUnlockEvent : MonoBehaviour
{
	private List<PortalObject> unlockedPortalList = new List<PortalObject>(10);

	private List<PortalObject.VIEW_TYPE> viewTypes = new List<PortalObject.VIEW_TYPE>(10);

	private Action onEndAllEventAction;

	private const float MOVE_TIME = 1.2f;

	public void AddPortal(PortalObject obj)
	{
		unlockedPortalList.Add(obj);
	}

	public void SetOnEndAllEvent(Action action)
	{
		onEndAllEventAction = action;
	}

	private IEnumerator Start()
	{
		for (int j = 0; j < unlockedPortalList.Count; j++)
		{
			viewTypes.Add(unlockedPortalList[j].viewType);
			unlockedPortalList[j].SetAndCreateView(PortalObject.VIEW_TYPE.NOT_CLEAR_ORDER);
		}
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_EVENT, disable: true);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, is_disable: true);
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.UNLOCK_EVENT;
		}
		MonoBehaviourSingleton<InGameCameraManager>.I.enabled = false;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedEffect = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_warp_lockbreak_01");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		UnityEngine.Object effectPrefab = loadedEffect.loadedObject;
		Transform mainCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		Vector3 cameraOriginalPostion = mainCameraTransform.position;
		Vector3 cameraOffset = cameraOriginalPostion - MonoBehaviourSingleton<StageObjectManager>.I.self._position;
		int i = 0;
		float timer;
		Vector3 cameraStartPos;
		Vector3 targetCameraPos;
		while (i < unlockedPortalList.Count)
		{
			timer = 0f;
			cameraStartPos = mainCameraTransform.position;
			targetCameraPos = unlockedPortalList[i]._transform.position + cameraOffset;
			while (timer < 1.2f)
			{
				Vector3 vector2 = mainCameraTransform.position = Vector3.Lerp(cameraStartPos, targetCameraPos, timer / 1.2f);
				timer += Time.deltaTime;
				yield return null;
			}
			ResourceUtility.Realizes(effectPrefab, unlockedPortalList[i]._transform);
			SoundManager.PlayOneShotUISE(40000159);
			yield return new WaitForSeconds(1.7f);
			unlockedPortalList[i].SetAndCreateView(viewTypes[i]);
			yield return new WaitForSeconds(0.5f);
			int num = i + 1;
			i = num;
		}
		timer = 0f;
		cameraStartPos = mainCameraTransform.position;
		targetCameraPos = cameraOriginalPostion;
		while (timer < 1.2f)
		{
			Vector3 vector4 = mainCameraTransform.position = Vector3.Lerp(cameraStartPos, targetCameraPos, timer / 1.2f);
			timer += Time.deltaTime;
			yield return null;
		}
		onEndAllEventAction.SafeInvoke();
		UnityEngine.Object.Destroy(this);
	}

	private void OnDestroy()
	{
		for (int i = 0; i < unlockedPortalList.Count; i++)
		{
			if (unlockedPortalList[i].viewType != viewTypes[i])
			{
				unlockedPortalList[i].SetAndCreateView(viewTypes[i]);
			}
		}
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_EVENT, disable: false);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, is_disable: false);
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.UNLOCK_EVENT;
		}
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.enabled = true;
			MonoBehaviourSingleton<InGameCameraManager>.I.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
	}
}
