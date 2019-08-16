using System.Collections.Generic;
using UnityEngine;

public class UIOracleStockUIController : MonoBehaviour
{
	private static readonly string stockIconPath = "InternalUI/UI_InGame/Oracle/InGameUIOracleStockIcon";

	[SerializeField]
	private GameObject iconsRoot;

	[SerializeField]
	private UISprite baseSprite;

	[SerializeField]
	private UISprite fulledEffect;

	[SerializeField]
	private int iconMax = 8;

	[SerializeField]
	private int iconWidth = 24;

	[SerializeField]
	private int baseWidth = 126;

	[SerializeField]
	private int baseEffectWidth = 90;

	[SerializeField]
	private float btnColliderExpansion = 20f;

	private List<UIOracleStockIconController> icons = new List<UIOracleStockIconController>();

	public int StockedCount
	{
		get
		{
			int count = 0;
			icons.ForEach(delegate(UIOracleStockIconController o)
			{
				if (o.Stocked)
				{
					count++;
				}
			});
			return count;
		}
	}

	public int StockMax => icons.Count;

	public UIOracleStockUIController()
		: this()
	{
	}

	public void Initialize(int max)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < max && icons.Count <= i; i++)
		{
			UIOracleStockIconController uIOracleStockIconController = CreateIcon(i);
			if (uIOracleStockIconController == null)
			{
				break;
			}
			icons.Add(uIOracleStockIconController);
		}
		baseSprite.width = baseWidth + iconWidth * icons.Count;
		BoxCollider component = baseSprite.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.set_size(new Vector3((float)baseSprite.width, (float)baseSprite.height + btnColliderExpansion, 1f));
			component.set_center(new Vector3((float)baseSprite.width / 2f, 0f, 0f));
		}
		fulledEffect.width = baseEffectWidth + iconWidth * icons.Count;
	}

	private UIOracleStockIconController CreateIcon(int index)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(Resources.Load(stockIconPath), iconsRoot.get_transform());
		if (val == null)
		{
			Debug.LogError((object)("failed to create icon(" + index + "). " + stockIconPath));
			return null;
		}
		val.get_transform().set_localPosition(new Vector3((float)(index * iconWidth), 0f, 0f));
		UIOracleStockIconController component = val.GetComponent<UIOracleStockIconController>();
		component.Initialize(index, (!(baseSprite == null)) ? (baseSprite.depth + 1) : 0);
		return component;
	}

	public void UpdateStock(int stockedCount)
	{
		for (int i = 0; i < StockMax; i++)
		{
			icons[i].Stock(stockedCount > i);
		}
		fulledEffect.set_enabled(stockedCount >= StockMax);
	}

	public void SetActive(bool enabled)
	{
		if (enabled && icons.Count > 0)
		{
			iconsRoot.SetActive(true);
			baseSprite.get_gameObject().SetActive(true);
		}
		else
		{
			iconsRoot.SetActive(false);
			baseSprite.get_gameObject().SetActive(false);
		}
	}

	public void StartGutsManually()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !(MonoBehaviourSingleton<StageObjectManager>.I.self == null))
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.spearCtrl.StartOracleGutsMode();
		}
	}
}
