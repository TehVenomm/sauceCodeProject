// Decompiled with JetBrains decompiler
// Type: AtkAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
[Serializable]
public class AtkAttribute
{
  [Tooltip("通常")]
  public float normal;
  [Tooltip("炎")]
  public float fire;
  [Tooltip("氷")]
  [FormerlySerializedAs("ice")]
  public float water;
  [Tooltip("雷")]
  [FormerlySerializedAs("wind")]
  public float thunder;
  [Tooltip("土")]
  public float soil;
  [Tooltip("光")]
  public float light;
  [Tooltip("闇")]
  public float dark;
  private float[] baseElementTolerances = new float[6];
  private int[,] ElementToleranceScrollTable;

  public void Set(float targetValue)
  {
    this.normal = this.fire = this.water = this.thunder = this.soil = this.light = this.dark = targetValue;
  }

  public void Copy(AtkAttribute srcAtk)
  {
    this.normal = srcAtk.normal;
    this.fire = srcAtk.fire;
    this.water = srcAtk.water;
    this.thunder = srcAtk.thunder;
    this.soil = srcAtk.soil;
    this.light = srcAtk.light;
    this.dark = srcAtk.dark;
  }

  public void Mul(AtkAttribute val)
  {
    this.normal *= val.normal;
    this.fire *= val.fire;
    this.water *= val.water;
    this.thunder *= val.thunder;
    this.soil *= val.soil;
    this.light *= val.light;
    this.dark *= val.dark;
  }

  public void Mul(float val)
  {
    this.normal *= val;
    this.fire *= val;
    this.water *= val;
    this.thunder *= val;
    this.soil *= val;
    this.light *= val;
    this.dark *= val;
  }

  public void Div(float val)
  {
    if ((double) val == 0.0)
      return;
    this.normal /= val;
    this.fire /= val;
    this.water /= val;
    this.thunder /= val;
    this.soil /= val;
    this.light /= val;
    this.dark /= val;
  }

  public void Add(AtkAttribute val)
  {
    if (val == null)
      return;
    this.normal += val.normal;
    this.fire += val.fire;
    this.water += val.water;
    this.thunder += val.thunder;
    this.soil += val.soil;
    this.light += val.light;
    this.dark += val.dark;
  }

  public void AddRate(float rate)
  {
    this.normal += rate;
    this.fire += rate;
    this.water += rate;
    this.thunder += rate;
    this.soil += rate;
    this.light += rate;
    this.dark += rate;
  }

  public void Sub(AtkAttribute val)
  {
    this.normal -= val.normal;
    this.fire -= val.fire;
    this.water -= val.water;
    this.thunder -= val.thunder;
    this.soil -= val.soil;
    this.light -= val.light;
    this.dark -= val.dark;
  }

