package im.getsocial.sdk.internal.p036a.p045h;

import com.facebook.internal.AnalyticsEvents;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p089m.ztWNWCuZiM;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

/* renamed from: im.getsocial.sdk.internal.a.h.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1199a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private final im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg f1200b;
    /* renamed from: c */
    private final im.getsocial.sdk.internal.p036a.p044g.cjrhisSQCL f1201c;

    @XdbacJlTDQ
    jjbQypPegg(im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p036a.p044g.cjrhisSQCL cjrhissqcl) {
        this.f1200b = jjbqyppegg;
        this.f1201c = cjrhissqcl;
    }

    /* renamed from: a */
    public final void m1051a(GetSocialException getSocialException) {
        zoToeBNOjF a = ztWNWCuZiM.m2152a(getSocialException);
        im.getsocial.sdk.internal.p089m.ztWNWCuZiM.jjbQypPegg jjbqyppegg = (im.getsocial.sdk.internal.p089m.ztWNWCuZiM.jjbQypPegg) a.mo4497a();
        if (((Boolean) a.mo4498b()).booleanValue()) {
            Writer stringWriter = new StringWriter();
            getSocialException.printStackTrace(new PrintWriter(stringWriter));
            Map hashMap = new HashMap();
            hashMap.put(AnalyticsEvents.PARAMETER_SHARE_ERROR_MESSAGE, getSocialException.getMessage());
            hashMap.put("error_source", stringWriter.toString());
            hashMap.put("error_key", String.valueOf(getSocialException.getErrorCode()));
            hashMap.put("error_severity", jjbqyppegg.name());
            m1053a("sdk_error", hashMap);
        }
    }

    /* renamed from: a */
    public final void m1052a(im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg jjbqyppegg) {
        try {
            jjbqyppegg.m1018b().put("is_online", String.valueOf(this.f1200b.mo4401a()));
            new im.getsocial.sdk.internal.p036a.p046i.upgqDBbsrL().m1057a(jjbqyppegg);
        } catch (Throwable th) {
            f1199a.mo4388a("Failed to track analytics event: %s, error: %s", jjbqyppegg.m1017a(), th.getMessage());
            f1199a.mo4389a(th);
        }
    }

    /* renamed from: a */
    public final void m1053a(String str, Map<String, String> map) {
        m1052a(im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg.m1015a(str, map, this.f1201c.mo4352a(), UUID.randomUUID().toString()));
    }
}
