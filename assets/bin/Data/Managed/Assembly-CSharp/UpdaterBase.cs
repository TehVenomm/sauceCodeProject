// Decompiled with JetBrains decompiler
// Type: UpdaterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public abstract class UpdaterBase : MonoBehaviourSingleton<UpdaterBase>
{
  private BetterList<System.Action> list = new BetterList<System.Action>();

  public static void Add(System.Action update_func)
  {
    if (!MonoBehaviourSingleton<UpdaterBase>.IsValid() || update_func == null)
      return;
    MonoBehaviourSingleton<UpdaterBase>.I.list.Add(update_func);
  }

  public static void Remove(System.Action update_func)
  {
    if (!MonoBehaviourSingleton<UpdaterBase>.IsValid() || update_func == null)
      return;
    MonoBehaviourSingleton<UpdaterBase>.I.list.Remove(update_func);
  }

  private void Update()
  {
    int i = 0;
    for (int size = this.list.size; i < size; ++i)
      this.list[i]();
  }
}
