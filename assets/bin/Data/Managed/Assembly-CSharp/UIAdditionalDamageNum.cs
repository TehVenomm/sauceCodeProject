// Decompiled with JetBrains decompiler
// Type: UIAdditionalDamageNum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class UIAdditionalDamageNum : UIDamageNum
{
  [SerializeField]
  private TweenPosition animPos;
  [SerializeField]
  private TweenScale animScale;
  [SerializeField]
  private TweenPosition animPosNormal;
  [SerializeField]
  private TweenScale animScaleNormal;
  [SerializeField]
  private TweenPosition animPosGood;
  [SerializeField]
  private TweenScale animScaleGood;
  [SerializeField]
  private TweenPosition animPosBad;
  [SerializeField]
  private TweenScale animScaleBad;
  [SerializeField]
  private GameObject damageNormal;
  [SerializeField]
  private GameObject damageGood;
  [SerializeField]
  private GameObject damageBad;
  [SerializeField]
  private UILabel damageNumNormal;
  [SerializeField]
  private UILabel damageNumGood;
  [SerializeField]
  private UILabel damageNumBad;

  public bool Initialize(
    Vector3 pos,
    int damage,
    UIDamageNum.DAMAGE_COLOR color,
    int groupOffset,
    UIDamageNum originalDamage,
    int effective)
  {
    this.worldPos = pos;
    this.worldPos.y += this.offsetY;
    float num1 = (float) Screen.height / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    float num2 = (float) Screen.width / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
    this.higthOffset_f = (float) (this.damadeNum.height * groupOffset) * this.heightOffsetRatio * num1;
    this.widthOffset = (float) this.damadeNum.width * 0.2f * (float) groupOffset * num2;
    if (Object.op_Inequality((Object) null, (Object) this.damageNormal) && Object.op_Inequality((Object) null, (Object) this.damageGood) && Object.op_Inequality((Object) null, (Object) this.damageBad))
    {
      float num3 = 1f;
      if (effective == 0)
      {
        this.damageNormal.SetActive(true);
        this.damageGood.SetActive(false);
        this.damageBad.SetActive(false);
        this.animPos = this.animPosNormal;
        this.animScale = this.animScaleNormal;
        this.damadeNum = this.damageNumNormal;
      }
      else if (0 < effective)
      {
        this.damageNormal.SetActive(false);
        this.damageGood.SetActive(true);
        this.damageBad.SetActive(false);
        this.animPos = this.animPosGood;
        this.animScale = this.animScaleGood;
        this.damadeNum = this.damageNumGood;
        num3 = 1.2f;
      }
      else
      {
        this.damageNormal.SetActive(false);
        this.damageGood.SetActive(false);
        this.damageBad.SetActive(true);
        this.animPos = this.animPosBad;
        this.animScale = this.animScaleBad;
        this.damadeNum = this.damageNumBad;
      }
      this.higthOffset_f *= num3;
      this.widthOffset *= num3;
      color = this.calcColor(color, effective);
    }
    if (groupOffset == 0 && 0 >= effective)
    {
      this.animScale.from = new Vector3(1f, 1f, 1f);
      this.animScale.to = new Vector3(1f, 1f, 1f);
    }
    if (!this.SetPosFromWorld(this.worldPos))
      return false;
    this.enable = true;
    this.damadeNum.text = damage.ToString();
    this.damageLength = this.damadeNum.text.Length;
    this.ChangeColor(color, this.damadeNum);
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
      this.animPos.ResetToBeginning();
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
      this.animScale.ResetToBeginning();
    this.StartCoroutine(this.DirectionNumber());
    return true;
  }

  private IEnumerator DirectionNumber()
  {
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
      this.animPos.PlayForward();
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
      this.animScale.PlayForward();
    if (Object.op_Inequality((Object) this.animPos, (Object) null))
    {
      while (((Behaviour) this.animPos).enabled)
        yield return (object) null;
    }
    if (Object.op_Inequality((Object) this.animScale, (Object) null))
    {
      while (((Behaviour) this.animScale).enabled)
        yield return (object) null;
    }
    this.enable = false;
    this.damadeNum.alpha = 0.01f;
  }

  private UIDamageNum.DAMAGE_COLOR calcColor(UIDamageNum.DAMAGE_COLOR color, int effective)
  {
    switch (color)
    {
      case UIDamageNum.DAMAGE_COLOR.FIRE:
      case UIDamageNum.DAMAGE_COLOR.WATER:
      case UIDamageNum.DAMAGE_COLOR.THUNDER:
      case UIDamageNum.DAMAGE_COLOR.SOIL:
      case UIDamageNum.DAMAGE_COLOR.LIGHT:
      case UIDamageNum.DAMAGE_COLOR.DARK:
        return color;
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_NORMAL:
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_ELEMENT:
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_BUFF:
        return color;
      default:
        if (effective == 0)
          return color;
        return 0 < effective ? UIDamageNum.DAMAGE_COLOR.GOOD : UIDamageNum.DAMAGE_COLOR.BAD;
    }
  }
}
