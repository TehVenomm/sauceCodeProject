// Decompiled with JetBrains decompiler
// Type: FieldDropObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldDropObject : MonoBehaviour, IAnimEvent
{
  private static int startAnimHash = -1;
  private static int endAnimHash = -1;
  private static int openAnimHash = -1;
  protected InGameSettingsManager.FieldDropItem parameter;
  protected List<UIDropAnnounce.DropAnnounceInfo> announceInfo = new List<UIDropAnnounce.DropAnnounceInfo>();
  protected List<InGameManager.DropItemInfo> itemInfo;
  protected List<InGameManager.DropDeliveryInfo> deliveryInfo;
  protected StageObject targetObject;
  protected FloatInterpolator distanceAnim = new FloatInterpolator();
  protected FloatInterpolator speedAnim = new FloatInterpolator();
  protected FloatInterpolator scaleAnim = new FloatInterpolator();
  protected FieldDropObject.AnimationStep animationStep;
  protected float moveTime;
  protected float distance;
  protected bool isGet;
  protected bool isDelete;
  protected bool isRare;
  protected AnimEventProcessor animEventProcessor;
  private bool isOpend;
  private Transform effect;
  private float animationTimer;
  private static readonly float MOVE_TO_TARGET_TIME = 0.5f;
  private Vector3 dropPos;
  private Vector3 targetPos;
  private Vector3 prefabDefaultScale = Vector3.one;

  public static FieldDropObject Create(
    Coop_Model_EnemyDefeat model,
    List<InGameManager.DropDeliveryInfo> deliveryList,
    List<InGameManager.DropItemInfo> itemList)
  {
    if (itemList.Count <= 0 && deliveryList.Count <= 0)
      return (FieldDropObject) null;
    UIDropAnnounce.COLOR color = FieldDropObject.GetColor(model, deliveryList);
    return FieldDropObject.CreateTreasureBox(model, deliveryList, itemList, color);
  }

  private static UIDropAnnounce.COLOR GetColor(
    Coop_Model_EnemyDefeat model,
    List<InGameManager.DropDeliveryInfo> deliveryList)
  {
    UIDropAnnounce.COLOR color = UIDropAnnounce.COLOR.NORMAL;
    if (model.dropLoungeShare)
      return UIDropAnnounce.COLOR.LOUNGE;
    switch (model.boxType)
    {
      case 1:
        return UIDropAnnounce.COLOR.SP_N;
      case 2:
        return UIDropAnnounce.COLOR.SP_HN;
      case 3:
        return UIDropAnnounce.COLOR.SP_R;
      case 4:
        return UIDropAnnounce.COLOR.HALLOWEEN;
      case 5:
        return UIDropAnnounce.COLOR.ESP_N;
      case 6:
        return UIDropAnnounce.COLOR.ESP_HN;
      case 7:
        return UIDropAnnounce.COLOR.ESP_R;
      case 8:
        return UIDropAnnounce.COLOR.SEASONAL;
      default:
        int index = 0;
        for (int count = model.dropIds.Count; index < count; ++index)
        {
          if (color == UIDropAnnounce.COLOR.NORMAL)
          {
            switch ((REWARD_TYPE) model.dropTypes[index])
            {
              case REWARD_TYPE.EQUIP_ITEM:
                EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) model.dropItemIds[index]);
                if (equipItemData != null && GameDefine.IsRare(equipItemData.rarity))
                {
                  color = UIDropAnnounce.COLOR.RARE;
                  continue;
                }
                continue;
              case REWARD_TYPE.SKILL_ITEM:
                color = UIDropAnnounce.COLOR.RARE;
                continue;
              case REWARD_TYPE.ACCESSORY:
                AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint) model.dropItemIds[index]);
                if (data != null && GameDefine.IsRare(data.rarity))
                {
                  color = UIDropAnnounce.COLOR.RARE;
                  continue;
                }
                continue;
              default:
                ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) model.dropItemIds[index]);
                if (itemData != null && GameDefine.IsRare(itemData.rarity))
                {
                  color = UIDropAnnounce.COLOR.RARE;
                  continue;
                }
                continue;
            }
          }
        }
        if (deliveryList.Count > 0 && color == UIDropAnnounce.COLOR.NORMAL)
          color = UIDropAnnounce.COLOR.DELIVERY;
        return color;
    }
  }

  public static FieldDropObject CreateTreasureBox(
    Coop_Model_EnemyDefeat model,
    List<InGameManager.DropDeliveryInfo> deliveryList,
    List<InGameManager.DropItemInfo> itemList,
    UIDropAnnounce.COLOR color)
  {
    GameObject treasureBox1 = MonoBehaviourSingleton<InGameManager>.I.CreateTreasureBox(color);
    FieldDropObject treasureBox2 = treasureBox1.GetComponent<FieldDropObject>();
    if (Object.op_Equality((Object) treasureBox2, (Object) null))
      treasureBox2 = treasureBox1.AddComponent<FieldDropObject>();
    treasureBox2.itemInfo = itemList;
    treasureBox2.deliveryInfo = deliveryList;
    treasureBox2.rewardId = model.rewardId;
    treasureBox2.isRare = color == UIDropAnnounce.COLOR.RARE;
    Vector3 zero = Vector3.zero;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      InGameSettingsManager.FieldDropItem fieldDrop = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
      float num1 = Random.value;
      float num2 = Random.value;
      float num3 = Random.value;
      float num4 = (double) Random.value > 0.5 ? -1f : 1f;
      float num5 = (double) Random.value > 0.5 ? -1f : 1f;
      // ISSUE: explicit constructor call
      ((Vector3) ref zero).\u002Ector(Mathf.Lerp(fieldDrop.offsetMin.x, fieldDrop.offsetMax.x, num1) * num4, Mathf.Lerp(fieldDrop.offsetMin.y, fieldDrop.offsetMax.y, num2), Mathf.Lerp(fieldDrop.offsetMin.z, fieldDrop.offsetMax.z, num3) * num5);
    }
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector((float) model.x, 0.0f, (float) model.z);
    Vector3 vector3_2 = Vector3.op_Addition(vector3_1, zero);
    int obstacleMask = AIUtility.GetObstacleMask();
    RaycastHit hit = new RaycastHit();
    if (AIUtility.RaycastForTargetPos(vector3_1, vector3_2, obstacleMask, out hit))
    {
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_2).\u002Ector(((RaycastHit) ref hit).point.x, vector3_2.y, ((RaycastHit) ref hit).point.z);
    }
    treasureBox2.Drop(vector3_1, vector3_2);
    return treasureBox2;
  }

  public Transform _transform { get; protected set; }

  public Animator animator { get; protected set; }

  public int rewardId { get; protected set; }

  public TargetPoint targetPoint { set; get; }

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop;
    this.animator = ((Component) this).gameObject.GetComponentInChildren<Animator>();
    if (FieldDropObject.startAnimHash == -1)
      FieldDropObject.startAnimHash = Animator.StringToHash("Base Layer.Pop");
    if (FieldDropObject.endAnimHash == -1)
      FieldDropObject.endAnimHash = Animator.StringToHash("Base Layer.Idle");
    if (FieldDropObject.openAnimHash == -1)
      FieldDropObject.openAnimHash = Animator.StringToHash("Base Layer.Open");
    if (!Object.op_Inequality((Object) this.parameter.animEventData, (Object) null) || !Object.op_Inequality((Object) this.animator, (Object) null))
      return;
    this.animEventProcessor = new AnimEventProcessor(this.parameter.animEventData, this.animator, (IAnimEvent) this);
  }

  private void OnEnable()
  {
    if (!this.isDelete)
      this.prefabDefaultScale = this._transform.localScale;
    this._transform.localScale = this.prefabDefaultScale;
    this.isDelete = false;
  }

  private void OnDisable()
  {
    if (this.isGet && MonoBehaviourSingleton<UIDropAnnounce>.IsValid())
    {
      int index = 0;
      for (int count = this.announceInfo.Count; index < count; ++index)
        MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(this.announceInfo[index]);
    }
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameManager>.I.DeleteDropObject(this);
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (!this.targetObject.isInitialized || this.isOpend || this.animationStep == FieldDropObject.AnimationStep.MOVE_TO_TARGET_POS || this.animationStep == FieldDropObject.AnimationStep.OPEN || !this.IsSelfAttack(((Component) collider).gameObject))
      return;
    this.OpenDropObject();
  }

  public bool IsSelfAttack(GameObject obj)
  {
    IAttackCollider component = obj.GetComponent<IAttackCollider>();
    if (component == null || component is HealAttackObject)
      return false;
    StageObject fromObject = component.GetFromObject();
    return fromObject != null && fromObject is Self;
  }

  public void OpenDropObject()
  {
    if (this.isOpend)
      return;
    this.isOpend = true;
    SoundManager.PlayOneShotUISE(10000071);
    if (this.animEventProcessor != null)
      this.animEventProcessor.CrossFade(FieldDropObject.openAnimHash, 0.0f);
    this.effect = EffectManager.GetEffect("ef_btl_treasurebox_01");
    this.effect.position = this._transform.position;
    rymFX component = ((Component) this.effect).GetComponent<rymFX>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      component.AutoDelete = true;
      component.LoopEnd = true;
    }
    MonoBehaviourSingleton<CoopNetworkManager>.I.RewardGet(this.rewardId);
    if (this.deliveryInfo != null && this.deliveryInfo.Count > 0)
      SoundManager.PlayOneShotUISE(40000155);
    else if (this.isRare)
      SoundManager.PlayOneShotUISE(40000154);
    else
      SoundManager.PlayOneShotUISE(10000064);
    this.animationStep = FieldDropObject.AnimationStep.OPEN;
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.targetObject, (Object) null))
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      if (this.animEventProcessor != null)
        this.animEventProcessor.Update();
      switch (this.animationStep)
      {
        case FieldDropObject.AnimationStep.MOVE_TO_TARGET_POS:
          this.animationTimer += Time.deltaTime;
          Vector3 vector3_1 = Vector3.Lerp(this.dropPos, this.targetPos, this.animationTimer / FieldDropObject.MOVE_TO_TARGET_TIME);
          // ISSUE: explicit constructor call
          ((Vector3) ref vector3_1).\u002Ector(vector3_1.x, this._transform.position.y, vector3_1.z);
          this._transform.position = vector3_1;
          if ((double) this.animationTimer < (double) FieldDropObject.MOVE_TO_TARGET_TIME)
            break;
          this.animationStep = FieldDropObject.AnimationStep.DROP_TO_GROUND;
          break;
        case FieldDropObject.AnimationStep.DROP_TO_GROUND:
          AnimatorStateInfo animatorStateInfo1 = this.animator.GetCurrentAnimatorStateInfo(0);
          if (((AnimatorStateInfo) ref animatorStateInfo1).fullPathHash == FieldDropObject.endAnimHash)
          {
            this.targetPoint = ((Component) this).GetComponent<TargetPoint>();
            this.animationStep = FieldDropObject.AnimationStep.NONE;
          }
          if (this.isRare)
          {
            SoundManager.PlayOneShotUISE(10000061);
            break;
          }
          SoundManager.PlayOneShotUISE(10000062);
          break;
        case FieldDropObject.AnimationStep.OPEN:
          AnimatorStateInfo animatorStateInfo2 = this.animator.GetCurrentAnimatorStateInfo(0);
          if (((AnimatorStateInfo) ref animatorStateInfo2).fullPathHash != FieldDropObject.openAnimHash || (double) ((AnimatorStateInfo) ref animatorStateInfo2).normalizedTime <= 0.99000000953674316)
            break;
          this.animationStep = FieldDropObject.AnimationStep.NONE;
          if (Object.op_Inequality((Object) this.effect, (Object) null))
            EffectManager.ReleaseEffect(((Component) this.effect).gameObject);
          ((Component) this).gameObject.SetActive(false);
          break;
        case FieldDropObject.AnimationStep.GET:
          if (this.distanceAnim.IsPlaying())
          {
            this.moveTime += Time.deltaTime;
            Bounds bounds = this.targetObject._collider.bounds;
            Vector3 center = ((Bounds) ref bounds).center;
            Vector3 vector3_2 = Vector3.op_Subtraction(this._transform.position, center);
            float magnitude = ((Vector3) ref vector3_2).magnitude;
            if ((double) this.distance < (double) magnitude)
              this.distance = magnitude;
            Vector3 vector3_3 = Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, this.distance), 1f - this.distanceAnim.Update());
            Vector3 vector3_4 = Quaternion.op_Multiply(Quaternion.AngleAxis(this.moveTime * this.speedAnim.Update(), Vector3.up), vector3_3);
            this._transform.position = Vector3.op_Addition(center, vector3_4);
            Vector3 vector3_5 = Vector3.op_Multiply(Vector3.one, this.scaleAnim.Update());
            if (!this.distanceAnim.IsPlaying())
              break;
            this._transform.localScale = vector3_5;
            break;
          }
          Transform effect = EffectManager.GetEffect("ef_btl_mpdrop_01");
          effect.position = this._transform.position;
          rymFX component = ((Component) effect).GetComponent<rymFX>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.AutoDelete = true;
            component.LoopEnd = true;
          }
          ((Component) this).gameObject.SetActive(false);
          break;
      }
    }
  }

  public virtual void Drop(Vector3 _dropPos, Vector3 _targetPos)
  {
    this.isOpend = false;
    this.animationTimer = 0.0f;
    this.animationStep = FieldDropObject.AnimationStep.MOVE_TO_TARGET_POS;
    this.targetObject = (StageObject) MonoBehaviourSingleton<StageObjectManager>.I.self;
    this.dropPos = _dropPos;
    this._transform.position = _dropPos;
    this.targetPos = _targetPos;
    if (this.animEventProcessor == null)
      return;
    this.animEventProcessor.CrossFade(FieldDropObject.startAnimHash, 0.0f);
  }

  public void Delete(bool is_get)
  {
    if (this.isDelete)
      return;
    this.isDelete = true;
    this.isGet = is_get;
    if (is_get)
    {
      this.targetObject = (StageObject) MonoBehaviourSingleton<StageObjectManager>.I.self;
      Bounds bounds = this.targetObject._collider.bounds;
      this.distance = Vector3.Distance(((Bounds) ref bounds).center, this._transform.position);
      this.distanceAnim.Set(this.parameter.getAnimTime, 0.0f, 1f, this.parameter.distanceAnim, 0.0f, (AnimationCurve) null);
      this.distanceAnim.Play();
      this.speedAnim.Set(this.parameter.getAnimTime, 0.0f, this.parameter.rotateSpeed, this.parameter.rotateSpeedAnim, 0.0f, (AnimationCurve) null);
      this.speedAnim.Play();
      this.scaleAnim.Set(this.parameter.getAnimTime, 0.0f, 1f, this.parameter.scaleAnim, 0.0f, (AnimationCurve) null);
      this.scaleAnim.Play();
      this.moveTime = 0.0f;
      this.animationStep = FieldDropObject.AnimationStep.OPEN;
      this.announceInfo = MonoBehaviourSingleton<InGameManager>.I.CreateDropAnnounceInfoList(this.deliveryInfo, this.itemInfo, true);
    }
    else
      ((Component) this).gameObject.SetActive(false);
  }

  public void OnAnimEvent(AnimEventData.EventData data)
  {
    if (data.id != AnimEventFormat.ID.SE_ONESHOT)
      return;
    int intArg = data.intArgs[0];
    if (intArg == 0)
      return;
    SoundManager.PlayOneShotUISE(intArg);
  }

  protected enum AnimationStep
  {
    NONE,
    MOVE_TO_TARGET_POS,
    DROP_TO_GROUND,
    OPEN,
    GET,
  }
}
