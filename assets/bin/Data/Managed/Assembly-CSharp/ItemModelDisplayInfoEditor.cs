using System;
using System.Collections;
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

	public ItemModelDisplayInfoEditor()
		: this()
	{
	}

	private void OnValidate()
	{
		if (!Application.get_isPlaying() && globalSettingsManager != null)
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
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (info == null || root == null)
		{
			return;
		}
		Transform val = root.Find("Pivot");
		if (!(val == null))
		{
			root.set_localPosition(Vector3.get_zero());
			root.set_localEulerAngles(Vector3.get_zero());
			root.set_localScale(Vector3.get_one());
			val.set_localPosition(new Vector3(0f, 0f, info.zFromCamera));
			val.set_localEulerAngles(Vector3.get_zero());
			val.set_localScale(Vector3.get_one());
			Transform val2 = root.Find("Pivot/Model0");
			Transform val3 = root.Find("Pivot/Model1");
			if (val2 != null)
			{
				val2.set_localPosition(info.mainPos);
				val2.set_localEulerAngles(info.mainRot);
				val2.set_localScale(Vector3.get_one());
			}
			if (val3 != null)
			{
				val3.set_localPosition(info.subPos);
				val3.set_localEulerAngles(info.subRot);
				val3.set_localScale(Vector3.get_one());
			}
		}
	}

	private void SaveSettings(GlobalSettingsManager.UIModelRenderingParam.DisplayInfo info, Transform root)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (info == null || root == null)
		{
			return;
		}
		Transform val = root.Find("Pivot");
		if (!(val == null))
		{
			Vector3 localPosition = val.get_localPosition();
			info.zFromCamera = localPosition.z;
			Transform val2 = root.Find("Pivot/Model0");
			Transform val3 = root.Find("Pivot/Model1");
			if (val2 != null)
			{
				info.mainPos = val2.get_localPosition();
				info.mainRot = val2.get_localEulerAngles();
			}
			if (val3 != null)
			{
				info.subPos = val3.get_localPosition();
				info.subRot = val3.get_localEulerAngles();
			}
			else
			{
				info.subPos = Vector3.get_zero();
				info.subRot = Vector3.get_zero();
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (root == null)
		{
			return;
		}
		Transform val = root.Find("Pivot");
		if (!(root == null))
		{
			root.set_localPosition(Vector3.get_zero());
			root.set_localEulerAngles(Vector3.get_zero());
			Vector3 localPosition = val.get_localPosition();
			localPosition.x = 0f;
			localPosition.y = 0f;
			val.set_localPosition(localPosition);
			Vector3 localEulerAngles = val.get_localEulerAngles();
			localEulerAngles.x = 0f;
			localEulerAngles.z = 0f;
			if (pivotAutoRotate)
			{
				localEulerAngles.y = (localEulerAngles.y + 5f) % 360f;
			}
			val.set_localEulerAngles(localEulerAngles);
			UpdateModel(root.Find("Pivot/Model0"));
			UpdateModel(root.Find("Pivot/Model1"));
		}
	}

	private void UpdateModel(Transform model)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (!(model == null))
		{
			IEnumerator enumerator = model.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform val = enumerator.Current;
					val.set_localPosition(Vector3.get_zero());
					val.set_localEulerAngles(Vector3.get_zero());
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	private void ResetPivotAngle(Transform root)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (!(root == null))
		{
			Transform val = root.Find("Pivot");
			if (!(val == null))
			{
				val.set_localEulerAngles(Vector3.get_zero());
			}
		}
	}

	private void OnGUI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()));
		GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
		GUILayout.Label("item model display info editor", (GUILayoutOption[])new GUILayoutOption[0]);
		if (!Application.get_isPlaying())
		{
			GUILayout.Label("実行して下さい。", (GUILayoutOption[])new GUILayoutOption[0]);
		}
		else
		{
			GUILayout.Label("中心点の前後の調整は「Pivot」のZ移動。\nモデルの位置・姿勢の調整は「Model0」「Model1」を移動・回転。", (GUILayoutOption[])new GUILayoutOption[0]);
			if (!pivotAutoRotate)
			{
				if (GUILayout.Button("「Pivot」自動回転をON", (GUILayoutOption[])new GUILayoutOption[0]))
				{
					pivotAutoRotate = true;
				}
			}
			else if (GUILayout.Button("「Pivot」自動回転をOFF", (GUILayoutOption[])new GUILayoutOption[0]))
			{
				pivotAutoRotate = false;
			}
			if (GUILayout.Button("PivotのY回転をリセット", (GUILayoutOption[])new GUILayoutOption[0]))
			{
				ForEachModels(delegate(Transform t)
				{
					ResetPivotAngle(t);
				});
				pivotAutoRotate = false;
			}
			if (GUILayout.Button("エディット内容をGlobalSettingsManagerに反映", (GUILayoutOption[])new GUILayoutOption[0]) && globalSettingsManager != null)
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
