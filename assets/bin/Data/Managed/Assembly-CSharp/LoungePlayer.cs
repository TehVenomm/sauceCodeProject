// Decompiled with JetBrains decompiler
// Type: LoungePlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using UnityEngine;

#nullable disable
public class LoungePlayer : HomePlayerCharacterBase
{
  protected const float NeedMovingDistance = 0.5f;
  protected const float AFKTime = 150f;
  protected Vector3 moveTargetPoint;
  protected bool isMoving;
  protected float sitTimer;
  protected LoungeNamePlateStatus namePlateStatus;
  protected float afkTimer;
  protected bool isInitPos;

  public CharaInfo LoungeCharaInfo { get; private set; }

  public override int GetUserId() => this.LoungeCharaInfo == null ? 0 : this.LoungeCharaInfo.userId;

  public Vector3? InitialPos { get; private set; }

  public void SetLoungeCharaInfo(CharaInfo info) => this.LoungeCharaInfo = info;

  public void SetInitialPosition(Vector3 pos, LOUNGE_ACTION_TYPE type)
  {
    this.InitialPos = new Vector3?(pos);
    this._transform.position = this.InitialPos.Value;
    this.isInitPos = true;
    this.moveTargetPos = this.InitialPos.Value;
    if (type != LOUNGE_ACTION_TYPE.SIT || this.isSitting)
      return;
    this.OnRecvSit();
  }

  public void SetMoveTargetPosition(Vector3 pos)
  {
    if (this.isPlayingSitAnimation)
      return;
    this.moveTargetPos = pos;
  }

  public void ResetAction() => this.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;

  public void ResetAFKTimer() => this.afkTimer = 0.0f;

  private void Update()
  {
    this.afkTimer += Time.deltaTime;
    if ((double) this.afkTimer > 150.0)
    {
      if (this.CurrentActionType == LOUNGE_ACTION_TYPE.NONE || this.CurrentActionType == LOUNGE_ACTION_TYPE.SIT)
        this.CurrentActionType = LOUNGE_ACTION_TYPE.AFK;
      else
        this.ResetAFKTimer();
    }
    if (!this.IsValidMove() || this.isSitting)
      return;
    if (!this.isSitting && Object.op_Inequality((Object) this.animCtrl, (Object) null) && !this.isStanding)
      this.animCtrl.PlayDefault();
    if ((double) Vector3.Distance(this._transform.position, this.moveTargetPos) <= 0.5)
      return;
    this.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
    this.isMoving = true;
    this.isSitting = false;
    this.StartCoroutine(this.Move());
  }

  protected override void CreateNamePlate()
  {
    if (this.LoungeCharaInfo == null)
      return;
    this.namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateLoungeNamePlate(this.LoungeCharaInfo.name);
    this.namePlateStatus = ((Component) this.namePlate).gameObject.AddComponent<LoungeNamePlateStatus>();
    this.namePlateStatus.SetPlayer(this);
  }

