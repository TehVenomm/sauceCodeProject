package net.gogame.gowrap.support;

import android.content.Context;
import android.util.Log;
import java.lang.reflect.Method;
import net.gogame.gowrap.Constants;

public final class PushUtils {
    private PushUtils() {
    }

    public static String getToken(Context context) {
        String tokenViaFirebase = getTokenViaFirebase(context);
        return tokenViaFirebase != null ? tokenViaFirebase : getTokenViaGcm(context);
    }

    private static String getTokenViaFirebase(Context context) {
        try {
            Class cls = Class.forName("com.google.firebase.iid.FirebaseInstanceId");
            Object invoke = cls.getMethod("getInstance", new Class[0]).invoke(null, new Object[0]);
            Method method = cls.getMethod("getToken", new Class[0]);
            int i = 0;
            while (i < 60) {
                String str = (String) method.invoke(invoke, new Object[0]);
                if (str != null) {
                    return str;
                }
                try {
                    Thread.sleep(1000);
                    i++;
                } catch (InterruptedException e) {
                    return null;
                }
            }
        } catch (Throwable e2) {
            Log.e(Constants.TAG, "Exception", e2);
        }
        return null;
    }

    private static String getTokenViaGcm(Context context) {
        try {
            Class cls = Class.forName("com.google.android.gms.iid.InstanceID");
            Object invoke = cls.getMethod("getInstance", new Class[]{Context.class}).invoke(null, new Object[]{context});
            String str = "GCM";
            return (String) cls.getMethod("getToken", new Class[]{String.class, String.class}).invoke(invoke, new Object[]{getSenderId(context), "GCM"});
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            return null;
        }
    }

    private static String getResourceString(Context context, String str) {
        int identifier = context.getResources().getIdentifier(str, "string", context.getPackageName());
        if (identifier == 0) {
            return null;
        }
        return context.getResources().getString(identifier);
    }

    private static String getSenderId(Context context) {
        return getResourceString(context, "gcm_defaultSenderId");
    }
}
