// Decompiled with JetBrains decompiler
// Type: FieldSonarObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class FieldSonarObject : FieldGimmickObject
{
  public const string SonarEffectName = "ef_btl_sonar_01";
  public const string SonarTouchEffectName = "ef_btl_sonar_02";
  public const int SonarSE = 40000107;
  private Transform sonarEffect;
  private Transform sonarTouchEffect;
  private Transform targetMarker;
  private Transform _transform;
  private bool acting;

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (isNear && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 35))
    {
      string sonarTargetEffect = ResourceName.GetSonarTargetEffect();
      if (Object.op_Equality((Object) this.targetMarker, (Object) null) && !string.IsNullOrEmpty(sonarTargetEffect))
        this.targetMarker = EffectManager.GetEffect(sonarTargetEffect, this._transform);
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      this.targetMarker.Set(Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this._transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetMarker, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetMarker).gameObject);
    }
  }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateSonar(this);
    base.Initialize(pointData);
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
      return;
    this.sonarEffect = EffectManager.GetEffect("ef_btl_sonar_01", this.modelTrans);
  }

  public void StartSonar()
  {
    if (!this.IsValidSonar())
      return;
    this.StartCoroutine(this.ActSonar());
  }

  public override void RequestDestroy()
  {
    if (Object.op_Inequality((Object) this.sonarEffect, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.sonarEffect).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.sonarEffect).gameObject);
      this.sonarEffect = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.sonarTouchEffect, (Object) null))
    {
      if (Object.op_Inequality((Object) ((Component) this.sonarTouchEffect).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) this.sonarTouchEffect).gameObject);
      this.sonarTouchEffect = (Transform) null;
    }
    base.RequestDestroy();
  }

  public override string GetObjectName() => "Sonar";

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  private IEnumerator ActSonar()
  {
    this.acting = true;
    SoundManager.PlayOneShotSE(40000107, MonoBehaviourSingleton<StageObjectManager>.I.self._position);
    if (Object.op_Equality((Object) this.sonarTouchEffect, (Object) null))
      this.sonarTouchEffect = EffectManager.GetEffect("ef_btl_sonar_02", this.modelTrans);
    ((Component) this.sonarTouchEffect).gameObject.SetActive(true);
    yield return (object) new WaitForSeconds(0.9f);
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("FieldSonarObject.OnTriggerEnter", ((Component) this).gameObject, "EXPLOREMAP", (object) ExploreMap.OPEN_MAP_TYPE.SONAR);
    this.acting = false;
  }

  private bool IsValidSonar()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart || MonoBehaviourSingleton<InGameProgress>.I.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    return !Object.op_Equality((Object) self, (Object) null) && !self.isDead && !this.acting;
  }
}
