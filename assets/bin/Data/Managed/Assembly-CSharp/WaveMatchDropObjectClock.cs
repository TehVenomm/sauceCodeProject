// Decompiled with JetBrains decompiler
// Type: WaveMatchDropObjectClock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class WaveMatchDropObjectClock : WaveMatchDropObject
{
  public override void OnPicked(Self self)
  {
    base.OnPicked(self);
    this._AddLimitTime();
  }

  public override void OnReceiveEffect() => this._AddLimitTime();

  private void _AddLimitTime() => WaveMatchDropObjectClock.PickedProcess(this.tableData);

  public static void PickedProcess(WaveMatchDropTable.WaveMatchDropData data)
  {
    MonoBehaviourSingleton<UISimpleAnnounce>.I.Announce(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 2U), string.Format(StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 3U), (object) data.value));
    MonoBehaviourSingleton<InGameProgress>.I.AddLimitTime((float) data.value);
  }
}
