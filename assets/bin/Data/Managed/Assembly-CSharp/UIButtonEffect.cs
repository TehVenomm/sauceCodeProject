// Decompiled with JetBrains decompiler
// Type: UIButtonEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIButtonEffect : MonoBehaviour
{
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
  private List<UIButtonEffect.CacheParam> cacheObjects;
  private static int TintColorID = -1;
  private static int MulColorID = -1;

  public bool wasSetup { get; private set; }

  private void Start()
  {
    if (this.mStarted)
      return;
    this.mStarted = true;
    if (Object.op_Equality((Object) this.buttonScale_tweenTarget, (Object) null))
      this.buttonScale_tweenTarget = ((Component) this).transform;
    this.buttonScale_mScale = this.buttonScale_tweenTarget.localScale;
    this.buttonScale_Collider = ((Component) this.buttonScale_tweenTarget).GetComponent<BoxCollider>();
    if (!Object.op_Inequality((Object) this.buttonScale_Collider, (Object) null))
      return;
    this.buttonScale_ColliderSize = this.buttonScale_Collider.size;
  }

  private void OnDisable()
  {
    if (!this.mStarted || !Object.op_Inequality((Object) this.buttonScale_tweenTarget, (Object) null))
      return;
    TweenScale component = ((Component) this.buttonScale_tweenTarget).GetComponent<TweenScale>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (Object.op_Inequality((Object) this.buttonScale_Collider, (Object) null))
      this.buttonScale_Collider.size = this.buttonScale_ColliderSize;
    component.value = this.buttonScale_mScale;
    ((Behaviour) component).enabled = false;
  }

  private void Awake()
  {
    this.wasSetup = false;
    this.thisTransform = ((Component) this).gameObject.transform;
  }

  public void Setup(Transform ef)
  {
    this.effect = ef;
    this.effectObj = ((Component) this.effect).gameObject;
    float num1 = 0.35f;
    float num2 = 1.4f;
    this.tweenScale = this.effectObj.GetComponent<TweenScale>();
    if (Object.op_Equality((Object) null, (Object) this.tweenScale))
      this.tweenScale = this.effectObj.AddComponent<TweenScale>();
    Vector3 vector3 = this.thisTransform.lossyScale.Div(MonoBehaviourSingleton<UIManager>.I.uiRootTransform.localScale);
    this.tweenScale.from = vector3;
    this.tweenScale.to = new Vector3(vector3.x * num2, vector3.y * num2, 1f);
    this.tweenScale.duration = num1;
    this.tweenAlpha = this.effectObj.GetComponent<TweenAlpha>();
    if (Object.op_Equality((Object) null, (Object) this.tweenAlpha))
      this.tweenAlpha = this.effectObj.AddComponent<TweenAlpha>();
    this.tweenAlpha.from = 1f;
    this.tweenAlpha.to = 0.0f;
    this.tweenAlpha.duration = num1;
    this.tweenAlpha.SetOnFinished((EventDelegate.Callback) (() => this.effectObj.SetActive(false)));
    if (Object.op_Equality((Object) null, (Object) this.effectObj.GetComponent<UIWidget>()))
      this.effectObj.AddComponent<UIWidget>();
    UIWidget component1 = this.effectObj.GetComponent<UIWidget>();
    UIWidget component2 = ((Component) this).gameObject.GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) null, (Object) component1) && Object.op_Inequality((Object) null, (Object) component2) && UIWidget.Pivot.Center != component1.pivot)
    {
      component1.pivot = UIWidget.Pivot.Center;
      this.pivotOffset = this.CalcPivotOffset(component2);
      Transform effect = this.effect;
      effect.localPosition = Vector3.op_Subtraction(effect.localPosition, this.pivotOffset);
      Vector3 offset = this.CalcPivotOffset(component2, false);
      int childCount = this.effect.childCount;
      for (int index = 0; index < childCount; ++index)
        this.SetOffsetHierarchy(this.effect.GetChild(index), offset);
    }
    this.CreateGlowAtlas();
    if (this.destroyObjects != null)
    {
      int length = this.destroyObjects.Length;
      for (int index = 0; index < length; ++index)
        this.FindAndDelete(((Object) this.destroyObjects[index]).name);
    }
    this.FindAndDelete("SPR_BADGE");
    if (this.toggleObjectsActive != null)
      this.toggleTargetsActive = this.CollectToggleSprites(this.toggleObjectsActive);
    if (this.toggleObjectsInactive != null)
      this.toggleTargetsInactive = this.CollectToggleSprites(this.toggleObjectsInactive);
    if (Object.op_Inequality((Object) null, (Object) this.toggleObjectSwitch) && this.toggleObjectsActive != null && this.toggleObjectsInactive != null)
      this.isToggle = true;
    this.effectObj.SetActive(false);
    this.wasSetup = true;
  }

  public void Reset()
  {
    this.mStarted = false;
    this.wasSetup = false;
    if (Object.op_Inequality((Object) null, (Object) this.effectObj))
    {
      Object.Destroy((Object) this.effectObj);
      this.effectObj = (GameObject) null;
    }
    this.effect = (Transform) null;
    this.cacheObjects = (List<UIButtonEffect.CacheParam>) null;
    this.cacheIconNames = (List<string>) null;
  }

  private UISprite[] CollectToggleSprites(UISprite[] orgArray)
  {
    int length = orgArray.Length;
    UISprite[] uiSpriteArray = new UISprite[length];
    for (int index = 0; index < length; ++index)
    {
      Transform transform = Utility.Find(this.effect, ((Object) orgArray[index]).name);
      if (Object.op_Inequality((Object) null, (Object) transform))
        uiSpriteArray[index] = ((Component) transform).gameObject.GetComponent<UISprite>();
      if (Object.op_Equality((Object) null, (Object) uiSpriteArray[index]))
      {
        uiSpriteArray = (UISprite[]) null;
        break;
      }
    }
    return uiSpriteArray;
  }

  private void FindAndDelete(string name)
  {
    switch (name)
    {
      case null:
        break;
      case "":
        break;
      default:
        int hashCode = name.GetHashCode();
        int count = this.cacheObjects.Count;
        for (int index = 0; index < count; ++index)
        {
          UIButtonEffect.CacheParam cacheObject = this.cacheObjects[index];
          if (hashCode == cacheObject.nameHash)
          {
            Object.DestroyImmediate((Object) cacheObject.clone);
            this.cacheObjects.Remove(cacheObject);
            break;
          }
        }
        UIButtonEffect.CacheParam[] array = this.cacheObjects.ToArray();
        int length = array.Length;
        for (int index = 0; index < length; ++index)
        {
          UIButtonEffect.CacheParam cacheParam = array[index];
          if (Object.op_Equality((Object) null, (Object) cacheParam.clone))
            this.cacheObjects.Remove(cacheParam);
        }
        break;
    }
  }

  private void OnDestroy()
  {
    if (Object.op_Inequality((Object) null, (Object) this.effectObj))
    {
      Object.Destroy((Object) this.effectObj);
      this.effectObj = (GameObject) null;
    }
    this.effect = (Transform) null;
  }

  private void UpdateTransform()
  {
    if (!Object.op_Inequality((Object) null, (Object) this.effect))
      return;
    this.effect.position = this.thisTransform.position;
    Transform effect = this.effect;
    effect.localPosition = Vector3.op_Subtraction(effect.localPosition, this.pivotOffset);
  }

  private void Update()
  {
    if (!Object.op_Inequality((Object) null, (Object) this.effectObj) || !this.effectObj.activeSelf)
      return;
    this.UpdateTransform();
  }

  private void SetOffsetHierarchy(Transform trs, Vector3 offset)
  {
    Transform transform = trs;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, offset);
    int childCount = trs.childCount;
    for (int index = 0; index < childCount; ++index)
      this.SetOffsetHierarchy(trs.GetChild(index), offset);
  }

  private void TraceActivate()
  {
    this.cacheObjects.ForEach((Action<UIButtonEffect.CacheParam>) (o =>
    {
      if (!Object.op_Inequality((Object) null, (Object) o.clone) || !Object.op_Inequality((Object) null, (Object) o.org))
        return;
      if (o.clone.activeSelf != o.org.activeSelf)
        o.clone.SetActive(o.org.activeSelf);
      if (!Object.op_Inequality((Object) o.clone.GetComponent<UISprite>(), (Object) null) || ((Behaviour) o.org.GetComponent<UISprite>()).enabled || !o.clone.activeSelf)
        return;
      o.clone.SetActive(false);
    }));
  }

  private void OnClick()
  {
    if (!TutorialMessage.IsActiveButton(((Component) this).gameObject) || !this.wasSetup || !((Behaviour) this).enabled || !Object.op_Inequality((Object) null, (Object) this.effect))
      return;
    if (this.isToggle)
      this.ToggleSprite();
    this.effectObj.SetActive(true);
    this.TraceActivate();
    if (Object.op_Inequality((Object) null, (Object) this.tweenScale))
    {
      this.tweenScale.ResetToBeginning();
      this.tweenScale.PlayForward();
    }
    if (Object.op_Inequality((Object) null, (Object) this.tweenAlpha))
    {
      this.tweenAlpha.ResetToBeginning();
      this.tweenAlpha.PlayForward();
    }
    this.UpdateTransform();
  }

  private void OnPress(bool isDown)
  {
    if (!TutorialMessage.IsActiveButton(((Component) this).gameObject))
      return;
    if (this.existsIcon & isDown && !this.isSimple)
    {
      ((Component) this).gameObject.GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
      for (int index = 0; index < Temporary.itemIconList.Count; ++index)
      {
        if (!Object.op_Equality((Object) Temporary.itemIconList[index].icon.mainTexture, (Object) null) && !this.cacheIconNames.Contains(((Object) Temporary.itemIconList[index].icon.mainTexture).name))
        {
          this.Reset();
          break;
        }
      }
      Temporary.itemIconList.Clear();
    }
    if (isDown && !this.wasSetup && !this.isSimple)
    {
      this.cacheObjects = new List<UIButtonEffect.CacheParam>();
      Transform sprites = UIButtonEffect.CreateSprites(this.thisTransform, MonoBehaviourSingleton<UIManager>.I.buttonEffectTop, this.cacheObjects);
      sprites.position = this.thisTransform.position;
      this.Setup(sprites);
      ((Component) this).gameObject.GetComponentsInChildren<ItemIcon>(true, Temporary.itemIconList);
      if (Temporary.itemIconList.Count > 0)
      {
        this.cacheIconNames = new List<string>();
        for (int index = 0; index < Temporary.itemIconList.Count; ++index)
        {
          if (!Object.op_Equality((Object) Temporary.itemIconList[index].icon.mainTexture, (Object) null))
            this.cacheIconNames.Add(((Object) Temporary.itemIconList[index].icon.mainTexture).name);
        }
        this.existsIcon = true;
      }
      Temporary.itemIconList.Clear();
    }
    if (!((Behaviour) this).enabled)
      return;
    if (!this.mStarted)
      this.Start();
    TweenScale.Begin(((Component) this.buttonScale_tweenTarget).gameObject, UIButtonEffect.buttonScale_duration, isDown ? Vector3.Scale(this.buttonScale_mScale, UIButtonEffect.buttonScale_pressed) : this.buttonScale_mScale).method = UITweener.Method.EaseInOut;
    if (!Object.op_Inequality((Object) this.buttonScale_Collider, (Object) null))
      return;
    if (isDown)
      this.buttonScale_Collider.size = Vector3.op_Addition(this.buttonScale_ColliderSize.Div(UIButtonEffect.buttonScale_pressed), new Vector3(0.01f, 0.01f, 0.01f));
    else
      this.buttonScale_Collider.size = this.buttonScale_ColliderSize;
  }

  private void CreateGlowAtlas()
  {
    Transform atlasTop = MonoBehaviourSingleton<UIManager>.I.atlasTop;
    ((Component) this.effect).GetComponentsInChildren<UISprite>(true, Temporary.uiSpriteList);
    int index = 0;
    for (int count = Temporary.uiSpriteList.Count; index < count; ++index)
    {
      UISprite uiSprite = Temporary.uiSpriteList[index];
      if (Object.op_Equality((Object) null, (Object) uiSprite.atlas))
      {
        Temporary.uiSpriteList.RemoveAt(index);
        --index;
        --count;
      }
      else
      {
        UISpriteAddShaderReplacer addShaderReplacer = ((Component) uiSprite).gameObject.GetComponent<UISpriteAddShaderReplacer>();
        if (Object.op_Equality((Object) null, (Object) addShaderReplacer))
          addShaderReplacer = ((Component) uiSprite).gameObject.AddComponent<UISpriteAddShaderReplacer>();
        addShaderReplacer.Replace("mobile/Custom/UI/ui_add_mul_internal");
        Material spriteMaterial = uiSprite.atlas.spriteMaterial;
        if (Object.op_Inequality((Object) null, (Object) spriteMaterial))
        {
          spriteMaterial.SetColor(UIButtonEffect.TintColorID, new Color(1f, 1f, 1f, 1f));
          spriteMaterial.SetFloat(UIButtonEffect.MulColorID, 4f);
        }
        ((Component) uiSprite.atlas).gameObject.transform.SetParent(atlasTop);
      }
    }
    this.cacheSprites = Temporary.uiSpriteList.ToArray();
    Temporary.uiSpriteList.Clear();
  }

  private void ToggleSprite()
  {
    bool enabled = ((Behaviour) this.toggleObjectSwitch).enabled;
    int length1 = this.toggleTargetsActive.Length;
    for (int index = 0; index < length1; ++index)
      ((Behaviour) this.toggleTargetsActive[index]).enabled = !enabled;
    int length2 = this.toggleTargetsInactive.Length;
    for (int index = 0; index < length2; ++index)
      ((Behaviour) this.toggleTargetsInactive[index]).enabled = enabled;
  }

  public void ResetAnim()
  {
    if (!((Behaviour) this).enabled || !Object.op_Inequality((Object) null, (Object) this.effectObj))
      return;
    this.effectObj.SetActive(false);
    if (Object.op_Inequality((Object) null, (Object) this.tweenScale))
      this.tweenScale.ResetToBeginning();
    if (!Object.op_Inequality((Object) null, (Object) this.tweenAlpha))
      return;
    this.tweenAlpha.ResetToBeginning();
  }

  private Vector3 CalcPivotOffset(UIWidget widget, bool isScaling = true)
  {
    if (!Object.op_Inequality((Object) null, (Object) widget))
      return Vector3.zero;
    Vector3 vector3 = Vector3.one;
    if (isScaling)
      vector3 = this.thisTransform.localScale;
    Vector2 pivotOffset = widget.pivotOffset;
    return new Vector3((pivotOffset.x - 0.5f) * vector3.x * (float) widget.width, (pivotOffset.y - 0.5f) * vector3.y * (float) widget.height, 0.0f);
  }

  public UISprite GetUISprite(string name)
  {
    if (this.cacheSprites == null)
      return (UISprite) null;
    for (int index = 0; index < this.cacheSprites.Length; ++index)
    {
      if (name == ((Object) this.cacheSprites[index]).name)
        return this.cacheSprites[index];
    }
    return (UISprite) null;
  }

  public UISprite[] GetUISprites() => this.cacheSprites;

  public static void CacheShaderPropertyId()
  {
    UIButtonEffect.TintColorID = Shader.PropertyToID("_TintColor");
    UIButtonEffect.MulColorID = Shader.PropertyToID("_MulColor");
  }

  private static Transform CreateSprites(
    Transform org,
    Transform parent,
    List<UIButtonEffect.CacheParam> cacheObjects)
  {
    GameObject gameObject = new GameObject(((Object) org).name);
    Transform transform = gameObject.transform;
    UISprite component = ((Component) org).GetComponent<UISprite>();
    if (Object.op_Inequality((Object) null, (Object) component))
    {
      UISprite uiSprite = gameObject.AddComponent<UISprite>();
      uiSprite.atlas = component.atlas;
      uiSprite.spriteName = component.spriteName;
      uiSprite.width = component.width;
      uiSprite.height = component.height;
      uiSprite.type = component.type;
      uiSprite.depth = component.depth + 10000;
      uiSprite.color = component.color;
      uiSprite.centerType = component.centerType;
      uiSprite.flip = component.flip;
      uiSprite.pivot = component.pivot;
    }
    transform.SetParent(parent);
    transform.localPosition = org.localPosition;
    transform.localScale = org.localScale;
    gameObject.layer = ((Component) org).gameObject.layer;
    gameObject.SetActive(((Component) org).gameObject.activeSelf);
    cacheObjects.Add(new UIButtonEffect.CacheParam()
    {
      nameHash = ((Object) org).name.GetHashCode(),
      org = ((Component) org).gameObject,
      clone = gameObject
    });
    int childCount = org.childCount;
    for (int index = 0; index < childCount; ++index)
      UIButtonEffect.CreateSprites(org.GetChild(index), transform, cacheObjects);
    return transform;
  }

  private class CacheParam
  {
    public int nameHash;
    public GameObject org;
    public GameObject clone;
  }
}
