using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviourSingleton<MiniMap>
{
	[SerializeField]
	protected Transform iconRoot;

	[SerializeField]
	protected Transform selfIcon;

	[SerializeField]
	protected float scaling = 0.1f;

	[SerializeField]
	protected float uiRadius = 150f;

	[SerializeField]
	protected GameObject[] iconPrefabs;

	[SerializeField]
	protected GameObject[] supplyIconPrefabs;

	[SerializeField]
	protected SimplePingPongAlpha alertAnim;

	private List<MiniMapIcon> icons = new List<MiniMapIcon>();

	private List<MiniMapIcon> playerIconStock = new List<MiniMapIcon>();

	private List<MiniMapIcon> enemyIconStock = new List<MiniMapIcon>();

	private List<MiniMapIcon> waveTargetIconStock = new List<MiniMapIcon>();

	private List<List<MiniMapIcon>> supplyIconStocks = new List<List<MiniMapIcon>>();

	private int updateCount;

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.SetActive((FieldManager.IsValidInGameNoQuest() && !MonoBehaviourSingleton<FieldManager>.I.isTutorialField) || QuestManager.IsValidInGameWaveMatch());
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		SyncRotatePosition();
		for (int i = 0; i < supplyIconPrefabs.Length; i++)
		{
			supplyIconStocks.Add(new List<MiniMapIcon>());
		}
	}

	protected override void OnDestroySingleton()
	{
		base.OnDestroySingleton();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		SyncRotatePosition();
	}

	private void SyncRotatePosition()
	{
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyMinimapPosition)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		UIWidget component = base.gameObject.GetComponent<UIWidget>();
		if (component != null)
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				component.leftAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.left;
				component.rightAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.right;
				component.bottomAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.bottom;
				component.topAnchor.absolute = specialDeviceInfo.MinimapAnchorPortrait.top;
			}
			else
			{
				component.leftAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.left;
				component.rightAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.right;
				component.bottomAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.bottom;
				component.topAnchor.absolute = specialDeviceInfo.MinimapAnchorLandscape.top;
			}
			component.UpdateAnchors();
		}
	}

	private void LateUpdate()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self == null)
		{
			return;
		}
		Vector3 position = self.transform.position;
		float x = position.x;
		float z = position.z;
		iconRoot.transform.localEulerAngles = new Vector3(0f, 0f, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles.y);
		selfIcon.localEulerAngles = new Vector3(0f, 0f, 0f - self._transform.localEulerAngles.y);
		updateCount--;
		if (updateCount <= 0)
		{
			int i = 0;
			for (int count = icons.Count; i < count; i++)
			{
				icons[i].UpdateIcon(x, z, scaling, uiRadius);
			}
			updateCount = 3;
		}
	}

	public void Attach(MonoBehaviour root_object)
	{
		if (root_object is Self)
		{
			return;
		}
		Transform transform = root_object.transform;
		int i = 0;
		for (int count = icons.Count; i < count; i++)
		{
			if (icons[i].target == transform)
			{
				return;
			}
		}
		string text = "";
		int num = -1;
		int num2 = -1;
		MiniMapIcon miniMapIcon = null;
		if (root_object is PortalObject)
		{
			num = 0;
		}
		else if (root_object is Player)
		{
			if (!(root_object as Player).isInitialized)
			{
				return;
			}
			num = 1;
			if (playerIconStock.Count > 0)
			{
				miniMapIcon = playerIconStock[0];
				playerIconStock.Remove(miniMapIcon);
			}
		}
		else if (root_object is Enemy)
		{
			if ((!GameSaveData.instance.enableMinimapEnemy && !QuestManager.IsValidInGameWaveMatch()) || !(root_object as Enemy).isInitialized)
			{
				return;
			}
			num = 2;
			if (enemyIconStock.Count > 0)
			{
				miniMapIcon = enemyIconStock[0];
				enemyIconStock.Remove(miniMapIcon);
			}
		}
		else if (root_object is FieldWaveTargetObject)
		{
			FieldWaveTargetObject fieldWaveTargetObject = root_object as FieldWaveTargetObject;
			if (!fieldWaveTargetObject.isInitialized)
			{
				return;
			}
			num = 3;
			if (waveTargetIconStock.Count > 0)
			{
				miniMapIcon = waveTargetIconStock[0];
				waveTargetIconStock.Remove(miniMapIcon);
			}
			string raderIconName = fieldWaveTargetObject.GetRaderIconName();
			if (!raderIconName.IsNullOrWhiteSpace())
			{
				text = "dp_radar_" + raderIconName;
			}
		}
		else if (root_object is FieldSupplyGimmickObject)
		{
			FieldSupplyGimmickObject fieldSupplyGimmickObject = root_object as FieldSupplyGimmickObject;
			if (!fieldSupplyGimmickObject.IsSearchableNearest())
			{
				return;
			}
			num2 = fieldSupplyGimmickObject.modelIndex;
			if (supplyIconStocks[num2].Count > 0)
			{
				miniMapIcon = supplyIconStocks[num2][0];
				supplyIconStocks[num2].Remove(miniMapIcon);
			}
		}
		if (miniMapIcon == null)
		{
			if (num >= 0 && num < iconPrefabs.Length)
			{
				GameObject gameObject = ResourceUtility.Instantiate(iconPrefabs[num]);
				if (gameObject != null)
				{
					miniMapIcon = gameObject.GetComponent<MiniMapIcon>();
				}
			}
			if (num2 >= 0 && num2 < supplyIconPrefabs.Length)
			{
				GameObject gameObject2 = ResourceUtility.Instantiate(supplyIconPrefabs[num2]);
				if (gameObject2 != null)
				{
					miniMapIcon = gameObject2.GetComponent<MiniMapIcon>();
				}
			}
		}
		if (miniMapIcon != null)
		{
			miniMapIcon.Initialize(root_object);
			if (!text.IsNullOrWhiteSpace())
			{
				miniMapIcon.SetIconSprite(text);
			}
			miniMapIcon.target = transform;
			Utility.Attach(iconRoot, miniMapIcon._trasform);
			icons.Add(miniMapIcon);
			updateCount = 0;
		}
	}

	public void Detach(MonoBehaviour root_object)
	{
		Transform transform = root_object.transform;
		int num = 0;
		int count = icons.Count;
		while (true)
		{
			if (num < count)
			{
				if (icons[num].target == transform)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		if (root_object is Player)
		{
			playerIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(value: false);
		}
		else if (root_object is Enemy)
		{
			enemyIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(value: false);
		}
		else if (root_object is FieldWaveTargetObject)
		{
			waveTargetIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(value: false);
		}
		else if (root_object is FieldSupplyGimmickObject)
		{
			FieldSupplyGimmickObject fieldSupplyGimmickObject = root_object as FieldSupplyGimmickObject;
			supplyIconStocks[fieldSupplyGimmickObject.modelIndex].Add(icons[num]);
			icons[num].gameObject.SetActive(value: false);
		}
		else
		{
			Object.Destroy(icons[num].gameObject);
		}
		icons.Remove(icons[num]);
	}

	public void ShowAlert()
	{
		alertAnim.Play(startDefaultValue: true);
	}
}
