package com.unity3d.player;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Handler.Callback;
import android.os.Looper;
import android.os.Message;
import android.os.Process;
import android.telephony.PhoneStateListener;
import android.telephony.TelephonyManager;
import android.view.InputEvent;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Surface;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ProgressBar;
import com.facebook.places.model.PlaceFields;
import com.google.firebase.messaging.cpp.SerializedEventUnion;
import com.unity3d.player.C1119n.C1120a;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Vector;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicInteger;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

public class UnityPlayer extends FrameLayout implements C1103d {
    public static Activity currentActivity = null;

    /* renamed from: n */
    private static boolean f453n;

    /* renamed from: a */
    C1092c f454a = new C1092c(this, 0);

    /* renamed from: b */
    C1108i f455b = null;
    /* access modifiers changed from: private */

    /* renamed from: c */
    public boolean f456c = false;

    /* renamed from: d */
    private boolean f457d = true;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public C1116l f458e = new C1116l();

    /* renamed from: f */
    private final ConcurrentLinkedQueue f459f = new ConcurrentLinkedQueue();

    /* renamed from: g */
    private BroadcastReceiver f460g = null;

    /* renamed from: h */
    private boolean f461h = false;

    /* renamed from: i */
    private C1090a f462i = new C1090a(this, 0);

    /* renamed from: j */
    private TelephonyManager f463j;
    /* access modifiers changed from: private */

    /* renamed from: k */
    public C1112j f464k;
    /* access modifiers changed from: private */

    /* renamed from: l */
    public ContextWrapper f465l;
    /* access modifiers changed from: private */

    /* renamed from: m */
    public SurfaceView f466m;
    /* access modifiers changed from: private */

    /* renamed from: o */
    public boolean f467o;
    /* access modifiers changed from: private */

    /* renamed from: p */
    public Bundle f468p = new Bundle();
    /* access modifiers changed from: private */

    /* renamed from: q */
    public C1119n f469q;

    /* renamed from: r */
    private boolean f470r = false;
    /* access modifiers changed from: private */

    /* renamed from: s */
    public ProgressBar f471s = null;

    /* renamed from: t */
    private Runnable f472t = new Runnable() {
        public final void run() {
            int p = UnityPlayer.this.nativeActivityIndicatorStyle();
            if (p >= 0) {
                if (UnityPlayer.this.f471s == null) {
                    UnityPlayer.this.f471s = new ProgressBar(UnityPlayer.this.f465l, null, new int[]{16842874, 16843401, 16842873, 16843400}[p]);
                    UnityPlayer.this.f471s.setIndeterminate(true);
                    UnityPlayer.this.f471s.setLayoutParams(new LayoutParams(-2, -2, 51));
                    UnityPlayer.this.addView(UnityPlayer.this.f471s);
                }
                UnityPlayer.this.f471s.setVisibility(0);
                UnityPlayer.this.bringChildToFront(UnityPlayer.this.f471s);
            }
        }
    };

    /* renamed from: u */
    private Runnable f473u = new Runnable() {
        public final void run() {
            if (UnityPlayer.this.f471s != null) {
                UnityPlayer.this.f471s.setVisibility(8);
                UnityPlayer.this.removeView(UnityPlayer.this.f471s);
                UnityPlayer.this.f471s = null;
            }
        }
    };
    /* access modifiers changed from: private */

    /* renamed from: v */
    public C1097b f474v = new C1097b(this);

    /* renamed from: com.unity3d.player.UnityPlayer$3 */
    class C10833 extends BroadcastReceiver {

        /* renamed from: a */
        final /* synthetic */ UnityPlayer f509a;

