package im.getsocial.sdk.internal.p036a.p040d;

import com.facebook.AccessToken;
import com.google.android.gms.measurement.AppMeasurement.Param;
import im.getsocial.p015a.p016a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.internal.a.d.upgqDBbsrL */
public class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private static final ScheduledExecutorService f1188a = Executors.newSingleThreadScheduledExecutor();
    /* renamed from: b */
    private static final cjrhisSQCL f1189b = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    /* renamed from: c */
    private final bpiSwUyLit f1190c;
    /* renamed from: d */
    private final List<jjbQypPegg> f1191d;
    /* renamed from: e */
    private ScheduledFuture f1192e;

    /* renamed from: im.getsocial.sdk.internal.a.d.upgqDBbsrL$1 */
    class C09271 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f1187a;

        C09271(upgqDBbsrL upgqdbbsrl) {
            this.f1187a = upgqdbbsrl;
        }

        public void run() {
            this.f1187a.f1190c.mo4360a("analytics_queue", this.f1187a.m1037c());
        }
    }

    @XdbacJlTDQ
    upgqDBbsrL(bpiSwUyLit bpiswuylit) {
        this.f1190c = bpiswuylit;
        this.f1191d = new CopyOnWriteArrayList(this.f1190c.mo4361a("analytics_queue") ? m1030a(this.f1190c.mo4362b("analytics_queue")) : new ArrayList());
    }

    /* renamed from: a */
    private static Object m1029a(pdwpUtZXDT pdwputzxdt, String str, String str2) {
        return pdwputzxdt.get(str2 + str);
    }

    /* renamed from: a */
    private List<jjbQypPegg> m1030a(String str) {
        List<jjbQypPegg> arrayList = new ArrayList();
        try {
            im.getsocial.p015a.p016a.upgqDBbsrL upgqdbbsrl;
            String str2;
            Object a = new im.getsocial.p015a.p016a.p017a.cjrhisSQCL().m721a(str, null);
            if (a instanceof im.getsocial.p015a.p016a.upgqDBbsrL) {
                upgqdbbsrl = (im.getsocial.p015a.p016a.upgqDBbsrL) a;
                str2 = EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR;
            } else {
                Object obj = ((pdwpUtZXDT) a).containsKey("queue_userid") ? (String) ((pdwpUtZXDT) a).get("queue_userid") : null;
                if (obj == null || m1032e().equals(obj)) {
                    upgqdbbsrl = (im.getsocial.p015a.p016a.upgqDBbsrL) ((pdwpUtZXDT) a).get("queue");
                    str2 = "";
                } else {
                    f1189b.mo4387a("Persisted analytics queue belongs to a different user, deleting it");
                    this.f1190c.mo4365e("analytics_queue");
                    return arrayList;
                }
            }
            Iterator it = upgqdbbsrl.iterator();
            while (it.hasNext()) {
                pdwpUtZXDT pdwputzxdt = (pdwpUtZXDT) it.next();
                Map map = pdwputzxdt.containsKey("properties") ? (Map) upgqDBbsrL.m1029a(pdwputzxdt, "properties", str2) : null;
                String str3 = pdwputzxdt.containsKey("unique_id") ? (String) upgqDBbsrL.m1029a(pdwputzxdt, "unique_id", str2) : null;
                if (str3 == null) {
                    str3 = UUID.randomUUID().toString();
                }
                long j = 0;
                if (pdwputzxdt.containsKey("retry_count")) {
                    j = ((Long) upgqDBbsrL.m1029a(pdwputzxdt, "retry_count", str2)).longValue();
                }
                arrayList.add(jjbQypPegg.m1016a((String) upgqDBbsrL.m1029a(pdwputzxdt, "name", str2), map, ((Long) upgqDBbsrL.m1029a(pdwputzxdt, Param.TIMESTAMP, str2)).longValue(), str3, j));
            }
        } catch (Exception e) {
            f1189b.mo4394c("Persisted analytics queue uses old format, deleting it", e);
            this.f1190c.mo4365e("analytics_queue");
        }
        return arrayList;
    }

    /* renamed from: d */
    private void m1031d() {
        if (this.f1192e != null) {
            this.f1192e.cancel(false);
        }
        this.f1192e = f1188a.schedule(new C09271(this), 100, TimeUnit.MILLISECONDS);
    }

    /* renamed from: e */
    private String m1032e() {
        return this.f1190c.mo4362b(AccessToken.USER_ID_KEY);
    }

    /* renamed from: a */
    public final List<jjbQypPegg> mo4347a() {
        return Collections.unmodifiableList(new ArrayList(this.f1191d));
    }

    /* renamed from: a */
    public final void mo4348a(jjbQypPegg jjbqyppegg) {
        f1189b.mo4387a("Queue analytics event: " + jjbqyppegg);
        this.f1191d.add(jjbqyppegg);
        m1031d();
    }

    /* renamed from: a */
    public final void mo4349a(List<jjbQypPegg> list) {
        f1189b.mo4387a("Queue analytics events: " + list);
        this.f1191d.addAll(list);
        m1031d();
    }

    /* renamed from: b */
    public final void mo4350b() {
        f1189b.mo4387a("Clear queue");
        this.f1191d.clear();
        m1031d();
    }

    /* renamed from: c */
    final String m1037c() {
        Map pdwputzxdt = new pdwpUtZXDT();
        pdwputzxdt.put("queue_userid", m1032e());
        im.getsocial.p015a.p016a.upgqDBbsrL upgqdbbsrl = new im.getsocial.p015a.p016a.upgqDBbsrL();
        for (jjbQypPegg jjbqyppegg : this.f1191d) {
            pdwpUtZXDT pdwputzxdt2 = new pdwpUtZXDT();
            pdwputzxdt2.put("name", jjbqyppegg.m1017a());
            pdwputzxdt2.put(Param.TIMESTAMP, Long.valueOf(jjbqyppegg.m1019c()));
            pdwputzxdt2.put("unique_id", jjbqyppegg.m1020d());
            pdwputzxdt2.put("retry_count", Long.valueOf(jjbqyppegg.m1021e()));
            pdwputzxdt2.put("properties", new pdwpUtZXDT(jjbqyppegg.m1018b()));
            upgqdbbsrl.add(pdwputzxdt2);
        }
        pdwputzxdt.put("queue", upgqdbbsrl);
        return pdwpUtZXDT.m725a(pdwputzxdt);
    }
}
