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
	protected SimplePingPongAlpha alertAnim;

	private List<MiniMapIcon> icons = new List<MiniMapIcon>();

	private List<MiniMapIcon> playerIconStock = new List<MiniMapIcon>();

	private List<MiniMapIcon> enemyIconStock = new List<MiniMapIcon>();

	private List<MiniMapIcon> waveTargetIconStock = new List<MiniMapIcon>();

	private int updateCount;

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().SetActive((FieldManager.IsValidInGameNoQuest() && !MonoBehaviourSingleton<FieldManager>.I.isTutorialField) || QuestManager.IsValidInGameWaveMatch(false));
	}

	private void LateUpdate()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null))
			{
				Vector3 position = self.get_transform().get_position();
				float x = position.x;
				float z = position.z;
				Transform transform = iconRoot.get_transform();
				Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_eulerAngles();
				transform.set_localEulerAngles(new Vector3(0f, 0f, eulerAngles.y));
				Transform obj = selfIcon;
				Vector3 localEulerAngles = self._transform.get_localEulerAngles();
				obj.set_localEulerAngles(new Vector3(0f, 0f, 0f - localEulerAngles.y));
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
		}
	}

	public void Attach(MonoBehaviour root_object)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		if (!(root_object is Self))
		{
			Transform val = root_object.get_transform();
			int i = 0;
			for (int count = icons.Count; i < count; i++)
			{
				if (icons[i].target == val)
				{
					return;
				}
			}
			string text = string.Empty;
			int num = -1;
			MiniMapIcon miniMapIcon = null;
			if (root_object is PortalObject)
			{
				num = 0;
			}
			else if (root_object is Player)
			{
				Player player = root_object as Player;
				if (!player.isInitialized)
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
				if (!GameSaveData.instance.enableMinimapEnemy && !QuestManager.IsValidInGameWaveMatch(false))
				{
					return;
				}
				Enemy enemy = root_object as Enemy;
				if (!enemy.isInitialized)
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
			if (miniMapIcon == null && num >= 0 && num < iconPrefabs.Length)
			{
				GameObject val2 = ResourceUtility.Instantiate<GameObject>(iconPrefabs[num]);
				if (val2 != null)
				{
					miniMapIcon = val2.GetComponent<MiniMapIcon>();
				}
			}
			if (miniMapIcon != null)
			{
				miniMapIcon.Initialize(root_object);
				if (!text.IsNullOrWhiteSpace())
				{
					miniMapIcon.SetIconSprite(text);
				}
				miniMapIcon.target = val;
				Utility.Attach(iconRoot, miniMapIcon._trasform);
				icons.Add(miniMapIcon);
				updateCount = 0;
			}
		}
	}

	public void Detach(MonoBehaviour root_object)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		Transform val = root_object.get_transform();
		int num = 0;
		int count = icons.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if (icons[num].target == val)
			{
				break;
			}
			num++;
		}
		if (root_object is Player)
		{
			playerIconStock.Add(icons[num]);
			icons[num].get_gameObject().SetActive(false);
		}
		else if (root_object is Enemy)
		{
			enemyIconStock.Add(icons[num]);
			icons[num].get_gameObject().SetActive(false);
		}
		else if (root_object is FieldWaveTargetObject)
		{
			waveTargetIconStock.Add(icons[num]);
			icons[num].get_gameObject().SetActive(false);
		}
		else
		{
			Object.Destroy(icons[num].get_gameObject());
		}
		icons.Remove(icons[num]);
	}

	public void ShowAlert()
	{
		alertAnim.Play(true);
	}
}
