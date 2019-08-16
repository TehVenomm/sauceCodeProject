package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.text.TextUtils;
import com.google.android.gms.iid.InstanceIDListenerService;
import com.google.android.gms.stats.CodePackage;
import com.google.firebase.iid.FirebaseInstanceIdService;
import com.google.firebase.messaging.MessageForwardingService;
import java.lang.ref.WeakReference;

/* renamed from: com.appsflyer.u */
final class C0467u {

    /* renamed from: com.appsflyer.u$c */
    static class C0468c extends AsyncTask<Void, Void, String> {

        /* renamed from: ˊ */
        private String f351;

        /* renamed from: ˋ */
        private final WeakReference<Context> f352;

        /* access modifiers changed from: protected */
        public final /* synthetic */ Object doInBackground(Object[] objArr) {
            return m370();
        }

        /* access modifiers changed from: protected */
        public final /* synthetic */ void onPostExecute(Object obj) {
            String str = (String) obj;
            if (!TextUtils.isEmpty(str)) {
                String string = AppsFlyerProperties.getInstance().getString("afUninstallToken");
                C0432d dVar = new C0432d(str);
                if (string == null) {
                    C0467u.m369((Context) this.f352.get(), dVar);
                    return;
                }
                C0432d r2 = C0432d.m277(string);
                if (r2.mo6558(dVar)) {
                    C0467u.m369((Context) this.f352.get(), r2);
                }
            }
        }

        C0468c(WeakReference<Context> weakReference) {
            this.f352 = weakReference;
        }

        /* access modifiers changed from: protected */
        public final void onPreExecute() {
            super.onPreExecute();
            this.f351 = AppsFlyerProperties.getInstance().getString("gcmProjectNumber");
        }

        /* renamed from: ˎ */
        private String m370() {
            try {
                if (this.f351 != null) {
                    return C0467u.m368(this.f352, this.f351);
                }
                return null;
            } catch (Throwable th) {
                AFLogger.afErrorLog("Error registering for uninstall feature", th);
                return null;
            }
        }
    }

    C0467u() {
    }

    /* renamed from: ˎ */
    static boolean m366(Context context) {
        return m367(context) | m365(context);
    }

    /* renamed from: ˏ */
    private static boolean m367(Context context) {
        boolean z;
        boolean z2;
        boolean z3;
        if (AppsFlyerLib.getInstance().isTrackingStopped()) {
            return false;
        }
        try {
            Class.forName("com.google.android.gms.iid.InstanceIDListenerService");
            Intent intent = new Intent("com.google.android.gms.iid.InstanceID", null, context, GcmInstanceIdListener.class);
            Intent intent2 = new Intent("com.google.android.gms.iid.InstanceID", null, context, InstanceIDListenerService.class);
            if (context.getPackageManager().queryIntentServices(intent, 0).size() > 0) {
                z = true;
            } else {
                z = false;
            }
            if (!z) {
                if (context.getPackageManager().queryIntentServices(intent2, 0).size() > 0) {
                    z3 = true;
                } else {
                    z3 = false;
                }
                if (!z3) {
                    return false;
                }
            }
            if (context.getPackageManager().queryBroadcastReceivers(new Intent(MessageForwardingService.ACTION_REMOTE_INTENT, null, context, Class.forName("com.google.android.gms.gcm.GcmReceiver")), 0).size() > 0) {
                z2 = true;
            } else {
                z2 = false;
            }
            if (z2) {
                if (C0439a.m291(context, new StringBuilder().append(context.getPackageName()).append(".permission.C2D_MESSAGE").toString())) {
                    return true;
                }
                AFLogger.afWarnLog("Cannot verify existence of the app's \"permission.C2D_MESSAGE\" permission in the manifest. Please refer to documentation.");
                return false;
            }
            AFLogger.afWarnLog("Cannot verify existence of GcmReceiver receiver in the manifest. Please refer to documentation.");
            return false;
        } catch (ClassNotFoundException e) {
            AFLogger.afRDLog(e.getMessage());
            return false;
        } catch (Throwable th) {
            AFLogger.afErrorLog("An error occurred while trying to verify manifest declarations: ", th);
            return false;
        }
    }

    /* renamed from: ˋ */
    private static boolean m365(Context context) {
        boolean z;
        boolean z2;
        if (AppsFlyerLib.getInstance().isTrackingStopped()) {
            return false;
        }
        try {
            Class.forName("com.google.firebase.iid.FirebaseInstanceIdService");
            Intent intent = new Intent("com.google.firebase.INSTANCE_ID_EVENT", null, context, FirebaseInstanceIdListener.class);
            Intent intent2 = new Intent("com.google.firebase.INSTANCE_ID_EVENT", null, context, FirebaseInstanceIdService.class);
            if (context.getPackageManager().queryIntentServices(intent, 0).size() > 0) {
                z = true;
            } else {
                z = false;
            }
            if (!z) {
                if (context.getPackageManager().queryIntentServices(intent2, 0).size() > 0) {
                    z2 = true;
                } else {
                    z2 = false;
                }
                if (!z2) {
                    AFLogger.afWarnLog("Cannot verify existence of our InstanceID Listener Service in the manifest. Please refer to documentation.");
                    return false;
                }
            }
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        } catch (Throwable th) {
            AFLogger.afErrorLog("An error occurred while trying to verify manifest declarations: ", th);
            return false;
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: ॱ */
    public static String m368(WeakReference<Context> weakReference, String str) {
        try {
            Class cls = Class.forName("com.google.android.gms.iid.InstanceID");
            Class.forName("com.google.android.gms.gcm.GcmReceiver");
            Object invoke = cls.getDeclaredMethod("getInstance", new Class[]{Context.class}).invoke(cls, new Object[]{weakReference.get()});
            String str2 = (String) cls.getDeclaredMethod("getToken", new Class[]{String.class, String.class}).invoke(invoke, new Object[]{str, CodePackage.GCM});
            if (str2 != null) {
                return str2;
            }
            AFLogger.afWarnLog("Couldn't get token using reflection.");
            return null;
        } catch (ClassNotFoundException e) {
            return null;
        } catch (Throwable th) {
            AFLogger.afErrorLog("Couldn't get token using GoogleCloudMessaging. ", th);
            return null;
        }
    }

    /* renamed from: ॱ */
    static void m369(Context context, C0432d dVar) {
        AFLogger.afInfoLog(new StringBuilder("updateServerUninstallToken called with: ").append(dVar.toString()).toString());
        C0432d r0 = C0432d.m277(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
        if (!context.getSharedPreferences("appsflyer-data", 0).getBoolean("sentRegisterRequestToAF", false) || r0.mo6557() == null || !r0.mo6557().equals(dVar.mo6557())) {
            AppsFlyerProperties.getInstance().set("afUninstallToken", dVar.toString());
            AppsFlyerLib.getInstance().mo6482(context, dVar.mo6557());
        }
    }
}
