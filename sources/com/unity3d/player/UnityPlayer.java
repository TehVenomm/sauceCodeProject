package com.unity3d.player;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.app.NativeActivity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.ContextWrapper;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Configuration;
import android.hardware.Camera;
import android.hardware.Camera.CameraInfo;
import android.hardware.Camera.Size;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.Environment;
import android.os.Process;
import android.view.InputEvent;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Surface;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;
import android.view.View;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ProgressBar;
import com.unity3d.player.C0747a.C0743a;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;

public class UnityPlayer extends FrameLayout implements C0743a {
    /* renamed from: D */
    private static Lock f439D = new ReentrantLock();
    public static Activity currentActivity = null;
    /* renamed from: p */
    private static boolean f440p;
    /* renamed from: A */
    private ProgressBar f441A = null;
    /* renamed from: B */
    private Runnable f442B = new C07332(this);
    /* renamed from: C */
    private Runnable f443C = new C07354(this);
    /* renamed from: a */
    C0742b f444a = new C0742b(this);
    /* renamed from: b */
    C0778s f445b = null;
    /* renamed from: c */
    private boolean f446c = false;
    /* renamed from: d */
    private boolean f447d = false;
    /* renamed from: e */
    private boolean f448e = true;
    /* renamed from: f */
    private final C0759j f449f;
    /* renamed from: g */
    private final C0779t f450g;
    /* renamed from: h */
    private boolean f451h = false;
    /* renamed from: i */
    private C0781v f452i = new C0781v();
    /* renamed from: j */
    private final ConcurrentLinkedQueue f453j = new ConcurrentLinkedQueue();
    /* renamed from: k */
    private BroadcastReceiver f454k = null;
    /* renamed from: l */
    private boolean f455l = false;
    /* renamed from: m */
    private ContextWrapper f456m;
    /* renamed from: n */
    private SurfaceView f457n;
    /* renamed from: o */
    private WindowManager f458o;
    /* renamed from: q */
    private boolean f459q;
    /* renamed from: r */
    private boolean f460r = true;
    /* renamed from: s */
    private int f461s = 0;
    /* renamed from: t */
    private int f462t = 0;
    /* renamed from: u */
    private final C0774r f463u;
    /* renamed from: v */
    private String f464v = null;
    /* renamed from: w */
    private NetworkInfo f465w = null;
    /* renamed from: x */
    private Bundle f466x = new Bundle();
    /* renamed from: y */
    private List f467y = new ArrayList();
    /* renamed from: z */
    private C0783w f468z;

    /* renamed from: com.unity3d.player.UnityPlayer$c */
    private abstract class C0730c implements Runnable {
        /* renamed from: f */
        final /* synthetic */ UnityPlayer f380f;

        private C0730c(UnityPlayer unityPlayer) {
            this.f380f = unityPlayer;
        }

        /* renamed from: a */
        public abstract void mo4190a();

