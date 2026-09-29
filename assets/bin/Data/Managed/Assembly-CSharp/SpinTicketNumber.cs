// Decompiled with JetBrains decompiler
// Type: SpinTicketNumber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
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
    int length1 = this.numberList.Length;
    int length2 = strValue.Length;
    for (int index1 = 0; index1 < length1; ++index1)
    {
      if (index1 < length2)
      {
        int index2 = int.Parse(strValue[length2 - 1 - index1].ToString());
        this.numberList[index1].spriteName = this.jackportNumbers[index2];
        ((Component) this.numberList[index1]).gameObject.SetActive(true);
      }
      else
        ((Component) this.numberList[index1]).gameObject.SetActive(false);
    }
    int num = 0;
    for (int index = 0; index < length2; ++index)
    {
      ((Component) this.numberList[index]).transform.localPosition = new Vector3((float) -num, 0.0f, 0.0f);
      this.numberList[index].height = this.sprHeight;
      num += this.numberList[index].width;
    }
    Vector3 localPosition = ((Component) this).transform.localPosition;
    localPosition.x = (float) num / 2f;
    ((Component) this).transform.localPosition = localPosition;
  }
}
