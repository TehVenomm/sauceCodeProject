// Decompiled with JetBrains decompiler
// Type: ConfigPPOFF
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class ConfigPPOFF : PPInputBase
{
  private void OnQuery_OK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendResetParentalPassword(this.GetInputValue((Enum) PPInputBase.UI.IPT_PW), (Action<Error>) (ret =>
    {
      if (ret != Error.None)
        GameSection.ChangeStayEvent("ERROR", (object) new object[1]
        {
          (object) (int) ret
        });
      GameSection.ResumeEvent(true);
    }));
  }
}
