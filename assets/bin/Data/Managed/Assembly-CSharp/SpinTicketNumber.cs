using UnityEngine;

public class SpinTicketNumber : MonoBehaviour
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

	public void ShowNumber(string strValue)
	{
		int num = numberList.Length;
		int length = strValue.Length;
		for (int i = 0; i < num; i++)
		{
			if (i < length)
			{
				int num2 = int.Parse(strValue[length - 1 - i].ToString());
				numberList[i].spriteName = jackportNumbers[num2];
				numberList[i].gameObject.SetActive(value: true);
			}
			else
			{
				numberList[i].gameObject.SetActive(value: false);
			}
		}
		int num3 = 0;
		for (int j = 0; j < length; j++)
		{
			numberList[j].transform.localPosition = new Vector3(-num3, 0f, 0f);
			numberList[j].height = sprHeight;
			num3 += numberList[j].width;
		}
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = (float)num3 / 2f;
		base.transform.localPosition = localPosition;
	}
}
