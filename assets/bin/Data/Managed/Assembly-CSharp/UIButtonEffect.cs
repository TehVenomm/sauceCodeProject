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
			if (buttonScale_tweenTarget == null)
			{
				buttonScale_tweenTarget = base.transform;
			}
			buttonScale_mScale = buttonScale_tweenTarget.localScale;
			buttonScale_Collider = buttonScale_tweenTarget.GetComponent<BoxCollider>();
			if (buttonScale_Collider != null)
			{
				buttonScale_ColliderSize = buttonScale_Collider.size;
			}
		}
	}

	private void OnDisable()
	{
		if (!mStarted || !(buttonScale_tweenTarget != null))
		{
			return;
		}
		TweenScale component = buttonScale_tweenTarget.GetComponent<TweenScale>();
		if (component != null)
		{
			if (buttonScale_Collider != null)
			{
				buttonScale_Collider.size = buttonScale_ColliderSize;
			}
			component.value = buttonScale_mScale;
			component.enabled = false;
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
		if (null == tweenScale)
		{
			tweenScale = effectObj.AddComponent<TweenScale>();
		}
		Vector3 lossyScale = thisTransform.lossyScale;
		lossyScale = lossyScale.Div(MonoBehaviourSingleton<UIManager>.I.uiRootTransform.localScale);
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
			effectObj.SetActive(value: false);
		});
		if (null == effectObj.GetComponent<UIWidget>())
		{
			effectObj.AddComponent<UIWidget>();
		}
		UIWidget component = effectObj.GetComponent<UIWidget>();
		UIWidget component2 = base.gameObject.GetComponent<UIWidget>();
		if (null != component && null != component2 && UIWidget.Pivot.Center != component.pivot)
		{
			component.pivot = UIWidget.Pivot.Center;
			pivotOffset = CalcPivotOffset(component2);
			effect.localPosition -= pivotOffset;
			Vector3 offset = CalcPivotOffset(component2, isScaling: false);
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
		if (null != toggleObjectSwitch && toggleObjectsActive != null && toggleObjectsInactive != null)
		{
			isToggle = true;
		}
		effectObj.SetActive(value: false);
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
		int num = orgArray.Length;
		UISprite[] array = new UISprite[num];
		for (int i = 0; i < num; i++)
		{
			UISprite uISprite = orgArray[i];
			Transform transform = Utility.Find(effect, uISprite.name);
			if (null != transform)
			{
				array[i] = transform.gameObject.GetComponent<UISprite>();
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
		if (name == null || name.Length == 0)
		{
			return;
		}
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
		if (null != effect)
		{
			effect.position = thisTransform.position;
			effect.localPosition -= pivotOffset;
		}
	}

	private void Update()
	{
		if (null != effectObj && effectObj.activeSelf)
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
			if (null != o.clone && null != o.org)
			{
				if (o.clone.activeSelf != o.org.activeSelf)
				{
					o.clone.SetActive(o.org.activeSelf);
				}
				if (o.clone.GetComponent<UISprite>() != null && !o.org.GetComponent<UISprite>().enabled && o.clone.activeSelf)
				{
					o.clone.SetActive(value: false);
				}
			}
		});
	}

	private void OnClick()
	{
		if (TutorialMessage.IsActiveButton(base.gameObject) && wasSetup && base.enabled && null != effect)
		{
			if (isToggle)
			{
				ToggleSprite();
			}
			effectObj.SetActive(value: true);
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
		if (!TutorialMessage.IsActiveButton(base.gameObject))
		{
			return;
		}
		if (existsIcon && isDown && !isSimple)
		{
			base.gameObject.GetComponentsInChildren(includeInactive: true, Temporary.itemIconList);
			for (int i = 0; i < Temporary.itemIconList.Count; i++)
			{
				if (!(Temporary.itemIconList[i].icon.mainTexture == null) && !cacheIconNames.Contains(Temporary.itemIconList[i].icon.mainTexture.name))
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
			base.gameObject.GetComponentsInChildren(includeInactive: true, Temporary.itemIconList);
			if (Temporary.itemIconList.Count > 0)
			{
				cacheIconNames = new List<string>();
				for (int j = 0; j < Temporary.itemIconList.Count; j++)
				{
					if (!(Temporary.itemIconList[j].icon.mainTexture == null))
					{
						cacheIconNames.Add(Temporary.itemIconList[j].icon.mainTexture.name);
					}
				}
				existsIcon = true;
			}
			Temporary.itemIconList.Clear();
		}
		if (!base.enabled)
		{
			return;
		}
		if (!mStarted)
		{
			Start();
		}
		TweenScale.Begin(buttonScale_tweenTarget.gameObject, buttonScale_duration, isDown ? Vector3.Scale(buttonScale_mScale, buttonScale_pressed) : buttonScale_mScale).method = UITweener.Method.EaseInOut;
		if (buttonScale_Collider != null)
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

	private void CreateGlowAtlas()
	{
		Transform atlasTop = MonoBehaviourSingleton<UIManager>.I.atlasTop;
		effect.GetComponentsInChildren(includeInactive: true, Temporary.uiSpriteList);
		int i = 0;
		for (int num = Temporary.uiSpriteList.Count; i < num; i++)
		{
			UISprite uISprite = Temporary.uiSpriteList[i];
			if (null == uISprite.atlas)
			{
				Temporary.uiSpriteList.RemoveAt(i);
				i--;
				num--;
				continue;
			}
			UISpriteAddShaderReplacer uISpriteAddShaderReplacer = uISprite.gameObject.GetComponent<UISpriteAddShaderReplacer>();
			if (null == uISpriteAddShaderReplacer)
			{
				uISpriteAddShaderReplacer = uISprite.gameObject.AddComponent<UISpriteAddShaderReplacer>();
			}
			uISpriteAddShaderReplacer.Replace("mobile/Custom/UI/ui_add_mul_internal");
			Material spriteMaterial = uISprite.atlas.spriteMaterial;
			if (null != spriteMaterial)
			{
				spriteMaterial.SetColor(TintColorID, new Color(1f, 1f, 1f, 1f));
				spriteMaterial.SetFloat(MulColorID, 4f);
			}
			uISprite.atlas.gameObject.transform.SetParent(atlasTop);
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
		if (base.enabled && null != effectObj)
		{
			effectObj.SetActive(value: false);
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
		if (null != widget)
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
		if (null != component)
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
