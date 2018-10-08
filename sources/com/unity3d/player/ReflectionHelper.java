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
    private static C0729a[] f379a = new C0729a[4096];

    /* renamed from: com.unity3d.player.ReflectionHelper$a */
    private static final class C0729a {
        /* renamed from: a */
        public volatile Member f374a;
        /* renamed from: b */
        private final Class f375b;
        /* renamed from: c */
        private final String f376c;
        /* renamed from: d */
        private final String f377d;
        /* renamed from: e */
        private final int f378e = (((((this.f375b.hashCode() + 527) * 31) + this.f376c.hashCode()) * 31) + this.f377d.hashCode());

        C0729a(Class cls, String str, String str2) {
            this.f375b = cls;
            this.f376c = str;
            this.f377d = str2;
        }

        public final boolean equals(Object obj) {
            if (obj == this) {
                return true;
            }
            if (!(obj instanceof C0729a)) {
                return false;
            }
            C0729a c0729a = (C0729a) obj;
            return this.f378e == c0729a.f378e && this.f377d.equals(c0729a.f377d) && this.f376c.equals(c0729a.f376c) && this.f375b.equals(c0729a.f375b);
        }

        public final int hashCode() {
            return this.f378e;
        }
    }

    ReflectionHelper() {
    }

    /* renamed from: a */
    private static float m398a(Class cls, Class cls2) {
        if (cls.equals(cls2)) {
            return 1.0f;
        }
        if (!(cls.isPrimitive() || cls2.isPrimitive())) {
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
    private static float m399a(Class cls, Class[] clsArr, Class[] clsArr2) {
        int i = 0;
        if (clsArr2.length == 0) {
            return 0.1f;
        }
        if ((clsArr == null ? 0 : clsArr.length) + 1 != clsArr2.length) {
            return 0.0f;
        }
        float f = 1.0f;
        if (clsArr != null) {
            int i2 = 0;
            while (i < clsArr.length) {
                float a = m398a(clsArr[i], clsArr2[i2]) * f;
                i++;
                i2++;
                f = a;
            }
        }
        return f * m398a(cls, clsArr2[clsArr2.length - 1]);
    }

    /* renamed from: a */
    private static Class m400a(String str, int[] iArr) {
        while (iArr[0] < str.length()) {
            int i = iArr[0];
            iArr[0] = i + 1;
            char charAt = str.charAt(i);
            if (charAt != '(' && charAt != ')') {
                if (charAt == 'L') {
                    i = str.indexOf(59, iArr[0]);
                    if (i != -1) {
                        String substring = str.substring(iArr[0], i);
                        iArr[0] = i + 1;
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
                        return Array.newInstance(m400a(str, iArr), 0).getClass();
                    }
                    C0767m.Log(5, "! parseType; " + charAt + " is not known!");
                }
                return null;
            }
        }
        return null;
    }

    /* renamed from: a */
    private static void m403a(C0729a c0729a, Member member) {
        c0729a.f374a = member;
        f379a[c0729a.hashCode() & (f379a.length - 1)] = c0729a;
    }

    /* renamed from: a */
    private static boolean m404a(C0729a c0729a) {
        C0729a c0729a2 = f379a[c0729a.hashCode() & (f379a.length - 1)];
        if (!c0729a.equals(c0729a2)) {
            return false;
        }
        c0729a.f374a = c0729a2.f374a;
        return true;
    }

    /* renamed from: a */
    private static Class[] m405a(String str) {
        int[] iArr = new int[]{0};
        ArrayList arrayList = new ArrayList();
        while (iArr[0] < str.length()) {
            Class a = m400a(str, iArr);
            if (a == null) {
                break;
            }
            arrayList.add(a);
        }
        Class[] clsArr = new Class[arrayList.size()];
        Iterator it = arrayList.iterator();
        int i = 0;
        while (it.hasNext()) {
            clsArr[i] = (Class) it.next();
            i++;
        }
        return clsArr;
    }

    protected static Constructor getConstructorID(Class cls, String str) {
        Constructor constructor;
        Constructor constructor2 = null;
        C0729a c0729a = new C0729a(cls, "", str);
        if (m404a(c0729a)) {
            constructor = (Constructor) c0729a.f374a;
        } else {
            Class[] a = m405a(str);
            float f = 0.0f;
            Constructor[] constructors = cls.getConstructors();
            int length = constructors.length;
            int i = 0;
            while (i < length) {
                float f2;
                constructor = constructors[i];
                float a2 = m399a(Void.TYPE, constructor.getParameterTypes(), a);
                if (a2 > f) {
                    if (a2 == 1.0f) {
                        break;
                    }
                    f2 = a2;
                } else {
                    constructor = constructor2;
                    f2 = f;
                }
                i++;
                f = f2;
                constructor2 = constructor;
            }
            constructor = constructor2;
            m403a(c0729a, r0);
        }
        if (constructor != null) {
            return constructor;
        }
        throw new NoSuchMethodError("<init>" + str + " in class " + cls.getName());
    }

    protected static Field getFieldID(Class cls, String str, String str2, boolean z) {
        Field field;
        C0729a c0729a = new C0729a(cls, str, str2);
        if (m404a(c0729a)) {
            field = (Field) c0729a.f374a;
        } else {
            Class[] a = m405a(str2);
            field = null;
            float f = 0.0f;
            while (cls != null) {
                Field[] declaredFields = cls.getDeclaredFields();
                int length = declaredFields.length;
                int i = 0;
                Field field2 = field;
                while (i < length) {
                    float a2;
                    Field field3;
                    Field field4 = declaredFields[i];
                    if (z == Modifier.isStatic(field4.getModifiers()) && field4.getName().compareTo(str) == 0) {
                        a2 = m399a(field4.getType(), null, a);
                        if (a2 > f) {
                            if (a2 == 1.0f) {
                                f = a2;
                                field = field4;
                                break;
                            }
                            field3 = field4;
                            i++;
                            field2 = field3;
                            f = a2;
                        }
                    }
                    a2 = f;
                    field3 = field2;
                    i++;
                    field2 = field3;
                    f = a2;
                }
                field = field2;
                if (f == 1.0f || cls.isPrimitive() || cls.isInterface() || cls.equals(Object.class) || cls.equals(Void.TYPE)) {
                    break;
                }
                cls = cls.getSuperclass();
            }
            m403a(c0729a, r0);
        }
        if (field != null) {
            return field;
        }
        String str3 = z ? "non-static" : "static";
        throw new NoSuchFieldError(String.format("no %s field with name='%s' signature='%s' in class L%s;", new Object[]{str3, str, str2, cls.getName()}));
    }

    protected static Method getMethodID(Class cls, String str, String str2, boolean z) {
        Method method;
        C0729a c0729a = new C0729a(cls, str, str2);
        if (m404a(c0729a)) {
            method = (Method) c0729a.f374a;
        } else {
            Class[] a = m405a(str2);
            method = null;
            float f = 0.0f;
            while (cls != null) {
                Method[] declaredMethods = cls.getDeclaredMethods();
                int length = declaredMethods.length;
                int i = 0;
                Method method2 = method;
                while (i < length) {
                    float a2;
                    Method method3;
                    Method method4 = declaredMethods[i];
                    if (z == Modifier.isStatic(method4.getModifiers()) && method4.getName().compareTo(str) == 0) {
                        a2 = m399a(method4.getReturnType(), method4.getParameterTypes(), a);
                        if (a2 > f) {
                            if (a2 == 1.0f) {
                                f = a2;
                                method = method4;
                                break;
                            }
                            method3 = method4;
                            i++;
                            method2 = method3;
                            f = a2;
                        }
                    }
                    a2 = f;
                    method3 = method2;
                    i++;
                    method2 = method3;
                    f = a2;
                }
                method = method2;
                if (f == 1.0f || cls.isPrimitive() || cls.isInterface() || cls.equals(Object.class) || cls.equals(Void.TYPE)) {
                    break;
                }
                cls = cls.getSuperclass();
            }
            m403a(c0729a, r0);
        }
        if (method != null) {
            return method;
        }
        String str3 = z ? "non-static" : "static";
        throw new NoSuchMethodError(String.format("no %s method with name='%s' signature='%s' in class L%s;", new Object[]{str3, str, str2, cls.getName()}));
    }

    private static native void nativeProxyFinalize(int i);

    private static native Object nativeProxyInvoke(int i, String str, Object[] objArr);

    protected static Object newProxyInstance(int i, Class cls) {
        return newProxyInstance(i, new Class[]{cls});
    }

    protected static Object newProxyInstance(final int i, final Class[] clsArr) {
        return Proxy.newProxyInstance(ReflectionHelper.class.getClassLoader(), clsArr, new InvocationHandler() {
            protected final void finalize() {
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
