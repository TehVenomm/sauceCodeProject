// Decompiled with JetBrains decompiler
// Type: FriendTestFollow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FriendTestFollow : GameSection
{
  private string follow_user_id = string.Empty;
  private string follow_user_id_2 = string.Empty;
  private string follow_user_id_3 = string.Empty;

  public override void Initialize() => base.Initialize();

  private void OnQuery_SEND()
  {
    if (string.IsNullOrEmpty(this.follow_user_id) && string.IsNullOrEmpty(this.follow_user_id_2) && string.IsNullOrEmpty(this.follow_user_id_3))
      return;
    List<int> id_list = new List<int>();
    int result;
    if (int.TryParse(this.follow_user_id, out result))
      id_list.Add(result);
    if (int.TryParse(this.follow_user_id_2, out result))
      id_list.Add(result);
    if (int.TryParse(this.follow_user_id_3, out result))
      id_list.Add(result);
    if (id_list.Count == 0)
      return;
    MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(id_list, (Action<Error, List<int>>) ((err, follow_list) =>
    {
      if (err == Error.None)
      {
        if (follow_list.Count <= 0)
          return;
        string names = string.Empty;
        follow_list.ForEach((Action<int>) (user_id => names = $"{names}「{(object) user_id}」\n"));
        this.DispatchEvent("SUCCESS", (object) (names + "をフォローしました"));
      }
      else
        this.DispatchEvent("SUCCESS", (object) "エラーが発生しました");
    }));
  }
}
