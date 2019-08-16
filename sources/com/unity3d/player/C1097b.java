package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.view.SurfaceView;
import android.view.View;
import com.google.android.gms.drive.DriveFile;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicLong;

/* renamed from: com.unity3d.player.b */
final class C1097b {
    /* access modifiers changed from: private */

    /* renamed from: a */
    public C1117m f557a = null;

    /* renamed from: b */
    private boolean f558b = false;

    /* renamed from: c */
    private boolean f559c = false;

    /* renamed from: d */
    private boolean f560d = false;

    /* renamed from: e */
    private boolean f561e = false;

    /* renamed from: f */
    private Context f562f = null;
    /* access modifiers changed from: private */

    /* renamed from: g */
    public C1103d f563g = null;

    /* renamed from: h */
    private String f564h = "";

    /* renamed from: i */
    private SurfaceView f565i = null;

    public C1097b(C1103d dVar) {
        this.f563g = dVar;
    }

    /* renamed from: a */
    private void m515a(Runnable runnable) {
        if (this.f562f instanceof Activity) {
            ((Activity) this.f562f).runOnUiThread(runnable);
        } else {
            C1104e.Log(5, "Not running Google VR from an Activity; Ignoring execution request...");
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m516a(String str) {
        if (this.f563g != null) {
            this.f563g.reportError("Google VR Error [" + this.f564h + "]", str);
        } else {
            C1104e.Log(6, "Google VR Error [" + this.f564h + "]: " + str);
        }
    }

    /* renamed from: a */
    private static boolean m517a(int i) {
        return VERSION.SDK_INT >= i;
    }

    /* renamed from: a */
    private boolean m518a(ClassLoader classLoader) {
        try {
            Class loadClass = classLoader.loadClass("com.unity3d.unitygvr.GoogleVR");
            this.f557a = new C1117m(loadClass, loadClass.getConstructor(new Class[0]).newInstance(new Object[0]));
            this.f557a.mo20539a("initialize", new Class[]{Activity.class, Context.class, SurfaceView.class, Boolean.TYPE});
            this.f557a.mo20539a("deinitialize", new Class[0]);
            this.f557a.mo20539a("load", new Class[]{Boolean.TYPE, Boolean.TYPE, Boolean.TYPE, Runnable.class});
            this.f557a.mo20539a("enable", new Class[]{Boolean.TYPE});
            this.f557a.mo20539a("unload", new Class[0]);
            this.f557a.mo20539a("pause", new Class[0]);
            this.f557a.mo20539a("resume", new Class[0]);
            this.f557a.mo20539a("getGvrLayout", new Class[0]);
            return true;
        } catch (Exception e) {
            m516a("Exception initializing GoogleVR from Unity library. " + e.getLocalizedMessage());
            return false;
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public void m519b(boolean z) {
        this.f559c = z;
    }

    /* renamed from: b */
    private boolean m521b(final Runnable runnable) {
        final Semaphore semaphore = new Semaphore(0);
        m515a((Runnable) new Runnable() {
            public final void run() {
                try {
                    runnable.run();
                } catch (Exception e) {
                    C1097b.this.m516a("Exception unloading Google VR on UI Thread. " + e.getLocalizedMessage());
                } finally {
                    semaphore.release();
                }
            }
        });
        try {
            if (semaphore.tryAcquire(4, TimeUnit.SECONDS)) {
                return true;
            }
            m516a("Timeout waiting for vr state change!");
            return false;
        } catch (InterruptedException e) {
            m516a("Interrupted while trying to acquire sync lock. " + e.getLocalizedMessage());
            return false;
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: f */
    public boolean m524f() {
        return this.f559c;
    }

    /* renamed from: g */
    private void m525g() {
        Activity activity = (Activity) this.f562f;
        if (this.f561e && !this.f558b && activity != null) {
            this.f558b = true;
            Intent intent = new Intent("android.intent.action.MAIN");
            intent.addCategory("android.intent.category.HOME");
            intent.setFlags(DriveFile.MODE_READ_ONLY);
            activity.startActivity(intent);
        }
    }

    /* renamed from: a */
    public final long mo20499a(boolean z, boolean z2, boolean z3, Runnable runnable) {
        final AtomicLong atomicLong = new AtomicLong(0);
        this.f564h = (z || z2) ? "Daydream" : "Cardboard";
        final boolean z4 = z;
        final boolean z5 = z2;
        final boolean z6 = z3;
        final Runnable runnable2 = runnable;
        if (!m521b((Runnable) new Runnable() {
            public final void run() {
                try {
                    atomicLong.set(((Long) C1097b.this.f557a.mo20538a("load", Boolean.valueOf(z4), Boolean.valueOf(z5), Boolean.valueOf(z6), runnable2)).longValue());
                    C1097b.this.mo20504b();
                } catch (Exception e) {
                    C1097b.this.m516a("Exception caught while loading GoogleVR. " + e.getLocalizedMessage());
                    atomicLong.set(0);
                }
            }
        }) || atomicLong.longValue() == 0) {
            m516a("Google VR had a fatal issue while loading. VR will not be available.");
        }
        return atomicLong.longValue();
    }

    /* renamed from: a */
    public final void mo20500a() {
        if (this.f557a != null) {
            this.f557a.mo20538a("pause", new Object[0]);
        }
    }

    /* renamed from: a */
    public final void mo20501a(Intent intent) {
        if (intent != null && intent.getBooleanExtra("android.intent.extra.VR_LAUNCH", false)) {
            this.f561e = true;
        }
    }

    /* renamed from: a */
    public final void mo20502a(final boolean z) {
        if (this.f563g != null && this.f562f != null) {
            if (!z && (this.f563g == null || this.f563g.isAppQuiting())) {
                m525g();
            }
            m515a((Runnable) new Runnable() {
                public final void run() {
                    if (z != C1097b.this.m524f()) {
                        try {
                            if (!z || C1097b.this.m524f()) {
                                if (!z && C1097b.this.m524f()) {
                                    C1097b.this.m519b(false);
                                    if (C1097b.this.f557a != null) {
                                        C1097b.this.f557a.mo20538a("enable", Boolean.valueOf(false));
                                    }
                                    if (C1097b.this.f557a != null && C1097b.this.f563g != null) {
                                        C1097b.this.f563g.removeViewFromPlayer((View) C1097b.this.f557a.mo20538a("getGvrLayout", new Object[0]));
                                    }
                                }
                            } else if (C1097b.this.f557a == null || C1097b.this.f563g == null || C1097b.this.f563g.addViewToPlayer((View) C1097b.this.f557a.mo20538a("getGvrLayout", new Object[0]), true)) {
                                if (C1097b.this.f557a != null) {
                                    C1097b.this.f557a.mo20538a("enable", Boolean.valueOf(true));
                                }
                                C1097b.this.m519b(true);
                            } else {
                                C1097b.this.m516a("Unable to add Google VR to view hierarchy.");
                            }
                        } catch (Exception e) {
                            C1097b.this.m516a("Exception enabling Google VR on UI Thread. " + e.getLocalizedMessage());
                        }
                    }
                }
            });
        }
    }

    /* renamed from: a */
    public final boolean mo20503a(Activity activity, Context context, SurfaceView surfaceView) {
        boolean z;
        if (activity == null || context == null || surfaceView == null) {
            m516a("Invalid parameters passed to Google VR initiialization.");
            return false;
        }
        this.f562f = context;
        if (!m517a(19)) {
            m516a("Google VR requires a device that supports an api version of 19 (KitKat) or better.");
            return false;
        } else if (this.f561e && !m517a(24)) {
            m516a("Daydream requires a device that supports an api version of 24 (Nougat) or better.");
            return false;
        } else if (!m518a(UnityPlayer.class.getClassLoader())) {
            return false;
        } else {
            try {
                z = ((Boolean) this.f557a.mo20538a("initialize", activity, context, surfaceView, Boolean.valueOf(this.f561e))).booleanValue();
            } catch (Exception e) {
                m516a("Exception while trying to intialize Unity Google VR Library. " + e.getLocalizedMessage());
                z = false;
            }
            if (!z) {
                m516a("Unable to initialize GoogleVR library.");
            }
            m519b(false);
            this.f565i = surfaceView;
            this.f558b = false;
            this.f560d = true;
            this.f564h = "";
            return true;
        }
    }

    /* renamed from: b */
    public final void mo20504b() {
        if (this.f557a != null) {
            this.f557a.mo20538a("resume", new Object[0]);
        }
    }

    /* renamed from: c */
    public final boolean mo20505c() {
        return this.f560d;
    }

    /* renamed from: d */
    public final void mo20506d() {
        mo20502a(false);
        this.f560d = false;
        this.f565i = null;
        m515a((Runnable) new Runnable() {
            public final void run() {
                try {
                    if (C1097b.this.f557a != null) {
                        C1097b.this.f557a.mo20538a("unload", new Object[0]);
                        C1097b.this.f557a.mo20538a("deinitialize", new Object[0]);
                        C1097b.this.f557a = null;
                    }
                    C1097b.this.m519b(false);
                } catch (Exception e) {
                    C1097b.this.m516a("Exception unloading Google VR on UI Thread. " + e.getLocalizedMessage());
                }
            }
        });
    }

    /* renamed from: e */
    public final void mo20507e() {
        if (this.f565i != null) {
            this.f565i.getHolder().setSizeFromLayout();
        }
    }
}
