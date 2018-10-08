using System.Collections.Generic;
using UnityEngine;

public class UIButtonEffect
{
	private class CacheParam
	{
		public int nameHash;

		public GameObject org;

		public GameObject clone;
	}

	private Transform effect;

	private GameObject effectObj;

	private TweenAlpha tweenAlpha;

	private TweenScale tweenScale;

	private Vector3 pivotOffset = Vector3.get_zero();

	private Transform thisTransform;

	public GameObject[] destroyObjects;

	public UISprite toggleObjectSwitch;

	public UISprite[] toggleObjectsActive;

	public UISprite[] toggleObjectsInactive;

	private bool isToggle;

	private UISprite[] toggleTargetsActive;

	private UISprite[] toggleTargetsInactive;

	private UISprite[] cacheSprites;

	private List<string> cacheIconNames;

	private bool existsIcon;

	private Transform buttonScale_tweenTarget;

	public static readonly Vector3 buttonScale_pressed = new Vector3(0.9f, 0.9f, 0.9f);

	public static readonly float buttonScale_duration = 0.03f;

	private Vector3 buttonScale_mScale;

	private BoxCollider buttonScale_Collider;

	private Vector3 buttonScale_ColliderSize;

	private bool mStarted;

	public bool isSimple;

	private List<CacheParam> cacheObjects;

	private static int TintColorID = -1;

	private static int MulColorID = -1;

	public bool wasSetup
	{
		get;
		private set;
	}

