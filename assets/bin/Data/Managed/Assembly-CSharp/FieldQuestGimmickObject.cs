// Decompiled with JetBrains decompiler
// Type: FieldQuestGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldQuestGimmickObject : FieldGatherGimmickObject
{
  private bool isUsing;
  private QuestTable.QuestTableData questData;
  private const int kGatherModelIndex = 10916;
  private uint gvid;

  public FieldMapTable.GatherPointViewTableData viewData { get; private set; }

  public static bool IsValidParam(string value2)
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || value2.IsNullOrWhiteSpace())
      return false;
    uint result1 = 0;
    uint result2 = 0;
    uint result3 = 0;
    uint result4 = 0;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "sdid":
            uint.TryParse(strArray[1], out result1);
            continue;
          case "edid":
            uint.TryParse(strArray[1], out result2);
            continue;
          case "gvid":
            uint.TryParse(strArray[1], out result4);
            continue;
          case "qid":
            uint.TryParse(strArray[1], out result3);
            continue;
          default:
            continue;
        }
      }
    }
    return result4 != 0U && (result2 == 0U || !MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result2)) && (result1 == 0U || MonoBehaviourSingleton<DeliveryManager>.I.IsAppearDelivery(result1)) && !MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int) result1) && Singleton<QuestTable>.I.GetQuestData(result3) != null;
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "gvid":
            if (uint.TryParse(strArray[1], out this.gvid))
            {
              this.viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(this.gvid);
              continue;
            }
            continue;
          case "qid":
            uint result;
            if (uint.TryParse(strArray[1], out result))
            {
              this.questData = Singleton<QuestTable>.I.GetQuestData(result);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
  }

  public override bool CanUse() => !this.isUsing;

  public bool StartAction(Player player)
  {
    if (!this.IsValid())
      return false;
    this.OnUseStart(player);
    return true;
  }

  public void OnUseStart(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null) || !(player is Self))
      return;
    this.isUsing = true;
    player.playerSender.OnActQuestGimmick(this.m_id);
  }

  protected override bool IsValid() => !this.isUsing && base.IsValid();

  public void OnEndAction()
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.OpenAllDropObject();
    MonoBehaviourSingleton<InGameProgress>.I.GimmickQuestDirection(this.questData.questID);
  }

  public override string GetObjectName() => "QuestGimmick";

  public override string GetMarkerName()
  {
    return this.viewData != null ? this.viewData.targetEffectName : "ef_btl_target_unknown_01";
  }

  public static FieldMapTable.GatherPointViewTableData GetGatherPointData(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return (FieldMapTable.GatherPointViewTableData) null;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      uint result;
      if (strArray != null && strArray.Length == 2 && strArray[0] == "gvid" && uint.TryParse(strArray[1], out result))
        return Singleton<FieldMapTable>.I.GetGatherPointViewData(result);
    }
    return (FieldMapTable.GatherPointViewTableData) null;
  }

  protected override void CreateModel()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldGimmickObject.ConvertModelIndexToKey(this.m_gimmickType, (int) this.gvid));
    if (loadObject != null)
      this.modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.m_transform);
    if (!Object.op_Inequality((Object) this.modelTrans, (Object) null) || this.viewData == null)
      return;
    if (!string.IsNullOrEmpty(this.viewData.gatherEffectName))
    {
      Transform effect = EffectManager.GetEffect(this.viewData.gatherEffectName, this._transform);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        ((Component) effect).gameObject.SetActive(true);
        Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
        Vector3 position = cameraTransform.position;
        Quaternion rotation = cameraTransform.rotation;
        Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
        Vector3 pos = Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.viewData.targetEffectShift), Vector3.op_Multiply(Vector3.up, this.viewData.targetEffectHeight)), this._transform.position);
        effect.Set(pos, rotation);
      }
    }
    this.sqlRadius = this.viewData.targetRadius * this.viewData.targetRadius;
    if ((double) this.viewData.colRadius <= 0.0)
      return;
    SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    sphereCollider.radius = this.viewData.colRadius;
  }
}
