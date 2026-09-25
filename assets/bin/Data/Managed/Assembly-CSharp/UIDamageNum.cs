// Decompiled with JetBrains decompiler
// Type: UIDamageNum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
public class UIDamageNum : MonoBehaviour
{
  [SerializeField]
  protected UILabel damadeNum;
  [SerializeField]
  public UIGrid grid;
  [Tooltip("表示時間（秒）")]
  public float showTime = 1f;
  [Tooltip("表示高さオフセット")]
  public float offsetY;
  [Tooltip("跳ねる高さ")]
  public float upHeight = 10f;
  [Tooltip("上昇速度（距離/秒）")]
  public float upSpeed = 100f;
  [Tooltip("バフ時カラー")]
  public Color buffColor;
  public Color buffOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("炎時カラー")]
  public Color fireColor;
  public Color fireOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("水時カラー")]
  [FormerlySerializedAs("iceColor")]
  public Color waterColor;
  public Color waterOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("雷時カラー")]
  [FormerlySerializedAs("windColor")]
  public Color thunderColor;
  public Color thunderOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("土時カラー")]
  public Color soilColor;
  public Color soilOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("光時カラー")]
  public Color lightColor;
  public Color lightOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("闇時カラー")]
  public Color darkColor;
  public Color darkOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("有効時カラー")]
  public Color goodColor;
  public Color goodOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("無効時カラー")]
  public Color badColor;
  public Color badOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("部位にしかダメージが通らないカラー(無属性)")]
  public Color regionOnlyColor;
  public Color regionOnlyOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("部位にしかダメージが通らないカラー(属性)")]
  public Color regionOnlyElementColor;
  public Color regionOnlyElementOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  [Tooltip("部位にしかダメージ通らないカラー(弱点)")]
  public Color regionOnlyBuffColor;
  public Color regionOnlyBuffOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  private List<GameObject> damageNumList = new List<GameObject>();
  protected Vector3 worldPos;
  protected int higthOffset;
  protected int damageLength;
  protected float heightOffsetRatio = 1f;
  protected float higthOffset_f;
  protected float widthOffset;
  protected Color normalColor = Color.white;
  protected Color normalOutLineColor = new Color(0.03f, 0.08f, 0.14f);
  public bool enable;

  public int DamageLength => this.damageLength;

  protected void Awake()
  {
    this.normalColor = this.damadeNum.color;
    this.normalOutLineColor = this.damadeNum.effectColor;
  }

  protected void OnDisable()
  {
    int index = 0;
    for (int count = this.damageNumList.Count; index < count; ++index)
      this.damageNumList[index].GetComponent<UILabel>().alpha = 0.01f;
    this.enable = false;
  }

  public bool Initialize(Vector3 pos, int damage, UIDamageNum.DAMAGE_COLOR color, int groupOffset)
  {
    this.worldPos = pos;
    this.worldPos.y += this.offsetY;
    float num1 = (float) Screen.height / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    float num2 = (float) Screen.width / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
    this.higthOffset_f = (float) (this.damadeNum.height * groupOffset) * this.heightOffsetRatio * num1;
    this.widthOffset = (float) this.damadeNum.width * 0.2f * (float) groupOffset * num2;
    if (!this.SetPosFromWorld(this.worldPos))
      return false;
    this.enable = true;
    int count = this.damageNumList.Count;
    string str = damage.ToString();
    this.damageLength = str.Length;
    int index = 0;
    for (int damageLength = this.damageLength; index < damageLength; ++index)
    {
      UILabel label;
      if (count > index)
      {
        label = this.damageNumList[index].GetComponent<UILabel>();
        this.damageNumList[index].SetActive(true);
      }
      else
      {
        GameObject gameObject;
        if (index == 0)
        {
          gameObject = ((Component) this.damadeNum).gameObject;
          label = this.damadeNum;
        }
        else
        {
          gameObject = ResourceUtility.Instantiate<GameObject>(((Component) this.damadeNum).gameObject);
          Utility.Attach(((Component) this).gameObject.transform, gameObject.transform);
          label = gameObject.GetComponent<UILabel>();
        }
        this.damageNumList.Add(gameObject);
      }
      label.text = str[index].ToString();
      label.alpha = 1f;
      this.ChangeColor(color, label);
    }
    if (count > this.damageLength)
    {
      for (int length = str.Length; length < count; ++length)
        this.damageNumList[length].SetActive(false);
    }
    this.grid.Reposition();
    this.StartCoroutine(this.DirectionNumber());
    return true;
  }

  private IEnumerator DirectionNumber()
  {
    int num_count = this.damageNumList.Count;
    for (int i = 0; i < num_count; ++i)
    {
      GameObject obj = this.damageNumList[i];
      Vector3 v = obj.transform.localPosition;
      while ((double) v.y < (double) this.upHeight)
      {
        v.y += this.upSpeed * Time.deltaTime;
        obj.transform.localPosition = v;
        yield return (object) null;
      }
      while ((double) v.y >= 0.0)
      {
        v.y -= this.upSpeed * Time.deltaTime;
        obj.transform.localPosition = v;
        yield return (object) null;
      }
      v.y = 0.0f;
      obj.transform.localPosition = v;
      obj = (GameObject) null;
      v = new Vector3();
    }
    yield return (object) new WaitForSeconds(this.showTime);
    for (int index = 0; index < num_count; ++index)
      this.damageNumList[index].GetComponent<UILabel>().alpha = 0.01f;
    this.enable = false;
  }

  protected void LateUpdate()
  {
    if (!this.enable || this.SetPosFromWorld(this.worldPos))
      return;
    this.enable = false;
    int index = 0;
    for (int count = this.damageNumList.Count; index < count; ++index)
      this.damageNumList[index].GetComponent<UILabel>().alpha = 0.01f;
  }

  public Vector3 GetUIPosFromWorld(Vector3 world_pos, int groupOffset)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return Vector3.zero;
    world_pos.y += this.offsetY;
    Vector3 screenPoint = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
    float num = (float) Screen.height / (float) MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    this.higthOffset_f = (float) (this.damadeNum.height * groupOffset) * this.heightOffsetRatio * num;
    screenPoint.y += this.higthOffset_f;
    if ((double) screenPoint.z < 0.0)
      return Vector3.zero;
    screenPoint.z = 0.0f;
    return MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenPoint);
  }

  protected bool SetPosFromWorld(Vector3 world_pos)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return false;
    Vector3 screenPoint = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
    screenPoint.y += this.higthOffset_f;
    screenPoint.x += this.widthOffset;
    if ((double) screenPoint.z < 0.0)
      return false;
    screenPoint.z = 0.0f;
    ((Component) this).gameObject.transform.position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(screenPoint);
    return true;
  }

  protected void ChangeColor(UIDamageNum.DAMAGE_COLOR color, UILabel label)
  {
    switch (color)
    {
      case UIDamageNum.DAMAGE_COLOR.BUFF:
        label.color = this.buffColor;
        label.effectColor = this.buffOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.FIRE:
        label.color = this.fireColor;
        label.effectColor = this.fireOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.WATER:
        label.color = this.waterColor;
        label.effectColor = this.waterOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.THUNDER:
        label.color = this.thunderColor;
        label.effectColor = this.thunderOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.SOIL:
        label.color = this.soilColor;
        label.effectColor = this.soilOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.LIGHT:
        label.color = this.lightColor;
        label.effectColor = this.lightOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.DARK:
        label.color = this.darkColor;
        label.effectColor = this.darkOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.GOOD:
        label.color = this.goodColor;
        label.effectColor = this.goodOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.BAD:
        label.color = this.badColor;
        label.effectColor = this.badOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_NORMAL:
        label.color = this.regionOnlyColor;
        label.effectColor = this.regionOnlyOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_ELEMENT:
        label.color = this.regionOnlyElementColor;
        label.effectColor = this.regionOnlyElementOutLineColor;
        break;
      case UIDamageNum.DAMAGE_COLOR.REGION_ONLY_BUFF:
        label.color = this.regionOnlyBuffColor;
        label.effectColor = this.regionOnlyBuffOutLineColor;
        break;
      default:
        label.color = this.normalColor;
        label.effectColor = this.normalOutLineColor;
        break;
    }
  }

  public enum DAMAGE_COLOR
  {
    NONE,
    BUFF,
    FIRE,
    WATER,
    THUNDER,
    SOIL,
    LIGHT,
    DARK,
    GOOD,
    BAD,
    REGION_ONLY_NORMAL,
    REGION_ONLY_ELEMENT,
    REGION_ONLY_BUFF,
  }
}
