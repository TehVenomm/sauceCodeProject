using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Google.Developers
{
	public class JavaObjWrapper
	{
		private IntPtr raw;

		private IntPtr cachedRawClass = IntPtr.Zero;

		public IntPtr RawObject => raw;

		public virtual IntPtr RawClass
		{
			get
			{
				if (cachedRawClass == IntPtr.Zero && raw != IntPtr.Zero)
				{
					cachedRawClass = AndroidJNI.GetObjectClass(raw);
				}
				return cachedRawClass;
			}
		}

		protected JavaObjWrapper()
		{
		}

		public JavaObjWrapper(string clazzName)
		{
			raw = AndroidJNI.AllocObject(AndroidJNI.FindClass(clazzName));
		}

		public JavaObjWrapper(IntPtr rawObject)
		{
			raw = rawObject;
		}

		public void CreateInstance(string clazzName, params object[] args)
		{
			if (raw != IntPtr.Zero)
			{
				throw new Exception("Java object already set");
			}
			IntPtr constructorID = AndroidJNIHelper.GetConstructorID(RawClass, args);
			jvalue[] array = ConstructArgArray(args);
			raw = AndroidJNI.NewObject(RawClass, constructorID, array);
		}

		protected static jvalue[] ConstructArgArray(object[] theArgs)
		{
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			object[] array = new object[theArgs.Length];
			for (int i = 0; i < theArgs.Length; i++)
			{
				if (theArgs[i] is JavaObjWrapper)
				{
					array[i] = ((JavaObjWrapper)theArgs[i]).raw;
				}
				else
				{
					array[i] = theArgs[i];
				}
			}
			jvalue[] array2 = AndroidJNIHelper.CreateJNIArgArray(array);
			for (int j = 0; j < theArgs.Length; j++)
			{
				if (theArgs[j] is JavaObjWrapper)
				{
					array2[j].l = ((JavaObjWrapper)theArgs[j]).raw;
				}
				else if (theArgs[j] is JavaInterfaceProxy)
				{
					IntPtr l = AndroidJNIHelper.CreateJavaProxy(theArgs[j]);
					array2[j].l = l;
				}
			}
			if (array2.Length == 1)
			{
				for (int k = 0; k < array2.Length; k++)
				{
					Debug.Log((object)("---- [" + k + "] -- " + array2[k].l));
				}
			}
			return array2;
		}

		public static T StaticInvokeObjectCall<T>(string type, string name, string sig, params object[] args)
		{
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = ConstructArgArray(args);
			IntPtr intPtr2 = AndroidJNI.CallStaticObjectMethod(intPtr, staticMethodID, array);
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[1]
			{
				intPtr2.GetType()
			});
			if (constructor != null)
			{
				return (T)constructor.Invoke(new object[1]
				{
					intPtr2
				});
			}
			if (typeof(T).IsArray)
			{
				return AndroidJNIHelper.ConvertFromJNIArray<T>(intPtr2);
			}
			Debug.Log((object)"Trying cast....");
			Type typeFromHandle = typeof(T);
			return (T)Marshal.PtrToStructure(intPtr2, typeFromHandle);
		}

		public static void StaticInvokeCallVoid(string type, string name, string sig, params object[] args)
		{
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = ConstructArgArray(args);
			AndroidJNI.CallStaticVoidMethod(intPtr, staticMethodID, array);
		}

		public static T GetStaticObjectField<T>(string clsName, string name, string sig)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, sig);
			IntPtr staticObjectField = AndroidJNI.GetStaticObjectField(intPtr, staticFieldID);
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[1]
			{
				staticObjectField.GetType()
			});
			if (constructor != null)
			{
				return (T)constructor.Invoke(new object[1]
				{
					staticObjectField
				});
			}
			Type typeFromHandle = typeof(T);
			return (T)Marshal.PtrToStructure(staticObjectField, typeFromHandle);
		}

		public static int GetStaticIntField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "I");
			return AndroidJNI.GetStaticIntField(intPtr, staticFieldID);
		}

		public static string GetStaticStringField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "Ljava/lang/String;");
			return AndroidJNI.GetStaticStringField(intPtr, staticFieldID);
		}

		public static float GetStaticFloatField(string clsName, string name)
		{
			IntPtr intPtr = AndroidJNI.FindClass(clsName);
			IntPtr staticFieldID = AndroidJNI.GetStaticFieldID(intPtr, name, "F");
			return AndroidJNI.GetStaticFloatField(intPtr, staticFieldID);
		}

		public void InvokeCallVoid(string name, string sig, params object[] args)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(RawClass, name, sig);
			jvalue[] array = ConstructArgArray(args);
			AndroidJNI.CallVoidMethod(raw, methodID, array);
		}

		public T InvokeCall<T>(string name, string sig, params object[] args)
		{
			Type typeFromHandle = typeof(T);
			IntPtr methodID = AndroidJNI.GetMethodID(RawClass, name, sig);
			jvalue[] array = ConstructArgArray(args);
			if (methodID == IntPtr.Zero)
			{
				Debug.LogError((object)("Cannot get method for " + name));
				throw new Exception("Cannot get method for " + name);
			}
			if (typeFromHandle == typeof(bool))
			{
				return (T)(object)AndroidJNI.CallBooleanMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(string))
			{
				return (T)(object)AndroidJNI.CallStringMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(int))
			{
				return (T)(object)AndroidJNI.CallIntMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(float))
			{
				return (T)(object)AndroidJNI.CallFloatMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(double))
			{
				return (T)(object)AndroidJNI.CallDoubleMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(byte))
			{
				return (T)(object)AndroidJNI.CallByteMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(char))
			{
				return (T)(object)AndroidJNI.CallCharMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(long))
			{
				return (T)(object)AndroidJNI.CallLongMethod(raw, methodID, array);
			}
			if (typeFromHandle == typeof(short))
			{
				return (T)(object)AndroidJNI.CallShortMethod(raw, methodID, array);
			}
			return InvokeObjectCall<T>(name, sig, args);
		}

		public static T StaticInvokeCall<T>(string type, string name, string sig, params object[] args)
		{
			Type typeFromHandle = typeof(T);
			IntPtr intPtr = AndroidJNI.FindClass(type);
			IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr, name, sig);
			jvalue[] array = ConstructArgArray(args);
			if (typeFromHandle == typeof(bool))
			{
				return (T)(object)AndroidJNI.CallStaticBooleanMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(string))
			{
				return (T)(object)AndroidJNI.CallStaticStringMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(int))
			{
				return (T)(object)AndroidJNI.CallStaticIntMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(float))
			{
				return (T)(object)AndroidJNI.CallStaticFloatMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(double))
			{
				return (T)(object)AndroidJNI.CallStaticDoubleMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(byte))
			{
				return (T)(object)AndroidJNI.CallStaticByteMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(char))
			{
				return (T)(object)AndroidJNI.CallStaticCharMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(long))
			{
				return (T)(object)AndroidJNI.CallStaticLongMethod(intPtr, staticMethodID, array);
			}
			if (typeFromHandle == typeof(short))
			{
				return (T)(object)AndroidJNI.CallStaticShortMethod(intPtr, staticMethodID, array);
			}
			return StaticInvokeObjectCall<T>(type, name, sig, args);
		}

		public T InvokeObjectCall<T>(string name, string sig, params object[] theArgs)
		{
			IntPtr methodID = AndroidJNI.GetMethodID(RawClass, name, sig);
			jvalue[] array = ConstructArgArray(theArgs);
			IntPtr intPtr = AndroidJNI.CallObjectMethod(raw, methodID, array);
			if (intPtr.Equals(IntPtr.Zero))
			{
				return default(T);
			}
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[1]
			{
				intPtr.GetType()
			});
			if (constructor != null)
			{
				return (T)constructor.Invoke(new object[1]
				{
					intPtr
				});
			}
			Type typeFromHandle = typeof(T);
			return (T)Marshal.PtrToStructure(intPtr, typeFromHandle);
		}
	}
}
