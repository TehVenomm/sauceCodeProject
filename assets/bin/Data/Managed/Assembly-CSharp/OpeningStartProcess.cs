using System.Collections;
using UnityEngine;

public class OpeningStartProcess : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return ResourceSizeInfo.Init();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(isShow: false);
		yield return ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetOpeningAssetSizeMB(isTutorial: true));
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: true);
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
		{
			"MainCamera",
			"InputManager"
		});
		LoadObject lo_field_map_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapTable");
		LoadObject lo_portal_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapPortalTable");
		LoadObject lo_enemy_pop_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapEnemyPopTable");
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<FieldManager>();
		Singleton<FieldMapTable>.Create();
		string csv_text = DataTableManager.Decrypt((lo_field_map_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreateFieldMapTable(csv_text);
		csv_text = DataTableManager.Decrypt((lo_portal_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreatePortalTable(csv_text);
		csv_text = DataTableManager.Decrypt((lo_enemy_pop_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreateEnemyPopTable(csv_text);
		if (Camera.main != null)
		{
			Object.DestroyImmediate(Camera.main.gameObject);
		}
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[0].obj, MonoBehaviourSingleton<AppMain>.I._transform);
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[1].obj, MonoBehaviourSingleton<AppMain>.I._transform);
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, isEnable: true);
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<WorldMapManager>();
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<FilterManager>();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "Opening");
	}
}
