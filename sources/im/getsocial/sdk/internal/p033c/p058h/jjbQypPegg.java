package im.getsocial.sdk.internal.p033c.p058h;

import im.getsocial.p026c.p027a.HptYHntaqF;
import im.getsocial.p026c.p027a.KluUZYuxme;
import im.getsocial.p026c.p027a.fOrCGNYyfk;
import im.getsocial.p026c.p027a.zoToeBNOjF;
import im.getsocial.p026c.p027a.ztWNWCuZiM;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.iFpupLCESp;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.qZypgoeblR;
import im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT;
import im.getsocial.sdk.ui.BuildConfig;
import io.fabric.sdk.android.services.common.IdManager;
import java.io.ByteArrayInputStream;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.h.jjbQypPegg */
public class jjbQypPegg implements iFpupLCESp {
    /* renamed from: a */
    private static final cjrhisSQCL f1296a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private final im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL f1297b;
    /* renamed from: c */
    private final im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg f1298c;
    /* renamed from: d */
    private final QhisXzMgay f1299d;

    @XdbacJlTDQ
    jjbQypPegg(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL upgqdbbsrl, im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg jjbqyppegg, QhisXzMgay qhisXzMgay) {
        this.f1297b = upgqdbbsrl;
        this.f1298c = jjbqyppegg;
        this.f1299d = qhisXzMgay;
    }

    /* renamed from: a */
    static /* synthetic */ int m1293a(jjbQypPegg jjbqyppegg) {
        qZypgoeblR v = jjbqyppegg.f1299d.mo4481v();
        String str = v.m1539d() ? "WIFI" : v.m1540e() ? "LTE" : v.m1541f() ? "3G" : "OTHER";
        return jjbqyppegg.f1297b.m1326f().m1104a(str);
    }

    /* renamed from: a */
    public final void mo4400a(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg, pdwpUtZXDT pdwputzxdt, final Callback<im.getsocial.sdk.internal.p086j.p088b.cjrhisSQCL> callback) {
        try {
            String a = this.f1297b.m1326f().m1105a();
            final zoToeBNOjF zotoebnojf = new zoToeBNOjF();
            zotoebnojf.m906a(new URL(a));
            zotoebnojf.m904a(new HptYHntaqF());
            Map hashMap = new HashMap();
            hashMap.put("purpose", pdwputzxdt.name());
            hashMap.put("owner", this.f1298c.m3698b().getId());
            hashMap.put("sdk_version", BuildConfig.VERSION_NAME);
            hashMap.put(IdManager.OS_VERSION_FIELD, this.f1299d.mo4463d());
            hashMap.put("network_type", this.f1299d.mo4481v().m1538c());
            hashMap.put("platform", this.f1299d.mo4460a().name());
            hashMap.put("app", this.f1297b.m1322b().m1132a());
            final fOrCGNYyfk forcgnyyfk = new fOrCGNYyfk();
            forcgnyyfk.m886a(new ByteArrayInputStream((byte[]) jjbqyppegg.m2009a().clone()));
            f1296a.mo4387a("File size to upload: " + jjbqyppegg.m2009a().length + " bytes");
            forcgnyyfk.m885a((long) jjbqyppegg.m2009a().length);
            forcgnyyfk.m887a(hashMap);
            ztWNWCuZiM c09431 = new ztWNWCuZiM(this) {
                /* renamed from: d */
                final /* synthetic */ jjbQypPegg f1295d;

                /* renamed from: im.getsocial.sdk.internal.c.h.jjbQypPegg$1$1 */
                class C09421 extends im.getsocial.p026c.p027a.pdwpUtZXDT {
                    /* renamed from: a */
                    final /* synthetic */ C09431 f1291a;

                    C09421(C09431 c09431, zoToeBNOjF zotoebnojf, String str, int i, String str2) {
                        this.f1291a = c09431;
                        super(zotoebnojf, str, 5, str2);
                    }

                    /* renamed from: a */
                    protected final void mo4397a(im.getsocial.p026c.p027a.upgqDBbsrL upgqdbbsrl) {
                        jjbQypPegg.f1296a.mo4387a("Uploaded resources are not ready: " + upgqdbbsrl.getMessage());
                        callback.onFailure(new GetSocialException(ErrorCode.MEDIAUPLOAD_RESOURCE_NOT_READY, upgqdbbsrl.getMessage(), upgqdbbsrl));
                    }

                    /* renamed from: a */
                    protected final void mo4398a(Map<String, String> map) {
                        jjbQypPegg.f1296a.mo4387a("Uploaded resources are ready: " + map);
                        callback.onSuccess(new im.getsocial.sdk.internal.p086j.p088b.cjrhisSQCL(map));
                    }
                }

                /* renamed from: a */
                protected final void mo4399a() {
                    KluUZYuxme a = zotoebnojf.m903a(forcgnyyfk);
                    int a2 = jjbQypPegg.m1293a(this.f1295d);
                    jjbQypPegg.f1296a.mo4387a("Calculated chunk size: " + a2 + " bytes");
                    a.m877a(a2);
                    do {
                        jjbQypPegg.f1296a.mo4387a("Upload progress: " + ((((double) a.m878b()) / ((double) forcgnyyfk.m884a())) * 100.0d));
                    } while (a.m876a() >= 0);
                    jjbQypPegg.f1296a.mo4387a("Finished upload to: " + a.m879c().toString());
                    a.m880d();
                    zoToeBNOjF zotoebnojf = zotoebnojf;
                    String url = a.m879c().toString();
                    fOrCGNYyfk forcgnyyfk = forcgnyyfk;
                    C09421 c09421 = new C09421(this, zotoebnojf, url, 5, null);
                }
            };
            c09431.m909a(2);
            f1296a.mo4387a("Starting upload to: " + a);
            c09431.m910b();
        } catch (Throwable e) {
            callback.onFailure(new GetSocialException(ErrorCode.MEDIAUPLOAD_FAILED, e.getMessage(), e));
        }
    }
}
