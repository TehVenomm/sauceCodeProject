package im.getsocial.sdk.internal.p047b;

import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.concurrent.Callable;

/* renamed from: im.getsocial.sdk.internal.b.upgqDBbsrL */
public class upgqDBbsrL {
    /* renamed from: a */
    private final jjbQypPegg f1218a;

    @XdbacJlTDQ
    upgqDBbsrL(jjbQypPegg jjbqyppegg) {
        this.f1218a = jjbqyppegg;
    }

    /* renamed from: a */
    public final <T> T m1060a(Class<T> cls, T t) {
        return m1061a(cls, t, null);
    }

    /* renamed from: a */
    public final <T> T m1061a(Class<T> cls, final T t, final Object obj) {
        return Proxy.newProxyInstance(cls.getClassLoader(), new Class[]{cls}, new InvocationHandler(this) {
            /* renamed from: c */
            final /* synthetic */ upgqDBbsrL f1217c;

            public Object invoke(Object obj, final Method method, final Object[] objArr) {
                return this.f1217c.f1218a.m1239a(new Callable<Object>(this) {
                    /* renamed from: c */
                    final /* synthetic */ C09311 f1214c;

                    public Object call() {
                        return t != null ? method.invoke(t, objArr) : obj;
                    }
                }, obj);
            }
        });
    }
}