  public void ChangeElementType(ELEMENT_TYPE type)
  {
    float normal = this.normal;
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        normal += this.fire;
        break;
      case ELEMENT_TYPE.WATER:
        normal += this.water;
        break;
      case ELEMENT_TYPE.THUNDER:
        normal += this.thunder;
        break;
      case ELEMENT_TYPE.SOIL:
        normal += this.soil;
        break;
      case ELEMENT_TYPE.LIGHT:
        normal += this.light;
        break;
      case ELEMENT_TYPE.DARK:
        normal += this.dark;
        break;
    }
    this.Mul(0.0f);
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        this.fire = normal;
        break;
      case ELEMENT_TYPE.WATER:
        this.water = normal;
        break;
      case ELEMENT_TYPE.THUNDER:
        this.thunder = normal;
        break;
      case ELEMENT_TYPE.SOIL:
        this.soil = normal;
        break;
      case ELEMENT_TYPE.LIGHT:
        this.light = normal;
        break;
      case ELEMENT_TYPE.DARK:
        this.dark = normal;
        break;
      default:
        this.normal = normal;
        break;
    }
  }

  public ELEMENT_TYPE GetElementType()
  {
    ELEMENT_TYPE elementType = ELEMENT_TYPE.MAX;
    float num = 0.0f;
    if ((double) this.fire > (double) num)
    {
      elementType = ELEMENT_TYPE.FIRE;
      num = this.fire;
    }
    if ((double) this.water > (double) num)
    {
      elementType = ELEMENT_TYPE.WATER;
      num = this.water;
    }
    if ((double) this.thunder > (double) num)
    {
      elementType = ELEMENT_TYPE.THUNDER;
      num = this.thunder;
    }
    if ((double) this.soil > (double) num)
    {
      elementType = ELEMENT_TYPE.SOIL;
      num = this.soil;
    }
    if ((double) this.light > (double) num)
    {
      elementType = ELEMENT_TYPE.LIGHT;
      num = this.light;
    }
    if ((double) this.dark > (double) num)
    {
      elementType = ELEMENT_TYPE.DARK;
      float dark = this.dark;
    }
    return elementType;
  }

  public ELEMENT_TYPE GetAntiElementType()
  {
    ELEMENT_TYPE antiElementType = ELEMENT_TYPE.MAX;
    float num = 1f;
    if ((double) this.normal < (double) num)
    {
      antiElementType = ELEMENT_TYPE.MAX;
      num = this.normal;
    }
    if ((double) this.fire < (double) num)
    {
      antiElementType = ELEMENT_TYPE.FIRE;
      num = this.fire;
    }
    if ((double) this.water < (double) num)
    {
      antiElementType = ELEMENT_TYPE.WATER;
      num = this.water;
    }
    if ((double) this.thunder < (double) num)
    {
      antiElementType = ELEMENT_TYPE.THUNDER;
      num = this.thunder;
    }
    if ((double) this.soil < (double) num)
    {
      antiElementType = ELEMENT_TYPE.SOIL;
      num = this.soil;
    }
    if ((double) this.light < (double) num)
    {
      antiElementType = ELEMENT_TYPE.LIGHT;
      num = this.light;
    }
    if ((double) this.dark < (double) num)
    {
      antiElementType = ELEMENT_TYPE.DARK;
      float dark = this.dark;
    }
    return antiElementType;
  }

  public void AddElementValueWithCheck(float targetValue)
  {
    if ((double) this.fire > 0.0)
      this.fire += targetValue;
    if ((double) this.water > 0.0)
      this.water += targetValue;
    if ((double) this.thunder > 0.0)
      this.thunder += targetValue;
    if ((double) this.soil > 0.0)
      this.soil += targetValue;
    if ((double) this.light > 0.0)
      this.light += targetValue;
    if ((double) this.dark <= 0.0)
      return;
    this.dark += targetValue;
  }

  public void AddElementOnly(float targetValue)
  {
    this.fire += targetValue;
    this.water += targetValue;
    this.thunder += targetValue;
    this.soil += targetValue;
    this.light += targetValue;
    this.dark += targetValue;
  }

  public void AddAll(float targetValue)
  {
    this.normal += targetValue;
    this.fire += targetValue;
    this.water += targetValue;
    this.thunder += targetValue;
    this.soil += targetValue;
    this.light += targetValue;
    this.dark += targetValue;
  }

  public void MulElementOnly(float targetValue)
  {
    this.fire *= targetValue;
    this.water *= targetValue;
    this.thunder *= targetValue;
    this.soil *= targetValue;
    this.light *= targetValue;
    this.dark *= targetValue;
  }

  public void SubElementOnly(float targetValue)
  {
    this.fire -= targetValue;
    if ((double) this.fire < 0.0)
      this.fire = 0.0f;
    this.water -= targetValue;
    if ((double) this.water < 0.0)
      this.water = 0.0f;
    this.thunder -= targetValue;
    if ((double) this.thunder < 0.0)
      this.thunder = 0.0f;
    this.soil -= targetValue;
    if ((double) this.soil < 0.0)
      this.soil = 0.0f;
    this.light -= targetValue;
    if ((double) this.light < 0.0)
      this.light = 0.0f;
    this.dark -= targetValue;
    if ((double) this.dark >= 0.0)
      return;
    this.dark = 0.0f;
  }

  public void CheckMinus()
  {
    if ((double) this.normal < 0.0)
      this.normal = 0.0f;
    if ((double) this.fire < 0.0)
      this.fire = 0.0f;
    if ((double) this.water < 0.0)
      this.water = 0.0f;
    if ((double) this.thunder < 0.0)
      this.thunder = 0.0f;
    if ((double) this.soil < 0.0)
      this.soil = 0.0f;
    if ((double) this.light < 0.0)
      this.light = 0.0f;
    if ((double) this.dark >= 0.0)
      return;
    this.dark = 0.0f;
  }

  public void SetTargetElemetAll(float val)
  {
    this.SetTargetElement(ELEMENT_TYPE.FIRE, val);
    this.SetTargetElement(ELEMENT_TYPE.WATER, val);
    this.SetTargetElement(ELEMENT_TYPE.THUNDER, val);
    this.SetTargetElement(ELEMENT_TYPE.SOIL, val);
    this.SetTargetElement(ELEMENT_TYPE.LIGHT, val);
    this.SetTargetElement(ELEMENT_TYPE.DARK, val);
  }

  public void SetTargetElement(ELEMENT_TYPE type, float val)
  {
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        this.fire = val;
        break;
      case ELEMENT_TYPE.WATER:
        this.water = val;
        break;
      case ELEMENT_TYPE.THUNDER:
        this.thunder = val;
        break;
      case ELEMENT_TYPE.SOIL:
        this.soil = val;
        break;
      case ELEMENT_TYPE.LIGHT:
        this.light = val;
        break;
      case ELEMENT_TYPE.DARK:
        this.dark = val;
        break;
    }
  }

  public void AddTargetElement(ELEMENT_TYPE type, float val)
  {
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        this.fire += val;
        break;
      case ELEMENT_TYPE.WATER:
        this.water += val;
        break;
      case ELEMENT_TYPE.THUNDER:
        this.thunder += val;
        break;
      case ELEMENT_TYPE.SOIL:
        this.soil += val;
        break;
      case ELEMENT_TYPE.LIGHT:
        this.light += val;
        break;
      case ELEMENT_TYPE.DARK:
        this.dark += val;
        break;
    }
  }

  public float CalcTotal()
  {
    return this.normal + this.fire + this.water + this.thunder + this.soil + this.light + this.dark;
  }

  public void InitializeElementTolerance(ConverteElementToleranceTable[] convTable)
  {
    int length = convTable.Length;
    if (length > 0)
    {
      this.ElementToleranceScrollTable = new int[length, 6];
      for (int index = 0; index < length; ++index)
      {
        for (int no = 0; no < 6; ++no)
          this.ElementToleranceScrollTable[index, no] = convTable[index].GetChangeElement(no);
      }
    }
    else
      this.ElementToleranceScrollTable = new int[5, 6]
      {
        {
          0,
          1,
          2,
          3,
          4,
          5
        },
        {
          1,
          2,
          3,
          0,
          4,
          5
        },
        {
          2,
          3,
          0,
          1,
          4,
          5
        },
        {
          3,
          0,
          1,
          2,
          4,
          5
        },
        {
          0,
          1,
          2,
          3,
          5,
          4
        }
      };
    this.baseElementTolerances = new float[6];
    this.baseElementTolerances[0] = this.fire;
    this.baseElementTolerances[1] = this.water;
    this.baseElementTolerances[2] = this.thunder;
    this.baseElementTolerances[3] = this.soil;
    this.baseElementTolerances[4] = this.light;
    this.baseElementTolerances[5] = this.dark;
  }

  public void ChangeElementTolerance(int scroll)
  {
    int length = this.ElementToleranceScrollTable.GetLength(0);
    if (scroll < 0 || scroll >= length)
    {
      Log.Error("scroll is out of range!! ");
    }
    else
    {
      int type = 0;
      for (int index1 = 6; type < index1; ++type)
      {
        int index2 = this.ElementToleranceScrollTable[scroll, type];
        this.SetTargetElement((ELEMENT_TYPE) type, this.baseElementTolerances[index2]);
      }
    }
  }

  public override string ToString()
  {
    return $"[AtkAttribute] normal:{this.normal} fire:{this.fire} water:{this.water} thunder:{this.thunder} soil:{this.soil} light:{this.light} dark:{this.dark}";
  }

  public string ToShortString()
  {
    return $"{$"{$"{$"{$"{$"{$"n:{(object) this.normal} "}f:{(object) this.fire} "}w:{(object) this.water} "}t:{(object) this.thunder} "}s:{(object) this.soil} "}l:{(object) this.light} "}d:{(object) this.dark} ";
  }
}
