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
import java.util.concurrent.RejectedExecutionException;

@RequiresApi(api = 14)
/* renamed from: com.appsflyer.q */
final class C0456q implements ActivityLifecycleCallbacks {

    /* renamed from: ˊ */
    private static C0456q f316;
    /* access modifiers changed from: private */

    /* renamed from: ˋ */
    public boolean f317 = true;
    /* access modifiers changed from: private */

    /* renamed from: ˎ */
    public C0457a f318 = null;
    /* access modifiers changed from: private */

    /* renamed from: ॱ */
    public boolean f319 = false;

    /* renamed from: com.appsflyer.q$a */
    interface C0457a {
        /* renamed from: ॱ */
        void mo6490(Activity activity);

        /* renamed from: ॱ */
        void mo6491(WeakReference<Context> weakReference);
    }

    /* renamed from: com.appsflyer.q$c */
    class C0458c extends AsyncTask<Void, Void, Void> {

        /* renamed from: ˊ */
        private WeakReference<Context> f320;

        /* access modifiers changed from: protected */
        public final /* synthetic */ Object doInBackground(Object[] objArr) {
            return m337();
        }

        public C0458c(WeakReference<Context> weakReference) {
            this.f320 = weakReference;
        }

        /* renamed from: ॱ */
        private Void m337() {
            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
                AFLogger.afErrorLog("Sleeping attempt failed (essential for background state verification)\n", e);
            }
            if (C0456q.this.f319 && C0456q.this.f317) {
                C0456q.this.f319 = false;
                try {
                    C0456q.this.f318.mo6491(this.f320);
                } catch (Exception e2) {
                    AFLogger.afErrorLog("Listener threw exception! ", e2);
                    cancel(true);
                }
            }
            this.f320.clear();
            return null;
        }
    }

    C0456q() {
    }

    /* renamed from: ˊ */
    static C0456q m329() {
        if (f316 == null) {
            f316 = new C0456q();
        }
        return f316;
    }

    /* renamed from: ˏ */
    public static C0456q m332() {
        if (f316 != null) {
            return f316;
        }
        throw new IllegalStateException("Foreground is not initialised - invoke at least once with parameter init/get");
    }

    /* renamed from: ˋ */
    public final void mo6607(Application application, C0457a aVar) {
        this.f318 = aVar;
        if (VERSION.SDK_INT >= 14) {
            application.registerActivityLifecycleCallbacks(f316);
        }
    }

    public final void onActivityResumed(Activity activity) {
        boolean z = false;
        this.f317 = false;
        if (!this.f319) {
            z = true;
        }
        this.f319 = true;
        if (z) {
            try {
                this.f318.mo6490(activity);
            } catch (Exception e) {
                AFLogger.afErrorLog("Listener threw exception! ", e);
            }
        }
    }

    public final void onActivityPaused(Activity activity) {
        this.f317 = true;
        try {
            new C0458c(new WeakReference(activity.getApplicationContext())).executeOnExecutor(AFExecutor.getInstance().getThreadPoolExecutor(), new Void[0]);
        } catch (RejectedExecutionException e) {
            AFLogger.afErrorLog("backgroundTask.executeOnExecutor failed with RejectedExecutionException Exception", e);
        } catch (Throwable th) {
            AFLogger.afErrorLog("backgroundTask.executeOnExecutor failed with Exception", th);
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
