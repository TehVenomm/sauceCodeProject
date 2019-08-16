package com.unity3d.player;

import java.lang.reflect.Array;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Member;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.lang.reflect.Proxy;
import java.util.ArrayList;
import java.util.Iterator;
import org.apache.commons.lang3.ClassUtils;

final class ReflectionHelper {
    protected static boolean LOG = false;
    protected static final boolean LOGV = false;

    /* renamed from: a */
    private static C1064a[] f445a = new C1064a[4096];

    /* renamed from: com.unity3d.player.ReflectionHelper$a */
    private static final class C1064a {

        /* renamed from: a */
        public volatile Member f448a;

        /* renamed from: b */
        private final Class f449b;

        /* renamed from: c */
        private final String f450c;

        /* renamed from: d */
        private final String f451d;

        /* renamed from: e */
        private final int f452e = (((((this.f449b.hashCode() + 527) * 31) + this.f450c.hashCode()) * 31) + this.f451d.hashCode());

        C1064a(Class cls, String str, String str2) {
            this.f449b = cls;
            this.f450c = str;
            this.f451d = str2;
        }

        public final boolean equals(Object obj) {
            if (obj == this) {
                return true;
            }
            if (!(obj instanceof C1064a)) {
                return false;
            }
            C1064a aVar = (C1064a) obj;
            return this.f452e == aVar.f452e && this.f451d.equals(aVar.f451d) && this.f450c.equals(aVar.f450c) && this.f449b.equals(aVar.f449b);
        }

        public final int hashCode() {
            return this.f452e;
        }
    }

    ReflectionHelper() {
    }

    /* renamed from: a */
    private static float m443a(Class cls, Class cls2) {
        if (cls.equals(cls2)) {
            return 1.0f;
        }
        if (!cls.isPrimitive() && !cls2.isPrimitive()) {
            try {
                if (cls.asSubclass(cls2) != null) {
                    return 0.5f;
                }
            } catch (ClassCastException e) {
            }
            try {
                if (cls2.asSubclass(cls) != null) {
                    return 0.1f;
                }
            } catch (ClassCastException e2) {
            }
        }
        return 0.0f;
    }

    /* renamed from: a */
    private static float m444a(Class cls, Class[] clsArr, Class[] clsArr2) {
        if (clsArr2.length == 0) {
            return 0.1f;
        }
        if ((clsArr == null ? 0 : clsArr.length) + 1 != clsArr2.length) {
            return 0.0f;
        }
        float f = 1.0f;
        if (clsArr != null) {
            int i = 0;
            for (Class a : clsArr) {
                f *= m443a(a, clsArr2[i]);
                i++;
            }
        }
        return f * m443a(cls, clsArr2[clsArr2.length - 1]);
    }

    /* renamed from: a */
    private static Class m445a(String str, int[] iArr) {
        while (true) {
            if (iArr[0] >= str.length()) {
                break;
            }
            int i = iArr[0];
            iArr[0] = i + 1;
            char charAt = str.charAt(i);
            if (charAt != '(' && charAt != ')') {
                if (charAt == 'L') {
                    int indexOf = str.indexOf(59, iArr[0]);
                    if (indexOf != -1) {
                        String substring = str.substring(iArr[0], indexOf);
                        iArr[0] = indexOf + 1;
                        try {
                            return Class.forName(substring.replace('/', ClassUtils.PACKAGE_SEPARATOR_CHAR));
                        } catch (ClassNotFoundException e) {
                        }
                    }
                } else if (charAt == 'Z') {
                    return Boolean.TYPE;
                } else {
                    if (charAt == 'I') {
                        return Integer.TYPE;
                    }
                    if (charAt == 'F') {
                        return Float.TYPE;
                    }
                    if (charAt == 'V') {
                        return Void.TYPE;
                    }
                    if (charAt == 'B') {
                        return Byte.TYPE;
                    }
                    if (charAt == 'S') {
                        return Short.TYPE;
                    }
                    if (charAt == 'J') {
                        return Long.TYPE;
                    }
                    if (charAt == 'D') {
                        return Double.TYPE;
                    }
                    if (charAt == '[') {
                        return Array.newInstance(m445a(str, iArr), 0).getClass();
                    }
                    C1104e.Log(5, "! parseType; " + charAt + " is not known!");
                }
            }
        }
        return null;
    }

    /* renamed from: a */
    private static void m448a(C1064a aVar, Member member) {
        aVar.f448a = member;
        f445a[aVar.hashCode() & (f445a.length - 1)] = aVar;
    }

    /* renamed from: a */
    private static boolean m449a(C1064a aVar) {
        C1064a aVar2 = f445a[aVar.hashCode() & (f445a.length - 1)];
        if (!aVar.equals(aVar2)) {
            return false;
        }
        aVar.f448a = aVar2.f448a;
        return true;
    }

    /* renamed from: a */
    private static Class[] m450a(String str) {
        int i = 0;
        int[] iArr = {0};
        ArrayList arrayList = new ArrayList();
        while (iArr[0] < str.length()) {
            Class a = m445a(str, iArr);
            if (a == null) {
                break;
            }
            arrayList.add(a);
        }
        Class[] clsArr = new Class[arrayList.size()];
        Iterator it = arrayList.iterator();
        while (true) {
            int i2 = i;
            if (!it.hasNext()) {
                return clsArr;
            }
            clsArr[i2] = (Class) it.next();
            i = i2 + 1;
        }
    }

