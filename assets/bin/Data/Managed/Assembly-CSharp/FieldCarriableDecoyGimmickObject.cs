// Decompiled with JetBrains decompiler
// Type: FieldCarriableDecoyGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldCarriableDecoyGimmickObject : FieldCarriableGimmickObject
{
  public static readonly string kDecoyEffectName = "ef_btl_trap_02_01";
  public static readonly string kBreakEffectName = "ef_btl_trap_02_02";
  public static readonly string kPutEffectName = "ef_btl_trap_01_02";
  public static readonly int kPutSEId = 10000058;
  public static readonly int kBreakSEId = 20000022;
  private static readonly int kShiftIndex = 1000;
  private static readonly float kRenderOffSet = 0.5f;
  public float activeTime;
  public float maxActiveTime = 10f;
  private Material gaugeMat;
  private DecoyBulletObject targetObjectForEnemy;
  private Transform decoyEffect;

  public bool isActive => !this.isCarrying && this.hasDeploied && (double) this.activeTime > 0.0;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.activeTime = this.maxActiveTime;
    this.targetObjectForEnemy = ((Component) this).gameObject.AddComponent<DecoyBulletObject>();
    if (Object.op_Inequality((Object) this.targetObjectForEnemy, (Object) null))
      this.targetObjectForEnemy.Initialize(-1, -1, (BulletData) null, this.m_transform.position, (SkillInfo.SkillParam) null, false);
    this.targetObjectForEnemy.SetCarriable(this);
    Transform transform = ((Component) this).transform.Find("CMN_decoytrap01");
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    Renderer component = ((Component) ((Component) transform).transform.Find("object01")).GetComponent<Renderer>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    this.gaugeMat = component.materials[1];
    this.UpdateGauge();
  }

  protected override void ParseParam(string value2)
  {
    base.ParseParam(value2);
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "t")
        this.maxActiveTime = float.Parse(strArray[1]);
    }
  }

  protected override void OnStartCarry(Player owner)
  {
    base.OnStartCarry(owner);
    if (Object.op_Inequality((Object) this.decoyEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.decoyEffect).gameObject);
      this.decoyEffect = (Transform) null;
    }
    MonoBehaviourSingleton<StageObjectManager>.I.CheckAllEnemiesMissDecoy((StageObject) this.targetObjectForEnemy);
  }

  protected override void OnEndCarry()
  {
    base.OnEndCarry();
    if (Object.op_Equality((Object) this.decoyEffect, (Object) null))
      this.decoyEffect = EffectManager.GetEffect(FieldCarriableDecoyGimmickObject.kDecoyEffectName, this.GetTransform());
    MonoBehaviourSingleton<StageObjectManager>.I.SetAllEnemiesTargetDecoy();
    EffectManager.OneShot(FieldCarriableDecoyGimmickObject.kPutEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableDecoyGimmickObject.kPutSEId, this.GetTransform().position);
  }

  private void LateUpdate()
  {
    if (!this.isActive)
      return;
    this.activeTime = Mathf.Max(0.0f, this.activeTime - Time.deltaTime);
    this.UpdateGauge();
    if ((double) this.activeTime > 0.0)
      return;
    this.OnActiveEnd();
  }

  private void UpdateGauge()
  {
    float num = (float) -((double) this.activeTime / (double) this.maxActiveTime);
    if (!Object.op_Inequality((Object) this.gaugeMat, (Object) null))
      return;
    this.gaugeMat.SetTextureOffset("_MainTex", new Vector2(0.0f, num + FieldCarriableDecoyGimmickObject.kRenderOffSet));
  }

  protected virtual void OnActiveEnd()
  {
    ((Component) this).gameObject.SetActive(false);
    EffectManager.OneShot(FieldCarriableDecoyGimmickObject.kBreakEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableDecoyGimmickObject.kBreakSEId, this.GetTransform().position);
    if (!Object.op_Inequality((Object) this.decoyEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.decoyEffect).gameObject);
    this.decoyEffect = (Transform) null;
  }
}
