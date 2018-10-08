package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.text.TextUtils;
import com.appsflyer.C0273g.C0271a;
import com.google.android.gms.iid.InstanceIDListenerService;
import com.google.firebase.iid.FirebaseInstanceIdService;
import com.google.firebase.messaging.MessageForwardingService;
import java.lang.ref.WeakReference;

/* renamed from: com.appsflyer.u */
final class C0299u {

    /* renamed from: com.appsflyer.u$c */
    static class C0298c extends AsyncTask<Void, Void, String> {
        /* renamed from: ˊ */
        private String f330;
        /* renamed from: ˋ */
        private final WeakReference<Context> f331;

        protected final /* synthetic */ Object doInBackground(Object[] objArr) {
            return m369();
        }

        protected final /* synthetic */ void onPostExecute(Object obj) {
            String str = (String) obj;
            if (!TextUtils.isEmpty(str)) {
                String string = AppsFlyerProperties.getInstance().getString("afUninstallToken");
                C0265d c0265d = new C0265d(str);
                if (string == null) {
                    C0299u.m375((Context) this.f331.get(), c0265d);
                    return;
                }
                C0265d ˊ = C0265d.m286(string);
                if (ˊ.m290(c0265d)) {
                    C0299u.m375((Context) this.f331.get(), ˊ);
                }
            }
        }

        C0298c(WeakReference<Context> weakReference) {
            this.f331 = weakReference;
        }

        protected final void onPreExecute() {
            super.onPreExecute();
            this.f330 = AppsFlyerProperties.getInstance().getString("gcmProjectNumber");
        }

        /* renamed from: ˎ */
        private String m369() {
            String str = null;
            try {
                if (this.f330 != null) {
                    str = C0299u.m374(this.f331, this.f330);
                }
            } catch (Throwable th) {
                AFLogger.afErrorLog("Error registering for uninstall feature", th);
            }
            return str;
        }
    }

    C0299u() {
    }

    /* renamed from: ˎ */
    static boolean m372(Context context) {
        return C0299u.m373(context) | C0299u.m371(context);
    }

    /* renamed from: ˏ */
    private static boolean m373(Context context) {
        if (AppsFlyerLib.getInstance().isTrackingStopped()) {
            return false;
        }
        try {
            boolean z;
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
                    z = true;
                } else {
                    z = false;
                }
                if (!z) {
                    return false;
                }
            }
            if (context.getPackageManager().queryBroadcastReceivers(new Intent(MessageForwardingService.ACTION_REMOTE_INTENT, null, context, Class.forName("com.google.android.gms.gcm.GcmReceiver")), 0).size() > 0) {
                z = true;
            } else {
                z = false;
            }
            if (z) {
                if (C0271a.m299(context, new StringBuilder().append(context.getPackageName()).append(".permission.C2D_MESSAGE").toString())) {
                    return true;
                }
                AFLogger.afWarnLog("Cannot verify existence of the app's \"permission.C2D_MESSAGE\" permission in the manifest. Please refer to documentation.");
                return false;
            }
            AFLogger.afWarnLog("Cannot verify existence of GcmReceiver receiver in the manifest. Please refer to documentation.");
            return false;
        } catch (Throwable e) {
            AFLogger.afRDLog(e.getMessage());
            return false;
        } catch (Throwable e2) {
            AFLogger.afErrorLog("An error occurred while trying to verify manifest declarations: ", e2);
            return false;
        }
    }

    /* renamed from: ˋ */
    private static boolean m371(Context context) {
        if (AppsFlyerLib.getInstance().isTrackingStopped()) {
            return false;
        }
        try {
            boolean z;
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
                    z = true;
                } else {
                    z = false;
                }
                if (!z) {
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

    /* renamed from: ॱ */
    private static String m374(WeakReference<Context> weakReference, String str) {
        try {
            Class cls = Class.forName("com.google.android.gms.iid.InstanceID");
            Class.forName("com.google.android.gms.gcm.GcmReceiver");
            Object invoke = cls.getDeclaredMethod("getInstance", new Class[]{Context.class}).invoke(cls, new Object[]{weakReference.get()});
            String str2 = (String) cls.getDeclaredMethod("getToken", new Class[]{String.class, String.class}).invoke(invoke, new Object[]{str, "GCM"});
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
    static void m375(Context context, C0265d c0265d) {
        AFLogger.afInfoLog(new StringBuilder("updateServerUninstallToken called with: ").append(c0265d.toString()).toString());
        C0265d ˊ = C0265d.m286(AppsFlyerProperties.getInstance().getString("afUninstallToken"));
        if (!context.getSharedPreferences("appsflyer-data", 0).getBoolean("sentRegisterRequestToAF", false) || ˊ.m289() == null || !ˊ.m289().equals(c0265d.m289())) {
            AppsFlyerProperties.getInstance().set("afUninstallToken", c0265d.toString());
            AppsFlyerLib.getInstance().m258(context, c0265d.m289());
        }
    }
}
