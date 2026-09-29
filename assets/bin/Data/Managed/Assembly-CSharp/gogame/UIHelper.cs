// Decompiled with JetBrains decompiler
// Type: gogame.UIHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace gogame;

public class UIHelper
{
  public static GameObject NewGameObject(string name, GameObject parent = null)
  {
    GameObject gameObject = new GameObject();
    ((Object) gameObject).name = name;
    if (Object.op_Inequality((Object) parent, (Object) null))
      gameObject.transform.parent = parent.transform;
    return gameObject;
  }

  public static void SetBackgroundColor(GameObject gameObject, Color color)
  {
    Image image = gameObject.AddComponent<Image>();
    ((Graphic) image).color = color;
    ((Graphic) image).material = (Material) null;
    ((Graphic) image).raycastTarget = true;
  }

  public static Button MakeButton(
    GameObject gameObject,
    string caption,
    Font font,
    int fontSize,
    Color textColor,
    Color backgroundColor)
  {
    Image image = gameObject.AddComponent<Image>();
    ((Graphic) image).color = backgroundColor;
    ((Graphic) image).raycastTarget = true;
    Button button = gameObject.AddComponent<Button>();
    Text text = UIHelper.NewGameObject("Text", gameObject).AddComponent<Text>();
    text.text = caption;
    text.font = font;
    text.fontSize = fontSize;
    text.alignment = (TextAnchor) 4;
    ((Graphic) text).color = textColor;
    return button;
  }
}
