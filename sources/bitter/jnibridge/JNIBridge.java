package bitter.jnibridge;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

public class JNIBridge {

    /* renamed from: bitter.jnibridge.JNIBridge$a */
    private static final class C0324a implements InvocationHandler {

        /* renamed from: a */
        private Object f29a = new Object[0];

        /* renamed from: b */
        private long f30b;

        public C0324a(long j) {
            this.f30b = j;
        }

        /* renamed from: a */
        public final void mo6093a() {
            synchronized (this.f29a) {
                this.f30b = 0;
            }
        }

        public final void finalize() {
            synchronized (this.f29a) {
                if (this.f30b != 0) {
                    JNIBridge.delete(this.f30b);
                }
            }
        }

        public final Object invoke(Object obj, Method method, Object[] objArr) {
            Object invoke;
            synchronized (this.f29a) {
                invoke = this.f30b == 0 ? null : JNIBridge.invoke(this.f30b, method.getDeclaringClass(), method, objArr);
            }
            return invoke;
        }
    }

    static native void delete(long j);

    static void disableInterfaceProxy(Object obj) {
        ((C0324a) Proxy.getInvocationHandler(obj)).mo6093a();
    }

    static native Object invoke(long j, Class cls, Method method, Object[] objArr);

    static Object newInterfaceProxy(long j, Class[] clsArr) {
        return Proxy.newProxyInstance(JNIBridge.class.getClassLoader(), clsArr, new C0324a(j));
    }
}
