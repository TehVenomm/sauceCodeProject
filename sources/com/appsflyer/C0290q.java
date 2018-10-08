package com.appsflyer;

import android.app.Activity;
import android.app.Application;
import android.app.Application.ActivityLifecycleCallbacks;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import java.lang.ref.WeakReference;

@RequiresApi(api = 14)
/* renamed from: com.appsflyer.q */
final class C0290q implements ActivityLifecycleCallbacks {
    /* renamed from: ˊ */
    private static C0290q f297;
    /* renamed from: ˋ */
    private boolean f298 = true;
    /* renamed from: ˎ */
    private C0251a f299 = null;
    /* renamed from: ॱ */
    private boolean f300 = false;

    /* renamed from: com.appsflyer.q$a */
    interface C0251a {
        /* renamed from: ॱ */
        void mo1206(Activity activity);

        /* renamed from: ॱ */
        void mo1207(WeakReference<Context> weakReference);
    }

    /* renamed from: com.appsflyer.q$c */
    class C0289c extends AsyncTask<Void, Void, Void> {
        /* renamed from: ˊ */
        private WeakReference<Context> f295;
        /* renamed from: ॱ */
        private /* synthetic */ C0290q f296;

        protected final /* synthetic */ Object doInBackground(Object[] objArr) {
            return m337();
        }

        public C0289c(C0290q c0290q, WeakReference<Context> weakReference) {
            this.f296 = c0290q;
            this.f295 = weakReference;
        }

        /* renamed from: ॱ */
        private Void m337() {
            try {
                Thread.sleep(500);
            } catch (Throwable e) {
                AFLogger.afErrorLog("Sleeping attempt failed (essential for background state verification)\n", e);
            }
            if (this.f296.f300 && this.f296.f298) {
                this.f296.f300 = false;
                try {
                    this.f296.f299.mo1207(this.f295);
                } catch (Throwable e2) {
                    AFLogger.afErrorLog("Listener threw exception! ", e2);
                    cancel(true);
                }
            }
            this.f295.clear();
            return null;
        }
    }

    C0290q() {
    }

    /* renamed from: ˊ */
    static C0290q m339() {
        if (f297 == null) {
            f297 = new C0290q();
        }
        return f297;
    }

    /* renamed from: ˏ */
    public static C0290q m342() {
        if (f297 != null) {
            return f297;
        }
        throw new IllegalStateException("Foreground is not initialised - invoke at least once with parameter init/get");
    }

    /* renamed from: ˋ */
    public final void m344(Application application, C0251a c0251a) {
        this.f299 = c0251a;
        if (VERSION.SDK_INT >= 14) {
            application.registerActivityLifecycleCallbacks(f297);
        }
    }

    public final void onActivityResumed(Activity activity) {
        boolean z = false;
        this.f298 = false;
        if (!this.f300) {
            z = true;
        }
        this.f300 = true;
        if (z) {
            try {
                this.f299.mo1206(activity);
            } catch (Throwable e) {
                AFLogger.afErrorLog("Listener threw exception! ", e);
            }
        }
    }

    public final void onActivityPaused(Activity activity) {
        this.f298 = true;
        try {
            new C0289c(this, new WeakReference(activity.getApplicationContext())).executeOnExecutor(AFExecutor.getInstance().getThreadPoolExecutor(), new Void[0]);
        } catch (Throwable e) {
            AFLogger.afErrorLog("backgroundTask.executeOnExecutor failed with RejectedExecutionException Exception", e);
        } catch (Throwable e2) {
            AFLogger.afErrorLog("backgroundTask.executeOnExecutor failed with Exception", e2);
        }
    }

    public final void onActivityCreated(Activity activity, Bundle bundle) {
    }

    public final void onActivityStarted(Activity activity) {
    }

    public final void onActivityStopped(Activity activity) {
    }

    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
    }

    public final void onActivityDestroyed(Activity activity) {
    }
}
