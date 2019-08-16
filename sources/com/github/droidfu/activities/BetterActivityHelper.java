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
import p018jp.colopl.drapro.LocalNotificationAlarmReceiver;

public class BetterActivityHelper {
    public static final String ERROR_DIALOG_TITLE_RESOURCE = "droidfu_error_dialog_title";
    private static final String PROGRESS_DIALOG_MESSAGE_RESOURCE = "droidfu_progress_dialog_message";
    private static final String PROGRESS_DIALOG_TITLE_RESOURCE = "droidfu_progress_dialog_title";

    public static ProgressDialog createProgressDialog(final Activity activity, int i, int i2) {
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
        progressDialog.setOnKeyListener(new OnKeyListener() {
            public boolean onKey(DialogInterface dialogInterface, int i, KeyEvent keyEvent) {
                activity.onKeyDown(i, keyEvent);
                return false;
            }
        });
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
        return !runningTasks.isEmpty() && !((RunningTaskInfo) runningTasks.get(0)).topActivity.getPackageName().equals(context.getPackageName());
    }

    public static AlertDialog newErrorHandlerDialog(final Activity activity, String str, Exception exc) {
        String localizedMessage = exc instanceof ResourceMessageException ? activity.getString(((ResourceMessageException) exc).getClientMessageResourceId()) : exc.getLocalizedMessage();
        Builder builder = new Builder(activity);
        builder.setTitle(str);
        builder.setMessage(localizedMessage);
        builder.setIcon(17301543);
        builder.setCancelable(false);
        builder.setPositiveButton(activity.getString(17039370), new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                dialogInterface.dismiss();
            }
        });
        if (IntentSupport.isIntentAvailable(activity, "android.intent.action.SEND", IntentSupport.MIME_TYPE_EMAIL)) {
            String string = activity.getString(activity.getResources().getIdentifier("droidfu_dialog_button_send_error_report", "string", activity.getPackageName()));
            final Intent newEmailIntent = IntentSupport.newEmailIntent(activity, activity.getString(activity.getResources().getIdentifier("droidfu_error_report_email_address", "string", activity.getPackageName())), activity.getString(activity.getResources().getIdentifier("droidfu_error_report_email_subject", "string", activity.getPackageName())), DiagnosticSupport.createDiagnosis(activity, exc));
            builder.setNegativeButton(string, new OnClickListener() {
                public void onClick(DialogInterface dialogInterface, int i) {
                    dialogInterface.dismiss();
                    activity.startActivity(newEmailIntent);
                }
            });
        }
        return builder.create();
    }

    public static <T> Dialog newListDialog(Activity activity, String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z) {
        return newListDialog(activity, str, list, dialogClickListener, z, 0);
    }

    public static <T> Dialog newListDialog(Activity activity, String str, final List<T> list, final DialogClickListener<T> dialogClickListener, final boolean z, int i) {
        int size = list.size();
        String[] strArr = new String[size];
        for (int i2 = 0; i2 < size; i2++) {
            strArr[i2] = list.get(i2).toString();
        }
        Builder builder = new Builder(activity);
        if (str != null) {
            builder.setTitle(str);
        }
        builder.setSingleChoiceItems(strArr, i, new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                if (z) {
                    dialogInterface.dismiss();
                }
                dialogClickListener.onClick(i, list.get(i));
            }
        });
        return builder.create();
    }

    public static AlertDialog newMessageDialog(Context context, String str, String str2, int i) {
        Builder builder = new Builder(context);
        builder.setCancelable(false);
        builder.setPositiveButton("Okay", new OnClickListener() {
            public void onClick(DialogInterface dialogInterface, int i) {
                dialogInterface.dismiss();
            }
        });
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