        public void onReceive(Context context, Intent intent) {
            this.f509a.m472d();
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$a */
    private final class C1090a extends PhoneStateListener {
        private C1090a() {
        }

        /* synthetic */ C1090a(UnityPlayer unityPlayer, byte b) {
            this();
        }

        public final void onCallStateChanged(int i, String str) {
            boolean z = true;
            UnityPlayer unityPlayer = UnityPlayer.this;
            if (i != 1) {
                z = false;
            }
            unityPlayer.nativeMuteMasterAudio(z);
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$b */
    enum C1091b {
        PAUSE,
        RESUME,
        QUIT,
        FOCUS_GAINED,
        FOCUS_LOST,
        NEXT_FRAME
    }

    /* renamed from: com.unity3d.player.UnityPlayer$c */
    private final class C1092c extends Thread {

        /* renamed from: a */
        Handler f534a;

        /* renamed from: b */
        boolean f535b;

        /* renamed from: c */
        int f536c;

        private C1092c() {
            this.f535b = false;
            this.f536c = 5;
        }

        /* synthetic */ C1092c(UnityPlayer unityPlayer, byte b) {
            this();
        }

        /* renamed from: a */
        private void m504a(C1091b bVar) {
            Message.obtain(this.f534a, 2269, bVar).sendToTarget();
        }

        /* renamed from: a */
        public final void mo20452a() {
            m504a(C1091b.QUIT);
        }

        /* renamed from: a */
        public final void mo20453a(boolean z) {
            m504a(z ? C1091b.FOCUS_GAINED : C1091b.FOCUS_LOST);
        }

        /* renamed from: b */
        public final void mo20454b() {
            m504a(C1091b.RESUME);
        }

        /* renamed from: c */
        public final void mo20455c() {
            m504a(C1091b.PAUSE);
        }

        public final void run() {
            setName("UnityMain");
            Looper.prepare();
            this.f534a = new Handler(new Callback() {
                public final boolean handleMessage(Message message) {
                    if (message.what != 2269) {
                        return false;
                    }
                    C1091b bVar = (C1091b) message.obj;
                    if (bVar == C1091b.QUIT) {
                        Looper.myLooper().quit();
                    } else if (bVar == C1091b.RESUME) {
                        C1092c.this.f535b = true;
                    } else if (bVar == C1091b.PAUSE) {
                        C1092c.this.f535b = false;
                        UnityPlayer.this.executeGLThreadJobs();
                    } else if (bVar == C1091b.FOCUS_LOST) {
                        if (!C1092c.this.f535b) {
                            UnityPlayer.this.executeGLThreadJobs();
                        }
                    } else if (bVar == C1091b.NEXT_FRAME) {
                        if (C1092c.this.f536c >= 0) {
                            if (C1092c.this.f536c == 0 && UnityPlayer.this.f468p.getBoolean("showSplash")) {
                                UnityPlayer.this.m455a();
                            }
                            C1092c.this.f536c--;
                        }
                        UnityPlayer.this.executeGLThreadJobs();
                        if (!UnityPlayer.this.isFinishing() && !UnityPlayer.this.nativeRender()) {
                            UnityPlayer.this.m472d();
                        }
                    }
                    if (C1092c.this.f535b) {
                        Message.obtain(C1092c.this.f534a, 2269, C1091b.NEXT_FRAME).sendToTarget();
                    }
                    return true;
                }
            });
            Looper.loop();
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$d */
    private abstract class C1094d implements Runnable {
        private C1094d() {
        }

        /* synthetic */ C1094d(UnityPlayer unityPlayer, byte b) {
            this();
        }

        /* renamed from: a */
        public abstract void mo20428a();

        public final void run() {
            if (!UnityPlayer.this.isFinishing()) {
                mo20428a();
            }
        }
    }

    static {
        new C1115k().mo20528a();
        f453n = false;
        f453n = loadLibraryStatic("main");
    }

    public UnityPlayer(ContextWrapper contextWrapper) {
        super(contextWrapper);
        if (contextWrapper instanceof Activity) {
            currentActivity = (Activity) contextWrapper;
        }
        m457a(currentActivity);
        this.f465l = contextWrapper;
        m470c();
        if (currentActivity != null && this.f468p.getBoolean("showSplash")) {
            this.f464k = new C1112j(this.f465l, C1114a.m546a()[getSplashMode()]);
            addView(this.f464k);
        }
        if (C1107h.f582c) {
            if (currentActivity != null) {
                C1107h.f583d.mo20512a(currentActivity, new Runnable() {
                    public final void run() {
                        UnityPlayer.this.mo20386a((Runnable) new Runnable() {
                            public final void run() {
                                UnityPlayer.this.f458e.mo20533d();
                                UnityPlayer.this.m479g();
                            }
                        });
                    }
                });
            } else {
                this.f458e.mo20533d();
            }
        }
        m458a(this.f465l.getApplicationInfo());
        if (!C1116l.m550c()) {
            AlertDialog create = new Builder(this.f465l).setTitle("Failure to initialize!").setPositiveButton("OK", new OnClickListener() {
                public final void onClick(DialogInterface dialogInterface, int i) {
                    UnityPlayer.this.m472d();
                }
            }).setMessage("Your hardware does not support this application, sorry!").create();
            create.setCancelable(false);
            create.show();
            return;
        }
        initJni(contextWrapper);
        nativeFile(this.f465l.getPackageCodePath());
        m482i();
        this.f466m = m465b();
        addView(this.f466m);
        bringChildToFront(this.f464k);
        this.f467o = false;
        if (currentActivity != null) {
            this.f474v.mo20501a(currentActivity.getIntent());
        }
        nativeInitWWW(WWW.class);
        nativeInitWebRequest(UnityWebRequest.class);
        m485j();
        this.f463j = (TelephonyManager) this.f465l.getSystemService(PlaceFields.PHONE);
        this.f454a.start();
    }

    public static void UnitySendMessage(String str, String str2, String str3) {
        if (!C1116l.m550c()) {
            C1104e.Log(5, "Native libraries not loaded - dropping message for " + str + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + str2);
        } else {
            nativeUnitySendMessage(str, str2, str3);
        }
    }

    /* renamed from: a */
    private static String m454a(String str) {
        byte[] bArr;
        try {
            MessageDigest instance = MessageDigest.getInstance("MD5");
            FileInputStream fileInputStream = new FileInputStream(str);
            long length = new File(str).length();
            fileInputStream.skip(length - Math.min(length, 65558));
            byte[] bArr2 = new byte[1024];
            for (int i = 0; i != -1; i = fileInputStream.read(bArr2)) {
                instance.update(bArr2, 0, i);
            }
            bArr = instance.digest();
        } catch (FileNotFoundException e) {
            bArr = null;
        } catch (IOException e2) {
            bArr = null;
        } catch (NoSuchAlgorithmException e3) {
            bArr = null;
        }
        if (bArr == null) {
            return null;
        }
        StringBuffer stringBuffer = new StringBuffer();
        for (byte b : bArr) {
            stringBuffer.append(Integer.toString((b & 255) + SerializedEventUnion.NONE, 16).substring(1));
        }
        return stringBuffer.toString();
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m455a() {
        mo20386a((Runnable) new Runnable() {
            public final void run() {
                UnityPlayer.this.removeView(UnityPlayer.this.f464k);
                UnityPlayer.this.f464k = null;
            }
        });
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m456a(int i, Surface surface) {
        if (!this.f456c) {
            m469b(0, surface);
        }
    }

    /* renamed from: a */
    private static void m457a(Activity activity) {
        if (activity != null && activity.getIntent().getBooleanExtra("android.intent.extra.VR_LAUNCH", false) && activity.getWindow() != null) {
            View decorView = activity.getWindow().getDecorView();
            if (decorView != null) {
                decorView.setSystemUiVisibility(7);
            }
        }
    }

    /* renamed from: a */
    private static void m458a(ApplicationInfo applicationInfo) {
        if (f453n && NativeLoader.load(applicationInfo.nativeLibraryDir)) {
            C1116l.m548a();
        }
    }

    /* renamed from: a */
    private void m459a(View view, View view2) {
        boolean z;
        if (!this.f458e.mo20534e()) {
            pause();
            z = true;
        } else {
            z = false;
        }
        if (view != null) {
            ViewParent parent = view.getParent();
            if (!(parent instanceof UnityPlayer) || ((UnityPlayer) parent) != this) {
                if (parent instanceof ViewGroup) {
                    ((ViewGroup) parent).removeView(view);
                }
                addView(view);
                bringChildToFront(view);
                view.setVisibility(0);
            }
        }
        if (view2 != null && view2.getParent() == this) {
            view2.setVisibility(8);
            removeView(view2);
        }
        if (z) {
            resume();
        }
    }

    /* renamed from: a */
    private void m460a(C1094d dVar) {
        if (!isFinishing()) {
            m468b((Runnable) dVar);
        }
    }

    /* renamed from: a */
    private static String[] m464a(Context context) {
        String packageName = context.getPackageName();
        Vector vector = new Vector();
        try {
            int i = context.getPackageManager().getPackageInfo(packageName, 0).versionCode;
            if (Environment.getExternalStorageState().equals("mounted")) {
                File file = new File(Environment.getExternalStorageDirectory().toString() + "/Android/obb/" + packageName);
                if (file.exists()) {
                    if (i > 0) {
                        String str = file + File.separator + "main." + i + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + packageName + ".obb";
                        if (new File(str).isFile()) {
                            vector.add(str);
                        }
                    }
                    if (i > 0) {
                        String str2 = file + File.separator + "patch." + i + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + packageName + ".obb";
                        if (new File(str2).isFile()) {
                            vector.add(str2);
                        }
                    }
                }
            }
            String[] strArr = new String[vector.size()];
            vector.toArray(strArr);
            return strArr;
        } catch (NameNotFoundException e) {
            return new String[0];
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public SurfaceView m465b() {
        SurfaceView surfaceView = new SurfaceView(this.f465l);
        surfaceView.getHolder().setFormat(2);
        surfaceView.getHolder().addCallback(new SurfaceHolder.Callback() {
            public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
                UnityPlayer.this.m456a(0, surfaceHolder.getSurface());
            }

            public final void surfaceCreated(SurfaceHolder surfaceHolder) {
                UnityPlayer.this.m456a(0, surfaceHolder.getSurface());
            }

            public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
                UnityPlayer.this.m456a(0, (Surface) null);
            }
        });
        surfaceView.setFocusable(true);
        surfaceView.setFocusableInTouchMode(true);
        return surfaceView;
    }

    /* renamed from: b */
    private void m468b(Runnable runnable) {
        if (C1116l.m550c()) {
            if (Thread.currentThread() == this.f454a) {
                runnable.run();
            } else {
                this.f459f.add(runnable);
            }
        }
    }

    /* renamed from: b */
    private boolean m469b(int i, Surface surface) {
        if (!C1116l.m550c()) {
            return false;
        }
        nativeRecreateGfxState(i, surface);
        return true;
    }

    /* renamed from: c */
    private void m470c() {
        try {
            File file = new File(this.f465l.getPackageCodePath(), "assets/bin/Data/settings.xml");
            InputStream open = file.exists() ? new FileInputStream(file) : this.f465l.getAssets().open("bin/Data/settings.xml");
            XmlPullParserFactory newInstance = XmlPullParserFactory.newInstance();
            newInstance.setNamespaceAware(true);
            XmlPullParser newPullParser = newInstance.newPullParser();
            newPullParser.setInput(open, null);
            String str = null;
            String str2 = null;
            for (int eventType = newPullParser.getEventType(); eventType != 1; eventType = newPullParser.next()) {
                if (eventType == 2) {
                    str = newPullParser.getName();
                    for (int i = 0; i < newPullParser.getAttributeCount(); i++) {
                        if (newPullParser.getAttributeName(i).equalsIgnoreCase("name")) {
                            str2 = newPullParser.getAttributeValue(i);
                        }
                    }
                } else if (eventType == 3) {
                    str = null;
                } else if (eventType == 4 && str2 != null) {
                    if (str.equalsIgnoreCase("integer")) {
                        this.f468p.putInt(str2, Integer.parseInt(newPullParser.getText()));
                    } else if (str.equalsIgnoreCase("string")) {
                        this.f468p.putString(str2, newPullParser.getText());
                    } else if (str.equalsIgnoreCase("bool")) {
                        this.f468p.putBoolean(str2, Boolean.parseBoolean(newPullParser.getText()));
                    } else if (str.equalsIgnoreCase("float")) {
                        this.f468p.putFloat(str2, Float.parseFloat(newPullParser.getText()));
                    }
                    str2 = null;
                }
            }
        } catch (Exception e) {
            C1104e.Log(6, "Unable to locate player settings. " + e.getLocalizedMessage());
            m472d();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: d */
    public void m472d() {
        if ((this.f465l instanceof Activity) && !((Activity) this.f465l).isFinishing()) {
            ((Activity) this.f465l).finish();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: e */
    public void m475e() {
        reportSoftInputStr(null, 1, true);
        if (this.f458e.mo20536g()) {
            if (C1116l.m550c()) {
                final Semaphore semaphore = new Semaphore(0);
                if (isFinishing()) {
                    m468b((Runnable) new Runnable() {
                        public final void run() {
                            UnityPlayer.this.m476f();
                            semaphore.release();
                        }
                    });
                } else {
                    m468b((Runnable) new Runnable() {
                        public final void run() {
                            if (UnityPlayer.this.nativePause()) {
                                UnityPlayer.this.f467o = true;
                                UnityPlayer.this.m476f();
                                semaphore.release(2);
                                return;
                            }
                            semaphore.release();
                        }
                    });
                }
                try {
                    if (!semaphore.tryAcquire(4, TimeUnit.SECONDS)) {
                        C1104e.Log(5, "Timeout while trying to pause the Unity Engine.");
                    }
                } catch (InterruptedException e) {
                    C1104e.Log(5, "UI thread got interrupted while trying to pause the Unity Engine.");
                }
                if (semaphore.drainPermits() > 0) {
                    quit();
                }
            }
            this.f458e.mo20532c(false);
            this.f458e.mo20531b(true);
            if (this.f461h) {
                this.f463j.listen(this.f462i, 0);
            }
            this.f454a.mo20455c();
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: f */
    public void m476f() {
        nativeDone();
    }

    /* access modifiers changed from: private */
    /* renamed from: g */
    public void m479g() {
        if (this.f458e.mo20535f()) {
            this.f458e.mo20532c(true);
            if (C1116l.m550c()) {
                m482i();
            }
            m468b((Runnable) new Runnable() {
                public final void run() {
                    UnityPlayer.this.nativeResume();
                }
            });
            this.f454a.mo20454b();
        }
    }

    /* renamed from: h */
    private static void m481h() {
        if (C1116l.m550c()) {
            if (!NativeLoader.unload()) {
                throw new UnsatisfiedLinkError("Unable to unload libraries from libmain.so");
            }
            C1116l.m549b();
        }
    }

    /* renamed from: i */
    private void m482i() {
        String[] a;
        if (this.f468p.getBoolean("useObb")) {
            for (String str : m464a((Context) this.f465l)) {
                String a2 = m454a(str);
                if (this.f468p.getBoolean(a2)) {
                    nativeFile(str);
                }
                this.f468p.remove(a2);
            }
        }
    }

    private final native void initJni(Context context);

    private final native boolean isQuiting();

    /* renamed from: j */
    private void m485j() {
        if (this.f465l instanceof Activity) {
            ((Activity) this.f465l).getWindow().setFlags(1024, 1024);
        }
    }

    protected static boolean loadLibraryStatic(String str) {
        try {
            System.loadLibrary(str);
            return true;
        } catch (UnsatisfiedLinkError e) {
            C1104e.Log(6, "Unable to find " + str);
            return false;
        } catch (Exception e2) {
            C1104e.Log(6, "Unknown error " + e2);
            return false;
        }
    }

    /* access modifiers changed from: private */
    public final native int nativeActivityIndicatorStyle();

    private final native void nativeDone();

    private final native void nativeFile(String str);

    /* access modifiers changed from: private */
    public final native void nativeFocusChanged(boolean z);

    private final native void nativeInitWWW(Class cls);

    private final native void nativeInitWebRequest(Class cls);

    private final native boolean nativeInjectEvent(InputEvent inputEvent);

    /* access modifiers changed from: private */
    public final native void nativeLowMemory();

    /* access modifiers changed from: private */
    public final native void nativeMuteMasterAudio(boolean z);

    /* access modifiers changed from: private */
    public final native boolean nativePause();

    private final native void nativeRecreateGfxState(int i, Surface surface);

    /* access modifiers changed from: private */
    public final native boolean nativeRender();

    /* access modifiers changed from: private */
    public final native void nativeResume();

    /* access modifiers changed from: private */
    public final native void nativeSetInputCanceled(boolean z);

    /* access modifiers changed from: private */
    public final native void nativeSetInputString(String str);

    /* access modifiers changed from: private */
    public final native void nativeSoftInputClosed();

    private static native void nativeUnitySendMessage(String str, String str2, String str3);

    /* access modifiers changed from: 0000 */
    /* renamed from: a */
    public final void mo20386a(Runnable runnable) {
        if (this.f465l instanceof Activity) {
            ((Activity) this.f465l).runOnUiThread(runnable);
        } else {
            C1104e.Log(5, "Not running Unity from an Activity; ignored...");
        }
    }

    /* access modifiers changed from: protected */
    public void addPhoneCallListener() {
        this.f461h = true;
        this.f463j.listen(this.f462i, 32);
    }

    public boolean addViewToPlayer(View view, boolean z) {
        boolean z2 = true;
        m459a(view, (View) z ? this.f466m : null);
        boolean z3 = view.getParent() == this;
        boolean z4 = z && this.f466m.getParent() == null;
        boolean z5 = this.f466m.getParent() == this;
        if (!z3 || (!z4 && !z5)) {
            z2 = false;
        }
        if (!z2) {
            if (!z3) {
                C1104e.Log(6, "addViewToPlayer: Failure adding view to hierarchy");
            }
            if (!z4 && !z5) {
                C1104e.Log(6, "addViewToPlayer: Failure removing old view from hierarchy");
            }
        }
        return z2;
    }

    public void configurationChanged(Configuration configuration) {
        if (this.f466m instanceof SurfaceView) {
            this.f466m.getHolder().setSizeFromLayout();
        }
        if (this.f469q != null) {
            this.f469q.updateVideoLayout();
        }
        if (this.f474v != null) {
            this.f474v.mo20507e();
        }
    }

    /* access modifiers changed from: protected */
    public void disableLogger() {
        C1104e.f578a = true;
    }

    public boolean displayChanged(int i, Surface surface) {
        if (i == 0) {
            this.f456c = surface != null;
            mo20386a((Runnable) new Runnable() {
                public final void run() {
                    if (UnityPlayer.this.f456c) {
                        UnityPlayer.this.removeView(UnityPlayer.this.f466m);
                    } else {
                        UnityPlayer.this.addView(UnityPlayer.this.f466m);
                    }
                }
            });
        }
        return m469b(i, surface);
    }

    /* access modifiers changed from: protected */
    public void executeGLThreadJobs() {
        while (true) {
            Runnable runnable = (Runnable) this.f459f.poll();
            if (runnable != null) {
                runnable.run();
            } else {
                return;
            }
        }
    }

    public Bundle getSettings() {
        return this.f468p;
    }

    /* access modifiers changed from: protected */
    public int getSplashMode() {
        return this.f468p.getInt("splash_mode", 0);
    }

    public View getView() {
        return this;
    }

    /* access modifiers changed from: protected */
    public void hideSoftInput() {
        final C10888 r0 = new Runnable() {
            public final void run() {
                if (UnityPlayer.this.f455b != null) {
                    UnityPlayer.this.f455b.dismiss();
                    UnityPlayer.this.f455b = null;
                }
            }
        };
        if (C1107h.f581b) {
            m460a((C1094d) new C1094d() {
                /* renamed from: a */
                public final void mo20428a() {
                    UnityPlayer.this.mo20386a(r0);
                }
            });
        } else {
            mo20386a((Runnable) r0);
        }
    }

    public void init(int i, boolean z) {
    }

    public boolean injectEvent(InputEvent inputEvent) {
        return nativeInjectEvent(inputEvent);
    }

    public boolean isAppQuiting() {
        return isQuiting();
    }

    /* access modifiers changed from: protected */
    public boolean isFinishing() {
        if (!this.f467o) {
            boolean z = (this.f465l instanceof Activity) && ((Activity) this.f465l).isFinishing();
            this.f467o = z;
            if (!z) {
                return false;
            }
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public void kill() {
        Process.killProcess(Process.myPid());
    }

    /* access modifiers changed from: protected */
    public long loadGoogleVR(boolean z, boolean z2, boolean z3) {
        final Semaphore semaphore = new Semaphore(0);
        mo20386a((Runnable) new Runnable() {
            public final void run() {
                if (!UnityPlayer.this.f474v.mo20503a(UnityPlayer.currentActivity, UnityPlayer.this.f465l, UnityPlayer.this.m465b())) {
                    C1104e.Log(6, "Unable to initialize Google VR subsystem.");
                }
                semaphore.release();
            }
        });
        try {
            if (!semaphore.tryAcquire(4, TimeUnit.SECONDS)) {
                C1104e.Log(5, "Timeout while trying to initialize Google VR.");
                return 0;
            }
            C107114 r2 = new Runnable() {
                public final void run() {
                    UnityPlayer.this.injectEvent(new KeyEvent(0, 4));
                    UnityPlayer.this.injectEvent(new KeyEvent(1, 4));
                }
            };
            if (this.f474v.mo20505c()) {
                return this.f474v.mo20499a(z, z2, z3, r2);
            }
            return 0;
        } catch (InterruptedException e) {
            C1104e.Log(5, "UI thread was interrupted while initializing Google VR. " + e.getLocalizedMessage());
            return 0;
        }
    }

    /* access modifiers changed from: protected */
    public boolean loadLibrary(String str) {
        return loadLibraryStatic(str);
    }

    public void lowMemory() {
        m468b((Runnable) new Runnable() {
            public final void run() {
                UnityPlayer.this.nativeLowMemory();
            }
        });
    }

    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    public boolean onKeyLongPress(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    public boolean onKeyMultiple(int i, int i2, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    public boolean onKeyUp(int i, KeyEvent keyEvent) {
        return injectEvent(keyEvent);
    }

    public boolean onTouchEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    public void pause() {
        if (this.f469q != null) {
            this.f470r = this.f469q.mo20541a();
            if (!this.f470r) {
                this.f469q.pause();
                return;
            }
            return;
        }
        this.f474v.mo20500a();
        m475e();
    }

    public void quit() {
        this.f467o = true;
        if (!this.f458e.mo20534e()) {
            pause();
        }
        unloadGoogleVR();
        this.f454a.mo20452a();
        try {
            this.f454a.join(4000);
        } catch (InterruptedException e) {
            this.f454a.interrupt();
        }
        if (this.f460g != null) {
            this.f465l.unregisterReceiver(this.f460g);
        }
        this.f460g = null;
        if (C1116l.m550c()) {
            removeAllViews();
        }
        kill();
        m481h();
    }

    public void removeViewFromPlayer(View view) {
        boolean z = true;
        m459a((View) this.f466m, view);
        boolean z2 = view.getParent() == null;
        boolean z3 = this.f466m.getParent() == this;
        if (!z2 || !z3) {
            z = false;
        }
        if (!z) {
            if (!z2) {
                C1104e.Log(6, "removeViewFromPlayer: Failure removing view from hierarchy");
            }
            if (!z3) {
                C1104e.Log(6, "removeVireFromPlayer: Failure agging old view to hierarchy");
            }
        }
    }

    public void reportError(String str, String str2) {
        StringBuilder sb = new StringBuilder();
        sb.append(str);
        sb.append(": ");
        sb.append(str2);
        C1104e.Log(6, sb.toString());
    }

    /* access modifiers changed from: protected */
    public void reportSoftInputStr(final String str, final int i, final boolean z) {
        if (i == 1) {
            hideSoftInput();
        }
        m460a((C1094d) new C1094d() {
            /* renamed from: a */
            public final void mo20428a() {
                if (z) {
                    UnityPlayer.this.nativeSetInputCanceled(true);
                } else if (str != null) {
                    UnityPlayer.this.nativeSetInputString(str);
                }
                if (i == 1) {
                    UnityPlayer.this.nativeSoftInputClosed();
                }
            }
        });
    }

    public void resume() {
        this.f458e.mo20531b(false);
        if (this.f469q == null) {
            m479g();
            this.f474v.mo20504b();
        } else if (!this.f470r) {
            this.f469q.start();
        }
    }

    /* access modifiers changed from: protected */
    public void setGoogleVREnabled(boolean z) {
        this.f474v.mo20502a(z);
    }

    /* access modifiers changed from: protected */
    public void setSoftInputStr(final String str) {
        mo20386a((Runnable) new Runnable() {
            public final void run() {
                if (UnityPlayer.this.f455b != null && str != null) {
                    UnityPlayer.this.f455b.mo20515a(str);
                }
            }
        });
    }

    /* access modifiers changed from: protected */
    public void showSoftInput(String str, int i, boolean z, boolean z2, boolean z3, boolean z4, String str2) {
        final String str3 = str;
        final int i2 = i;
        final boolean z5 = z;
        final boolean z6 = z2;
        final boolean z7 = z3;
        final boolean z8 = z4;
        final String str4 = str2;
        mo20386a((Runnable) new Runnable() {
            public final void run() {
                UnityPlayer.this.f455b = new C1108i(UnityPlayer.this.f465l, this, str3, i2, z5, z6, z7, str4);
                UnityPlayer.this.f455b.show();
            }
        });
    }

    /* access modifiers changed from: protected */
    public boolean showVideoPlayer(String str, int i, int i2, int i3, boolean z, int i4, int i5) {
        final Semaphore semaphore = new Semaphore(0);
        final AtomicInteger atomicInteger = new AtomicInteger(-1);
        final String str2 = str;
        final int i6 = i;
        final int i7 = i2;
        final int i8 = i3;
        final boolean z2 = z;
        final int i9 = i4;
        final int i10 = i5;
        mo20386a((Runnable) new Runnable() {
            public final void run() {
                if (UnityPlayer.this.f469q != null) {
                    C1104e.Log(5, "Video already playing");
                    atomicInteger.set(2);
                    semaphore.release();
                    return;
                }
                UnityPlayer.this.f469q = new C1119n(UnityPlayer.this.f465l, str2, i6, i7, i8, z2, (long) i9, (long) i10, new C1120a() {
                    /* renamed from: a */
                    public final void mo20433a(int i) {
                        atomicInteger.set(i);
                        if (i == 3) {
                            if (UnityPlayer.this.f466m.getParent() == null) {
                                UnityPlayer.this.addView(UnityPlayer.this.f466m);
                            }
                            if (UnityPlayer.this.f469q != null) {
                                UnityPlayer.this.f469q.destroyPlayer();
                                UnityPlayer.this.removeView(UnityPlayer.this.f469q);
                                UnityPlayer.this.f469q = null;
                            }
                            UnityPlayer.this.resume();
                        }
                        if (i != 0) {
                            semaphore.release();
                        }
                    }
                });
                UnityPlayer.this.addView(UnityPlayer.this.f469q);
            }
        });
        boolean z3 = false;
        try {
            semaphore.acquire();
            z3 = atomicInteger.get() != 2;
        } catch (InterruptedException e) {
        }
        if (z3) {
            if (this.f469q != null) {
                mo20386a((Runnable) new Runnable() {
                    public final void run() {
                        if (UnityPlayer.this.f469q != null) {
                            UnityPlayer.this.m475e();
                            UnityPlayer.this.f469q.requestFocus();
                            UnityPlayer.this.removeView(UnityPlayer.this.f466m);
                        }
                    }
                });
            }
        } else if (this.f469q != null) {
            mo20386a((Runnable) new Runnable() {
                public final void run() {
                    if (UnityPlayer.this.f466m.getParent() == null) {
                        UnityPlayer.this.addView(UnityPlayer.this.f466m);
                    }
                    if (UnityPlayer.this.f469q != null) {
                        UnityPlayer.this.f469q.destroyPlayer();
                        UnityPlayer.this.removeView(UnityPlayer.this.f469q);
                        UnityPlayer.this.f469q = null;
                    }
                    UnityPlayer.this.resume();
                }
            });
        }
        return z3;
    }

    /* access modifiers changed from: protected */
    public void startActivityIndicator() {
        mo20386a(this.f472t);
    }

    /* access modifiers changed from: protected */
    public void stopActivityIndicator() {
        mo20386a(this.f473u);
    }

    /* access modifiers changed from: protected */
    public void unloadGoogleVR() {
        this.f474v.mo20506d();
    }

    public void windowFocusChanged(final boolean z) {
        this.f458e.mo20530a(z);
        if (z && this.f455b != null) {
            reportSoftInputStr(null, 1, false);
        }
        m468b((Runnable) new Runnable() {
            public final void run() {
                UnityPlayer.this.nativeFocusChanged(z);
            }
        });
        this.f454a.mo20453a(z);
        m479g();
    }
}
