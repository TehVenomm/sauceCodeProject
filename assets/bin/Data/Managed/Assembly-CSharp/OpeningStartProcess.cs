// Decompiled with JetBrains decompiler
// Type: OpeningStartProcess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class OpeningStartProcess : MonoBehaviour
{
  private IEnumerator Start()
  {
    yield return (object) ResourceSizeInfo.Init();
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
    MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(false);
    yield return (object) ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetOpeningAssetSizeMB(true));
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, true);
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
    {
      "MainCamera",
      "InputManager"
    });
    LoadObject lo_field_map_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapTable");
    LoadObject lo_portal_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapPortalTable");
    LoadObject lo_enemy_pop_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapEnemyPopTable");
    while (load_queue.IsLoading())
      yield return (object) null;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FieldManager>();
    Singleton<FieldMapTable>.Create();
    string csv_text1 = DataTableManager.Decrypt((lo_field_map_table.loadedObject as TextAsset).text);
    Singleton<FieldMapTable>.I.CreateFieldMapTable(csv_text1);
    string csv_text2 = DataTableManager.Decrypt((lo_portal_table.loadedObject as TextAsset).text);
    Singleton<FieldMapTable>.I.CreatePortalTable(csv_text2);
    string csv_text3 = DataTableManager.Decrypt((lo_enemy_pop_table.loadedObject as TextAsset).text);
    Singleton<FieldMapTable>.I.CreateEnemyPopTable(csv_text3);
    if (Object.op_Inequality((Object) Camera.main, (Object) null))
      Object.DestroyImmediate((Object) ((Component) Camera.main).gameObject);
    ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[0].obj, MonoBehaviourSingleton<AppMain>.I._transform);
    ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[1].obj, MonoBehaviourSingleton<AppMain>.I._transform);
    MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
    MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, true);
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<WorldMapManager>();
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<FilterManager>();
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
    MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
    if (!TitleTop.isFirstBoot)
      TitleTop.isFirstServerSelection = false;
    if (TitleTop.isFirstServerSelection)
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "ServerSelection");
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "Opening");
  }
}
