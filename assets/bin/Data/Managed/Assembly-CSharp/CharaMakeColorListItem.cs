// Decompiled with JetBrains decompiler
// Type: CharaMakeColorListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CharaMakeColorListItem : MonoBehaviour
{
  [SerializeField]
  private UISprite m_Sprite;
  [SerializeField]
  private UIButton m_Button;
  [SerializeField]
  private GameObject m_OnRoot;
  [SerializeField]
  private GameObject m_OffRoot;
  public int id;

  public Transform uiEventSender => ((Component) this.m_Button).transform;

  private void Awake() => this.m_Button.tweenTarget = (GameObject) null;

  public void Init(Color color, int id, UIScrollView scroll)
  {
    this.m_Sprite.color = color;
    this.m_Button.defaultColor = color;
    this.m_Button.hover = color;
    this.m_Button.pressed = color;
    this.m_Button.disabledColor = color;
    this.m_Button.CacheDefaultColor();
    this.id = id;
    ((Component) this.m_Button).gameObject.AddComponent<UIDragScrollView>().scrollView = scroll;
  }

  public void On()
  {
    this.m_OnRoot.SetActive(true);
    this.m_OffRoot.SetActive(false);
  }

  public void Off()
  {
    this.m_OnRoot.SetActive(false);
    this.m_OffRoot.SetActive(true);
  }
}
