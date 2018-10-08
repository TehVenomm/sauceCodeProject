package im.getsocial.sdk.internal.p033c.p066m;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

/* renamed from: im.getsocial.sdk.internal.c.m.cjrhisSQCL */
public final class cjrhisSQCL {

    /* renamed from: im.getsocial.sdk.internal.c.m.cjrhisSQCL$1 */
    static final class C09671 implements InvocationHandler {
        C09671() {
        }

        public final Object invoke(Object obj, Method method, Object... objArr) {
            return null;
        }
    }

    private cjrhisSQCL() {
    }

    /* renamed from: a */
    public static <T> T m1509a(Class<T> cls) {
        return cls.cast(Proxy.newProxyInstance(cls.getClassLoader(), new Class[]{cls}, new C09671()));
    }

    /* renamed from: a */
    public static <T> T m1510a(Class<T> cls, T t) {
        return t == null ? cjrhisSQCL.m1509a(cls) : t;
    }
}
