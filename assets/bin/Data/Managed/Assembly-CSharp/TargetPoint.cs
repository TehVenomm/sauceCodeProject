// Decompiled with JetBrains decompiler
// Type: TargetPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TargetPoint : MonoBehaviour
{
  [Tooltip("位置のオフセット")]
  public Vector3 offset;
  [Tooltip("マーカー表示位置のZオフセット。マイナス値で手前に移動")]
  public float markerZShift;
  [Tooltip("部位ID")]
  public int regionID = -1;
  [Tooltip("通常ターゲット有効")]
  public bool isTargetEnable = true;
  [Tooltip("弓の狙い時有効")]
  public bool isAimEnable = true;
  [Tooltip("isAimEnableはfalseだけどArrowSpWeak表示したい")]
  public bool isDispArrowSpWeak;
  [Tooltip("継続ダメージエフェクトオフセット")]
  public Vector3 bleedOffsetPos = Vector3.zero;
  [Tooltip("継続ダメージエフェクト回転オフセット")]
  public Vector3 bleedOffsetRot = Vector3.zero;
  [Tooltip("狙いマーカーのポイントサイズ")]
  public float aimMarkerPointRate = 1f;
  [Tooltip("ターゲット選定時のウェイト")]
  public float weight;
  [Tooltip("ヒット判定のDot計算をスキップ")]
  public bool isSkipDotCalc;
  private bool m_isForceDisplay;
  private Transform effectTransform;
  private Vector3 effectOrgLossyScele = new Vector3(1f, 1f, 1f);
  private int effectShowIndex;

  public Transform _transform { get; private set; }

  public Vector3 scaledOffset { get; private set; }

  public float scaledMarkerZShift { get; private set; }

  public StageObject owner { get; set; }

  public RegionRoot subRegionRoot { get; set; }

  public bool IsForceDisplay => this.m_isForceDisplay;

  public void ForceDisplay() => this.m_isForceDisplay = true;

  public TargetPoint.Param param { get; set; }

  public TargetPoint()
  {
    this.param = new TargetPoint.Param();
    this.owner = (StageObject) null;
    this.subRegionRoot = (RegionRoot) null;
  }

  private void Awake() => this._transform = ((Component) this).transform;

  private void Start() => this.scaledCalc();

  public void scaledCalc()
  {
    this.scaledOffset = this.offset.Mul(this._transform.lossyScale);
    this.scaledMarkerZShift = this.markerZShift * this._transform.lossyScale.z;
  }

  public bool IsEneble()
  {
    Enemy owner = this.owner as Enemy;
    return !Object.op_Equality((Object) owner, (Object) null) && !owner.isDead && owner.enableTargetPoint;
  }

  public Vector3 GetTargetPoint()
  {
    return Vector3.op_Addition(this._transform.position, Quaternion.op_Multiply(this._transform.rotation, this.scaledOffset));
  }

  public Transform PlayArrowBleedEffect(string effect_name, int show_index)
  {
    Transform effect = EffectManager.GetEffect(effect_name, this._transform);
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      effect.localScale = Vector3.one.Div(this._transform.lossyScale);
      effect.localPosition = this.GetArrowBleedLocalPosition();
      effect.localRotation = this.GetArrowBleedLocalRotation(show_index);
      this.effectTransform = effect;
      this.effectOrgLossyScele = this.effectTransform.lossyScale;
      this.effectShowIndex = show_index;
    }
    return effect;
  }

  public Transform PlayArrowBurstEffect(string effect_name, Transform trs)
  {
    Transform effect = EffectManager.GetEffect(effect_name);
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      effect.localScale = Vector3.one.Div(this._transform.lossyScale);
      effect.position = !Object.op_Inequality((Object) null, (Object) trs) ? this._transform.position : trs.position;
      effect.rotation = Quaternion.identity;
    }
    return effect;
  }

  public Vector3 GetArrowBleedLocalPosition() => this.bleedOffsetPos;

  public Quaternion GetArrowBleedLocalRotation(int show_index = 0)
  {
    Quaternion quaternion1 = Quaternion.identity;
    if (Vector3.op_Inequality(this.offset, Vector3.zero))
      quaternion1 = Quaternion.LookRotation(Vector3.op_UnaryNegation(this.offset));
    Quaternion bleedLocalRotation = Quaternion.op_Multiply(quaternion1, Quaternion.Euler(this.bleedOffsetRot.x, this.bleedOffsetRot.y, this.bleedOffsetRot.z));
    if (show_index > 0 && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      InGameSettingsManager.Player.SpecialActionInfo.ArrowBleedOther arrowBleedOther = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedOther;
      if (arrowBleedOther != null)
      {
        Quaternion quaternion2 = Quaternion.AngleAxis((float) ((double) (show_index - 1) * 120.0 + (double) arrowBleedOther.axisRandomAngle * (double) Random.value - (double) arrowBleedOther.axisRandomAngle * 0.5), Vector3.forward);
        Quaternion quaternion3 = Quaternion.Euler(arrowBleedOther.openFixAngle + arrowBleedOther.openRandomAngle * Random.value, 0.0f, 0.0f);
        bleedLocalRotation = Quaternion.op_Multiply(Quaternion.op_Multiply(bleedLocalRotation, quaternion2), quaternion3);
      }
    }
    return bleedLocalRotation;
  }

  public void ArrowBleedEffectUpdate(bool isPostion, bool isRotation, bool isOrgScele)
  {
    if (Object.op_Equality((Object) this.effectTransform, (Object) null))
      return;
    if (isOrgScele)
    {
      Vector3 lossyScale = this.effectTransform.lossyScale;
      Vector3 localScale = this.effectTransform.localScale;
      this.effectTransform.localScale = new Vector3(localScale.x / lossyScale.x * this.effectOrgLossyScele.x, localScale.y / lossyScale.y * this.effectOrgLossyScele.y, localScale.z / lossyScale.z * this.effectOrgLossyScele.z);
    }
    if (isPostion)
      this.effectTransform.localPosition = this.GetArrowBleedLocalPosition();
    if (!isRotation)
      return;
    this.effectTransform.localRotation = this.GetArrowBleedLocalRotation(this.effectShowIndex);
  }

  public class Param
  {
    public bool isShowRange;
    public Vector3 markerPos = Vector3.zero;
    public Quaternion markerRot = Quaternion.identity;
    public Vector3 targetPos = Vector3.zero;
    public float vecSqrMagnitude;
    public float targetSelectCounter;
    public bool isTargetEnable;
    public float aimMarkerScale = 1f;
    public Enemy.WEAK_STATE weakState;
    public int weakSubParam = -1;
    public Enemy.WEAK_STATE prevWeakState;
    public int prevWeakSubParam = -1;
    public bool playSignEffect;
    public int validElementType = -1;
  }
}
