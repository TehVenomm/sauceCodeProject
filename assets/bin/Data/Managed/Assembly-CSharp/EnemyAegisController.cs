// Decompiled with JetBrains decompiler
// Type: EnemyAegisController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyAegisController : MonoBehaviour
{
  private const float kAngularVelocity = 50f;
  private EnemyAegisController.SetupParam _setupParam = new EnemyAegisController.SetupParam();
  private EnemyAegisController.SyncParam _syncParam = new EnemyAegisController.SyncParam();
  private List<EnemyAegisController.AegisParam> aegisParams = new List<EnemyAegisController.AegisParam>();
  private Enemy owner;
  private Transform cachedTransform;
  private List<Transform> effectParentList = new List<Transform>();
  private bool isNeedUpdate;
  private float nowAngle;

  public EnemyAegisController.SyncParam syncParam => this._syncParam;

  public void Init(Enemy enemy)
  {
    if (enemy == null)
      return;
    this.owner = enemy;
    this.cachedTransform = ((Component) this).transform;
    this.isNeedUpdate = false;
    this.nowAngle = 0.0f;
    this.cachedTransform.localRotation = Quaternion.identity;
  }

  public void Generate(AnimEventData.EventData data)
  {
    if (this.owner == null)
      return;
    this._setupParam.maxNum = data.intArgs[0];
    this._setupParam.maxHp = (int) ((double) data.intArgs[1] * 0.0099999997764825821 * (double) this.owner.hpMax);
    this._setupParam.generateSeId = data.intArgs.Length >= 3 ? data.intArgs[2] : 0;
    this._setupParam.breakSeId = data.intArgs.Length >= 4 ? data.intArgs[3] : 0;
    this._setupParam.allBreakSeId = data.intArgs.Length >= 5 ? data.intArgs[4] : 0;
    this._setupParam.offset = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    this._setupParam.scale = data.floatArgs[3];
    this._setupParam.effectName = data.stringArgs[0];
    this._setupParam.nodeName = data.stringArgs.Length > 1 ? data.stringArgs[1] : "";
    this._setupParam.nowNum = this._setupParam.maxNum;
    this._setupParam.nowHp = this._setupParam.maxHp;
    this.Setup(this._setupParam, true);
  }

  public void Setup(EnemyAegisController.SetupParam param, bool announce)
  {
    this._setupParam = param;
    if (this._setupParam.maxNum <= 0)
      return;
    for (int index = 0; index < this.aegisParams.Count; ++index)
      this._ReleaseEffect(ref this.aegisParams[index].effect);
    this.aegisParams.Clear();
    this.cachedTransform.SetParent(this.owner.FindNode(this._setupParam.nodeName));
    this.cachedTransform.localPosition = Vector3.zero;
    this.cachedTransform.localScale = Vector3.one;
    float num1 = 360f / (float) this._setupParam.maxNum;
    float num2 = (float) (this._setupParam.maxNum - this._setupParam.nowNum);
    bool flag = true;
    for (int index = 0; index < this._setupParam.maxNum; ++index)
    {
      Transform parent;
      if (this.effectParentList.Count <= index)
      {
        parent = new GameObject("Child").transform;
        parent.SetParent(this.cachedTransform);
        parent.localPosition = Vector3.zero;
        parent.localScale = Vector3.one;
        parent.localRotation = Quaternion.identity;
        this.effectParentList.Add(parent);
      }
      else
        parent = this.effectParentList[index];
      parent.localRotation = Quaternion.AngleAxis(num1 * (float) index, Vector3.up);
      if ((double) index >= (double) num2)
      {
        EnemyAegisController.AegisParam aegisParam = new EnemyAegisController.AegisParam()
        {
          hp = flag ? this._setupParam.nowHp : this._setupParam.maxHp,
          effect = EffectManager.GetEffect(this._setupParam.effectName, parent)
        };
        aegisParam.effect.localPosition = this._setupParam.offset;
        aegisParam.effect.localScale = new Vector3(this._setupParam.scale, this._setupParam.scale, this._setupParam.scale);
        aegisParam.effect.localRotation = Quaternion.identity;
        this.aegisParams.Add(aegisParam);
        flag = false;
      }
    }
    this.isNeedUpdate = true;
    this._syncParam.nowNum = this._setupParam.nowNum;
    this._syncParam.nowHp = this._setupParam.nowHp;
    this._UpdateGaugeUI();
    if (!announce || !MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 2U);
    if (this._setupParam.generateSeId <= 0 || !MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.PlayOneShotSE(this._setupParam.generateSeId);
  }

  public EnemyAegisController.SetupParam GetSetupParam()
  {
    this._setupParam.nowNum = this._syncParam.nowNum;
    this._setupParam.nowHp = this._syncParam.nowHp;
    return this._setupParam;
  }

  public bool Damage(int damage)
  {
    if (!this.IsValid() || damage <= 0)
      return false;
    EnemyAegisController.AegisParam aegisParam = this.aegisParams[0];
    aegisParam.hp -= damage;
    if (aegisParam.hp <= 0)
    {
      this._ReleaseEffect(ref aegisParam.effect);
      this.aegisParams.RemoveAt(0);
      this._syncParam.nowNum = this.aegisParams.Count;
      this._syncParam.nowHp = this._syncParam.nowNum == 0 ? 0 : this.aegisParams[0].hp;
      this._UpdateGaugeUI();
      if (MonoBehaviourSingleton<SoundManager>.IsValid())
      {
        if (this._setupParam.allBreakSeId > 0 && !this.IsValid())
          SoundManager.PlayOneShotSE(this._setupParam.allBreakSeId);
        else if (this._setupParam.breakSeId > 0)
          SoundManager.PlayOneShotSE(this._setupParam.breakSeId);
      }
    }
    else
      this._syncParam.nowHp = aegisParam.hp;
    this._syncParam.isChange = true;
    if (!this.IsValid())
    {
      this.isNeedUpdate = false;
      if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
        MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 3U);
    }
    return true;
  }

  public void Sync(EnemyAegisController.SyncParam sync)
  {
    if (!sync.isChange || this._syncParam.Equal(sync))
      return;
    int num = this._syncParam.nowNum - sync.nowNum;
    this._syncParam.isChange = false;
    this._syncParam.nowNum = sync.nowNum;
    this._syncParam.nowHp = sync.nowHp;
    for (int index = 0; index < num; ++index)
    {
      if (this.aegisParams.Count > 0)
      {
        this._ReleaseEffect(ref this.aegisParams[0].effect);
        this.aegisParams.RemoveAt(0);
        this._UpdateGaugeUI();
        if (MonoBehaviourSingleton<SoundManager>.IsValid())
        {
          if (this._setupParam.allBreakSeId > 0 && !this.IsValid())
            SoundManager.PlayOneShotSE(this._setupParam.allBreakSeId);
          else if (this._setupParam.breakSeId > 0)
            SoundManager.PlayOneShotSE(this._setupParam.breakSeId);
        }
      }
    }
    if (this.aegisParams.Count > 0)
    {
      this.aegisParams[0].hp = sync.nowHp;
    }
    else
    {
      this.isNeedUpdate = false;
      if (!MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
        return;
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 3U);
    }
  }

  public bool IsValid() => this.aegisParams.Count > 0;

  public void FlagReset() => this._syncParam.isChange = false;

  public float GetPercent()
  {
    return this._setupParam.maxNum <= 0 || this._syncParam.nowNum == 0 ? 0.0f : (float) this._syncParam.nowNum / (float) this._setupParam.maxNum;
  }

  private void Update()
  {
    if (!this.isNeedUpdate)
      return;
    this.nowAngle += 50f * Time.deltaTime;
    if ((double) this.nowAngle >= 360.0)
      this.nowAngle -= 360f;
    this.cachedTransform.localRotation = Quaternion.AngleAxis(this.nowAngle, Vector3.up);
  }

  private void OnDestroy()
  {
    this._setupParam = (EnemyAegisController.SetupParam) null;
    this._syncParam = (EnemyAegisController.SyncParam) null;
    this.isNeedUpdate = false;
    for (int index = 0; index < this.aegisParams.Count; ++index)
      this._ReleaseEffect(ref this.aegisParams[index].effect);
    this.aegisParams.Clear();
    this.aegisParams = (List<EnemyAegisController.AegisParam>) null;
    if (this.effectParentList != null)
    {
      for (int index = 0; index < this.effectParentList.Count; ++index)
        Object.Destroy((Object) ((Component) this.effectParentList[index]).gameObject);
      this.effectParentList.Clear();
    }
    this.effectParentList = (List<Transform>) null;
  }

  private void _UpdateGaugeUI()
  {
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid() || this._setupParam.maxNum <= 0)
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.SetAegisBarPercent(this.GetPercent());
  }

  private void _ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid() || t == null)
      return;
    EffectManager.ReleaseEffect(((Component) t).gameObject, isPlayEndAnimation);
    t = (Transform) null;
  }

  public class SetupParam
  {
    public int maxNum;
    public int maxHp;
    public int generateSeId;
    public int breakSeId;
    public int allBreakSeId;
    public Vector3 offset;
    public float scale;
    public string effectName = "";
    public string nodeName = "";
    public int nowNum;
    public int nowHp;
  }

  public class SyncParam
  {
    public bool isChange;
    public int nowNum;
    public int nowHp;

    public void Copy(EnemyAegisController.SyncParam sync)
    {
      this.isChange = sync.isChange;
      this.nowNum = sync.nowNum;
      this.nowHp = sync.nowHp;
    }

    public bool Equal(EnemyAegisController.SyncParam s)
    {
      return this.nowNum == s.nowNum && this.nowHp == s.nowHp;
    }
  }

  public class AegisParam
  {
    public int hp;
    public Transform effect;
  }
}
