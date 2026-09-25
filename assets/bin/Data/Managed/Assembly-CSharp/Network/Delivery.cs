// Decompiled with JetBrains decompiler
// Type: Network.Delivery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class Delivery
{
  public string uId;
  public int dId;
  public int type;
  public int mode;
  public string limit;
  public int order;

  public DIFFICULTY_MODE fieldMode => (DIFFICULTY_MODE) this.mode;
}
