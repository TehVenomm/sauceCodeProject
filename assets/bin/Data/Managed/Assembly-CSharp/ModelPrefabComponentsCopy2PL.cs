using UnityEngine;

public class ModelPrefabComponentsCopy2PL : MonoBehaviour
{
	public enum TOOL_MODE
	{
		CHANGE_FBX_CONVERT,
		SETTINGS_COPY,
		HIERARCHY_CHECK
	}

	public const int DEST_PREFAB_MAX = 10;

	public TOOL_MODE toolMode;

	public GameObject beforeFbx;

	public GameObject afterFbx;

	public GameObject workPrefab;

	public GameObject[] changePrefabList = (GameObject[])new GameObject[10];

	public int numDstPrefab = 1;

	public bool prefabAutoDestroy = true;

	public bool isForceApply2Bones = true;

	public GameObject srcPrefab;

	public GameObject srcBaseFbx;

	public GameObject destPrefab;

	public bool prefabAutoSave = true;

	public GameObject srcObject;

	public GameObject dstObject;

	public bool enableComponentLog;

	public ModelPrefabComponentsCopy2PL()
		: this()
	{
	}
}
