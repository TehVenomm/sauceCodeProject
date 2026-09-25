// Decompiled with JetBrains decompiler
// Type: QuestUserStatusIconController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class QuestUserStatusIconController : MonoBehaviour
{
  [SerializeField]
  private UISprite[] m_statusIcons;

  public void Initialize(QuestUserStatusIconController.InitParam _param)
  {
    this.ActivateIcons(_param.StatusBit);
  }

  private void ActivateIcons(uint _statusBit)
  {
    if (this.m_statusIcons == null || this.m_statusIcons.Length < 1)
      return;
    for (int index = 0; index < this.m_statusIcons.Length; ++index)
    {
      if (!Object.op_Equality((Object) this.m_statusIcons[index], (Object) null))
      {
        int num = 1 << index + 1;
        ((Component) this.m_statusIcons[index]).gameObject.SetActive(((ulong) num & (ulong) _statusBit) > 0UL);
      }
    }
  }

  public class InitParam
  {
    public uint StatusBit;
  }
}
