package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.annotation.NonNull;
import com.facebook.internal.FacebookRequestErrorClassification;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

/* renamed from: com.appsflyer.c */
final class C0264c {
    /* renamed from: ˊ */
    private IntentFilter f217 = new IntentFilter("android.intent.action.BATTERY_CHANGED");

    /* renamed from: com.appsflyer.c$c */
    static final class C0262c {
        /* renamed from: ˊ */
        static final C0264c f214 = new C0264c();
    }

    /* renamed from: com.appsflyer.c$e */
    static final class C0263e {
        /* renamed from: ˋ */
        private final String f215;
        /* renamed from: ˎ */
        private final float f216;

        C0263e(float f, String str) {
            this.f216 = f;
            this.f215 = str;
        }

        /* renamed from: ˎ */
        final float m282() {
            return this.f216;
        }

        /* renamed from: ॱ */
        final String m283() {
            return this.f215;
        }

        C0263e() {
        }
    }

    C0264c() {
    }

    @NonNull
    /* renamed from: ˊ */
    final C0263e m284(Context context) {
        String str = null;
        float f = 0.0f;
        try {
            Intent registerReceiver = context.registerReceiver(null, this.f217);
            if (registerReceiver != null) {
                if ((2 == registerReceiver.getIntExtra("status", -1) ? 1 : null) != null) {
                    switch (registerReceiver.getIntExtra("plugged", -1)) {
                        case 1:
                            str = "ac";
                            break;
                        case 2:
                            str = "usb";
                            break;
                        case 4:
                            str = "wireless";
                            break;
                        default:
                            str = FacebookRequestErrorClassification.KEY_OTHER;
                            break;
                    }
                }
                str = "no";
                int intExtra = registerReceiver.getIntExtra(Param.LEVEL, -1);
                int intExtra2 = registerReceiver.getIntExtra("scale", -1);
                if (!(-1 == intExtra || -1 == intExtra2)) {
                    f = (100.0f * ((float) intExtra)) / ((float) intExtra2);
                }
            }
        } catch (Throwable th) {
        }
        return new C0263e(f, str);
    }
}
