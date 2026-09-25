// Decompiled with JetBrains decompiler
// Type: JackportNumber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
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
    int length1 = this.numberList.Length;
    strValue = long.Parse(strValue).ToString();
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
    for (int index = 0; index < this.dotList.Length; ++index)
      ((Component) this.dotList[index]).gameObject.SetActive(false);
    int num1 = 0;
    int index3 = 0;
    for (int index4 = 0; index4 < length2; ++index4)
    {
      ((Component) this.numberList[index4]).transform.localPosition = new Vector3((float) -num1, 0.0f, 0.0f);
      this.numberList[index4].height = this.sprHeight;
      if (index4 % 3 == 2 && index4 < length2 - 1)
      {
        int num2 = num1 + this.numberList[index4].width;
        ((Component) this.dotList[index3]).gameObject.SetActive(true);
        this.dotList[index3].localPosition = new Vector3((float) -num2, (float) (-this.sprHeight + this.spaceWidth), 0.0f);
        ++index3;
        num1 = num2 + this.spaceWidth;
      }
      else
        num1 += this.numberList[index4].width;
    }
    Vector3 localPosition = ((Component) this).transform.localPosition;
    localPosition.x = (float) num1 / 2f;
    ((Component) this).transform.localPosition = localPosition;
  }
}
