using System.Collections;
using UnityEngine;

public class OpeningStartProcess : MonoBehaviour
{
	private IEnumerator Start()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
		{
			"MainCamera",
			"InputManager"
		}, false);
		LoadObject lo_field_map_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapTable", false);
		LoadObject lo_portal_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapPortalTable", false);
		LoadObject lo_enemy_pop_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapEnemyPopTable", false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<FieldManager>();
		Singleton<FieldMapTable>.Create();
		string csv3 = DataTableManager.Decrypt((lo_field_map_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreateFieldMapTable(csv3);
		csv3 = DataTableManager.Decrypt((lo_portal_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreatePortalTable(csv3);
		csv3 = DataTableManager.Decrypt((lo_enemy_pop_table.loadedObject as TextAsset).text);
		Singleton<FieldMapTable>.I.CreateEnemyPopTable(csv3);
		if ((Object)Camera.main != (Object)null)
		{
			Object.DestroyImmediate(Camera.main.gameObject);
		}
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[0].obj, MonoBehaviourSingleton<AppMain>.I._transform, -1);
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[1].obj, MonoBehaviourSingleton<AppMain>.I._transform, -1);
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, true);
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<WorldMapManager>();
		MonoBehaviourSingleton<AppMain>.I.gameObject.AddComponent<FilterManager>();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, false);
		MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "Opening", UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}
}
