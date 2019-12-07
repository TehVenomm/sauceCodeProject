using System;
using UnityEngine;

[ExecuteInEditMode]
public class ItemModelDisplayInfoEditor : MonoBehaviour
{
	public GlobalSettingsManager globalSettingsManager;

	public Transform[] weapons;

	public Transform armor;

	public Transform helm;

	public Transform arm;

	public Transform leg;

	public Transform item;

	private bool pivotAutoRotate;

	private void OnValidate()
	{
		if (!Application.isPlaying && globalSettingsManager != null)
		{
			GlobalSettingsManager.UIModelRenderingParam uiModelRendering = globalSettingsManager.uiModelRendering;
			GlobalSettingsManager.UIModelRenderingParam.DisplayInfo[] weaponDisplayInfos = uiModelRendering.WeaponDisplayInfos;
			int i = 0;
			int num = weaponDisplayInfos.Length;
			for (int num2 = weapons.Length; i < num && i < num2; i++)
			{
				LoadSettings(weaponDisplayInfos[i], weapons[i]);
			}
			LoadSettings(uiModelRendering.armorDisplayInfo, armor);
			LoadSettings(uiModelRendering.helmDisplayInfo, helm);
			LoadSettings(uiModelRendering.armDisplayInfo, arm);
			LoadSettings(uiModelRendering.legDisplayInfo, leg);
			LoadSettings(uiModelRendering.itemDisplayInfo, item);
		}
	}

	private void LoadSettings(GlobalSettingsManager.UIModelRenderingParam.DisplayInfo info, Transform root)
	{
		if (info == null || root == null)
		{
			return;
		}
		Transform transform = root.Find("Pivot");
		if (!(transform == null))
		{
			root.localPosition = Vector3.zero;
			root.localEulerAngles = Vector3.zero;
			root.localScale = Vector3.one;
			transform.localPosition = new Vector3(0f, 0f, info.zFromCamera);
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
			Transform transform2 = root.Find("Pivot/Model0");
			Transform transform3 = root.Find("Pivot/Model1");
			if (transform2 != null)
			{
				transform2.localPosition = info.mainPos;
				transform2.localEulerAngles = info.mainRot;
				transform2.localScale = Vector3.one;
			}
			if (transform3 != null)
			{
				transform3.localPosition = info.subPos;
				transform3.localEulerAngles = info.subRot;
				transform3.localScale = Vector3.one;
			}
		}
	}

	private void SaveSettings(GlobalSettingsManager.UIModelRenderingParam.DisplayInfo info, Transform root)
	{
		if (info == null || root == null)
		{
			return;
		}
		Transform transform = root.Find("Pivot");
		if (!(transform == null))
		{
			info.zFromCamera = transform.localPosition.z;
			Transform transform2 = root.Find("Pivot/Model0");
			Transform transform3 = root.Find("Pivot/Model1");
			if (transform2 != null)
			{
				info.mainPos = transform2.localPosition;
				info.mainRot = transform2.localEulerAngles;
			}
			if (transform3 != null)
			{
				info.subPos = transform3.localPosition;
				info.subRot = transform3.localEulerAngles;
			}
			else
			{
				info.subPos = Vector3.zero;
				info.subRot = Vector3.zero;
			}
		}
	}

	private void Update()
	{
		ForEachModels(delegate(Transform t)
		{
			UpdateInfo(t);
		});
	}

	private void ForEachModels(Action<Transform> callback)
	{
		Transform[] array = weapons;
		foreach (Transform obj in array)
		{
			callback(obj);
		}
		callback(armor);
		callback(helm);
		callback(arm);
		callback(leg);
		callback(item);
	}

	private void UpdateInfo(Transform root)
	{
		if (root == null)
		{
			return;
		}
		Transform transform = root.Find("Pivot");
		if (!(root == null))
		{
			root.localPosition = Vector3.zero;
			root.localEulerAngles = Vector3.zero;
			Vector3 localPosition = transform.localPosition;
			localPosition.x = 0f;
			localPosition.y = 0f;
			transform.localPosition = localPosition;
			Vector3 localEulerAngles = transform.localEulerAngles;
			localEulerAngles.x = 0f;
			localEulerAngles.z = 0f;
			if (pivotAutoRotate)
			{
				localEulerAngles.y = (localEulerAngles.y + 5f) % 360f;
			}
			transform.localEulerAngles = localEulerAngles;
			UpdateModel(root.Find("Pivot/Model0"));
			UpdateModel(root.Find("Pivot/Model1"));
		}
	}

	private void UpdateModel(Transform model)
	{
		if (!(model == null))
		{
			foreach (Transform item2 in model)
			{
				item2.localPosition = Vector3.zero;
				item2.localEulerAngles = Vector3.zero;
			}
		}
	}

	private void ResetPivotAngle(Transform root)
	{
		if (!(root == null))
		{
			Transform transform = root.Find("Pivot");
			if (!(transform == null))
			{
				transform.localEulerAngles = Vector3.zero;
			}
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.Label("item model display info editor");
		if (!Application.isPlaying)
		{
			GUILayout.Label("実行して下さい。");
		}
		else
		{
			GUILayout.Label("中心点の前後の調整は「Pivot」のZ移動。\nモデルの位置・姿勢の調整は「Model0」「Model1」を移動・回転。");
			if (!pivotAutoRotate)
			{
				if (GUILayout.Button("「Pivot」自動回転をON"))
				{
					pivotAutoRotate = true;
				}
			}
			else if (GUILayout.Button("「Pivot」自動回転をOFF"))
			{
				pivotAutoRotate = false;
			}
			if (GUILayout.Button("PivotのY回転をリセット"))
			{
				ForEachModels(delegate(Transform t)
				{
					ResetPivotAngle(t);
				});
				pivotAutoRotate = false;
			}
			if (GUILayout.Button("エディット内容をGlobalSettingsManagerに反映") && globalSettingsManager != null)
			{
				GlobalSettingsManager.UIModelRenderingParam uiModelRendering = globalSettingsManager.uiModelRendering;
				GlobalSettingsManager.UIModelRenderingParam.DisplayInfo[] weaponDisplayInfos = globalSettingsManager.uiModelRendering.WeaponDisplayInfos;
				int i = 0;
				int num = weaponDisplayInfos.Length;
				for (int num2 = weapons.Length; i < num && i < num2; i++)
				{
					SaveSettings(weaponDisplayInfos[i], weapons[i]);
				}
				SaveSettings(uiModelRendering.armorDisplayInfo, armor);
				SaveSettings(uiModelRendering.helmDisplayInfo, helm);
				SaveSettings(uiModelRendering.armDisplayInfo, arm);
				SaveSettings(uiModelRendering.legDisplayInfo, leg);
				SaveSettings(uiModelRendering.itemDisplayInfo, item);
			}
		}
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
