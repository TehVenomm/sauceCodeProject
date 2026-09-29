// Decompiled with JetBrains decompiler
// Type: EventData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class EventData
{
  public string name;
  public object data;

  public EventData(string name)
    : this(name, (object) null)
  {
  }

  public EventData(string _name, object _data)
  {
    this.name = _name;
    this.data = _data;
  }
}