        public final void run() {
            if (!this.f380f.isFinishing()) {
                mo4190a();
            }
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$1 */
    final class C07321 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ UnityPlayer f408a;

        C07321(UnityPlayer unityPlayer) {
            this.f408a = unityPlayer;
        }

        public final void onClick(DialogInterface dialogInterface, int i) {
            this.f408a.m429b();
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$2 */
    final class C07332 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ UnityPlayer f411a;

        C07332(UnityPlayer unityPlayer) {
            this.f411a = unityPlayer;
        }

        public final void run() {
            int l = this.f411a.nativeActivityIndicatorStyle();
            if (l >= 0) {
                if (this.f411a.f441A == null) {
                    this.f411a.f441A = new ProgressBar(this.f411a.f456m, null, new int[]{16842874, 16843401, 16842873, 16843400}[l]);
                    this.f411a.f441A.setIndeterminate(true);
                    this.f411a.f441A.setLayoutParams(new LayoutParams(-2, -2, 51));
                    this.f411a.addView(this.f411a.f441A);
                }
                this.f411a.f441A.setVisibility(0);
                this.f411a.bringChildToFront(this.f411a.f441A);
            }
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$3 */
    class C07343 extends BroadcastReceiver {
        /* renamed from: a */
        final /* synthetic */ UnityPlayer f412a;

        public void onReceive(Context context, Intent intent) {
            this.f412a.m429b();
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$4 */
    final class C07354 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ UnityPlayer f413a;

        C07354(UnityPlayer unityPlayer) {
            this.f413a = unityPlayer;
        }

        public final void run() {
            if (this.f413a.f441A != null) {
                this.f413a.f441A.setVisibility(8);
                this.f413a.removeView(this.f413a.f441A);
                this.f413a.f441A = null;
            }
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$7 */
    final class C07387 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ UnityPlayer f425a;

        C07387(UnityPlayer unityPlayer) {
            this.f425a = unityPlayer;
        }

        public final void run() {
            if (this.f425a.f445b != null) {
                this.f425a.f445b.dismiss();
                this.f425a.f445b = null;
            }
        }
    }

    /* renamed from: com.unity3d.player.UnityPlayer$a */
    enum C0741a {
        PAUSE,
        RESUME,
        QUIT,
        FOCUS_GAINED,
        FOCUS_LOST
    }

    /* renamed from: com.unity3d.player.UnityPlayer$b */
    private final class C0742b extends Thread {
        /* renamed from: a */
        ArrayBlockingQueue f436a = new ArrayBlockingQueue(32);
        /* renamed from: b */
        boolean f437b = false;
        /* renamed from: c */
        final /* synthetic */ UnityPlayer f438c;

        C0742b(UnityPlayer unityPlayer) {
            this.f438c = unityPlayer;
        }

        /* renamed from: a */
        private void m410a(C0741a c0741a) {
            try {
                this.f436a.put(c0741a);
            } catch (InterruptedException e) {
                interrupt();
            }
        }

        /* renamed from: a */
        public final void m411a() {
            m410a(C0741a.QUIT);
        }

        /* renamed from: a */
        public final void m412a(boolean z) {
            m410a(z ? C0741a.FOCUS_GAINED : C0741a.FOCUS_LOST);
        }

        /* renamed from: b */
        public final void m413b() {
            m410a(C0741a.RESUME);
        }

        /* renamed from: c */
        public final void m414c() {
            m410a(C0741a.PAUSE);
        }

        public final void run() {
            setName("UnityMain");
            while (true) {
                try {
                    C0741a c0741a = (C0741a) this.f436a.take();
                    if (c0741a != C0741a.QUIT) {
                        if (c0741a == C0741a.RESUME) {
                            this.f437b = true;
                        } else if (c0741a == C0741a.PAUSE) {
                            this.f437b = false;
                            this.f438c.executeGLThreadJobs();
                        } else if (c0741a == C0741a.FOCUS_LOST && !this.f437b) {
                            this.f438c.executeGLThreadJobs();
                        }
                        if (this.f437b) {
                            do {
                                this.f438c.executeGLThreadJobs();
                                if (this.f436a.peek() != null) {
                                    break;
                                } else if (!(this.f438c.isFinishing() || this.f438c.nativeRender())) {
                                    this.f438c.m429b();
                                }
                            } while (!C0742b.interrupted());
                        }
                    } else {
                        return;
                    }
                } catch (InterruptedException e) {
                    return;
                }
            }
        }
    }

    static {
        new C0780u().m542a();
        f440p = false;
        f440p = loadLibraryStatic("main");
    }

    public UnityPlayer(ContextWrapper contextWrapper) {
        super(contextWrapper);
        if (contextWrapper instanceof Activity) {
            currentActivity = (Activity) contextWrapper;
        }
        this.f450g = new C0779t(this);
        this.f456m = contextWrapper;
        this.f449f = contextWrapper instanceof Activity ? new C0772p(contextWrapper) : null;
        this.f463u = new C0774r(contextWrapper, this);
        m418a();
        if (C0773q.f535a) {
            C0773q.f543i.mo4194a((View) this);
        }
        setFullscreen(true);
        m420a(this.f456m.getApplicationInfo());
        if (C0781v.m545c()) {
            nativeFile(this.f456m.getPackageCodePath());
            m447j();
            this.f457n = new SurfaceView(contextWrapper);
            this.f457n.getHolder().setFormat(2);
            this.f457n.getHolder().addCallback(new Callback(this) {
                /* renamed from: a */
                final /* synthetic */ UnityPlayer f398a;

                {
                    this.f398a = r1;
                }

                public final void surfaceChanged(SurfaceHolder surfaceHolder, int i, int i2, int i3) {
                    this.f398a.m419a(0, surfaceHolder.getSurface());
                }

                public final void surfaceCreated(SurfaceHolder surfaceHolder) {
                    this.f398a.m419a(0, surfaceHolder.getSurface());
                }

                public final void surfaceDestroyed(SurfaceHolder surfaceHolder) {
                    this.f398a.m419a(0, null);
                }
            });
            this.f457n.setFocusable(true);
            this.f457n.setFocusableInTouchMode(true);
            this.f450g.m540c(this.f457n);
            this.f459q = false;
            m433c();
            initJni(contextWrapper);
            nativeInitWWW(WWW.class);
            nativeInitWebRequest(UnityWebRequest.class);
            if (C0773q.f539e) {
                C0773q.f546l.mo4199a(this, this.f456m);
            }
            if (C0773q.f542h && currentActivity != null) {
                C0773q.f547m.mo4203a(currentActivity, new Runnable(this) {
                    /* renamed from: a */
                    final /* synthetic */ UnityPlayer f401a;

                    /* renamed from: com.unity3d.player.UnityPlayer$15$1 */
                    final class C07311 implements Runnable {
                        /* renamed from: a */
                        final /* synthetic */ AnonymousClass15 f400a;

                        C07311(AnonymousClass15 anonymousClass15) {
                            this.f400a = anonymousClass15;
                        }

                        public final void run() {
                            this.f400a.f401a.f452i.m549d();
                            this.f400a.f401a.m442g();
                        }
                    }

                    {
                        this.f401a = r1;
                    }

                    public final void run() {
                        this.f401a.m457b(new C07311(this));
                    }
                });
            }
            if (C0773q.f538d) {
                C0773q.f545k.mo4202a(this);
            }
            this.f458o = (WindowManager) this.f456m.getSystemService("window");
            m449k();
            this.f444a.start();
            return;
        }
        AlertDialog create = new Builder(this.f456m).setTitle("Failure to initialize!").setPositiveButton("OK", new C07321(this)).setMessage("Your hardware does not support this application, sorry!").create();
        create.setCancelable(false);
        create.show();
    }

    public static native void UnitySendMessage(String str, String str2, String str3);

    /* renamed from: a */
    private static String m417a(String str) {
        byte[] digest;
        int i = 0;
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.MD5_INSTANCE);
            FileInputStream fileInputStream = new FileInputStream(str);
            long length = new File(str).length();
            fileInputStream.skip(length - Math.min(length, 65558));
            byte[] bArr = new byte[1024];
            for (int i2 = 0; i2 != -1; i2 = fileInputStream.read(bArr)) {
                instance.update(bArr, 0, i2);
            }
            digest = instance.digest();
        } catch (FileNotFoundException e) {
            digest = null;
        } catch (IOException e2) {
            digest = null;
        } catch (NoSuchAlgorithmException e3) {
            digest = null;
        }
        if (digest == null) {
            return null;
        }
        StringBuffer stringBuffer = new StringBuffer();
        while (i < digest.length) {
            stringBuffer.append(Integer.toString((digest[i] & 255) + 256, 16).substring(1));
            i++;
        }
        return stringBuffer.toString();
    }

    /* renamed from: a */
    private void m418a() {
        try {
            File file = new File(this.f456m.getPackageCodePath(), "assets/bin/Data/settings.xml");
            InputStream fileInputStream = file.exists() ? new FileInputStream(file) : this.f456m.getAssets().open("bin/Data/settings.xml");
            XmlPullParserFactory newInstance = XmlPullParserFactory.newInstance();
            newInstance.setNamespaceAware(true);
            XmlPullParser newPullParser = newInstance.newPullParser();
            newPullParser.setInput(fileInputStream, null);
            String str = null;
            int eventType = newPullParser.getEventType();
            String str2 = null;
            while (eventType != 1) {
                String name;
                String str3;
                if (eventType == 2) {
                    name = newPullParser.getName();
                    str3 = str;
                    str2 = str3;
                    for (int i = 0; i < newPullParser.getAttributeCount(); i++) {
                        if (newPullParser.getAttributeName(i).equalsIgnoreCase("name")) {
                            str2 = newPullParser.getAttributeValue(i);
                        }
                    }
                    str3 = name;
                    name = str2;
                    str2 = str3;
                } else if (eventType == 3) {
                    str2 = null;
                    name = str;
                } else if (eventType != 4 || str == null) {
                    name = str;
                } else {
                    if (str2.equalsIgnoreCase("integer")) {
                        this.f466x.putInt(str, Integer.parseInt(newPullParser.getText()));
                    } else if (str2.equalsIgnoreCase("string")) {
                        this.f466x.putString(str, newPullParser.getText());
                    } else if (str2.equalsIgnoreCase("bool")) {
                        this.f466x.putBoolean(str, Boolean.parseBoolean(newPullParser.getText()));
                    } else if (str2.equalsIgnoreCase("float")) {
                        this.f466x.putFloat(str, Float.parseFloat(newPullParser.getText()));
                    }
                    name = null;
                }
                str3 = name;
                eventType = newPullParser.next();
                str = str3;
            }
        } catch (Exception e) {
            C0767m.Log(6, "Unable to locate player settings. " + e.getLocalizedMessage());
            m429b();
        }
    }

    /* renamed from: a */
    private void m419a(int i, Surface surface) {
        if (!this.f446c) {
            m431b(0, surface);
        }
    }

    /* renamed from: a */
    private static void m420a(ApplicationInfo applicationInfo) {
        if (f440p && NativeLoader.load(applicationInfo.nativeLibraryDir)) {
            C0781v.m543a();
        }
    }

    /* renamed from: a */
    private void m421a(C0730c c0730c) {
        if (!isFinishing()) {
            m434c((Runnable) c0730c);
        }
    }

    /* renamed from: a */
    static void m426a(Runnable runnable) {
        new Thread(runnable).start();
    }

    /* renamed from: a */
    private static String[] m428a(Context context) {
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
                        packageName = file + File.separator + "patch." + i + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + packageName + ".obb";
                        if (new File(packageName).isFile()) {
                            vector.add(packageName);
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

    /* renamed from: b */
    private void m429b() {
        if ((this.f456m instanceof Activity) && !((Activity) this.f456m).isFinishing()) {
            ((Activity) this.f456m).finish();
        }
    }

    /* renamed from: b */
    private boolean m431b(int i, Surface surface) {
        if (!C0781v.m545c()) {
            return false;
        }
        nativeRecreateGfxState(i, surface);
        return true;
    }

    /* renamed from: c */
    private void m433c() {
        C0770o c0770o = new C0770o((Activity) this.f456m);
        if (this.f456m instanceof NativeActivity) {
            boolean a = c0770o.m509a();
            this.f455l = a;
            nativeForwardEventsToDalvik(a);
        }
    }

    /* renamed from: c */
    private void m434c(Runnable runnable) {
        if (!C0781v.m545c()) {
            return;
        }
        if (Thread.currentThread() == this.f444a) {
            runnable.run();
        } else {
            this.f453j.add(runnable);
        }
    }

    /* renamed from: d */
    private void m435d() {
        for (C0747a c : this.f467y) {
            c.m474c();
        }
    }

    /* renamed from: e */
    private void m437e() {
        for (C0747a c0747a : this.f467y) {
            try {
                c0747a.m471a((C0743a) this);
            } catch (Exception e) {
                C0767m.Log(6, "Unable to initialize camera: " + e.getMessage());
                c0747a.m474c();
            }
        }
    }

    /* renamed from: f */
    private void m440f() {
        nativeDone();
    }

    /* renamed from: g */
    private void m442g() {
        if (!this.f452i.m551f()) {
            return;
        }
        if (this.f468z != null) {
            this.f468z.onResume();
            return;
        }
        this.f452i.m548c(true);
        m437e();
        this.f463u.m526e();
        this.f464v = null;
        this.f465w = null;
        if (C0781v.m545c()) {
            m447j();
        }
        m434c(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ UnityPlayer f407a;

            {
                this.f407a = r1;
            }

            public final void run() {
                this.f407a.nativeResume();
            }
        });
        this.f444a.m413b();
    }

    /* renamed from: h */
    private static void m443h() {
        if (C0781v.m545c()) {
            lockNativeAccess();
            if (NativeLoader.unload()) {
                C0781v.m544b();
                unlockNativeAccess();
                return;
            }
            unlockNativeAccess();
            throw new UnsatisfiedLinkError("Unable to unload libraries from libmain.so");
        }
    }

    /* renamed from: i */
    private boolean m445i() {
        return this.f456m.getPackageManager().hasSystemFeature("android.hardware.camera") || this.f456m.getPackageManager().hasSystemFeature("android.hardware.camera.front");
    }

    private final native void initJni(Context context);

    /* renamed from: j */
    private void m447j() {
        if (this.f466x.getBoolean("useObb")) {
            for (String str : m428a(this.f456m)) {
                String a = m417a(str);
                if (this.f466x.getBoolean(a)) {
                    nativeFile(str);
                }
                this.f466x.remove(a);
            }
        }
    }

    /* renamed from: k */
    private void m449k() {
        ((Activity) this.f456m).getWindow().setFlags(1024, 1024);
    }

    protected static boolean loadLibraryStatic(String str) {
        try {
            System.loadLibrary(str);
            return true;
        } catch (UnsatisfiedLinkError e) {
            C0767m.Log(6, "Unable to find " + str);
            return false;
        } catch (Exception e2) {
            C0767m.Log(6, "Unknown error " + e2);
            return false;
        }
    }

    protected static void lockNativeAccess() {
        f439D.lock();
    }

    private final native int nativeActivityIndicatorStyle();

    private final native void nativeDone();

    private final native void nativeFile(String str);

    private final native void nativeFocusChanged(boolean z);

    private final native void nativeInitWWW(Class cls);

    private final native void nativeInitWebRequest(Class cls);

    private final native boolean nativeInjectEvent(InputEvent inputEvent);

    private final native boolean nativePause();

    private final native void nativeRecreateGfxState(int i, Surface surface);

    private final native boolean nativeRender();

    private final native void nativeResume();

    private final native void nativeSetExtras(Bundle bundle);

    private final native void nativeSetInputCanceled(boolean z);

    private final native void nativeSetInputString(String str);

    private final native void nativeSetTouchDeltaY(float f);

    private final native void nativeSoftInputClosed();

    private final native void nativeVideoFrameCallback(int i, byte[] bArr, int i2, int i3);

    protected static void unlockNativeAccess() {
        f439D.unlock();
    }

    protected boolean Location_IsServiceEnabledByUser() {
        return this.f463u.m521a();
    }

    protected void Location_SetDesiredAccuracy(float f) {
        this.f463u.m523b(f);
    }

    protected void Location_SetDistanceFilter(float f) {
        this.f463u.m520a(f);
    }

    protected void Location_StartUpdatingLocation() {
        this.f463u.m522b();
    }

    protected void Location_StopUpdatingLocation() {
        this.f463u.m524c();
    }

    /* renamed from: b */
    final void m457b(Runnable runnable) {
        if (this.f456m instanceof Activity) {
            ((Activity) this.f456m).runOnUiThread(runnable);
        } else {
            C0767m.Log(5, "Not running Unity from an Activity; ignored...");
        }
    }

    protected void closeCamera(int i) {
        for (C0747a c0747a : this.f467y) {
            if (c0747a.m470a() == i) {
                c0747a.m474c();
                this.f467y.remove(c0747a);
                return;
            }
        }
    }

    public void configurationChanged(Configuration configuration) {
        if (this.f457n instanceof SurfaceView) {
            this.f457n.getHolder().setSizeFromLayout();
        }
        if (this.f468z != null) {
            this.f468z.updateVideoLayout();
        }
    }

    protected void disableLogger() {
        C0767m.f525a = true;
    }

    public boolean displayChanged(int i, Surface surface) {
        if (i == 0) {
            this.f446c = surface != null;
            m457b(new Runnable(this) {
                /* renamed from: a */
                final /* synthetic */ UnityPlayer f402a;

                {
                    this.f402a = r1;
                }

                public final void run() {
                    if (this.f402a.f446c) {
                        this.f402a.f450g.m541d(this.f402a.f457n);
                    } else {
                        this.f402a.f450g.m540c(this.f402a.f457n);
                    }
                }
            });
        }
        return m431b(i, surface);
    }

    protected void executeGLThreadJobs() {
        while (true) {
            Runnable runnable = (Runnable) this.f453j.poll();
            if (runnable != null) {
                runnable.run();
            } else {
                return;
            }
        }
    }

    protected void forwardMotionEventToDalvik(long j, long j2, int i, int i2, int[] iArr, float[] fArr, int i3, float f, float f2, int i4, int i5, int i6, int i7, int i8, long[] jArr, float[] fArr2) {
        this.f449f.mo4204a(j, j2, i, i2, iArr, fArr, i3, f, f2, i4, i5, i6, i7, i8, jArr, fArr2);
    }

    protected int getCameraOrientation(int i) {
        CameraInfo cameraInfo = new CameraInfo();
        Camera.getCameraInfo(i, cameraInfo);
        return cameraInfo.orientation;
    }

    protected int getNumCameras() {
        return !m445i() ? 0 : Camera.getNumberOfCameras();
    }

    public Bundle getSettings() {
        return this.f466x;
    }

    protected int getSplashMode() {
        return this.f466x.getInt("splash_mode");
    }

    public View getView() {
        return this;
    }

    protected void hideSoftInput() {
        final Runnable c07387 = new C07387(this);
        if (C0773q.f541g) {
            m421a(new C0730c(this) {
                /* renamed from: b */
                final /* synthetic */ UnityPlayer f427b;

                /* renamed from: a */
                public final void mo4190a() {
                    this.f427b.m457b(c07387);
                }
            });
        } else {
            m457b(c07387);
        }
    }

    protected void hideVideoPlayer() {
        m457b(new Runnable(this) {
            /* renamed from: a */
            final /* synthetic */ UnityPlayer f399a;

            {
                this.f399a = r1;
            }

            public final void run() {
                if (this.f399a.f468z != null) {
                    this.f399a.f450g.m540c(this.f399a.f457n);
                    this.f399a.removeView(this.f399a.f468z);
                    this.f399a.f468z = null;
                    this.f399a.resume();
                }
            }
        });
    }

    public void init(int i, boolean z) {
    }

    protected int[] initCamera(int i, int i2, int i3, int i4) {
        C0747a c0747a = new C0747a(i, i2, i3, i4);
        try {
            c0747a.m471a((C0743a) this);
            this.f467y.add(c0747a);
            Size b = c0747a.m473b();
            return new int[]{b.width, b.height};
        } catch (Exception e) {
            C0767m.Log(6, "Unable to initialize camera: " + e.getMessage());
            c0747a.m474c();
            return null;
        }
    }

    public boolean injectEvent(InputEvent inputEvent) {
        return nativeInjectEvent(inputEvent);
    }

    protected boolean installPresentationDisplay(int i) {
        return C0773q.f539e ? C0773q.f546l.mo4200a(this, this.f456m, i) : false;
    }

    protected boolean isCameraFrontFacing(int i) {
        CameraInfo cameraInfo = new CameraInfo();
        Camera.getCameraInfo(i, cameraInfo);
        return cameraInfo.facing == 1;
    }

    protected boolean isFinishing() {
        if (!this.f459q) {
            boolean z = (this.f456m instanceof Activity) && ((Activity) this.f456m).isFinishing();
            this.f459q = z;
            if (!z) {
                return false;
            }
        }
        return true;
    }

    protected void kill() {
        Process.killProcess(Process.myPid());
    }

    protected boolean loadLibrary(String str) {
        return loadLibraryStatic(str);
    }

    protected final native void nativeAddVSyncTime(long j);

    final native void nativeForwardEventsToDalvik(boolean z);

    protected native void nativeSetLocation(float f, float f2, float f3, float f4, double d, float f5);

    protected native void nativeSetLocationStatus(int i);

    public void onCameraFrame(C0747a c0747a, byte[] bArr) {
        final int a = c0747a.m470a();
        final Size b = c0747a.m473b();
        final byte[] bArr2 = bArr;
        final C0747a c0747a2 = c0747a;
        m421a(new C0730c(this) {
            /* renamed from: e */
            final /* synthetic */ UnityPlayer f389e;

            /* renamed from: a */
            public final void mo4190a() {
                this.f389e.nativeVideoFrameCallback(a, bArr2, b.width, b.height);
                c0747a2.m472a(bArr2);
            }
        });
    }

    public boolean onGenericMotionEvent(MotionEvent motionEvent) {
        return injectEvent(motionEvent);
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
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
        if (this.f468z != null) {
            this.f468z.onPause();
            return;
        }
        reportSoftInputStr(null, 1, true);
        if (this.f452i.m552g()) {
            if (C0781v.m545c()) {
                final Semaphore semaphore = new Semaphore(0);
                if (isFinishing()) {
                    m434c(new Runnable(this) {
                        /* renamed from: b */
                        final /* synthetic */ UnityPlayer f404b;

                        public final void run() {
                            this.f404b.m440f();
                            semaphore.release();
                        }
                    });
                } else {
                    m434c(new Runnable(this) {
                        /* renamed from: b */
                        final /* synthetic */ UnityPlayer f406b;

                        public final void run() {
                            if (this.f406b.nativePause()) {
                                this.f406b.f459q = true;
                                this.f406b.m440f();
                                semaphore.release(2);
                                return;
                            }
                            semaphore.release();
                        }
                    });
                }
                try {
                    if (!semaphore.tryAcquire(4, TimeUnit.SECONDS)) {
                        C0767m.Log(5, "Timeout while trying to pause the Unity Engine.");
                    }
                } catch (InterruptedException e) {
                    C0767m.Log(5, "UI thread got interrupted while trying to pause the Unity Engine.");
                }
                if (semaphore.drainPermits() > 0) {
                    quit();
                }
            }
            this.f452i.m548c(false);
            this.f452i.m547b(true);
            m435d();
            this.f444a.m414c();
            this.f463u.m525d();
        }
    }

    public void quit() {
        this.f459q = true;
        if (!this.f452i.m550e()) {
            pause();
        }
        this.f444a.m411a();
        try {
            this.f444a.join(4000);
        } catch (InterruptedException e) {
            this.f444a.interrupt();
        }
        if (this.f454k != null) {
            this.f456m.unregisterReceiver(this.f454k);
        }
        this.f454k = null;
        if (C0781v.m545c()) {
            removeAllViews();
        }
        if (C0773q.f539e) {
            C0773q.f546l.mo4198a(this.f456m);
        }
        if (C0773q.f538d) {
            C0773q.f545k.mo4201a();
        }
        kill();
        m443h();
    }

    protected void reportSoftInputStr(final String str, final int i, final boolean z) {
        if (i == 1) {
            hideSoftInput();
        }
        m421a(new C0730c(this) {
            /* renamed from: d */
            final /* synthetic */ UnityPlayer f384d;

            /* renamed from: a */
            public final void mo4190a() {
                if (z) {
                    this.f384d.nativeSetInputCanceled(true);
                } else if (str != null) {
                    this.f384d.nativeSetInputString(str);
                }
                if (i == 1) {
                    this.f384d.nativeSoftInputClosed();
                }
            }
        });
    }

    public void resume() {
        if (C0773q.f535a) {
            C0773q.f543i.mo4197b(this);
        }
        this.f452i.m547b(false);
        m442g();
    }

    protected void setFullscreen(final boolean z) {
        this.f448e = z;
        if (C0773q.f535a) {
            m457b(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ UnityPlayer f415b;

                public final void run() {
                    C0773q.f543i.mo4195a(this.f415b, z);
                }
            });
        }
    }

    protected void setSoftInputStr(final String str) {
        m457b(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ UnityPlayer f429b;

            public final void run() {
                if (this.f429b.f445b != null && str != null) {
                    this.f429b.f445b.m534a(str);
                }
            }
        });
    }

    protected void showSoftInput(String str, int i, boolean z, boolean z2, boolean z3, boolean z4, String str2) {
        final UnityPlayer unityPlayer = this;
        final String str3 = str;
        final int i2 = i;
        final boolean z5 = z;
        final boolean z6 = z2;
        final boolean z7 = z3;
        final boolean z8 = z4;
        final String str4 = str2;
        m457b(new Runnable(this) {
            /* renamed from: i */
            final /* synthetic */ UnityPlayer f424i;

            public final void run() {
                this.f424i.f445b = new C0778s(this.f424i.f456m, unityPlayer, str3, i2, z5, z6, z7, str4);
                this.f424i.f445b.show();
            }
        });
    }

    protected void showVideoPlayer(String str, int i, int i2, int i3, boolean z, int i4, int i5) {
        final String str2 = str;
        final int i6 = i;
        final int i7 = i2;
        final int i8 = i3;
        final boolean z2 = z;
        final int i9 = i4;
        final int i10 = i5;
        m457b(new Runnable(this) {
            /* renamed from: h */
            final /* synthetic */ UnityPlayer f397h;

            public final void run() {
                if (this.f397h.f468z == null) {
                    this.f397h.pause();
                    this.f397h.f468z = new C0783w(this.f397h, this.f397h.f456m, str2, i6, i7, i8, z2, (long) i9, (long) i10);
                    this.f397h.addView(this.f397h.f468z);
                    this.f397h.f468z.requestFocus();
                    this.f397h.f450g.m541d(this.f397h.f457n);
                }
            }
        });
    }

    protected void startActivityIndicator() {
        m457b(this.f442B);
    }

    protected void stopActivityIndicator() {
        m457b(this.f443C);
    }

    public void windowFocusChanged(final boolean z) {
        this.f452i.m546a(z);
        if (z && this.f445b != null) {
            reportSoftInputStr(null, 1, false);
        }
        if (C0773q.f535a && z) {
            C0773q.f543i.mo4197b(this);
        }
        m434c(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ UnityPlayer f410b;

            public final void run() {
                this.f410b.nativeFocusChanged(z);
            }
        });
        this.f444a.m412a(z);
        m442g();
    }
}
