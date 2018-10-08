using UnityEngine;

public class MaterialInfoButton : MonoBehaviour
{
	private Transform parentButton;

	private Transform parentScroll;

	private MaterialInfo materialInfo;

	private UILabel lblItem;

	private string itemName;

	private bool touched;

	public static void Set(Transform icon, Transform material_info, REWARD_TYPE reward_type, uint id, string section_name, Transform parentScroll)
	{
		UIButton componentInChildren = icon.GetComponentInChildren<UIButton>();
		if (!((Object)componentInChildren == (Object)null))
		{
			MaterialInfoButton materialInfoButton = icon.GetComponent<MaterialInfoButton>();
			if ((Object)materialInfoButton == (Object)null)
			{
				materialInfoButton = icon.gameObject.AddComponent<MaterialInfoButton>();
			}
			materialInfoButton.parentButton = componentInChildren.transform;
			materialInfoButton.itemName = Utility.GetRewardName(reward_type, id);
			materialInfoButton.parentScroll = parentScroll;
			MaterialInfo component = material_info.GetComponent<MaterialInfo>();
			component.Initialize(section_name);
			materialInfoButton.materialInfo = component;
		}
	}

	private void OnHover(bool isOver)
	{
		if (!isOver && touched)
		{
			Send(false);
		}
	}

	private void OnPress(bool isPressed)
	{
		Send(isPressed);
	}

	private void OnDisable()
	{
		if (!AppMain.isApplicationQuit && touched)
		{
			Send(false);
		}
	}

	private void Send(bool is_touch)
	{
		if (touched != is_touch)
		{
			touched = is_touch;
			if ((Object)materialInfo != (Object)null)
			{
				materialInfo.Send(is_touch, parentButton, itemName, parentScroll);
			}
		}
	}

	private void Update()
	{
		if (touched)
		{
			materialInfo.UpdatePosision(parentButton);
		}
	}
}
