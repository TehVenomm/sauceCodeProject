// Decompiled with JetBrains decompiler
// Type: WaveMatchDropObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WaveMatchDropObject : MonoBehaviour
{
  private const string kObjectName = "WM_Drop:";
  private readonly float kColliderSize = 1.2f;
  private readonly float kDropTime = 0.5f;
  private int id;
  private StageObjectManager cachedStageObjMgr;
  private Transform cachedTransform;
  private SphereCollider cachedCollider;
  private int ignoreLayerMask;
  private float lifeSpan;
  private Transform effectTrans;
  private WaveMatchDropObject.eState state;
  private float dropTime;
  private Vector3 basePos;
  private Vector3 targetPos;
  protected WaveMatchDropTable.WaveMatchDropData tableData;

  public int GetId() => this.id;

  public void Initialize(
    int _id,
    Vector3 _pos,
    Vector3 _offset,
    float _sec,
    WaveMatchDropTable.WaveMatchDropData _data)
  {
    ((Object) this).name = "WM_Drop:" + this.id.ToString();
    this.id = _id;
    this.basePos = _pos;
    this.targetPos = Vector3.op_Addition(_pos, _offset);
    this.lifeSpan = _sec;
    this.dropTime = 0.0f;
    this.tableData = _data;
    this.cachedStageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
    this.cachedTransform = ((Component) this).transform;
    this.cachedTransform.localPosition = this.basePos;
    this.cachedCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    this.cachedCollider.radius = this.kColliderSize;
    ((Collider) this.cachedCollider).isTrigger = true;
    ((Collider) this.cachedCollider).enabled = false;
    ((Component) this).gameObject.layer = 31 /*0x1F*/;
    this.ignoreLayerMask |= 41984;
    this.ignoreLayerMask |= 20480 /*0x5000*/;
    this.ignoreLayerMask |= 2490880;
    this.state = WaveMatchDropObject.eState.Drop;
    MonoBehaviourSingleton<StageObjectManager>.I.AddWaveMatchDropObject(this);
  }

  private void Update()
  {
    switch (this.state)
    {
      case WaveMatchDropObject.eState.Drop:
        this._UpdateDrop();
        break;
      case WaveMatchDropObject.eState.Idle:
        this._UpdateIdle();
        break;
    }
  }

  private void _UpdateDrop()
  {
    this.dropTime += Time.deltaTime;
    float num = this.dropTime / this.kDropTime;
    if ((double) num > 1.0)
      num = 1f;
    this.cachedTransform.localPosition = Vector3.Lerp(this.basePos, this.targetPos, num);
    if ((double) num < 1.0)
      return;
    this.effectTrans = EffectManager.GetEffect("ef_btl_target_dropitem_01", this.cachedTransform);
    ((Collider) this.cachedCollider).enabled = true;
    this.state = WaveMatchDropObject.eState.Idle;
  }

  private void _UpdateIdle()
  {
    this.lifeSpan -= Time.deltaTime;
    if ((double) this.lifeSpan > 0.0)
      return;
    this.cachedStageObjMgr.RemoveWaveMatchDropObject(this.id);
  }

  public void OnDisappear()
  {
    this.state = WaveMatchDropObject.eState.None;
    if (Object.op_Inequality((Object) this.cachedCollider, (Object) null))
      ((Collider) this.cachedCollider).enabled = false;
    EffectManager.ReleaseEffect(ref this.effectTrans);
    if (!Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  public virtual void OnPicked(Self self)
  {
    if (!this.tableData.getEffect.IsNullOrWhiteSpace())
      EffectManager.OneShot(this.tableData.getEffect, self._position, Quaternion.identity);
    if (this.tableData.getSE != 0)
      SoundManager.PlayOneShotSE(this.tableData.getSE, self._position);
    int index = 0;
    for (int count = this.tableData.buffTableIds.Count; index < count; ++index)
      self.StartBuffByBuffTableId(this.tableData.buffTableIds[index], (SkillInfo.SkillParam) null);
  }

  public virtual void OnReceiveEffect()
  {
  }

  private void OnTriggerEnter(Collider collider)
  {
    int layer = ((Component) collider).gameObject.layer;
    if ((1 << layer & this.ignoreLayerMask) > 0 || layer == 8 && Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null))
      return;
    Self component = ((Component) collider).gameObject.GetComponent<Self>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.OnPicked(component);
    this.cachedStageObjMgr.RemoveWaveMatchDropObject(this.id);
    if (!Object.op_Inequality((Object) component.playerSender, (Object) null))
      return;
    component.playerSender.OnPickedWaveMatchDropObject(this.id, this.tableData.id);
  }

  private enum eState
  {
    None,
    Drop,
    Idle,
  }
}
