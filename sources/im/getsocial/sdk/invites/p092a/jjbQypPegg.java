package im.getsocial.sdk.invites.p092a;

import android.content.Context;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.rWfbqYooCV;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.invites.a.jjbQypPegg */
public class jjbQypPegg implements rWfbqYooCV, InvocationHandler {
    /* renamed from: a */
    private static final cjrhisSQCL f2442a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private im.getsocial.sdk.internal.p090k.upgqDBbsrL f2443b;
    /* renamed from: c */
    private final Context f2444c;
    /* renamed from: d */
    private im.getsocial.sdk.internal.p033c.rWfbqYooCV.jjbQypPegg f2445d;
    /* renamed from: e */
    private boolean f2446e;

    @XdbacJlTDQ
    jjbQypPegg(Context context) {
        this.f2444c = context;
    }

    /* renamed from: a */
    private Object m2400a(Class cls) {
        try {
            return Proxy.newProxyInstance(cls.getClassLoader(), new Class[]{cls}, this);
        } catch (Exception e) {
            f2442a.mo4387a("Failed to create InstallReferrerStateListener instance, error: " + e);
            m2402a();
            return null;
        }
    }

    /* renamed from: a */
    private Object m2401a(String str, im.getsocial.sdk.internal.p090k.upgqDBbsrL upgqdbbsrl) {
        try {
            im.getsocial.sdk.internal.p090k.upgqDBbsrL a = upgqdbbsrl.m2090a(str, new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]);
            if (a.m2091a() != null) {
                return a.m2091a();
            }
        } catch (Exception e) {
            f2442a.mo4388a("Failed to " + str + " from ReferrerDetails object.", e);
            m2402a();
        }
        return null;
    }

    /* renamed from: a */
    private void m2402a() {
        if (!this.f2446e) {
            this.f2445d.mo4567a(null);
            this.f2446e = true;
        }
    }

    /* renamed from: a */
    public final void mo4575a(im.getsocial.sdk.internal.p033c.rWfbqYooCV.jjbQypPegg jjbqyppegg) {
        this.f2445d = jjbqyppegg;
        Context context = this.f2444c;
        try {
            this.f2443b = im.getsocial.sdk.internal.p090k.jjbQypPegg.m2088a("com.android.installreferrer.api.InstallReferrerClient").m2089a("newBuilder", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(context.getApplicationContext(), Context.class)).m2090a("build", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]);
        } catch (Exception e) {
            f2442a.mo4387a("Failed to create InstallReferrerClient instance, error: " + e);
            m2402a();
        }
        if (this.f2443b != null) {
            try {
                Class cls = Class.forName("com.android.installreferrer.api.InstallReferrerStateListener");
                this.f2443b.m2090a("startConnection", im.getsocial.sdk.internal.p090k.cjrhisSQCL.m2087a(m2400a(cls), cls));
            } catch (Exception e2) {
                f2442a.mo4387a("Failed to start connection on ReferrerClient instance, error: " + e2);
                m2402a();
            }
        }
    }

    public Object invoke(Object obj, Method method, Object[] objArr) {
        if (method != null) {
            f2442a.mo4387a("Received callback from InstallReferrerStateListener instance, method: " + method.getName() + ", parameters: " + objArr);
            if ("onInstallReferrerSetupFinished".equalsIgnoreCase(method.getName()) && objArr.length == 1) {
                Object obj2 = objArr[0];
                if (obj2 instanceof Integer) {
                    int intValue = ((Integer) obj2).intValue();
                    switch (intValue) {
                        case -1:
                            break;
                        case 0:
                            try {
                                im.getsocial.sdk.internal.p090k.upgqDBbsrL a = this.f2443b.m2090a("getInstallReferrer", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]);
                                if (a.m2091a() == null) {
                                    f2442a.mo4387a("No referrer found");
                                } else {
                                    String str = (String) m2401a("getInstallReferrer", a);
                                    Long l = (Long) m2401a("getInstallBeginTimestampSeconds", a);
                                    Long l2 = (Long) m2401a("getReferrerClickTimestampSeconds", a);
                                    if (str != null) {
                                        f2442a.mo4387a("Referrer received from Google Play Service: " + str);
                                        Map hashMap = new HashMap();
                                        hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a, str);
                                        if (l != null) {
                                            hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2300b, l.toString());
                                        }
                                        if (l2 != null) {
                                            hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2301c, l2.toString());
                                        }
                                        this.f2445d.mo4567a(hashMap);
                                    }
                                }
                                if (this.f2443b != null) {
                                    try {
                                        this.f2443b.m2090a("endConnection", new im.getsocial.sdk.internal.p090k.cjrhisSQCL[0]);
                                    } catch (Exception e) {
                                        f2442a.mo4387a("Failed to disconnect from InstallReferrerService, error: " + e);
                                        m2402a();
                                    }
                                }
                                this.f2443b = null;
                                break;
                            } catch (Exception e2) {
                                f2442a.mo4388a("Failed to read ReferrerDetails.", e2);
                                m2402a();
                                break;
                            }
                        case 1:
                        case 2:
                        case 3:
                            f2442a.mo4387a("Failed to setup connection with InstallReferrerService, response code:" + intValue);
                            m2402a();
                            break;
                        default:
                            f2442a.mo4387a("Unknown response code received: " + intValue);
                            m2402a();
                            break;
                    }
                }
            }
        }
        return null;
    }
}
