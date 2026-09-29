// Decompiled with JetBrains decompiler
// Type: GatherPointObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class GatherPointObject : MonoBehaviour
{
  protected Transform modelView;
  protected Transform gatherEffect;
  protected Transform targetEffect;
  protected Self self;
  public FieldGimmickObject gimmick;

  public Transform _transform { get; private set; }

  public FieldMapTable.GatherPointTableData pointData { get; protected set; }

  public FieldMapTable.GatherPointViewTableData viewData { get; protected set; }

  public bool isGathered { get; protected set; }

  public static T Create<T>(FieldMapTable.GatherPointTableData point_data, Transform parent) where T : GatherPointObject
  {
    Transform gameObject = Utility.CreateGameObject("GatherPoint", parent, 9);
    gameObject.position = new Vector3(point_data.pointX, 0.0f, point_data.pointZ);
    gameObject.rotation = Quaternion.AngleAxis(point_data.pointDir, Vector3.up);
    T obj = ((Component) gameObject).gameObject.AddComponent<T>();
    if (Object.op_Equality((Object) (object) obj, (Object) null))
      return default (T);
    obj.Initialize(point_data);
    return obj;
  }

  private void Awake() => this._transform = ((Component) this).transform;

  public virtual void Initialize(FieldMapTable.GatherPointTableData point_data)
  {
    this.pointData = point_data;
    this.viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(this.pointData.viewID);
    if (this.viewData == null)
    {
      Log.Error(LOG.INGAME, "GatherPointObject::Initialize() viewData is null. pointID = {0}, viewID = {1}", (object) this.pointData.pointID, (object) this.pointData.viewID);
    }
    else
    {
      if (this.viewData.viewID != 0U)
        this.modelView = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameProgress>.I.gatherPointModelTable.Get(this.viewData.viewID).loadedObject, this._transform);
      if (!string.IsNullOrEmpty(this.viewData.gatherEffectName))
        this.gatherEffect = EffectManager.GetEffect(this.viewData.gatherEffectName, this._transform);
      if ((double) this.viewData.colRadius > 0.0)
      {
        SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
        sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
        sphereCollider.radius = this.viewData.colRadius;
      }
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
        this.self = MonoBehaviourSingleton<StageObjectManager>.I.self;
      this.CheckGather();
    }
  }

  public virtual void CheckGather() => this.UpdateView();

  public virtual void Gather()
  {
  }

  public virtual void UpdateView()
  {
    if (Object.op_Inequality((Object) this.gatherEffect, (Object) null))
      ((Component) this.gatherEffect).gameObject.SetActive(!this.isGathered);
    if (!Object.op_Inequality((Object) this.modelView, (Object) null) || string.IsNullOrEmpty(this.viewData.modelHideNodeName))
      return;
    Transform transform = Utility.Find(this.modelView, this.viewData.modelHideNodeName);
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    ((Component) transform).gameObject.SetActive(!this.isGathered);
  }

  public virtual void UpdateTargetMarker(bool is_near)
  {
    if (is_near && Object.op_Inequality((Object) this.self, (Object) null) && this.self.IsChangeableAction((Character.ACTION_ID) 28))
    {
      if (Object.op_Equality((Object) this.targetEffect, (Object) null) && !string.IsNullOrEmpty(this.viewData.targetEffectName))
        this.targetEffect = EffectManager.GetEffect(this.viewData.targetEffectName, this._transform);
      if (!Object.op_Inequality((Object) this.targetEffect, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      this.targetEffect.Set(Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.viewData.targetEffectShift), Vector3.op_Multiply(Vector3.up, this.viewData.targetEffectHeight)), this._transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetEffect, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetEffect).gameObject);
    }
  }
}
