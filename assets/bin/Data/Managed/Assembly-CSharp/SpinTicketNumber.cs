using UnityEngine;

public class SpinTicketNumber
{
	[SerializeField]
	private UISprite[] numberList;

	[SerializeField]
	private int sprHeight;

	private readonly string[] jackportNumbers = new string[10]
	{
		"ticket_number_0",
		"ticket_number_1",
		"ticket_number_2",
		"ticket_number_3",
		"ticket_number_4",
		"ticket_number_5",
		"ticket_number_6",
		"ticket_number_7",
		"ticket_number_8",
		"ticket_number_9"
	};

	public SpinTicketNumber()
		: this()
	{
	}

	public void ShowNumber(string strValue)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		int num = numberList.Length;
		int length = strValue.Length;
		for (int i = 0; i < num; i++)
		{
			if (i < length)
			{
				int num2 = int.Parse(strValue[length - 1 - i].ToString());
				numberList[i].spriteName = jackportNumbers[num2];
				numberList[i].get_gameObject().SetActive(true);
			}
			else
			{
				numberList[i].get_gameObject().SetActive(false);
			}
		}
		int num3 = 0;
		for (int j = 0; j < length; j++)
		{
			numberList[j].get_transform().set_localPosition(new Vector3((float)(-num3), 0f, 0f));
			numberList[j].height = sprHeight;
			num3 += numberList[j].width;
		}
		Vector3 localPosition = this.get_transform().get_localPosition();
		localPosition.x = (float)num3 / 2f;
		this.get_transform().set_localPosition(localPosition);
	}
}
