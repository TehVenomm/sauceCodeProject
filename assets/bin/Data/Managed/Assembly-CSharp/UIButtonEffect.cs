using System.Collections.Generic;
using UnityEngine;

public class UIButtonEffect : MonoBehaviour
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

	private Vector3 pivotOffset = Vector3.zero;

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

	private void Start()
	{
		if (!mStarted)
		{
			mStarted = true;
			if ((Object)buttonScale_tweenTarget == (Object)null)
			{
				buttonScale_tweenTarget = base.transform;
			}
			buttonScale_mScale = buttonScale_tweenTarget.localScale;
			buttonScale_Collider = buttonScale_tweenTarget.GetComponent<BoxCollider>();
			if ((Object)buttonScale_Collider != (Object)null)
			{
				buttonScale_ColliderSize = buttonScale_Collider.size;
			}
		}
	}

	private void OnDisable()
	{
		if (mStarted && (Object)buttonScale_tweenTarget != (Object)null)
		{
			TweenScale component = buttonScale_tweenTarget.GetComponent<TweenScale>();
			if ((Object)component != (Object)null)
			{
				if ((Object)buttonScale_Collider != (Object)null)
				{
					buttonScale_Collider.size = buttonScale_ColliderSize;
				}
				component.value = buttonScale_mScale;
				component.enabled = false;
			}
		}
	}

	private void Awake()
	{
		wasSetup = false;
		thisTransform = base.gameObject.transform;
	}

	public void Setup(Transform ef)
	{
		effect = ef;
		effectObj = effect.gameObject;
		float duration = 0.35f;
		float num = 1.4f;
		tweenScale = effectObj.GetComponent<TweenScale>();
		if ((Object)null == (Object)tweenScale)
		{
			tweenScale = effectObj.AddComponent<TweenScale>();
		}
		Vector3 lossyScale = thisTransform.lossyScale;
		lossyScale = lossyScale.Div(MonoBehaviourSingleton<UIManager>.I.uiRootTransform.localScale);
		tweenScale.from = lossyScale;
		tweenScale.to = new Vector3(lossyScale.x * num, lossyScale.y * num, 1f);
		tweenScale.duration = duration;
		tweenAlpha = effectObj.GetComponent<TweenAlpha>();
		if ((Object)null == (Object)tweenAlpha)
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
		if ((Object)null == (Object)effectObj.GetComponent<UIWidget>())
		{
			effectObj.AddComponent<UIWidget>();
		}
		UIWidget component = effectObj.GetComponent<UIWidget>();
		UIWidget component2 = base.gameObject.GetComponent<UIWidget>();
		if ((Object)null != (Object)component && (Object)null != (Object)component2 && component.pivot != UIWidget.Pivot.Center)
		{
			component.pivot = UIWidget.Pivot.Center;
			pivotOffset = CalcPivotOffset(component2, true);
			effect.localPosition -= pivotOffset;
			Vector3 offset = CalcPivotOffset(component2, false);
			int childCount = effect.childCount;
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
				FindAndDelete(destroyObjects[j].name);
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
		if ((Object)null != (Object)toggleObjectSwitch && toggleObjectsActive != null && toggleObjectsInactive != null)
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
		if ((Object)null != (Object)effectObj)
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
		int num = orgArray.Length;
		UISprite[] array = new UISprite[num];
		for (int i = 0; i < num; i++)
		{
			UISprite uISprite = orgArray[i];
			Transform transform = Utility.Find(effect, uISprite.name);
			if ((Object)null != (Object)transform)
			{
				array[i] = transform.gameObject.GetComponent<UISprite>();
			}
			if ((Object)null == (Object)array[i])
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
				if ((Object)null == (Object)cacheParam2.clone)
				{
					cacheObjects.Remove(cacheParam2);
				}
			}
		}
	}

	private void OnDestroy()
	{
		if ((Object)null != (Object)effectObj)
		{
			Object.Destroy(effectObj);
			effectObj = null;
		}
		effect = null;
	}

	private void UpdateTransform()
	{
		if ((Object)null != (Object)effect)
		{
			effect.position = thisTransform.position;
			effect.localPosition -= pivotOffset;
		}
	}

	private void Update()
	{
		if ((Object)null != (Object)effectObj && effectObj.activeSelf)
		{
			UpdateTransform();
		}
	}

	private void SetOffsetHierarchy(Transform trs, Vector3 offset)
	{
		trs.localPosition += offset;
		int childCount = trs.childCount;
		for (int i = 0; i < childCount; i++)
		{
			SetOffsetHierarchy(trs.GetChild(i), offset);
		}
	}

	private void TraceActivate()
	{
		cacheObjects.ForEach(delegate(CacheParam o)
		{
			if ((Object)null != (Object)o.clone && (Object)null != (Object)o.org)
			{
				if (o.clone.activeSelf != o.org.activeSelf)
				{
					o.clone.SetActive(o.org.activeSelf);
				}
				if ((Object)o.clone.GetComponent<UISprite>() != (Object)null && !o.org.GetComponent<UISprite>().enabled && o.clone.activeSelf)
				{
					o.clone.SetActive(false);
				}
			}
		});
	}

	private void OnClick()
	{
		if (TutorialMessage.IsActiveButton(base.gameObject) && wasSetup && base.enabled && (Object)null != (Object)effect)
		{
			if (isToggle)
			{
				ToggleSprite();
			}
			effectObj.SetActive(true);
			TraceActivate();
			if ((Object)null != (Object)tweenScale)
			{
				tweenScale.ResetToBeginning();
				tweenScale.PlayForward();
			}
			if ((Object)null != (Object)tweenAlpha)
			{
				tweenAlpha.ResetToBeginning();
				tweenAlpha.PlayForward();
			}
			UpdateTransform();
		}
	}

	private void OnPress(bool isDown)
	{
		if (TutorialMessage.IsActiveButton(base.gameObject))
		{
			if (existsIcon && isDown && !isSimple)
			{
				base.gameObject.GetComponentsInChildren(true, Temporary.itemIconList);
				for (int i = 0; i < Temporary.itemIconList.Count; i++)
				{
					if (!((Object)Temporary.itemIconList[i].icon.mainTexture == (Object)null) && !cacheIconNames.Contains(Temporary.itemIconList[i].icon.mainTexture.name))
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
				Transform transform = CreateSprites(thisTransform, MonoBehaviourSingleton<UIManager>.I.buttonEffectTop, cacheObjects);
				transform.position = thisTransform.position;
				Setup(transform);
				base.gameObject.GetComponentsInChildren(true, Temporary.itemIconList);
				if (Temporary.itemIconList.Count > 0)
				{
					cacheIconNames = new List<string>();
					for (int j = 0; j < Temporary.itemIconList.Count; j++)
					{
						if (!((Object)Temporary.itemIconList[j].icon.mainTexture == (Object)null))
						{
							cacheIconNames.Add(Temporary.itemIconList[j].icon.mainTexture.name);
						}
					}
					existsIcon = true;
				}
				Temporary.itemIconList.Clear();
			}
			if (base.enabled)
			{
				if (!mStarted)
				{
					Start();
				}
				TweenScale.Begin(buttonScale_tweenTarget.gameObject, buttonScale_duration, (!isDown) ? buttonScale_mScale : Vector3.Scale(buttonScale_mScale, buttonScale_pressed)).method = UITweener.Method.EaseInOut;
				if ((Object)buttonScale_Collider != (Object)null)
				{
					if (isDown)
					{
						buttonScale_Collider.size = buttonScale_ColliderSize.Div(buttonScale_pressed) + new Vector3(0.01f, 0.01f, 0.01f);
					}
					else
					{
						buttonScale_Collider.size = buttonScale_ColliderSize;
					}
				}
			}
		}
	}

	private void CreateGlowAtlas()
	{
		Transform atlasTop = MonoBehaviourSingleton<UIManager>.I.atlasTop;
		effect.GetComponentsInChildren(true, Temporary.uiSpriteList);
		int i = 0;
		for (int num = Temporary.uiSpriteList.Count; i < num; i++)
		{
			UISprite uISprite = Temporary.uiSpriteList[i];
			if ((Object)null == (Object)uISprite.atlas)
			{
				Temporary.uiSpriteList.RemoveAt(i);
				i--;
				num--;
			}
			else
			{
				UISpriteAddShaderReplacer uISpriteAddShaderReplacer = uISprite.gameObject.GetComponent<UISpriteAddShaderReplacer>();
				if ((Object)null == (Object)uISpriteAddShaderReplacer)
				{
					uISpriteAddShaderReplacer = uISprite.gameObject.AddComponent<UISpriteAddShaderReplacer>();
				}
				uISpriteAddShaderReplacer.Replace("mobile/Custom/UI/ui_add_mul_internal");
				Material spriteMaterial = uISprite.atlas.spriteMaterial;
				if ((Object)null != (Object)spriteMaterial)
				{
					spriteMaterial.SetColor(TintColorID, new Color(1f, 1f, 1f, 1f));
					spriteMaterial.SetFloat(MulColorID, 4f);
				}
				uISprite.atlas.gameObject.transform.SetParent(atlasTop);
			}
		}
		cacheSprites = Temporary.uiSpriteList.ToArray();
		Temporary.uiSpriteList.Clear();
	}

	private void ToggleSprite()
	{
		bool enabled = toggleObjectSwitch.enabled;
		int num = toggleTargetsActive.Length;
		for (int i = 0; i < num; i++)
		{
			toggleTargetsActive[i].enabled = !enabled;
		}
		num = toggleTargetsInactive.Length;
		for (int j = 0; j < num; j++)
		{
			toggleTargetsInactive[j].enabled = enabled;
		}
	}

	public void ResetAnim()
	{
		if (base.enabled && (Object)null != (Object)effectObj)
		{
			effectObj.SetActive(false);
			if ((Object)null != (Object)tweenScale)
			{
				tweenScale.ResetToBeginning();
			}
			if ((Object)null != (Object)tweenAlpha)
			{
				tweenAlpha.ResetToBeginning();
			}
		}
	}

	private Vector3 CalcPivotOffset(UIWidget widget, bool isScaling = true)
	{
		if ((Object)null != (Object)widget)
		{
			Vector3 vector = Vector3.one;
			if (isScaling)
			{
				vector = thisTransform.localScale;
			}
			Vector2 vector2 = widget.pivotOffset;
			float x = (vector2.x - 0.5f) * vector.x * (float)widget.width;
			float y = (vector2.y - 0.5f) * vector.y * (float)widget.height;
			return new Vector3(x, y, 0f);
		}
		return Vector3.zero;
	}

	public UISprite GetUISprite(string name)
	{
		if (cacheSprites == null)
		{
			return null;
		}
		for (int i = 0; i < cacheSprites.Length; i++)
		{
			if (name == cacheSprites[i].name)
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
		GameObject gameObject = new GameObject(org.name);
		Transform transform = gameObject.transform;
		UISprite component = org.GetComponent<UISprite>();
		if ((Object)null != (Object)component)
		{
			UISprite uISprite = gameObject.AddComponent<UISprite>();
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
		transform.SetParent(parent);
		transform.localPosition = org.localPosition;
		transform.localScale = org.localScale;
		gameObject.layer = org.gameObject.layer;
		gameObject.SetActive(org.gameObject.activeSelf);
		CacheParam cacheParam = new CacheParam();
		cacheParam.nameHash = org.name.GetHashCode();
		cacheParam.org = org.gameObject;
		cacheParam.clone = gameObject;
		cacheObjects.Add(cacheParam);
		int childCount = org.childCount;
		for (int i = 0; i < childCount; i++)
		{
			CreateSprites(org.GetChild(i), transform, cacheObjects);
		}
		return transform;
	}
}
