package im.getsocial.sdk.invites.p092a.p097j;

import android.content.Context;
import com.facebook.applinks.AppLinkData;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.p092a.p097j.cjrhisSQCL.jjbQypPegg;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import org.json.JSONObject;

/* renamed from: im.getsocial.sdk.invites.a.j.pdwpUtZXDT */
public class pdwpUtZXDT implements cjrhisSQCL {
    /* renamed from: a */
    private static final cjrhisSQCL f2431a = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    /* renamed from: b */
    private final Context f2432b;

    @XdbacJlTDQ
    pdwpUtZXDT(Context context) {
        this.f2432b = context;
    }

    /* renamed from: b */
    private static String m2389b(Object obj) {
        if (obj != null) {
            try {
                Field declaredField = Class.forName("com.facebook.applinks.AppLinkData").getDeclaredField("arguments");
                declaredField.setAccessible(true);
                return ((JSONObject) declaredField.get(obj)).getString(AppLinkData.ARGUMENTS_NATIVE_URL);
            } catch (Throwable e) {
                f2431a.mo4395c(e);
            } catch (Throwable e2) {
                f2431a.mo4395c(e2);
            } catch (Throwable e22) {
                f2431a.mo4395c(e22);
            } catch (Throwable e222) {
                f2431a.mo4395c(e222);
            }
        }
        return null;
    }

    /* renamed from: a */
    public final void mo4572a(final jjbQypPegg jjbqyppegg) {
        try {
            Class[] clsArr = new Class[]{r0};
            Object newProxyInstance = Proxy.newProxyInstance(Class.forName("com.facebook.applinks.AppLinkData$CompletionHandler").getClassLoader(), clsArr, new InvocationHandler(this) {
                /* renamed from: b */
                final /* synthetic */ pdwpUtZXDT f2430b;

                public Object invoke(Object obj, Method method, Object[] objArr) {
                    String a;
                    if (objArr.length == 1) {
                        a = pdwpUtZXDT.m2389b(objArr[0]);
                    } else {
                        pdwpUtZXDT.f2431a.mo4394c("Got unexpected proxy method invocation: %s", method.getName());
                        a = null;
                    }
                    jjbqyppegg.mo4566a(a);
                    return null;
                }
            });
            Class.forName("com.facebook.applinks.AppLinkData").getMethod("fetchDeferredAppLinkData", new Class[]{Context.class, r0}).invoke(null, new Object[]{this.f2432b.getApplicationContext(), newProxyInstance});
        } catch (Throwable e) {
            f2431a.mo4395c(e);
            jjbqyppegg.mo4566a(null);
        } catch (Throwable e2) {
            f2431a.mo4395c(e2);
            jjbqyppegg.mo4566a(null);
        } catch (ClassNotFoundException e3) {
            f2431a.mo4393c("Failed to find class com.facebook.applinks.AppLinkData, make sure you're using Facebook SDK 4.x");
            jjbqyppegg.mo4566a(null);
        } catch (Throwable e22) {
            f2431a.mo4395c(e22);
            jjbqyppegg.mo4566a(null);
        }
    }
}
