package im.getsocial.sdk.internal.p033c.p056i;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.Iterator;

/* renamed from: im.getsocial.sdk.internal.c.i.cjrhisSQCL */
public class cjrhisSQCL {
    /* renamed from: a */
    private static final im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL f1301a = upgqDBbsrL.m1274a(cjrhisSQCL.class);
    /* renamed from: b */
    private final Object f1302b = new Object();
    /* renamed from: c */
    private final ArrayList<WeakReference<upgqDBbsrL>> f1303c = new ArrayList();
    /* renamed from: d */
    private final Context f1304d;
    /* renamed from: e */
    private final ConnectivityManager f1305e;
    /* renamed from: f */
    private final jjbQypPegg f1306f;
    /* renamed from: g */
    private volatile boolean f1307g;

    /* renamed from: im.getsocial.sdk.internal.c.i.cjrhisSQCL$upgqDBbsrL */
    public interface upgqDBbsrL {
        /* renamed from: a */
        void mo4384a(boolean z);
    }

    /* renamed from: im.getsocial.sdk.internal.c.i.cjrhisSQCL$jjbQypPegg */
    private class jjbQypPegg extends BroadcastReceiver {
        /* renamed from: a */
        final /* synthetic */ cjrhisSQCL f1300a;

        private jjbQypPegg(cjrhisSQCL cjrhissqcl) {
            this.f1300a = cjrhissqcl;
        }

        public void onReceive(Context context, Intent intent) {
            this.f1300a.m1297c();
        }
    }

    @XdbacJlTDQ
    protected cjrhisSQCL(Context context) {
        this.f1304d = context;
        this.f1305e = (ConnectivityManager) this.f1304d.getSystemService("connectivity");
        this.f1306f = new jjbQypPegg();
    }

    /* renamed from: c */
    private void m1297c() {
        boolean z = false;
        if (this.f1305e != null) {
            NetworkInfo activeNetworkInfo = this.f1305e.getActiveNetworkInfo();
            if (activeNetworkInfo != null) {
                z = activeNetworkInfo.isConnected();
            }
            f1301a.mo4387a("Network info: " + activeNetworkInfo);
        }
        if (this.f1307g != z) {
            this.f1307g = z;
            m1298d();
        }
    }

    /* renamed from: d */
    private void m1298d() {
        Iterator it = new ArrayList(this.f1303c).iterator();
        while (it.hasNext()) {
            WeakReference weakReference = (WeakReference) it.next();
            upgqDBbsrL upgqdbbsrl = (upgqDBbsrL) weakReference.get();
            if (upgqdbbsrl == null) {
                this.f1303c.remove(weakReference);
            } else {
                upgqdbbsrl.mo4384a(this.f1307g);
            }
        }
    }

    /* renamed from: a */
    public final void m1299a() {
        synchronized (this.f1302b) {
            m1297c();
            this.f1304d.registerReceiver(this.f1306f, new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
        }
    }

    /* renamed from: a */
    public final void m1300a(upgqDBbsrL upgqdbbsrl) {
        this.f1303c.add(new WeakReference(upgqdbbsrl));
    }

    /* renamed from: b */
    public final void m1301b(upgqDBbsrL upgqdbbsrl) {
        Iterator it = this.f1303c.iterator();
        while (it.hasNext()) {
            WeakReference weakReference = (WeakReference) it.next();
            if (weakReference.get() != null && weakReference.get() == upgqdbbsrl) {
                it.remove();
            }
        }
    }

    /* renamed from: b */
    public final boolean m1302b() {
        return this.f1307g;
    }
}
