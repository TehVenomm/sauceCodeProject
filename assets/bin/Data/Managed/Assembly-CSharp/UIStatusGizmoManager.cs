// Decompiled with JetBrains decompiler
// Type: UIStatusGizmoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIStatusGizmoManager : MonoBehaviourSingleton<UIStatusGizmoManager>
{
  [SerializeField]
  protected GameObject playerStatusGizmo;
  [SerializeField]
  protected GameObject enemyStatusGizmo;
  [SerializeField]
  protected GameObject portalStatusGizmo;
  [SerializeField]
  protected GameObject cannonGizmo;
  [SerializeField]
  protected GameObject grabStatusGizmo;
  [SerializeField]
  protected GameObject sonarGizmo;
  [SerializeField]
  protected GameObject waveTargetGizmo;
  [SerializeField]
  protected GameObject chatGimmickGizmo;
  protected List<UIPlayerStatusGizmo> playerList = new List<UIPlayerStatusGizmo>();
  protected List<UIEnemyStatusGizmo> enemyList = new List<UIEnemyStatusGizmo>();
  protected List<UIPortalStatusGizmo> portalList = new List<UIPortalStatusGizmo>();
  protected List<UICannonGizmo> cannonList = new List<UICannonGizmo>();
  protected List<UIGrabStatusGizmo> grabList = new List<UIGrabStatusGizmo>();
  protected List<UISonarGizmo> sonarList = new List<UISonarGizmo>();
  protected List<UIWaveTargetGizmo> waveTargetList = new List<UIWaveTargetGizmo>();
  protected List<UIChatGimmickGizmo> chatGimmickList = new List<UIChatGimmickGizmo>();
  private int depth;

  private void LateUpdate()
  {
    if (!GameSaveData.instance.headName || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Vector3 vector3 = Vector3.zero;
    StageObject stageObject = (StageObject) null;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      vector3 = MonoBehaviourSingleton<StageObjectManager>.I.self.GetCameraTargetPos();
      vector3.y = 0.0f;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint, (Object) null))
        stageObject = MonoBehaviourSingleton<StageObjectManager>.I.self.targetingPoint.owner;
    }
    List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
    int index = 0;
    for (int count = enemyList.Count; index < count; ++index)
    {
      Enemy enemy = enemyList[index] as Enemy;
      if (!enemy.isDead && enemy.isInitialized)
      {
        float num = Vector3.Distance(vector3, enemyList[index]._position);
        if ((double) enemy.uiShowDistance > 0.0)
        {
          if ((double) num < (double) enemy.uiShowDistance || Object.op_Equality((Object) stageObject, (Object) enemy))
          {
            enemy.CreateStatusGizmo();
            break;
          }
          if ((double) num <= (double) enemy.uiShowDistance + 1.0)
            break;
          enemy.DeleteStatusGizmo();
          break;
        }
        if ((double) num < (double) enemy.enemyParameter.showStatusUIRange || Object.op_Equality((Object) stageObject, (Object) enemy))
          enemy.CreateStatusGizmo();
        else if ((double) num > (double) enemy.enemyParameter.showStatusUIRange + 1.0)
          enemy.DeleteStatusGizmo();
      }
    }
  }

  public UIPlayerStatusGizmo Create(Player owner)
  {
    UIPlayerStatusGizmo playerStatusGizmo = (UIPlayerStatusGizmo) null;
    int index = 0;
    for (int count = this.playerList.Count; index < count; ++index)
    {
      if (!((Component) this.playerList[index]).gameObject.activeSelf)
      {
        playerStatusGizmo = this.playerList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) playerStatusGizmo, (Object) null))
    {
      Transform transform = ResourceUtility.Realizes((Object) this.playerStatusGizmo, this._transform);
      if (Object.op_Equality((Object) transform, (Object) null))
        return (UIPlayerStatusGizmo) null;
      playerStatusGizmo = ((Component) transform).GetComponent<UIPlayerStatusGizmo>();
      if (Object.op_Equality((Object) playerStatusGizmo, (Object) null))
      {
        Object.Destroy((Object) transform);
        return (UIPlayerStatusGizmo) null;
      }
      this.playerList.Add(playerStatusGizmo);
    }
    playerStatusGizmo.targetPlayer = owner;
    this.SetDepth(((Component) playerStatusGizmo).gameObject);
    return playerStatusGizmo;
  }

  public UIEnemyStatusGizmo Create(Enemy owner)
  {
    UIEnemyStatusGizmo enemyStatusGizmo = (UIEnemyStatusGizmo) null;
    int index = 0;
    for (int count = this.enemyList.Count; index < count; ++index)
    {
      if (!((Component) this.enemyList[index]).gameObject.activeSelf)
      {
        enemyStatusGizmo = this.enemyList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) enemyStatusGizmo, (Object) null))
    {
      Transform transform = ResourceUtility.Realizes((Object) this.enemyStatusGizmo, this._transform);
      if (Object.op_Equality((Object) transform, (Object) null))
        return (UIEnemyStatusGizmo) null;
      enemyStatusGizmo = ((Component) transform).GetComponent<UIEnemyStatusGizmo>();
      if (Object.op_Equality((Object) enemyStatusGizmo, (Object) null))
      {
        Object.Destroy((Object) transform);
        return (UIEnemyStatusGizmo) null;
      }
      this.enemyList.Add(enemyStatusGizmo);
    }
    enemyStatusGizmo.targetEnemy = owner;
    this.SetDepth(((Component) enemyStatusGizmo).gameObject);
    return enemyStatusGizmo;
  }

  public UIPortalStatusGizmo Create(PortalObject owner)
  {
    UIPortalStatusGizmo portalStatusGizmo = (UIPortalStatusGizmo) null;
    int index = 0;
    for (int count = this.portalList.Count; index < count; ++index)
    {
      if (!((Component) this.portalList[index]).gameObject.activeSelf)
      {
        portalStatusGizmo = this.portalList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) portalStatusGizmo, (Object) null))
    {
      Transform transform = ResourceUtility.Realizes((Object) this.portalStatusGizmo, this._transform);
      if (Object.op_Equality((Object) transform, (Object) null))
        return (UIPortalStatusGizmo) null;
      portalStatusGizmo = ((Component) transform).GetComponent<UIPortalStatusGizmo>();
      if (Object.op_Equality((Object) portalStatusGizmo, (Object) null))
      {
        Object.Destroy((Object) transform);
        return (UIPortalStatusGizmo) null;
      }
      this.portalList.Add(portalStatusGizmo);
    }
    portalStatusGizmo.portal = owner;
    this.SetDepth(((Component) portalStatusGizmo).gameObject);
    return portalStatusGizmo;
  }

  public UICannonGizmo Create(FieldGimmickCannonObject owner)
  {
    UICannonGizmo uiCannonGizmo = this.GetOrCreate<UICannonGizmo>(this.cannonList, this.cannonGizmo);
    uiCannonGizmo.owner = owner;
    this.SetDepth(((Component) uiCannonGizmo).gameObject);
    return uiCannonGizmo;
  }

  public UISonarGizmo CreateSonar(FieldSonarObject sonar)
  {
    UISonarGizmo sonar1 = this.GetOrCreate<UISonarGizmo>(this.sonarList, this.sonarGizmo);
    sonar1.sonar = sonar;
    this.SetDepth(((Component) sonar1).gameObject);
    return sonar1;
  }

  public UIGrabStatusGizmo CreateGrab()
  {
    UIGrabStatusGizmo grab = this.GetOrCreate<UIGrabStatusGizmo>(this.grabList, this.grabStatusGizmo);
    this.SetDepth(((Component) grab).gameObject);
    return grab;
  }

  public UIWaveTargetGizmo CreateWaveTarget(FieldWaveTargetObject wt)
  {
    UIWaveTargetGizmo waveTarget = this.GetOrCreate<UIWaveTargetGizmo>(this.waveTargetList, this.waveTargetGizmo);
    waveTarget.waveTarget = wt;
    waveTarget.Initialize();
    this.SetDepth(((Component) waveTarget).gameObject);
    return waveTarget;
  }

  public UIChatGimmickGizmo CreateGimmick(FieldChatGimmickObject cg)
  {
    UIChatGimmickGizmo gimmick = this.GetOrCreate<UIChatGimmickGizmo>(this.chatGimmickList, this.chatGimmickGizmo);
    gimmick.Initialize(cg);
    this.SetDepth(((Component) gimmick).gameObject);
    return gimmick;
  }

  protected T GetOrCreate<T>(List<T> objects, GameObject obj) where T : MonoBehaviour
  {
    T obj1 = default (T);
    for (int index = 0; index < objects.Count; ++index)
    {
      if (!((Component) (object) objects[index]).gameObject.activeSelf)
      {
        obj1 = objects[index];
        break;
      }
    }
    if (Object.op_Equality((Object) (object) obj1, (Object) null))
    {
      Transform transform = ResourceUtility.Realizes((Object) obj, this._transform);
      if (Object.op_Equality((Object) transform, (Object) null))
        return default (T);
      obj1 = ((Component) transform).GetComponent<T>();
      if (Object.op_Equality((Object) (object) obj1, (Object) null))
      {
        Object.Destroy((Object) transform);
        return default (T);
      }
      objects.Add(obj1);
    }
    return obj1;
  }

  private void SetDepth(GameObject go)
  {
    UIPanel component = go.GetComponent<UIPanel>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.depth = this.depth;
    ++this.depth;
    if (this.depth <= 10000)
      return;
    this.depth = 0;
  }
}