    protected static Constructor getConstructorID(Class cls, String str) {
        Constructor constructor;
        Constructor constructor2 = null;
        C1064a aVar = new C1064a(cls, "", str);
        if (m449a(aVar)) {
            constructor = (Constructor) aVar.f448a;
        } else {
            Class[] a = m450a(str);
            float f = 0.0f;
            Constructor[] constructors = cls.getConstructors();
            int length = constructors.length;
            int i = 0;
            while (true) {
                if (i >= length) {
                    constructor = constructor2;
                    break;
                }
                constructor = constructors[i];
                float a2 = m444a(Void.TYPE, constructor.getParameterTypes(), a);
                if (a2 > f) {
                    if (a2 == 1.0f) {
                        break;
                    }
                } else {
                    constructor = constructor2;
                    a2 = f;
                }
                i++;
                constructor2 = constructor;
                f = a2;
            }
            m448a(aVar, (Member) constructor);
        }
        if (constructor != null) {
            return constructor;
        }
        throw new NoSuchMethodError("<init>" + str + " in class " + cls.getName());
    }

    protected static Field getFieldID(Class cls, String str, String str2, boolean z) {
        Field field;
        float f;
        C1064a aVar = new C1064a(cls, str, str2);
        if (m449a(aVar)) {
            field = (Field) aVar.f448a;
        } else {
            Class[] a = m450a(str2);
            field = null;
            float f2 = 0.0f;
            while (cls != null) {
                Field[] declaredFields = cls.getDeclaredFields();
                int length = declaredFields.length;
                int i = 0;
                float f3 = f2;
                Field field2 = field;
                while (true) {
                    if (i >= length) {
                        f2 = f3;
                        field = field2;
                        break;
                    }
                    Field field3 = declaredFields[i];
                    if (z == Modifier.isStatic(field3.getModifiers()) && field3.getName().compareTo(str) == 0) {
                        f2 = m444a(field3.getType(), (Class[]) null, a);
                        if (f2 > f3) {
                            if (f2 == 1.0f) {
                                field = field3;
                                break;
                            }
                            f = f2;
                            i++;
                            f3 = f;
                            field2 = field3;
                        }
                    }
                    f = f3;
                    field3 = field2;
                    i++;
                    f3 = f;
                    field2 = field3;
                }
                if (f2 == 1.0f || cls.isPrimitive() || cls.isInterface() || cls.equals(Object.class) || cls.equals(Void.TYPE)) {
                    break;
                }
                cls = cls.getSuperclass();
            }
            m448a(aVar, (Member) field);
        }
        if (field != null) {
            return field;
        }
        throw new NoSuchFieldError(String.format("no %s field with name='%s' signature='%s' in class L%s;", new Object[]{z ? "static" : "non-static", str, str2, cls.getName()}));
    }

    protected static Method getMethodID(Class cls, String str, String str2, boolean z) {
        Method method;
        float f;
        C1064a aVar = new C1064a(cls, str, str2);
        if (m449a(aVar)) {
            method = (Method) aVar.f448a;
        } else {
            Class[] a = m450a(str2);
            method = null;
            float f2 = 0.0f;
            while (cls != null) {
                Method[] declaredMethods = cls.getDeclaredMethods();
                int length = declaredMethods.length;
                int i = 0;
                float f3 = f2;
                Method method2 = method;
                while (true) {
                    if (i >= length) {
                        f2 = f3;
                        method = method2;
                        break;
                    }
                    Method method3 = declaredMethods[i];
                    if (z == Modifier.isStatic(method3.getModifiers()) && method3.getName().compareTo(str) == 0) {
                        f2 = m444a(method3.getReturnType(), method3.getParameterTypes(), a);
                        if (f2 > f3) {
                            if (f2 == 1.0f) {
                                method = method3;
                                break;
                            }
                            f = f2;
                            i++;
                            f3 = f;
                            method2 = method3;
                        }
                    }
                    f = f3;
                    method3 = method2;
                    i++;
                    f3 = f;
                    method2 = method3;
                }
                if (f2 == 1.0f || cls.isPrimitive() || cls.isInterface() || cls.equals(Object.class) || cls.equals(Void.TYPE)) {
                    break;
                }
                cls = cls.getSuperclass();
            }
            m448a(aVar, (Member) method);
        }
        if (method != null) {
            return method;
        }
        throw new NoSuchMethodError(String.format("no %s method with name='%s' signature='%s' in class L%s;", new Object[]{z ? "static" : "non-static", str, str2, cls.getName()}));
    }

    /* access modifiers changed from: private */
    public static native void nativeProxyFinalize(int i);

    /* access modifiers changed from: private */
    public static native Object nativeProxyInvoke(int i, String str, Object[] objArr);

    protected static Object newProxyInstance(int i, Class cls) {
        return newProxyInstance(i, new Class[]{cls});
    }

    protected static Object newProxyInstance(final int i, final Class[] clsArr) {
        return Proxy.newProxyInstance(ReflectionHelper.class.getClassLoader(), clsArr, new InvocationHandler() {
            /* access modifiers changed from: protected */
            public final void finalize() {
                try {
                    ReflectionHelper.nativeProxyFinalize(i);
                } finally {
                    super.finalize();
                }
            }

            public final Object invoke(Object obj, Method method, Object[] objArr) {
                return ReflectionHelper.nativeProxyInvoke(i, method.getName(), objArr);
            }
        });
    }
}
