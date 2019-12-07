using UnityEngine;

public class JackportNumber : MonoBehaviour
{
	private const int JACKPOT_LENGTH = 10;

	[SerializeField]
	private UISprite[] numberList;

	[SerializeField]
	private Transform[] dotList;

	[SerializeField]
	private int sprHeight;

	[SerializeField]
	private int spaceWidth = 5;

	private readonly string[] jackportNumbers = new string[10]
	{
		"number_0",
		"number_1",
		"number_2",
		"number_3",
		"number_4",
		"number_5",
		"number_6",
		"number_7",
		"number_8",
		"number_9"
	};

	private readonly string numberDot = "number_dot";

	public void ShowNumber(string strValue)
	{
		int num = numberList.Length;
		strValue = long.Parse(strValue).ToString();
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
		for (int j = 0; j < dotList.Length; j++)
		{
			dotList[j].gameObject.SetActive(value: false);
		}
		int num3 = 0;
		int num4 = 0;
		for (int k = 0; k < length; k++)
		{
			numberList[k].transform.localPosition = new Vector3(-num3, 0f, 0f);
			numberList[k].height = sprHeight;
			if (k % 3 == 2 && k < length - 1)
			{
				num3 += numberList[k].width;
				dotList[num4].gameObject.SetActive(value: true);
				dotList[num4].localPosition = new Vector3(-num3, -sprHeight + spaceWidth, 0f);
				num4++;
				num3 += spaceWidth;
			}
			else
			{
				num3 += numberList[k].width;
			}
		}
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = (float)num3 / 2f;
		base.transform.localPosition = localPosition;
	}
}
