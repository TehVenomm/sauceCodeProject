package com.github.droidfu.concurrent;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import com.github.droidfu.DroidFuApplication;
import com.github.droidfu.activities.BetterActivity;

public abstract class BetterAsyncTask<ParameterT, ProgressT, ReturnT> extends AsyncTask<ParameterT, ProgressT, ReturnT> {
    private final DroidFuApplication appContext;
    private BetterAsyncTaskCallable<ParameterT, ProgressT, ReturnT> callable;
    private final String callerId;
    private final boolean contextIsDroidFuActivity;
    private int dialogId = 0;
    private Exception error;
    private boolean isTitleProgressEnabled;
    private boolean isTitleProgressIndeterminateEnabled = true;

    public BetterAsyncTask(Context context) {
        if (!(context.getApplicationContext() instanceof DroidFuApplication)) {
            throw new IllegalArgumentException("context bound to this task must be a DroidFu context (DroidFuApplication)");
        }
        this.appContext = (DroidFuApplication) context.getApplicationContext();
        this.callerId = context.getClass().getCanonicalName();
        this.contextIsDroidFuActivity = context instanceof BetterActivity;
        this.appContext.setActiveContext(this.callerId, context);
        if (this.contextIsDroidFuActivity) {
            int windowFeatures = ((BetterActivity) context).getWindowFeatures();
            if (2 == (windowFeatures & 2)) {
                this.isTitleProgressEnabled = true;
            } else if (5 == (windowFeatures & 5)) {
                this.isTitleProgressIndeterminateEnabled = true;
            }
        }
    }

    /* access modifiers changed from: protected */
    public abstract void after(Context context, ReturnT returnt);

    /* access modifiers changed from: protected */
    public void before(Context context) {
    }

    public void disableDialog() {
        this.dialogId = -1;
    }

    /* access modifiers changed from: protected */
    public ReturnT doCheckedInBackground(Context context, ParameterT... parametertArr) throws Exception {
        if (this.callable != null) {
            return this.callable.call(this);
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public final ReturnT doInBackground(ParameterT... parametertArr) {
        boolean z = false;
        try {
            return doCheckedInBackground(getCallingContext(), parametertArr);
        } catch (Exception e) {
            this.error = e;
            return z;
        }
    }

    public boolean failed() {
        return this.error != null;
    }

    /* access modifiers changed from: protected */
    public Context getCallingContext() {
        try {
            Context activeContext = this.appContext.getActiveContext(this.callerId);
            if (activeContext == null || !this.callerId.equals(activeContext.getClass().getCanonicalName()) || ((activeContext instanceof Activity) && ((Activity) activeContext).isFinishing())) {
                return null;
            }
            return activeContext;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    /* access modifiers changed from: protected */
    public abstract void handleError(Context context, Exception exc);

    /* access modifiers changed from: protected */
    public final void onPostExecute(ReturnT returnt) {
        Context callingContext = getCallingContext();
        if (callingContext == null) {
            Log.d(BetterAsyncTask.class.getSimpleName(), "skipping post-exec handler for task " + hashCode() + " (context is null)");
            return;
        }
        if (this.contextIsDroidFuActivity) {
            Activity activity = (Activity) callingContext;
            if (this.dialogId > -1) {
                activity.removeDialog(this.dialogId);
            }
            if (this.isTitleProgressEnabled) {
                activity.setProgressBarVisibility(false);
            } else if (this.isTitleProgressIndeterminateEnabled) {
                activity.setProgressBarIndeterminateVisibility(false);
            }
        }
        if (failed()) {
            handleError(callingContext, this.error);
        } else {
            after(callingContext, returnt);
        }
    }

    /* access modifiers changed from: protected */
    public final void onPreExecute() {
        Context callingContext = getCallingContext();
        if (callingContext == null) {
            Log.d(BetterAsyncTask.class.getSimpleName(), "skipping pre-exec handler for task " + hashCode() + " (context is null)");
            cancel(true);
            return;
        }
        if (this.contextIsDroidFuActivity) {
            Activity activity = (Activity) callingContext;
            if (this.dialogId > -1) {
                activity.showDialog(this.dialogId);
            }
            if (this.isTitleProgressEnabled) {
                activity.setProgressBarVisibility(true);
            } else if (this.isTitleProgressIndeterminateEnabled) {
                activity.setProgressBarIndeterminateVisibility(true);
            }
        }
        before(callingContext);
    }

    public void setCallable(BetterAsyncTaskCallable<ParameterT, ProgressT, ReturnT> betterAsyncTaskCallable) {
        this.callable = betterAsyncTaskCallable;
    }

    public void useCustomDialog(int i) {
        this.dialogId = i;
    }
}