  protected override void UpdateNamePlatePos()
  {
    if (Object.op_Equality((Object) this.namePlate, (Object) null))
      return;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(!Object.op_Inequality((Object) this.Head, (Object) null) ? Vector3.op_Addition(this._transform.position, new Vector3(0.0f, 1.9f, 0.0f)) : Vector3.op_Addition(this.Head.position, new Vector3(0.0f, 0.4f, 0.0f))));
    if ((double) worldPoint.z >= 0.0)
    {
      worldPoint.z = 0.0f;
      this.namePlateStatus.SetActiveNamePlate(GameSaveData.instance.headName);
      this.namePlate.position = worldPoint;
    }
    else
      ((Component) this.namePlate).gameObject.SetActive(false);
  }

  protected override IEnumerator DoSit()
  {
    while (this.isMoving)
    {
      this.sitTimer += Time.deltaTime;
      if (10.0 < (double) this.sitTimer)
        yield break;
      yield return (object) null;
    }
    while (!this.isInitPos)
      yield return (object) null;
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      this.chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this._transform.position);
    if (MonoBehaviourSingleton<ClanManager>.IsValid())
      this.chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(this._transform.position);
    yield return (object) this.StartCoroutine(base.DoSit());
  }

  protected override IEnumerator StandUp()
  {
    while (!this.isInitPos)
      yield return (object) null;
    yield return (object) this.StartCoroutine(base.StandUp());
  }

  protected override void InitAnim()
  {
    base.InitAnim();
    this.animCtrl.moveAnim = this.sexType == 0 ? PLCA.RUN : PLCA.RUN_F;
    this.animCtrl.transitionDuration = 0.15f;
    this.animCtrl.animator.speed = 1f;
  }

  protected virtual IEnumerator Move()
  {
    this.isInitPos = true;
    this.isMoving = true;
    while (true)
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.moveTargetPos, this._transform.position);
      Vector2 vector2Xz = vector3.ToVector2XZ();
      Quaternion quaternion = Quaternion.LookRotation(((Vector2) ref vector2Xz).normalized.ToVector3XZ());
      float y = ((Quaternion) ref quaternion).eulerAngles.y;
      float num = 0.0f;
      this._transform.eulerAngles = new Vector3(0.0f, Mathf.SmoothDampAngle(this._transform.eulerAngles.y, y, ref num, 0.1f), 0.0f);
      if ((double) ((Vector3) ref vector3).magnitude < 1.0)
        this.animCtrl.Play(PLCA.WALK);
      else
        this.animCtrl.PlayMove();
      if ((double) ((Vector3) ref vector3).magnitude >= 0.5)
        yield return (object) null;
      else
        break;
    }
    this.isMoving = false;
  }

  protected override ModelLoaderBase LoadModel()
  {
    return (ModelLoaderBase) this.Load(this, ((Component) this).gameObject, this.LoungeCharaInfo, (PlayerLoader.OnCompleteLoad) null);
  }

  protected virtual PlayerLoader Load(
    LoungePlayer chara,
    GameObject go,
    CharaInfo chara_info,
    PlayerLoader.OnCompleteLoad callback)
  {
    PlayerLoader playerLoader = go.AddComponent<PlayerLoader>();
    PlayerLoadInfo player_load_info = new PlayerLoadInfo();
    if (chara_info != null)
    {
      player_load_info.Apply(chara_info, false, true, true, true);
      chara.sexType = chara_info.sex;
    }
    playerLoader.StartLoad(player_load_info, 8, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, callback);
    return playerLoader;
  }

  public virtual void OnRecvSit()
  {
    this.isSitting = true;
    this.StartCoroutine(this.DoSit());
    this.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
  }

  public virtual void OnRecvStandUp()
  {
    this.isSitting = false;
    this.StartCoroutine(this.StandUp());
    if (Object.op_Inequality((Object) this.chairPoint, (Object) null))
      this.chairPoint.ResetSittingCharacter();
    this.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
  }

  public void OnRecvToGacha() => this.CurrentActionType = LOUNGE_ACTION_TYPE.TO_GACHA;

  public void OnRecvToEquip() => this.CurrentActionType = LOUNGE_ACTION_TYPE.TO_EQUIP;

  public void OnRecvAFK() => this.CurrentActionType = LOUNGE_ACTION_TYPE.AFK;

  public void OnRecvNone() => this.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;

  public override bool DispatchEvent()
  {
    if (this.LoungeCharaInfo == null || MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(this.LoungeCharaInfo.userId) == null)
      return false;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("LoungePlayerCharacter", ((Component) this).gameObject, "LOUNGE_FRIEND", (object) this.LoungeCharaInfo);
    return true;
  }

  protected virtual bool IsValidMove()
  {
    return !Object.op_Equality((Object) this.animCtrl, (Object) null) && !this.isMoving && !this.isPlayingSitAnimation && !this.isSit;
  }
}
