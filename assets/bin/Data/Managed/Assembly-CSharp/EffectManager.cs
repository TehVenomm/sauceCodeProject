using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviourSingleton<EffectManager>
{
	private class Pool_OneShotInfo
	{
	}

	public class OneShotInfo
	{
		public string name;

		public Vector3 pos;

		public Quaternion rot;

		public Vector3 scale;

		public float time;

		public Action<Transform> onCreateCallBack;
	}

	private List<OneShotInfo> infoList = new List<OneShotInfo>();

	private List<OneShotInfo> infoSecondList = new List<OneShotInfo>();

	public bool enableStock;

	public int maxStockCount = 64;

	private Transform stockParent;

	public static void ClearPoolObjects()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			MonoBehaviourSingleton<EffectManager>.I.ClearStocks();
		}
	}

	public static void Startup()
	{
		EeLSettings.Startup();
	}

	private void Start()
	{
		ClearStocks();
	}

	private void OnEnable()
	{
		Trail.onQueryDestroy = (Func<Trail, bool>)Delegate.Combine(Trail.onQueryDestroy, new Func<Trail, bool>(OnTrailQueryDestroy));
	}

	protected override void OnDisable()
	{
		int i = 0;
		for (int count = infoList.Count; i < count; i++)
		{
			OneShotInfo oneShotInfo = infoList[i];
			rymTPool<OneShotInfo>.Release(ref oneShotInfo);
		}
		infoList.Clear();
		int j = 0;
		for (int count2 = infoSecondList.Count; j < count2; j++)
		{
			OneShotInfo oneShotInfo2 = infoSecondList[j];
			rymTPool<OneShotInfo>.Release(ref oneShotInfo2);
		}
		infoSecondList.Clear();
		base.OnDisable();
		Trail.onQueryDestroy = (Func<Trail, bool>)Delegate.Remove(Trail.onQueryDestroy, new Func<Trail, bool>(OnTrailQueryDestroy));
	}

	private bool OnTrailQueryDestroy(Trail trail)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		if (StockOrDestroy(trail.get_gameObject(), false))
		{
			return false;
		}
		return true;
	}

	private void LateUpdate()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (infoList.Count > 0)
		{
			OneShotInfo oneShotInfo = infoList[0];
			_OneShot(oneShotInfo.name, oneShotInfo.pos, oneShotInfo.rot, oneShotInfo.scale, oneShotInfo.onCreateCallBack);
			rymTPool<OneShotInfo>.Release(ref oneShotInfo);
			infoList.RemoveAt(0);
		}
		else
		{
			int count = infoSecondList.Count;
			if (count > 0)
			{
				float time = Time.get_time();
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					OneShotInfo oneShotInfo2 = infoSecondList[i];
					if (!(time - oneShotInfo2.time > 0.1f))
					{
						_OneShot(oneShotInfo2.name, oneShotInfo2.pos, oneShotInfo2.rot, oneShotInfo2.scale, oneShotInfo2.onCreateCallBack);
						num++;
						break;
					}
					num++;
				}
				for (int j = 0; j < num; j++)
				{
					OneShotInfo oneShotInfo3 = infoSecondList[j];
					rymTPool<OneShotInfo>.Release(ref oneShotInfo3);
				}
				infoSecondList.RemoveRange(0, num);
			}
		}
	}

	public bool StockOrDestroy(GameObject go, bool no_stock_to_destroy)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if (go == null)
		{
			return false;
		}
		if (enableStock)
		{
			EffectStock component = go.GetComponent<EffectStock>();
			if (component != null && !component.IsLoop())
			{
				component.Stock();
				go.get_transform().SetParent(stockParent, false);
				if (stockParent.get_childCount() >= maxStockCount)
				{
					Object.DestroyImmediate(stockParent.GetChild(0).get_gameObject());
				}
				return true;
			}
		}
		if (no_stock_to_destroy)
		{
			Object.Destroy(go);
		}
		return false;
	}

	public void ClearStocks()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (stockParent != null)
		{
			Object.DestroyImmediate(stockParent.get_gameObject());
		}
		stockParent = Utility.CreateGameObject("Stocks", base._transform, -1);
		stockParent.get_gameObject().SetActive(false);
	}

	public static Transform GetEffect(string effect_name, Transform parent = null)
	{
		return GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, parent, -1, false);
	}

	public static Transform GetCameraLinkEffect(string effect_name, bool y0, Transform parent = null)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, parent, -1, false);
		if (effect == null)
		{
			return null;
		}
		effect.get_gameObject().AddComponent<CameraPosLink>().y0 = y0;
		return effect;
	}

	public static Transform GetUIEffect(string effect_name)
	{
		return GetUIEffect(effect_name, null, -0.001f, 0, null);
	}

	public static Transform GetUIEffect(string effect_name, UIWidget widget, float z = -0.001f, int add_render_queue = 0)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		return GetUIEffect(effect_name, widget.get_transform(), z, add_render_queue, null);
	}

	public static Transform GetUIEffect(string effect_name, Transform parent, float z = -0.001f, int add_render_queue = 0, UIWidget ref_render_queue = null)
	{
		if (parent == null)
		{
			parent = MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeCommonDialog()._transform;
		}
		Transform effect = GetEffect(RESOURCE_CATEGORY.EFFECT_UI, effect_name, parent, 5, false);
		if (effect != null && add_render_queue != -1)
		{
			SetUIEffectDepth(effect, parent, z, add_render_queue, ref_render_queue);
		}
		return effect;
	}

	public static void SetUIEffectDepth(Transform effect, Transform parent, float z = -0.001f, int add_render_queue = 0, UIWidget ref_render_queue = null)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		effect.set_localPosition(effect.get_localPosition() + new Vector3(0f, 0f, z));
		if (ref_render_queue == null)
		{
			ref_render_queue = parent.GetComponentInChildren<UIWidget>();
		}
		rymFX fx = effect.GetComponent<rymFX>();
		if (fx != null)
		{
			fx.Cameras = MonoBehaviourSingleton<UIManager>.I.cameras;
			if (ref_render_queue != null)
			{
				UIWidget uIWidget = ref_render_queue;
				uIWidget.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget.onRender, (UIDrawCall.OnRenderCallback)delegate(Material mate)
				{
					if (fx != null)
					{
						fx.SetRenderQueue(mate.get_renderQueue() + add_render_queue);
					}
				});
			}
			else
			{
				fx.ChangeRenderQueue = 3000 + add_render_queue;
			}
		}
		else if (effect.GetComponent<EffectCtrl>() != null)
		{
			Renderer[] renderers = effect.GetComponentsInChildren<Renderer>();
			if (renderers.Length > 0)
			{
				UIWidget uIWidget2 = ref_render_queue;
				uIWidget2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget2.onRender, (UIDrawCall.OnRenderCallback)delegate(Material mate)
				{
					int renderQueue = mate.get_renderQueue() + add_render_queue;
					int i = 0;
					for (int num = renderers.Length; i < num; i++)
					{
						Renderer val = renderers[i];
						if (val != null)
						{
							Material[] materials = val.get_materials();
							int j = 0;
							for (int num2 = materials.Length; j < num2; j++)
							{
								Material val2 = materials[j];
								if (val2 != null)
								{
									val2.set_renderQueue(renderQueue);
								}
							}
						}
					}
				});
			}
		}
	}

	private static Transform GetEffect(RESOURCE_CATEGORY category, string effect_name, Transform parent = null, int layer = -1, bool enable_stock = false)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		if (string.IsNullOrEmpty(effect_name))
		{
			return null;
		}
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			EffectManager i = MonoBehaviourSingleton<EffectManager>.I;
			effect_name = ResourceName.AddAttributID(effect_name);
			if (MonoBehaviourSingleton<ResourceManager>.IsValid())
			{
				if (parent == null)
				{
					parent = i._transform;
				}
				GameObject val = null;
				GameObject inactive_inctance = null;
				Transform val2 = null;
				bool flag = i.enableStock && enable_stock;
				if (flag)
				{
					Transform val3 = i.stockParent.FindChild(effect_name);
					if (val3 != null)
					{
						val3.GetComponent<EffectStock>().Recycle(parent, layer);
						return val3;
					}
				}
				inactive_inctance = InstantiateManager.FindStock(category, effect_name);
				if (inactive_inctance != null)
				{
					val2 = InstantiateManager.Realizes(ref inactive_inctance, parent, layer);
					inactive_inctance = val2.get_gameObject();
				}
				else
				{
					val = ((!ResourceManager.enableLoadDirect) ? MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, effect_name) : MonoBehaviourSingleton<ResourceManager>.I.LoadDirect(category, effect_name));
					if (val != null)
					{
						val2 = ResourceUtility.Realizes(val, parent, layer);
						inactive_inctance = val2.get_gameObject();
					}
				}
				if (inactive_inctance != null)
				{
					if (flag)
					{
						inactive_inctance.AddComponent<EffectStock>();
					}
					return val2;
				}
			}
		}
		return null;
	}

	public void AddOneShotInfo(OneShotInfo info, bool is_priority)
	{
		if (is_priority)
		{
			infoList.Add(info);
		}
		else
		{
			infoSecondList.Add(info);
		}
	}

	public static void OneShot(string effect_name, Vector3 pos, Quaternion rot, bool is_priority = false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		OneShot(effect_name, pos, rot, Vector3.get_one(), is_priority, null);
	}

	public static void OneShot(string effect_name, Vector3 pos, Quaternion rot, Vector3 scale, bool is_priority = false, Action<Transform> callback = null)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 2)
		{
			flag = true;
		}
		if (flag)
		{
			_OneShot(effect_name, pos, rot, scale, callback);
		}
		else
		{
			Vector3 val = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToViewportPoint(pos);
			if (!(val.x < -0.5f) && !(val.x > 1.5f) && !(val.y < -0.5f) && !(val.y > 1.5f) && !(val.z < 0f))
			{
				if (MonoBehaviourSingleton<EffectManager>.IsValid())
				{
					OneShotInfo oneShotInfo = rymTPool<OneShotInfo>.Get();
					oneShotInfo.name = effect_name;
					oneShotInfo.pos = pos;
					oneShotInfo.rot = rot;
					oneShotInfo.scale = scale;
					oneShotInfo.time = Time.get_time();
					oneShotInfo.onCreateCallBack = callback;
					MonoBehaviourSingleton<EffectManager>.I.AddOneShotInfo(oneShotInfo, is_priority);
				}
				else
				{
					_OneShot(effect_name, pos, rot, scale, callback);
				}
			}
		}
	}

	public static void _OneShot(string effect_name, Vector3 pos, Quaternion rot, Vector3 scale, Action<Transform> callback = null)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, null, -1, true);
		if (!(effect == null))
		{
			effect.set_position(pos);
			effect.set_rotation(rot);
			effect.set_localScale(Vector3.Scale(effect.get_localScale(), scale));
			callback?.Invoke(effect);
		}
	}

	public void DeleteManagerChildrenEffects()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		infoList.Clear();
		this.get_gameObject().GetComponentsInChildren<rymFX>(Temporary.fxList);
		int i = 0;
		for (int count = Temporary.fxList.Count; i < count; i++)
		{
			Object.Destroy(Temporary.fxList[i].get_gameObject());
		}
		Temporary.fxList.Clear();
		this.get_gameObject().GetComponentsInChildren<EffectCtrl>(Temporary.effectCtrlList);
		int j = 0;
		for (int count2 = Temporary.effectCtrlList.Count; j < count2; j++)
		{
			Object.Destroy(Temporary.effectCtrlList[j].get_gameObject());
		}
		Temporary.effectCtrlList.Clear();
	}

	public static void ReleaseEffect(GameObject effect_object, bool isPlayEndAnimation = true, bool immediate = false)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (!(effect_object == null))
		{
			if (!MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				Object.Destroy(effect_object);
			}
			else
			{
				EffectManager i = MonoBehaviourSingleton<EffectManager>.I;
				EffectInfoComponent component = effect_object.GetComponent<EffectInfoComponent>();
				if (component != null && component.destroyLoopEnd)
				{
					component.SetLoopAudioObject(null);
					rymFX component2 = effect_object.GetComponent<rymFX>();
					EffectCtrl effectCtrl = null;
					if (component2 == null)
					{
						effectCtrl = effect_object.GetComponent<EffectCtrl>();
					}
					if (effectCtrl == null && effect_object.get_transform().get_childCount() > 0)
					{
						effect_object.GetComponentsInChildren<Renderer>(Temporary.rendererList);
						int j = 0;
						for (int count = Temporary.rendererList.Count; j < count; j++)
						{
							Temporary.rendererList[j].set_enabled(false);
						}
						Temporary.rendererList.Clear();
					}
					effect_object.GetComponents<Trail>(Temporary.trailList);
					bool flag = false;
					if (component2 != null && component2.get_enabled())
					{
						component2.AutoDelete = true;
						component2.LoopEnd = true;
						flag = true;
					}
					else if (effectCtrl != null && effectCtrl.get_enabled())
					{
						effectCtrl.EndLoop(isPlayEndAnimation);
						flag = true;
					}
					if (flag && !immediate)
					{
						int k = 0;
						for (int count2 = Temporary.trailList.Count; k < count2; k++)
						{
							Temporary.trailList[k].StartDeleteFade();
						}
						Temporary.trailList.Clear();
					}
					else
					{
						i.StockOrDestroy(effect_object, true);
						int l = 0;
						for (int count3 = Temporary.trailList.Count; l < count3; l++)
						{
							Temporary.trailList[l].SetAutoDelete();
						}
						Temporary.trailList.Clear();
					}
				}
				else
				{
					i.StockOrDestroy(effect_object, true);
				}
			}
		}
	}

	public static void ReleaseEffect(ref Transform t)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		if (t != null)
		{
			ReleaseEffect(t.get_gameObject(), true, false);
			t = null;
		}
	}
}
