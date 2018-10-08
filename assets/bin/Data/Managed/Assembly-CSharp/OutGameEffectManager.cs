using rhyme;
using System;
using System.Collections;
using UnityEngine;

public class OutGameEffectManager : MonoBehaviourSingleton<OutGameEffectManager>
{
	private const string TOUCH_EFFECT_NAME = "ef_ui_tap_01";

	private const int MAX_TOUHC_EFFECT = 5;

	private const string AUTO_MOVE_EFFECT_NAME = "ef_ui_downenergy_01";

	private const string SILHOUETTE_EFFECT_NAME = "ef_ui_questselect_01";

	private LoadObject touchEffectPrefab;

	private int effectCount;

	private LoadObject moveEffectPrefab;

	private Transform autoEventEffect;

	private LoadObject silhouetteEffectPrefab;

	private Transform silhouetteEffect;

	private MAIN_SCENE sceneNow = MAIN_SCENE.MAX;

	private Transform sceneButtonEffect;

	public Vector3 lastTouchEffectPos
	{
		get;
		private set;
	}

	private IEnumerator Start()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		ResourceManager.enableCache = false;
		touchEffectPrefab = load_queue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_tap_01", false);
		moveEffectPrefab = load_queue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_downenergy_01", false);
		silhouetteEffectPrefab = load_queue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_01", false);
		ResourceManager.enableCache = true;
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			InputManager.OnTouchOnAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOnAlways, new InputManager.OnTouchDelegate(OnTouchOn));
		}
		rymFXManager.DestroyFxDelegate = (rymFXManager.DestroyFxFunc)Delegate.Combine(rymFXManager.DestroyFxDelegate, new rymFXManager.DestroyFxFunc(OnDestroyFx));
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			InputManager.OnTouchOnAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOnAlways, new InputManager.OnTouchDelegate(OnTouchOn));
		}
		rymFXManager.DestroyFxDelegate = OnDestroyFx;
	}

	private void OnTouchOn(InputManager.TouchInfo touch_info)
	{
		if (!MonoBehaviourSingleton<PuniConManager>.IsValid() || !touch_info.enable)
		{
			PopTouchEffect(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(touch_info.position.ToVector3XY()));
		}
	}

	private void OnDestroyFx(rymFX fx)
	{
		if (fx.name == "ef_ui_tap_01")
		{
			effectCount--;
		}
	}

	public void PopTouchEffect(Vector3 pos)
	{
		lastTouchEffectPos = pos;
		if (touchEffectPrefab != null && !(touchEffectPrefab.loadedObject == (UnityEngine.Object)null) && effectCount < 5)
		{
			Transform transform = ResourceUtility.Realizes(touchEffectPrefab.loadedObject, MonoBehaviourSingleton<UIManager>.I.uiCamera.transform, 5);
			transform.position = pos;
			effectCount++;
		}
	}

	public void ShowAutoEventEffect()
	{
		if (!((UnityEngine.Object)autoEventEffect != (UnityEngine.Object)null))
		{
			autoEventEffect = ResourceUtility.Realizes(moveEffectPrefab.loadedObject, MonoBehaviourSingleton<UIManager>.I.uiCamera.transform, 5);
			autoEventEffect.GetComponent<rymFX>().ChangeRenderQueue = 3999;
			autoEventEffect.position = lastTouchEffectPos;
			autoEventEffect.gameObject.AddComponent<TransformInterpolator>();
		}
	}

	public void HideAutoEventEffect()
	{
		if (!((UnityEngine.Object)autoEventEffect == (UnityEngine.Object)null))
		{
			EffectManager.ReleaseEffect(autoEventEffect.gameObject, true, false);
			autoEventEffect = null;
		}
	}

	public Coroutine MoveAutoEventEffect(Vector3 to)
	{
		if ((UnityEngine.Object)autoEventEffect == (UnityEngine.Object)null)
		{
			return null;
		}
		TransformInterpolator component = autoEventEffect.GetComponent<TransformInterpolator>();
		to = autoEventEffect.parent.InverseTransformPoint(to);
		Vector3 normalized = (to - autoEventEffect.localPosition).normalized;
		Vector3 add_value = Vector3.Cross(normalized, Vector3.forward) * UnityEngine.Random.Range(-64f, 64f);
		add_value.z = 0f;
		component.Translate(0.25f, to, null, add_value, Curves.arcHalfCurve);
		return component.Wait();
	}

	public void ShowSilhoutteffect(Transform t, int layer)
	{
		if (!((UnityEngine.Object)silhouetteEffect != (UnityEngine.Object)null))
		{
			silhouetteEffect = ResourceUtility.Realizes(silhouetteEffectPrefab.loadedObject, t, layer);
			silhouetteEffect.transform.localPosition = new Vector3(0f, 0f, 500f);
		}
	}

	public void HideSilhoutteEffect()
	{
		if (!((UnityEngine.Object)silhouetteEffect == (UnityEngine.Object)null))
		{
			EffectManager.ReleaseEffect(silhouetteEffect.gameObject, true, false);
			silhouetteEffect = null;
		}
	}

	public void UpdateSceneButtonEffect(MAIN_SCENE scene, Transform button)
	{
		if (sceneNow != scene)
		{
			sceneNow = scene;
		}
	}

	public void ReleaseSceneButtonEffect()
	{
		if (!((UnityEngine.Object)sceneButtonEffect == (UnityEngine.Object)null))
		{
			EffectManager.ReleaseEffect(sceneButtonEffect.gameObject, true, false);
			sceneButtonEffect = null;
			sceneNow = MAIN_SCENE.MAX;
		}
	}
}
