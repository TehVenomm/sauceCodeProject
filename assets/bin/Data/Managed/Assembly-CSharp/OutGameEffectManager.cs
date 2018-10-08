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

	private unsafe IEnumerator Start()
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
		rymFXManager.DestroyFxDelegate = Delegate.Combine((Delegate)rymFXManager.DestroyFxDelegate, (Delegate)new DestroyFxFunc((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void OnDestroy()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InputManager>.IsValid())
		{
			InputManager.OnTouchOnAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOnAlways, new InputManager.OnTouchDelegate(OnTouchOn));
		}
		rymFXManager.DestroyFxDelegate = new DestroyFxFunc((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
	}

	private void OnTouchOn(InputManager.TouchInfo touch_info)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<PuniConManager>.IsValid() || !touch_info.enable)
		{
			PopTouchEffect(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(touch_info.position.ToVector3XY()));
		}
	}

	private void OnDestroyFx(rymFX fx)
	{
		if (fx.get_name() == "ef_ui_tap_01")
		{
			effectCount--;
		}
	}

	public void PopTouchEffect(Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		lastTouchEffectPos = pos;
		if (touchEffectPrefab != null && !(touchEffectPrefab.loadedObject == null) && effectCount < 5)
		{
			Transform val = ResourceUtility.Realizes(touchEffectPrefab.loadedObject, MonoBehaviourSingleton<UIManager>.I.uiCamera.get_transform(), 5);
			val.set_position(pos);
			effectCount++;
		}
	}

	public void ShowAutoEventEffect()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (!(autoEventEffect != null))
		{
			autoEventEffect = ResourceUtility.Realizes(moveEffectPrefab.loadedObject, MonoBehaviourSingleton<UIManager>.I.uiCamera.get_transform(), 5);
			autoEventEffect.GetComponent<rymFX>().ChangeRenderQueue = 3999;
			autoEventEffect.set_position(lastTouchEffectPos);
			autoEventEffect.get_gameObject().AddComponent<TransformInterpolator>();
		}
	}

	public void HideAutoEventEffect()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (!(autoEventEffect == null))
		{
			EffectManager.ReleaseEffect(autoEventEffect.get_gameObject(), true, false);
			autoEventEffect = null;
		}
	}

	public Coroutine MoveAutoEventEffect(Vector3 to)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (autoEventEffect == null)
		{
			return null;
		}
		TransformInterpolator component = autoEventEffect.GetComponent<TransformInterpolator>();
		to = autoEventEffect.get_parent().InverseTransformPoint(to);
		Vector3 val = to - autoEventEffect.get_localPosition();
		Vector3 normalized = val.get_normalized();
		Vector3 add_value = Vector3.Cross(normalized, Vector3.get_forward()) * Random.Range(-64f, 64f);
		add_value.z = 0f;
		component.Translate(0.25f, to, null, add_value, Curves.arcHalfCurve);
		return component.Wait();
	}

	public void ShowSilhoutteffect(Transform t, int layer)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (!(silhouetteEffect != null))
		{
			silhouetteEffect = ResourceUtility.Realizes(silhouetteEffectPrefab.loadedObject, t, layer);
			silhouetteEffect.get_transform().set_localPosition(new Vector3(0f, 0f, 500f));
		}
	}

	public void HideSilhoutteEffect()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (!(silhouetteEffect == null))
		{
			EffectManager.ReleaseEffect(silhouetteEffect.get_gameObject(), true, false);
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (!(sceneButtonEffect == null))
		{
			EffectManager.ReleaseEffect(sceneButtonEffect.get_gameObject(), true, false);
			sceneButtonEffect = null;
			sceneNow = MAIN_SCENE.MAX;
		}
	}
}
