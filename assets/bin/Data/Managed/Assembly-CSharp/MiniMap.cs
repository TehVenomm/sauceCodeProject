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
		base.Awake();
		base.gameObject.SetActive((FieldManager.IsValidInGameNoQuest() && !MonoBehaviourSingleton<FieldManager>.I.isTutorialField) || QuestManager.IsValidInGameWaveMatch(false));
	}

	private void LateUpdate()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!((Object)self == (Object)null))
			{
				Vector3 position = self.transform.position;
				float x = position.x;
				float z = position.z;
				Transform transform = iconRoot.transform;
				Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles;
				transform.localEulerAngles = new Vector3(0f, 0f, eulerAngles.y);
				Transform transform2 = selfIcon;
				Vector3 localEulerAngles = self._transform.localEulerAngles;
				transform2.localEulerAngles = new Vector3(0f, 0f, 0f - localEulerAngles.y);
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
		if (!(root_object is Self))
		{
			Transform transform = root_object.transform;
			int i = 0;
			for (int count = icons.Count; i < count; i++)
			{
				if ((Object)icons[i].target == (Object)transform)
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
			if ((Object)miniMapIcon == (Object)null && num >= 0 && num < iconPrefabs.Length)
			{
				GameObject gameObject = ResourceUtility.Instantiate(iconPrefabs[num]);
				if ((Object)gameObject != (Object)null)
				{
					miniMapIcon = gameObject.GetComponent<MiniMapIcon>();
				}
			}
			if ((Object)miniMapIcon != (Object)null)
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
	}

	public void Detach(MonoBehaviour root_object)
	{
		Transform transform = root_object.transform;
		int num = 0;
		int count = icons.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if ((Object)icons[num].target == (Object)transform)
			{
				break;
			}
			num++;
		}
		if (root_object is Player)
		{
			playerIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(false);
		}
		else if (root_object is Enemy)
		{
			enemyIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(false);
		}
		else if (root_object is FieldWaveTargetObject)
		{
			waveTargetIconStock.Add(icons[num]);
			icons[num].gameObject.SetActive(false);
		}
		else
		{
			Object.Destroy(icons[num].gameObject);
		}
		icons.Remove(icons[num]);
	}

	public void ShowAlert()
	{
		alertAnim.Play(true);
	}
}
