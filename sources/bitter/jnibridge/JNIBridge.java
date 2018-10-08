package bitter.jnibridge;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

public class JNIBridge {

    /* renamed from: bitter.jnibridge.JNIBridge$a */
    private static final class C0165a implements InvocationHandler {
        /* renamed from: a */
        private Object f10a = new Object[0];
        /* renamed from: b */
        private long f11b;

        public C0165a(long j) {
            this.f11b = j;
        }

        /* renamed from: a */
        public final void m15a() {
            synchronized (this.f10a) {
                this.f11b = 0;
            }
        }

        public final void finalize() {
            synchronized (this.f10a) {
                if (this.f11b == 0) {
                    return;
                }
                JNIBridge.delete(this.f11b);
            }
        }

        public final Object invoke(Object obj, Method method, Object[] objArr) {
            Object obj2;
            synchronized (this.f10a) {
                if (this.f11b == 0) {
                    obj2 = null;
                } else {
                    obj2 = JNIBridge.invoke(this.f11b, method.getDeclaringClass(), method, objArr);
                }
            }
            return obj2;
        }
    }

    static native void delete(long j);

    static void disableInterfaceProxy(Object obj) {
        ((C0165a) Proxy.getInvocationHandler(obj)).m15a();
    }

    static native Object invoke(long j, Class cls, Method method, Object[] objArr);

    static Object newInterfaceProxy(long j, Class[] clsArr) {
        return Proxy.newProxyInstance(JNIBridge.class.getClassLoader(), clsArr, new C0165a(j));
    }
}
