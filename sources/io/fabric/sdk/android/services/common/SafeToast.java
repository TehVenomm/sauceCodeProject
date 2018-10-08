package io.fabric.sdk.android.services.common;

import android.content.Context;
import android.content.res.Resources.NotFoundException;
import android.os.Handler;
import android.os.Looper;
import android.widget.Toast;
import io.fabric.sdk.android.services.concurrency.PriorityRunnable;

public class SafeToast extends Toast {

    /* renamed from: io.fabric.sdk.android.services.common.SafeToast$1 */
    class C09241 extends PriorityRunnable {
        C09241() {
        }

        public void run() {
            super.show();
        }
    }

    public SafeToast(Context context) {
        super(context);
    }

    public static Toast makeText(Context context, int i, int i2) throws NotFoundException {
        return makeText(context, context.getResources().getText(i), i2);
    }

    public static Toast makeText(Context context, CharSequence charSequence, int i) {
        Toast makeText = Toast.makeText(context, charSequence, i);
        Toast safeToast = new SafeToast(context);
        safeToast.setView(makeText.getView());
        safeToast.setDuration(makeText.getDuration());
        return safeToast;
    }

    public void show() {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            super.show();
        } else {
            new Handler(Looper.getMainLooper()).post(new C09241());
        }
    }
}