	public UIButtonEffect()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (!mStarted)
		{
			mStarted = true;
			if (buttonScale_tweenTarget == null)
			{
				buttonScale_tweenTarget = this.get_transform();
			}
			buttonScale_mScale = buttonScale_tweenTarget.get_localScale();
			buttonScale_Collider = buttonScale_tweenTarget.GetComponent<BoxCollider>();
			if (buttonScale_Collider != null)
			{
				buttonScale_ColliderSize = buttonScale_Collider.get_size();
			}
		}
	}

	private void OnDisable()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (mStarted && buttonScale_tweenTarget != null)
		{
			TweenScale component = buttonScale_tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				if (buttonScale_Collider != null)
				{
					buttonScale_Collider.set_size(buttonScale_ColliderSize);
				}
				component.value = buttonScale_mScale;
				component.set_enabled(false);
			}
		}
	}

	private void Awake()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		wasSetup = false;
		thisTransform = this.get_gameObject().get_transform();
	}

	public void Setup(Transform ef)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		effect = ef;
		effectObj = effect.get_gameObject();
		float duration = 0.35f;
		float num = 1.4f;
		tweenScale = effectObj.GetComponent<TweenScale>();
		if (null == tweenScale)
		{
			tweenScale = effectObj.AddComponent<TweenScale>();
		}
		Vector3 lossyScale = thisTransform.get_lossyScale();
		lossyScale = lossyScale.Div(MonoBehaviourSingleton<UIManager>.I.uiRootTransform.get_localScale());
		tweenScale.from = lossyScale;
		tweenScale.to = new Vector3(lossyScale.x * num, lossyScale.y * num, 1f);
		tweenScale.duration = duration;
		tweenAlpha = effectObj.GetComponent<TweenAlpha>();
		if (null == tweenAlpha)
		{
			tweenAlpha = effectObj.AddComponent<TweenAlpha>();
		}
		tweenAlpha.from = 1f;
		tweenAlpha.to = 0f;
		tweenAlpha.duration = duration;
		tweenAlpha.SetOnFinished(delegate
		{
			effectObj.SetActive(false);
		});
		if (null == effectObj.GetComponent<UIWidget>())
		{
			effectObj.AddComponent<UIWidget>();
		}
		UIWidget component = effectObj.GetComponent<UIWidget>();
		UIWidget component2 = this.get_gameObject().GetComponent<UIWidget>();
		if (null != component && null != component2 && component.pivot != UIWidget.Pivot.Center)
		{
			component.pivot = UIWidget.Pivot.Center;
			pivotOffset = CalcPivotOffset(component2, true);
			Transform obj = effect;
			obj.set_localPosition(obj.get_localPosition() - pivotOffset);
			Vector3 offset = CalcPivotOffset(component2, false);
			int childCount = effect.get_childCount();
			for (int i = 0; i < childCount; i++)
			{
				SetOffsetHierarchy(effect.GetChild(i), offset);
			}
		}
		CreateGlowAtlas();
		if (destroyObjects != null)
		{
			int num2 = destroyObjects.Length;
			for (int j = 0; j < num2; j++)
			{
				FindAndDelete(destroyObjects[j].get_name());
			}
		}
		FindAndDelete("SPR_BADGE");
		if (toggleObjectsActive != null)
		{
			toggleTargetsActive = CollectToggleSprites(toggleObjectsActive);
		}
		if (toggleObjectsInactive != null)
		{
			toggleTargetsInactive = CollectToggleSprites(toggleObjectsInactive);
		}
		if (null != toggleObjectSwitch && toggleObjectsActive != null && toggleObjectsInactive != null)
		{
			isToggle = true;
		}
		effectObj.SetActive(false);
		wasSetup = true;
	}

	public void Reset()
	{
		mStarted = false;
		wasSetup = false;
		if (null != effectObj)
		{
			Object.Destroy(effectObj);
			effectObj = null;
		}
		effect = null;
		cacheObjects = null;
		cacheIconNames = null;
	}

	private UISprite[] CollectToggleSprites(UISprite[] orgArray)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		int num = orgArray.Length;
		UISprite[] array = new UISprite[num];
		for (int i = 0; i < num; i++)
		{
			UISprite uISprite = orgArray[i];
			Transform val = Utility.Find(effect, uISprite.get_name());
			if (null != val)
			{
				array[i] = val.get_gameObject().GetComponent<UISprite>();
			}
			if (null == array[i])
			{
				array = null;
				break;
			}
		}
		return array;
	}

	private void FindAndDelete(string name)
	{
		if (name != null && name.Length != 0)
		{
			int hashCode = name.GetHashCode();
			int count = cacheObjects.Count;
			for (int i = 0; i < count; i++)
			{
				CacheParam cacheParam = cacheObjects[i];
				if (hashCode == cacheParam.nameHash)
				{
					Object.DestroyImmediate(cacheParam.clone);
					cacheObjects.Remove(cacheParam);
					break;
				}
			}
			CacheParam[] array = cacheObjects.ToArray();
			count = array.Length;
			for (int j = 0; j < count; j++)
			{
				CacheParam cacheParam2 = array[j];
				if (null == cacheParam2.clone)
				{
					cacheObjects.Remove(cacheParam2);
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (null != effectObj)
		{
			Object.Destroy(effectObj);
			effectObj = null;
		}
		effect = null;
	}

	private void UpdateTransform()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (null != effect)
		{
			effect.set_position(thisTransform.get_position());
			Transform obj = effect;
			obj.set_localPosition(obj.get_localPosition() - pivotOffset);
		}
	}

	private void Update()
	{
		if (null != effectObj && effectObj.get_activeSelf())
		{
			UpdateTransform();
		}
	}

	private void SetOffsetHierarchy(Transform trs, Vector3 offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		trs.set_localPosition(trs.get_localPosition() + offset);
		int childCount = trs.get_childCount();
		for (int i = 0; i < childCount; i++)
		{
			SetOffsetHierarchy(trs.GetChild(i), offset);
		}
	}

	private void TraceActivate()
	{
		cacheObjects.ForEach(delegate(CacheParam o)
		{
			if (null != o.clone && null != o.org)
			{
				if (o.clone.get_activeSelf() != o.org.get_activeSelf())
				{
					o.clone.SetActive(o.org.get_activeSelf());
				}
				if (o.clone.GetComponent<UISprite>() != null && !o.org.GetComponent<UISprite>().get_enabled() && o.clone.get_activeSelf())
				{
					o.clone.SetActive(false);
				}
			}
		});
	}

	private void OnClick()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		if (TutorialMessage.IsActiveButton(this.get_gameObject()) && wasSetup && this.get_enabled() && null != effect)
		{
			if (isToggle)
			{
				ToggleSprite();
			}
			effectObj.SetActive(true);
			TraceActivate();
			if (null != tweenScale)
			{
				tweenScale.ResetToBeginning();
				tweenScale.PlayForward();
			}
			if (null != tweenAlpha)
			{
				tweenAlpha.ResetToBeginning();
				tweenAlpha.PlayForward();
			}
			UpdateTransform();
		}
	}

	private void OnPress(bool isDown)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected O, but got Unknown
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		if (TutorialMessage.IsActiveButton(this.get_gameObject()))
		{
			if (existsIcon && isDown && !isSimple)
			{
				this.get_gameObject().GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
				for (int i = 0; i < Temporary.itemIconList.Count; i++)
				{
					if (!(Temporary.itemIconList[i].icon.mainTexture == null) && !cacheIconNames.Contains(Temporary.itemIconList[i].icon.mainTexture.get_name()))
					{
						Reset();
						break;
					}
				}
				Temporary.itemIconList.Clear();
			}
			if (isDown && !wasSetup && !isSimple)
			{
				cacheObjects = new List<CacheParam>();
				Transform val = CreateSprites(thisTransform, MonoBehaviourSingleton<UIManager>.I.buttonEffectTop, cacheObjects);
				val.set_position(thisTransform.get_position());
				Setup(val);
				this.get_gameObject().GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
				if (Temporary.itemIconList.Count > 0)
				{
					cacheIconNames = new List<string>();
					for (int j = 0; j < Temporary.itemIconList.Count; j++)
					{
						if (!(Temporary.itemIconList[j].icon.mainTexture == null))
						{
							cacheIconNames.Add(Temporary.itemIconList[j].icon.mainTexture.get_name());
						}
					}
					existsIcon = true;
				}
				Temporary.itemIconList.Clear();
			}
			if (this.get_enabled())
			{
				if (!mStarted)
				{
					Start();
				}
				TweenScale.Begin(buttonScale_tweenTarget.get_gameObject(), buttonScale_duration, (!isDown) ? buttonScale_mScale : Vector3.Scale(buttonScale_mScale, buttonScale_pressed)).method = UITweener.Method.EaseInOut;
				if (buttonScale_Collider != null)
				{
					if (isDown)
					{
						buttonScale_Collider.set_size(buttonScale_ColliderSize.Div(buttonScale_pressed) + new Vector3(0.01f, 0.01f, 0.01f));
					}
					else
					{
						buttonScale_Collider.set_size(buttonScale_ColliderSize);
					}
				}
			}
		}
	}

	private void CreateGlowAtlas()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		Transform atlasTop = MonoBehaviourSingleton<UIManager>.I.atlasTop;
		effect.GetComponentsInChildren<UISprite>(true, Temporary.uiSpriteList);
		int i = 0;
		for (int num = Temporary.uiSpriteList.Count; i < num; i++)
		{
			UISprite uISprite = Temporary.uiSpriteList[i];
			if (null == uISprite.atlas)
			{
				Temporary.uiSpriteList.RemoveAt(i);
				i--;
				num--;
			}
			else
			{
				UISpriteAddShaderReplacer uISpriteAddShaderReplacer = uISprite.get_gameObject().GetComponent<UISpriteAddShaderReplacer>();
				if (null == uISpriteAddShaderReplacer)
				{
					uISpriteAddShaderReplacer = uISprite.get_gameObject().AddComponent<UISpriteAddShaderReplacer>();
				}
				uISpriteAddShaderReplacer.Replace("mobile/Custom/UI/ui_add_mul_internal");
				Material spriteMaterial = uISprite.atlas.spriteMaterial;
				if (null != spriteMaterial)
				{
					spriteMaterial.SetColor(TintColorID, new Color(1f, 1f, 1f, 1f));
					spriteMaterial.SetFloat(MulColorID, 4f);
				}
				uISprite.atlas.get_gameObject().get_transform().SetParent(atlasTop);
			}
		}
		cacheSprites = Temporary.uiSpriteList.ToArray();
		Temporary.uiSpriteList.Clear();
	}

	private void ToggleSprite()
	{
		bool enabled = toggleObjectSwitch.get_enabled();
		int num = toggleTargetsActive.Length;
		for (int i = 0; i < num; i++)
		{
			toggleTargetsActive[i].set_enabled(!enabled);
		}
		num = toggleTargetsInactive.Length;
		for (int j = 0; j < num; j++)
		{
			toggleTargetsInactive[j].set_enabled(enabled);
		}
	}

	public void ResetAnim()
	{
		if (this.get_enabled() && null != effectObj)
		{
			effectObj.SetActive(false);
			if (null != tweenScale)
			{
				tweenScale.ResetToBeginning();
			}
			if (null != tweenAlpha)
			{
				tweenAlpha.ResetToBeginning();
			}
		}
	}

	private Vector3 CalcPivotOffset(UIWidget widget, bool isScaling = true)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (null != widget)
		{
			Vector3 val = Vector3.get_one();
			if (isScaling)
			{
				val = thisTransform.get_localScale();
			}
			Vector2 val2 = widget.pivotOffset;
			float num = (val2.x - 0.5f) * val.x * (float)widget.width;
			float num2 = (val2.y - 0.5f) * val.y * (float)widget.height;
			return new Vector3(num, num2, 0f);
		}
		return Vector3.get_zero();
	}

	public UISprite GetUISprite(string name)
	{
		if (cacheSprites == null)
		{
			return null;
		}
		for (int i = 0; i < cacheSprites.Length; i++)
		{
			if (name == cacheSprites[i].get_name())
			{
				return cacheSprites[i];
			}
		}
		return null;
	}

	public UISprite[] GetUISprites()
	{
		return cacheSprites;
	}

	public static void CacheShaderPropertyId()
	{
		TintColorID = Shader.PropertyToID("_TintColor");
		MulColorID = Shader.PropertyToID("_MulColor");
	}

	private static Transform CreateSprites(Transform org, Transform parent, List<CacheParam> cacheObjects)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		GameObject val = new GameObject(org.get_name());
		Transform val2 = val.get_transform();
		UISprite component = org.GetComponent<UISprite>();
		if (null != component)
		{
			UISprite uISprite = val.AddComponent<UISprite>();
			uISprite.atlas = component.atlas;
			uISprite.spriteName = component.spriteName;
			uISprite.width = component.width;
			uISprite.height = component.height;
			uISprite.type = component.type;
			uISprite.depth = component.depth + 10000;
			uISprite.color = component.color;
			uISprite.centerType = component.centerType;
			uISprite.flip = component.flip;
			uISprite.pivot = component.pivot;
		}
		val2.SetParent(parent);
		val2.set_localPosition(org.get_localPosition());
		val2.set_localScale(org.get_localScale());
		val.set_layer(org.get_gameObject().get_layer());
		val.SetActive(org.get_gameObject().get_activeSelf());
		CacheParam cacheParam = new CacheParam();
		cacheParam.nameHash = org.get_name().GetHashCode();
		cacheParam.org = org.get_gameObject();
		cacheParam.clone = val;
		cacheObjects.Add(cacheParam);
		int childCount = org.get_childCount();
		for (int i = 0; i < childCount; i++)
		{
			CreateSprites(org.GetChild(i), val2, cacheObjects);
		}
		return val2;
	}
}
