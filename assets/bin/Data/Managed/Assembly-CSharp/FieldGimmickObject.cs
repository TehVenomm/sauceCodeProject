// Decompiled with JetBrains decompiler
// Type: FieldGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldGimmickObject : MonoBehaviour, IFieldGimmickObject
{
  protected int m_id;
  protected Transform m_transform;
  protected FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;
  protected Transform modelTrans;

  public FieldMapTable.FieldGimmickPointTableData m_pointData { get; protected set; }

  public static IFieldGimmickObject Create<T>(
    FieldMapTable.FieldGimmickPointTableData pointData,
    int layer,
    Transform parent)
    where T : MonoBehaviour
  {
    if (pointData == null)
      return (IFieldGimmickObject) null;
    Transform gameObject = Utility.CreateGameObject("GimmickObject", parent, layer);
    gameObject.position = new Vector3(pointData.pointX, 0.0f, pointData.pointZ);
    gameObject.rotation = Quaternion.AngleAxis(pointData.pointDir, Vector3.up);
    IFieldGimmickObject fieldGimmickObject = (object) ((Component) gameObject).gameObject.AddComponent<T>() as IFieldGimmickObject;
    fieldGimmickObject.SetTransform(gameObject);
    string objectName = fieldGimmickObject.GetObjectName();
    if (string.IsNullOrEmpty(objectName))
      return fieldGimmickObject;
    ((Object) gameObject).name = objectName;
    return fieldGimmickObject;
  }

  public static uint ConvertModelIndexToKey(
    FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE type,
    int index)
  {
    return (uint) (index * GameDefine.kShiftIndex + type);
  }

  public static void CacheResources(
    LoadingQueue loadQueue,
    FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE type,
    string[] effectNameList,
    int[] seIdList,
    int[] modelIndexes)
  {
    for (int index = 0; index < modelIndexes.Length; ++index)
    {
      int modelIndex = modelIndexes[index];
      uint key;
      string resource_name;
      switch (type)
      {
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
          key = FieldGimmickCannonField.ConvertModelIndexToKey(modelIndex);
          resource_name = FieldGimmickCannonField.ConvertModelIndexToName(modelIndex);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
          key = (uint) type;
          resource_name = FieldFishingGimmickObject.ConvertModelIndexToName(modelIndex);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
          key = FieldWaveTargetObject.ConvertModelIndexToKey(modelIndex);
          resource_name = FieldWaveTargetObject.ConvertModelIndexToName(modelIndex);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SUPPLY:
          key = FieldSupplyGimmickObject.ConvertModelIndexToKey(modelIndex);
          resource_name = FieldSupplyGimmickObject.ConvertModelIndexToName(modelIndex);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_EVOLVE_ITEM:
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_DECOY:
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BUFF_POINT:
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
          key = FieldGimmickObject.ConvertModelIndexToKey(type, modelIndex);
          resource_name = ResourceName.GetFieldGimmickModel(type, modelIndex);
          break;
        case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST:
          key = FieldGimmickObject.ConvertModelIndexToKey(type, modelIndex);
          resource_name = ResourceName.GetFieldGimmickModel(type, modelIndex);
          break;
        default:
          key = (uint) type;
          resource_name = ResourceName.GetFieldGimmickModel(type, modelIndex);
          break;
      }
      if (MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(key) == null && !string.IsNullOrEmpty(resource_name))
      {
        RESOURCE_CATEGORY category = RESOURCE_CATEGORY.STAGE_GIMMICK;
        if (type == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST)
          category = RESOURCE_CATEGORY.INGAME_GATHER_POINT;
        MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Add(key, loadQueue.Load(category, resource_name));
      }
    }
    foreach (string effectName in effectNameList)
      loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName);
    foreach (int seId in seIdList)
      loadQueue.CacheSE(seId);
  }

  public virtual void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    this.m_pointData = pointData;
    this.m_id = (int) this.m_pointData.pointID;
    this.m_gimmickType = this.m_pointData.gimmickType;
    this.ParseParam(pointData.value2);
    this.CreateModel();
  }

  protected virtual void ParseParam(string value2)
  {
  }

  protected virtual void CreateModel()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint) this.m_gimmickType);
    if (loadObject == null)
      return;
    this.modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.m_transform);
  }

  public virtual int GetId() => this.m_id;

  public virtual Transform GetTransform() => this.m_transform;

  public virtual void RequestDestroy() => Object.Destroy((Object) ((Component) this).gameObject);

  public virtual void OnNotify(object value)
  {
  }

  public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE GetGimmickType()
  {
    return this.m_gimmickType;
  }

  public virtual string GetObjectName() => string.Empty;

  public virtual void SetTransform(Transform trans) => this.m_transform = trans;

  public virtual float GetTargetRadius() => 2f;

  public virtual float GetTargetSqrRadius() => 4f;

  public virtual void UpdateTargetMarker(bool isNear)
  {
  }

  public virtual bool IsSearchableNearest() => true;

  protected virtual void Awake()
  {
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
    SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    sphereCollider.radius = 1.5f;
    ((Collider) sphereCollider).isTrigger = true;
  }
}
