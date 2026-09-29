// Decompiled with JetBrains decompiler
// Type: MsgPack.CompiledPacker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

#nullable disable
namespace MsgPack;

public class CompiledPacker
{
  private static CompiledPacker.PackerBase _publicFieldPacker = (CompiledPacker.PackerBase) new CompiledPacker.MethodBuilderPacker();
  private static CompiledPacker.PackerBase _allFieldPacker = (CompiledPacker.PackerBase) new CompiledPacker.DynamicMethodPacker();
  private CompiledPacker.PackerBase _packer;

  public CompiledPacker()
    : this(false)
  {
  }

  public CompiledPacker(bool packPrivateField)
  {
    this._packer = packPrivateField ? CompiledPacker._allFieldPacker : CompiledPacker._publicFieldPacker;
  }

  public void Prepare<T>()
  {
    this._packer.CreatePacker<T>();
    this._packer.CreateUnpacker<T>();
  }

  public byte[] Pack<T>(T o)
  {
    using (MemoryStream strm = new MemoryStream())
    {
      this.Pack<T>((Stream) strm, o);
      return strm.ToArray();
    }
  }

  public void Pack<T>(Stream strm, T o)
  {
    this._packer.CreatePacker<T>()(new MsgPackWriter(strm), o);
  }

  public T Unpack<T>(byte[] buf) => this.Unpack<T>(buf, 0, buf.Length);

  public T Unpack<T>(byte[] buf, int offset, int size)
  {
    using (MemoryStream strm = new MemoryStream(buf, offset, size))
      return this.Unpack<T>((Stream) strm);
  }

  public T Unpack<T>(Stream strm) => this._packer.CreateUnpacker<T>()(new MsgPackReader(strm));

  public byte[] Pack(object o)
  {
    using (MemoryStream strm = new MemoryStream())
    {
      this.Pack((Stream) strm, o);
      return strm.ToArray();
    }
  }

  public void Pack(Stream strm, object o) => throw new NotImplementedException();

  public object Unpack(Type t, byte[] buf) => this.Unpack(t, buf, 0, buf.Length);

  public object Unpack(Type t, byte[] buf, int offset, int size)
  {
    using (MemoryStream strm = new MemoryStream(buf, offset, size))
      return this.Unpack(t, (Stream) strm);
  }

  public object Unpack(Type t, Stream strm) => throw new NotImplementedException();

  public abstract class PackerBase
  {
    private Dictionary<Type, Delegate> _packers = new Dictionary<Type, Delegate>();
    private Dictionary<Type, Delegate> _unpackers = new Dictionary<Type, Delegate>();
    protected Dictionary<Type, MethodInfo> _packMethods = new Dictionary<Type, MethodInfo>();
    protected Dictionary<Type, MethodInfo> _unpackMethods = new Dictionary<Type, MethodInfo>();

    protected PackerBase()
    {
      CompiledPacker.DefaultPackMethods.Register(this._packMethods, this._unpackMethods);
    }

    public Action<MsgPackWriter, T> CreatePacker<T>()
    {
      Delegate packerInternal;
      lock (this._packers)
      {
        if (!this._packers.TryGetValue(typeof (T), out packerInternal))
        {
          packerInternal = (Delegate) this.CreatePacker_Internal<T>();
          this._packers.Add(typeof (T), packerInternal);
        }
      }
      return (Action<MsgPackWriter, T>) packerInternal;
    }

    public Func<MsgPackReader, T> CreateUnpacker<T>()
    {
      Delegate unpackerInternal;
      lock (this._unpackers)
      {
        if (!this._unpackers.TryGetValue(typeof (T), out unpackerInternal))
        {
          unpackerInternal = (Delegate) this.CreateUnpacker_Internal<T>();
          this._unpackers.Add(typeof (T), unpackerInternal);
        }
      }
      return (Func<MsgPackReader, T>) unpackerInternal;
    }

    protected abstract Action<MsgPackWriter, T> CreatePacker_Internal<T>();

    protected abstract Func<MsgPackReader, T> CreateUnpacker_Internal<T>();
  }

  public sealed class DynamicMethodPacker : CompiledPacker.PackerBase
  {
    private static MethodInfo LookupMemberMappingMethod;
    private static Dictionary<Type, IDictionary<string, int>> UnpackMemberMappings = new Dictionary<Type, IDictionary<string, int>>();
    private static int _dynamicMethodIdx;

