// Decompiled with JetBrains decompiler
// Type: WaveMatchDropResource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WaveMatchDropResource
{
  public const string kCommonEffectName = "ef_btl_target_dropitem_01";
  private Dictionary<string, LoadObject> dropModelList;

  public void Clear()
  {
    if (this.dropModelList != null)
      this.dropModelList.Clear();
    this.dropModelList = (Dictionary<string, LoadObject>) null;
  }

  public void Cache(LoadingQueue loadQueue)
  {
    if (!Singleton<WaveMatchDropTable>.IsValid())
      return;
    List<WaveMatchDropTable.WaveMatchDropData> allData = Singleton<WaveMatchDropTable>.I.GetAllData();
    if (allData == null)
      return;
    this.dropModelList = new Dictionary<string, LoadObject>();
    this.dropModelList.Clear();
    List<string> stringList = new List<string>();
    List<int> intList = new List<int>();
    for (int index = 0; index < allData.Count; ++index)
    {
      WaveMatchDropTable.WaveMatchDropData waveMatchDropData = allData[index];
      if (!this.dropModelList.ContainsKey(waveMatchDropData.model))
        this.dropModelList.Add(waveMatchDropData.model, loadQueue.Load(RESOURCE_CATEGORY.STAGE_GIMMICK, waveMatchDropData.model));
      if (!waveMatchDropData.getEffect.IsNullOrWhiteSpace() && !stringList.Contains(waveMatchDropData.getEffect))
      {
        loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchDropData.getEffect);
        stringList.Add(waveMatchDropData.getEffect);
      }
      if (waveMatchDropData.getSE != 0 && !intList.Contains(waveMatchDropData.getSE))
      {
        loadQueue.CacheSE(waveMatchDropData.getSE);
        intList.Add(waveMatchDropData.getSE);
      }
    }
    loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_target_dropitem_01");
    stringList.Clear();
    intList.Clear();
  }

  public void Create(Coop_Model_WaveMatchDrop model)
  {
    if (!Singleton<WaveMatchDropTable>.IsValid() || this.dropModelList == null || model.fiIds == null || model.fiIds.Count == 0)
      return;
    for (int index = 0; index < model.fiIds.Count; ++index)
    {
      uint fiId = (uint) model.fiIds[index];
      WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(fiId);
      if (data != null && this.dropModelList.ContainsKey(data.model))
      {
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
        this.OnCreate(MonoBehaviourSingleton<StageObjectManager>.I.waveMatchDropObjIndex++, fiId, new Vector3((float) model.x, 0.0f, (float) model.z), zero, model.sec, true);
      }
    }
  }

  public void OnCreate(
    int manageId,
    uint dataId,
    Vector3 basePos,
    Vector3 offset,
    float sec,
    bool send = false)
  {
    WaveMatchDropTable.WaveMatchDropData data = Singleton<WaveMatchDropTable>.I.GetData(dataId);
    if (data == null || !this.dropModelList.ContainsKey(data.model))
      return;
    LoadObject dropModel = this.dropModelList[data.model];
    if (dropModel == null)
      return;
    Transform transform = ResourceUtility.Realizes(dropModel.loadedObject, MonoBehaviourSingleton<StageObjectManager>.I._transform);
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    WaveMatchDropObject waveMatchDropObject;
    switch (data.type)
    {
      case WAVEMATCH_ITEM_TYPE.HEAL_HP:
        waveMatchDropObject = (WaveMatchDropObject) ((Component) transform).gameObject.AddComponent<WaveMatchDropObjectHealHp>();
        break;
      case WAVEMATCH_ITEM_TYPE.HEAL_SKILL:
        waveMatchDropObject = (WaveMatchDropObject) ((Component) transform).gameObject.AddComponent<WaveMatchDropObjectHealSkill>();
        break;
      case WAVEMATCH_ITEM_TYPE.CLOCK:
        waveMatchDropObject = (WaveMatchDropObject) ((Component) transform).gameObject.AddComponent<WaveMatchDropObjectClock>();
        break;
      default:
        waveMatchDropObject = ((Component) transform).gameObject.AddComponent<WaveMatchDropObject>();
        break;
    }
    if (!Object.op_Equality((Object) waveMatchDropObject, (Object) null))
    {
      waveMatchDropObject.Initialize(manageId, basePos, offset, sec, data);
      if (!send)
        return;
      MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnCreateWaveMatchDropObject(manageId, dataId, basePos, offset, sec);
    }
  }
}
