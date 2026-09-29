// Decompiled with JetBrains decompiler
// Type: UIPlayerDamageNum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIPlayerDamageNum : MonoBehaviour
{
  [SerializeField]
  protected UILabel damadeNum;
  [SerializeField]
  protected TweenPosition animPos;
  [SerializeField]
  protected TweenAlpha animAlpha;
  [SerializeField]
  protected TweenScale animScale;
  [Tooltip("表示高さオフセット")]
  public Vector3 offset = Vector3.zero;
  [Tooltip("ダメージカラー")]
  public UIPlayerDamageNum.LabelColor damageColor;
  [Tooltip("回復カラー")]
  public UIPlayerDamageNum.LabelColor healColor;
  [Tooltip("属性カラー")]
  public List<UIPlayerDamageNum.LabelColor> elementColor;
  public UIPlayerDamageNum.LabelColor defaultColor;
  protected Character chara;
  protected int higthOffset;
  private bool isPlaying;
  private bool isAutoDelete;
  public bool enable;

  public void EnableAutoDelete() => this.isAutoDelete = true;

  private void OnDisable() => this.OnFinishAnimation();

  public bool Initialize(Character _chara, AttackedHitStatus status, bool isAutoPlay = true)
  {
    return this.Initialize(_chara, status.damage + status.shieldDamage, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE, status.damageDetails.GetElementType(), isAutoPlay);
  }

  public bool Initialize(
    Character _chara,
    int damage,
    UIPlayerDamageNum.DAMAGE_COLOR color,
    bool isAutoPlay = true)
  {
    return this.Initialize(_chara, damage, color, ELEMENT_TYPE.MAX, isAutoPlay);
  }

  public bool Initialize(
    Character _chara,
    int damage,
    UIPlayerDamageNum.DAMAGE_COLOR color,
    ELEMENT_TYPE element,
    bool isAutoPlay = true)
  {
    this.chara = _chara;
    this.higthOffset = this.damadeNum.height;
    if (!this.SetPosFromWorld(Vector3.op_Addition(this.chara._position, this.offset), true))
      return false;
    this.enable = true;
    this.damadeNum.text = damage.ToString();
    switch (color)
    {
      case UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE:
        if (damage == 0)
        {
          this.damadeNum.color = this.defaultColor.main;
          this.damadeNum.effectColor = this.defaultColor.effect;
          break;
        }
        if (this.elementColor.Count >= 0 && (ELEMENT_TYPE) this.elementColor.Count > element)
        {
          this.damadeNum.color = this.elementColor[(int) element].main;
          this.damadeNum.effectColor = this.elementColor[(int) element].effect;
          break;
        }
        this.damadeNum.color = this.elementColor[6].main;
        this.damadeNum.effectColor = this.elementColor[6].effect;
        break;
      case UIPlayerDamageNum.DAMAGE_COLOR.HEAL:
        this.damadeNum.color = this.healColor.main;
        this.damadeNum.effectColor = this.healColor.effect;
        break;
    }
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
    {
      this.animPos.ResetToBeginning();
      ((Behaviour) this.animPos).enabled = false;
    }
    if (Object.op_Inequality((Object) this.animAlpha, (Object) null))
    {
      this.animAlpha.ResetToBeginning();
      ((Behaviour) this.animAlpha).enabled = false;
    }
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
    {
      this.animScale.ResetToBeginning();
      ((Behaviour) this.animScale).enabled = false;
    }
    ((Component) this).transform.localScale = Vector3.zero;
    if (isAutoPlay)
      this.Play();
    return true;
  }

  private IEnumerator DirectionNumber()
  {
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
      this.animPos.PlayForward();
    if (Object.op_Inequality((Object) this.animAlpha, (Object) null))
      this.animAlpha.PlayForward();
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
      this.animScale.PlayForward();
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
    {
      while (((Behaviour) this.animPos).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.animAlpha, (Object) null))
    {
      while (((Behaviour) this.animAlpha).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
    {
      while (((Behaviour) this.animScale).enabled)
        yield return (object) null;
    }
    this.OnFinishAnimation();
  }

  private void LateUpdate()
  {
    if (!this.isPlaying || !this.enable || Object.op_Equality((Object) this.chara, (Object) null) || this.SetPosFromWorld(Vector3.op_Addition(this.chara._transform.position, this.offset), false))
      return;
    this.OnFinishAnimation();
  }

  private void OnFinishAnimation()
  {
    this.enable = false;
    this.damadeNum.alpha = 0.0f;
    this.chara = (Character) null;
    this.isPlaying = false;
    if (!this.isAutoDelete)
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
    this.isAutoDelete = false;
  }

  private bool SetPosFromWorld(Vector3 world_pos, bool bUpdatePosY)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return false;
    Vector3 screenPoint = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
    screenPoint.y += (float) this.higthOffset;
    if ((double) screenPoint.z < 0.0)
      return false;
    screenPoint.z = 0.0f;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenPoint);
    if (!bUpdatePosY)
      worldPoint.y = ((Component) this).gameObject.transform.position.y;
    ((Component) this).gameObject.transform.position = worldPoint;
    return true;
  }

  public void Play()
  {
    this.isPlaying = true;
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
      ((Behaviour) this.animPos).enabled = true;
    if (Object.op_Inequality((Object) this.animAlpha, (Object) null))
      ((Behaviour) this.animAlpha).enabled = true;
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
      ((Behaviour) this.animScale).enabled = true;
    ((Component) this).transform.localScale = Vector3.one;
    this.StartCoroutine(this.DirectionNumber());
  }

  public float AlphaRate
  {
    get
    {
      return !Object.op_Inequality((Object) this.damadeNum, (Object) null) ? 0.0f : this.damadeNum.alpha;
    }
  }

  [Serializable]
  public class LabelColor
  {
    public Color main;
    public Color effect;
  }

  public enum DAMAGE_COLOR
  {
    DAMAGE,
    HEAL,
  }
}