    static DynamicMethodPacker()
    {
      CompiledPacker.DynamicMethodPacker.LookupMemberMappingMethod = typeof (CompiledPacker.DynamicMethodPacker).GetMethod("LookupMemberMapping", BindingFlags.Static | BindingFlags.NonPublic);
    }

    protected override Action<MsgPackWriter, T> CreatePacker_Internal<T>()
    {
      return (Action<MsgPackWriter, T>) this.CreatePacker(typeof (T), CompiledPacker.DynamicMethodPacker.CreatePackDynamicMethod(typeof (T))).CreateDelegate(typeof (Action<MsgPackWriter, T>));
    }

    protected override Func<MsgPackReader, T> CreateUnpacker_Internal<T>()
    {
      return (Func<MsgPackReader, T>) this.CreateUnpacker(typeof (T), CompiledPacker.DynamicMethodPacker.CreateUnpackDynamicMethod(typeof (T))).CreateDelegate(typeof (Func<MsgPackReader, T>));
    }

    private DynamicMethod CreatePacker(Type t, DynamicMethod dm)
    {
      ILGenerator ilGenerator = dm.GetILGenerator();
      this._packMethods.Add(t, (MethodInfo) dm);
      PackILGenerator.EmitPackCode(t, (MethodInfo) dm, ilGenerator, new Func<Type, MemberInfo[]>(CompiledPacker.DynamicMethodPacker.LookupMembers), new Func<MemberInfo, string>(CompiledPacker.DynamicMethodPacker.FormatMemberName), new Func<Type, MethodInfo>(this.LookupPackMethod));
      return dm;
    }

    private DynamicMethod CreateUnpacker(Type t, DynamicMethod dm)
    {
      ILGenerator ilGenerator = dm.GetILGenerator();
      this._unpackMethods.Add(t, (MethodInfo) dm);
      PackILGenerator.EmitUnpackCode(t, (MethodInfo) dm, ilGenerator, new Func<Type, MemberInfo[]>(CompiledPacker.DynamicMethodPacker.LookupMembers), new Func<MemberInfo, string>(CompiledPacker.DynamicMethodPacker.FormatMemberName), new Func<Type, MethodInfo>(this.LookupUnpackMethod), new Func<Type, IDictionary<string, int>>(CompiledPacker.DynamicMethodPacker.LookupMemberMapping), CompiledPacker.DynamicMethodPacker.LookupMemberMappingMethod);
      return dm;
    }

    private static DynamicMethod CreatePackDynamicMethod(Type t)
    {
      return CompiledPacker.DynamicMethodPacker.CreateDynamicMethod(typeof (void), new Type[2]
      {
        typeof (MsgPackWriter),
        t
      });
    }

    private static DynamicMethod CreateUnpackDynamicMethod(Type t)
    {
      return CompiledPacker.DynamicMethodPacker.CreateDynamicMethod(t, new Type[1]
      {
        typeof (MsgPackReader)
      });
    }

    private static MemberInfo[] LookupMembers(Type t)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
      List<MemberInfo> memberInfoList = new List<MemberInfo>();
      memberInfoList.AddRange((IEnumerable<MemberInfo>) t.GetFields(bindingAttr));
      return memberInfoList.ToArray();
    }

    private MethodInfo LookupPackMethod(Type t)
    {
      MethodInfo methodInfo;
      if (this._packMethods.TryGetValue(t, out methodInfo))
        return methodInfo;
      DynamicMethod packDynamicMethod = CompiledPacker.DynamicMethodPacker.CreatePackDynamicMethod(t);
      return (MethodInfo) this.CreatePacker(t, packDynamicMethod);
    }

    private MethodInfo LookupUnpackMethod(Type t)
    {
      MethodInfo methodInfo;
      if (this._unpackMethods.TryGetValue(t, out methodInfo))
        return methodInfo;
      DynamicMethod unpackDynamicMethod = CompiledPacker.DynamicMethodPacker.CreateUnpackDynamicMethod(t);
      return (MethodInfo) this.CreateUnpacker(t, unpackDynamicMethod);
    }

