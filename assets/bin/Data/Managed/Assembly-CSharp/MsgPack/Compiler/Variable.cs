// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.Variable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Reflection.Emit;

#nullable disable
namespace MsgPack.Compiler;

public class Variable
{
  private Variable(VariableType type, int index)
  {
    this.VarType = type;
    this.Index = index;
  }

  public static Variable CreateLocal(LocalBuilder local)
  {
    return new Variable(VariableType.Local, local.LocalIndex);
  }

  public static Variable CreateArg(int idx) => new Variable(VariableType.Arg, idx);

  public VariableType VarType { get; set; }

  public int Index { get; set; }
}
