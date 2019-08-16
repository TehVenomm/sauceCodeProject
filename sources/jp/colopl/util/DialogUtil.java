package p018jp.colopl.util;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.app.Dialog;
import android.content.Context;
import android.view.ContextThemeWrapper;

/* renamed from: jp.colopl.util.DialogUtil */
public class DialogUtil {
    public static Dialog show(Builder builder) {
        return show((Dialog) builder.create());
    }

    public static Dialog show(Dialog dialog) {
        Activity activity;
        Context context = dialog.getContext();
        if (context instanceof Activity) {
            activity = (Activity) context;
        } else {
            if (context instanceof ContextThemeWrapper) {
                Context baseContext = ((ContextThemeWrapper) context).getBaseContext();
                if (baseContext instanceof Activity) {
                    activity = (Activity) baseContext;
                }
            }
            activity = null;
        }
        if (activity != null && !activity.isFinishing()) {
            dialog.show();
        }
        return dialog;
    }

    private static void showErrorDialog(Context context, int i, int i2, int i3) {
        new Builder(context).setTitle(i).setMessage(i2).setCancelable(false).setPositiveButton(i3, null).show();
    }
}