    private static string FormatMemberName(MemberInfo m)
    {
      if (m.MemberType != MemberTypes.Field)
        return m.Name;
      string str = m.Name;
      int num;
      if (str[0] == '<' && (num = str.IndexOf('>')) > 1)
        str = str.Substring(1, num - 1);
      return str;
    }

    private static DynamicMethod CreateDynamicMethod(Type returnType, Type[] parameterTypes)
    {
      return new DynamicMethod("_" + Interlocked.Increment(ref CompiledPacker.DynamicMethodPacker._dynamicMethodIdx).ToString(), returnType, parameterTypes, true);
    }

    internal static IDictionary<string, int> LookupMemberMapping(Type t)
    {
      IDictionary<string, int> dictionary;
      lock (CompiledPacker.DynamicMethodPacker.UnpackMemberMappings)
      {
        if (!CompiledPacker.DynamicMethodPacker.UnpackMemberMappings.TryGetValue(t, out dictionary))
        {
          dictionary = (IDictionary<string, int>) new Dictionary<string, int>();
          CompiledPacker.DynamicMethodPacker.UnpackMemberMappings.Add(t, dictionary);
        }
      }
      return dictionary;
    }
  }

  public sealed class MethodBuilderPacker : CompiledPacker.PackerBase
  {
    public const string AssemblyName = "MessagePackInternalAssembly";
    private static AssemblyName DynamicAsmName;
    private static AssemblyBuilder DynamicAsmBuilder;
    private static ModuleBuilder DynamicModuleBuilder;
    private static MethodInfo LookupMemberMappingMethod;
    private static Dictionary<Type, IDictionary<string, int>> UnpackMemberMappings = new Dictionary<Type, IDictionary<string, int>>();

    static MethodBuilderPacker()
    {
      CompiledPacker.MethodBuilderPacker.LookupMemberMappingMethod = typeof (CompiledPacker.MethodBuilderPacker).GetMethod("LookupMemberMapping", BindingFlags.Static | BindingFlags.NonPublic);
      CompiledPacker.MethodBuilderPacker.DynamicAsmName = new AssemblyName("MessagePackInternalAssembly");
      CompiledPacker.MethodBuilderPacker.DynamicAsmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(CompiledPacker.MethodBuilderPacker.DynamicAsmName, AssemblyBuilderAccess.Run);
      CompiledPacker.MethodBuilderPacker.DynamicModuleBuilder = CompiledPacker.MethodBuilderPacker.DynamicAsmBuilder.DefineDynamicModule(CompiledPacker.MethodBuilderPacker.DynamicAsmName.Name);
    }

    protected override Action<MsgPackWriter, T> CreatePacker_Internal<T>()
    {
      TypeBuilder tb;
      MethodBuilder mb;
      CompiledPacker.MethodBuilderPacker.CreatePackMethodBuilder(typeof (T), out tb, out mb);
      this._packMethods.Add(typeof (T), (MethodInfo) mb);
      this.CreatePacker(typeof (T), mb);
      return (Action<MsgPackWriter, T>) Delegate.CreateDelegate(typeof (Action<MsgPackWriter, T>), this.ToCallableMethodInfo(typeof (T), tb, true));
    }

    protected override Func<MsgPackReader, T> CreateUnpacker_Internal<T>()
    {
      TypeBuilder tb;
      MethodBuilder mb;
      CompiledPacker.MethodBuilderPacker.CreateUnpackMethodBuilder(typeof (T), out tb, out mb);
      this._unpackMethods.Add(typeof (T), (MethodInfo) mb);
      this.CreateUnpacker(typeof (T), mb);
      return (Func<MsgPackReader, T>) Delegate.CreateDelegate(typeof (Func<MsgPackReader, T>), this.ToCallableMethodInfo(typeof (T), tb, false));
    }

    private void CreatePacker(Type t, MethodBuilder mb)
    {
      ILGenerator ilGenerator = mb.GetILGenerator();
      PackILGenerator.EmitPackCode(t, (MethodInfo) mb, ilGenerator, new Func<Type, MemberInfo[]>(CompiledPacker.MethodBuilderPacker.LookupMembers), new Func<MemberInfo, string>(CompiledPacker.MethodBuilderPacker.FormatMemberName), new Func<Type, MethodInfo>(this.LookupPackMethod));
    }

    private void CreateUnpacker(Type t, MethodBuilder mb)
    {
      ILGenerator ilGenerator = mb.GetILGenerator();
      PackILGenerator.EmitUnpackCode(t, (MethodInfo) mb, ilGenerator, new Func<Type, MemberInfo[]>(CompiledPacker.MethodBuilderPacker.LookupMembers), new Func<MemberInfo, string>(CompiledPacker.MethodBuilderPacker.FormatMemberName), new Func<Type, MethodInfo>(this.LookupUnpackMethod), new Func<Type, IDictionary<string, int>>(CompiledPacker.MethodBuilderPacker.LookupMemberMapping), CompiledPacker.MethodBuilderPacker.LookupMemberMappingMethod);
    }

    private MethodInfo ToCallableMethodInfo(Type t, TypeBuilder tb, bool isPacker)
    {
      MethodInfo method = tb.CreateType().GetMethod(isPacker ? "Pack" : "Unpack", BindingFlags.Static | BindingFlags.Public);
      if (isPacker)
        this._packMethods[t] = method;
      else
        this._unpackMethods[t] = method;
      return method;
    }

    private MethodInfo LookupPackMethod(Type t)
    {
      MethodInfo methodInfo;
      if (this._packMethods.TryGetValue(t, out methodInfo))
        return methodInfo;
      TypeBuilder tb;
      MethodBuilder mb;
      CompiledPacker.MethodBuilderPacker.CreatePackMethodBuilder(t, out tb, out mb);
      this._packMethods.Add(t, (MethodInfo) mb);
      this.CreatePacker(t, mb);
      return this.ToCallableMethodInfo(t, tb, true);
    }

    private MethodInfo LookupUnpackMethod(Type t)
    {
      MethodInfo methodInfo;
      if (this._unpackMethods.TryGetValue(t, out methodInfo))
        return methodInfo;
      TypeBuilder tb;
      MethodBuilder mb;
      CompiledPacker.MethodBuilderPacker.CreateUnpackMethodBuilder(t, out tb, out mb);
      this._unpackMethods.Add(t, (MethodInfo) mb);
      this.CreateUnpacker(t, mb);
      return this.ToCallableMethodInfo(t, tb, false);
    }

    private static string FormatMemberName(MemberInfo m) => m.Name;

    private static MemberInfo[] LookupMembers(Type t)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
      List<MemberInfo> memberInfoList = new List<MemberInfo>();
      memberInfoList.AddRange((IEnumerable<MemberInfo>) t.GetFields(bindingAttr));
      return memberInfoList.ToArray();
    }

    private static void CreatePackMethodBuilder(Type t, out TypeBuilder tb, out MethodBuilder mb)
    {
      tb = CompiledPacker.MethodBuilderPacker.DynamicModuleBuilder.DefineType(t.Name + "PackerType", TypeAttributes.Public);
      mb = tb.DefineMethod("Pack", MethodAttributes.Public | MethodAttributes.Static, typeof (void), new Type[2]
      {
        typeof (MsgPackWriter),
        t
      });
    }

    private static void CreateUnpackMethodBuilder(Type t, out TypeBuilder tb, out MethodBuilder mb)
    {
      tb = CompiledPacker.MethodBuilderPacker.DynamicModuleBuilder.DefineType(t.Name + "UnpackerType", TypeAttributes.Public);
      mb = tb.DefineMethod("Unpack", MethodAttributes.Public | MethodAttributes.Static, t, new Type[1]
      {
        typeof (MsgPackReader)
      });
    }

    internal static IDictionary<string, int> LookupMemberMapping(Type t)
    {
      IDictionary<string, int> dictionary;
      lock (CompiledPacker.MethodBuilderPacker.UnpackMemberMappings)
      {
        if (!CompiledPacker.MethodBuilderPacker.UnpackMemberMappings.TryGetValue(t, out dictionary))
        {
          dictionary = (IDictionary<string, int>) new Dictionary<string, int>();
          CompiledPacker.MethodBuilderPacker.UnpackMemberMappings.Add(t, dictionary);
        }
      }
      return dictionary;
    }
  }

  internal static class DefaultPackMethods
  {
    public static void Register(
      Dictionary<Type, MethodInfo> packMethods,
      Dictionary<Type, MethodInfo> unpackMethods)
    {
      CompiledPacker.DefaultPackMethods.RegisterPackMethods(packMethods);
      CompiledPacker.DefaultPackMethods.RegisterUnpackMethods(unpackMethods);
    }

    private static void RegisterPackMethods(Dictionary<Type, MethodInfo> packMethods)
    {
      MethodInfo[] methods = typeof (CompiledPacker.DefaultPackMethods).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
      string str = "Pack";
      for (int index = 0; index < methods.Length; ++index)
      {
        if (str.Equals(methods[index].Name))
        {
          ParameterInfo[] parameters = methods[index].GetParameters();
          if (parameters.Length == 2 && !(parameters[0].ParameterType != typeof (MsgPackWriter)))
            packMethods.Add(parameters[1].ParameterType, methods[index]);
        }
      }
    }

    internal static void Pack(MsgPackWriter writer, string x)
    {
      if (x == null)
        writer.WriteNil();
      else
        writer.Write(x, false);
    }

    private static void RegisterUnpackMethods(Dictionary<Type, MethodInfo> unpackMethods)
    {
      BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.NonPublic;
      Type type = typeof (CompiledPacker.DefaultPackMethods);
      MethodInfo method1 = type.GetMethod("Unpack_Signed", bindingAttr);
      unpackMethods.Add(typeof (sbyte), method1);
      unpackMethods.Add(typeof (short), method1);
      unpackMethods.Add(typeof (int), method1);
      MethodInfo method2 = type.GetMethod("Unpack_Signed64", bindingAttr);
      unpackMethods.Add(typeof (long), method2);
      MethodInfo method3 = type.GetMethod("Unpack_Unsigned", bindingAttr);
      unpackMethods.Add(typeof (byte), method3);
      unpackMethods.Add(typeof (ushort), method3);
      unpackMethods.Add(typeof (char), method3);
      unpackMethods.Add(typeof (uint), method3);
      MethodInfo method4 = type.GetMethod("Unpack_Unsigned64", bindingAttr);
      unpackMethods.Add(typeof (ulong), method4);
      MethodInfo method5 = type.GetMethod("Unpack_Boolean", bindingAttr);
      unpackMethods.Add(typeof (bool), method5);
      MethodInfo method6 = type.GetMethod("Unpack_Float", bindingAttr);
      unpackMethods.Add(typeof (float), method6);
      MethodInfo method7 = type.GetMethod("Unpack_Double", bindingAttr);
      unpackMethods.Add(typeof (double), method7);
      MethodInfo method8 = type.GetMethod("Unpack_String", bindingAttr);
      unpackMethods.Add(typeof (string), method8);
    }

    internal static int Unpack_Signed(MsgPackReader reader)
    {
      if (!reader.Read() || !reader.IsSigned())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ValueSigned;
    }

    internal static long Unpack_Signed64(MsgPackReader reader)
    {
      if (!reader.Read())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      if (reader.IsSigned())
        return (long) reader.ValueSigned;
      if (reader.IsSigned64())
        return reader.ValueSigned64;
      CompiledPacker.DefaultPackMethods.UnpackFailed();
      return 0;
    }

    internal static uint Unpack_Unsigned(MsgPackReader reader)
    {
      if (!reader.Read() || !reader.IsUnsigned())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ValueUnsigned;
    }

    internal static ulong Unpack_Unsigned64(MsgPackReader reader)
    {
      if (!reader.Read())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      if (reader.IsUnsigned())
        return (ulong) reader.ValueUnsigned;
      if (reader.IsUnsigned64())
        return reader.ValueUnsigned64;
      CompiledPacker.DefaultPackMethods.UnpackFailed();
      return 0;
    }

    internal static bool Unpack_Boolean(MsgPackReader reader)
    {
      if (!reader.Read() || !reader.IsBoolean())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ValueBoolean;
    }

    internal static float Unpack_Float(MsgPackReader reader)
    {
      if (!reader.Read() || reader.Type != TypePrefixes.Float)
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ValueFloat;
    }

    internal static double Unpack_Double(MsgPackReader reader)
    {
      if (!reader.Read() || reader.Type != TypePrefixes.Double)
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ValueDouble;
    }

    internal static string Unpack_String(MsgPackReader reader)
    {
      if (!reader.Read() || !reader.IsRaw())
        CompiledPacker.DefaultPackMethods.UnpackFailed();
      return reader.ReadRawString();
    }

    internal static void UnpackFailed() => throw new FormatException();
  }
}
