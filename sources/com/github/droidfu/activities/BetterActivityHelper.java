package com.github.droidfu.activities;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager.RunningTaskInfo;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.DialogInterface.OnKeyListener;
import android.content.Intent;
import android.view.KeyEvent;
import com.github.droidfu.DroidFuApplication;
import com.github.droidfu.dialogs.DialogClickListener;
import com.github.droidfu.exception.ResourceMessageException;
import com.github.droidfu.support.DiagnosticSupport;
import com.github.droidfu.support.IntentSupport;
import java.util.List;
import jp.colopl.drapro.LocalNotificationAlarmReceiver;

public class BetterActivityHelper {
    public static final String ERROR_DIALOG_TITLE_RESOURCE = "droidfu_error_dialog_title";
    private static final String PROGRESS_DIALOG_MESSAGE_RESOURCE = "droidfu_progress_dialog_message";
    private static final String PROGRESS_DIALOG_TITLE_RESOURCE = "droidfu_progress_dialog_title";

    /* renamed from: com.github.droidfu.activities.BetterActivityHelper$1 */
    class C05861 implements OnKeyListener {
        private final /* synthetic */ Activity val$activity;

        C05861(Activity activity) {
            this.val$activity = activity;
        }

        public boolean onKey(DialogInterface dialogInterface, int i, KeyEvent keyEvent) {
            this.val$activity.onKeyDown(i, keyEvent);
            return false;
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterActivityHelper$2 */
    class C05872 implements OnClickListener {
        C05872() {
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterActivityHelper$3 */
    class C05883 implements OnClickListener {
        C05883() {
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterActivityHelper$4 */
    class C05894 implements OnClickListener {
        private final /* synthetic */ Activity val$activity;
        private final /* synthetic */ Intent val$intent;

        C05894(Activity activity, Intent intent) {
            this.val$activity = activity;
            this.val$intent = intent;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            dialogInterface.dismiss();
            this.val$activity.startActivity(this.val$intent);
        }
    }

    /* renamed from: com.github.droidfu.activities.BetterActivityHelper$5 */
    class C05905 implements OnClickListener {
        private final /* synthetic */ boolean val$closeOnSelect;
        private final /* synthetic */ List val$elements;
        private final /* synthetic */ DialogClickListener val$listener;

        C05905(boolean z, DialogClickListener dialogClickListener, List list) {
            this.val$closeOnSelect = z;
            this.val$listener = dialogClickListener;
            this.val$elements = list;
        }

        public void onClick(DialogInterface dialogInterface, int i) {
            if (this.val$closeOnSelect) {
                dialogInterface.dismiss();
            }
            this.val$listener.onClick(i, this.val$elements.get(i));
        }
    }

    public static ProgressDialog createProgressDialog(Activity activity, int i, int i2) {
        ProgressDialog progressDialog = new ProgressDialog(activity);
        if (i <= 0) {
            i = activity.getResources().getIdentifier(PROGRESS_DIALOG_TITLE_RESOURCE, "string", activity.getPackageName());
        }
        progressDialog.setTitle(i);
        if (i2 <= 0) {
            i2 = activity.getResources().getIdentifier(PROGRESS_DIALOG_MESSAGE_RESOURCE, "string", activity.getPackageName());
        }
        progressDialog.setMessage(activity.getString(i2));
        progressDialog.setIndeterminate(true);
        progressDialog.setOnKeyListener(new C05861(activity));
        return progressDialog;
    }

    public static int getWindowFeatures(Activity activity) {
        if (activity.getWindow() == null) {
        }
        return 0;
    }

    static void handleApplicationClosing(Context context, int i) {
        if (i == 4) {
            List runningTasks = ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getRunningTasks(2);
            RunningTaskInfo runningTaskInfo = (RunningTaskInfo) runningTasks.get(0);
            RunningTaskInfo runningTaskInfo2 = (RunningTaskInfo) runningTasks.get(1);
            if (runningTaskInfo.topActivity.equals(runningTaskInfo.baseActivity) && runningTaskInfo2.baseActivity.getPackageName().startsWith("com.android.launcher")) {
                ((DroidFuApplication) context.getApplicationContext()).onClose();
            }
        }
    }

    public static boolean isApplicationBroughtToBackground(Context context) {
        List runningTasks = ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getRunningTasks(1);
        return (runningTasks.isEmpty() || ((RunningTaskInfo) runningTasks.get(0)).topActivity.getPackageName().equals(context.getPackageName())) ? false : true;
    }

    public static AlertDialog newErrorHandlerDialog(Activity activity, String str, Exception exception) {
        CharSequence string = exception instanceof ResourceMessageException ? activity.getString(((ResourceMessageException) exception).getClientMessageResourceId()) : exception.getLocalizedMessage();
        Builder builder = new Builder(activity);
        builder.setTitle(str);
        builder.setMessage(string);
        builder.setIcon(17301543);
        builder.setCancelable(false);
        builder.setPositiveButton(activity.getString(17039370), new C05883());
        if (IntentSupport.isIntentAvailable(activity, "android.intent.action.SEND", IntentSupport.MIME_TYPE_EMAIL)) {
            builder.setNegativeButton(activity.getString(activity.getResources().getIdentifier("droidfu_dialog_button_send_error_report", "string", activity.getPackageName())), new C05894(activity, IntentSupport.newEmailIntent(activity, activity.getString(activity.getResources().getIdentifier("droidfu_error_report_email_address", "string", activity.getPackageName())), activity.getString(activity.getResources().getIdentifier("droidfu_error_report_email_subject", "string", activity.getPackageName())), DiagnosticSupport.createDiagnosis(activity, exception))));
        }
        return builder.create();
    }

    public static <T> Dialog newListDialog(Activity activity, String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z) {
        return newListDialog(activity, str, list, dialogClickListener, z, 0);
    }

    public static <T> Dialog newListDialog(Activity activity, String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z, int i) {
        int size = list.size();
        CharSequence[] charSequenceArr = new String[size];
        for (int i2 = 0; i2 < size; i2++) {
            charSequenceArr[i2] = list.get(i2).toString();
        }
        Builder builder = new Builder(activity);
        if (str != null) {
            builder.setTitle(str);
        }
        builder.setSingleChoiceItems(charSequenceArr, i, new C05905(z, dialogClickListener, list));
        return builder.create();
    }

    public static AlertDialog newMessageDialog(Context context, String str, String str2, int i) {
        Builder builder = new Builder(context);
        builder.setCancelable(false);
        builder.setPositiveButton("Okay", new C05872());
        builder.setTitle(str);
        builder.setMessage(str2);
        builder.setIcon(i);
        return builder.create();
    }

    public static AlertDialog newYesNoDialog(Context context, String str, String str2, int i, OnClickListener onClickListener) {
        Builder builder = new Builder(context);
        builder.setCancelable(false);
        builder.setPositiveButton(17039379, onClickListener);
        builder.setNegativeButton(17039369, onClickListener);
        builder.setTitle(str);
        builder.setMessage(str2);
        builder.setIcon(i);
        return builder.create();
    }
}
