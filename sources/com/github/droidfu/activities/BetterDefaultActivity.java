package com.github.droidfu.activities;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Application;
import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import com.github.droidfu.DroidFuApplication;
import com.github.droidfu.dialogs.DialogClickListener;
import java.util.List;

public class BetterDefaultActivity extends Activity implements BetterActivity {
    private Intent currentIntent;
    private int progressDialogMsgId;
    private int progressDialogTitleId;
    private boolean wasCreated;
    private boolean wasInterrupted;

    public Intent getCurrentIntent() {
        return this.currentIntent;
    }

    public int getWindowFeatures() {
        return BetterActivityHelper.getWindowFeatures(this);
    }

    public boolean isApplicationBroughtToBackground() {
        return BetterActivityHelper.isApplicationBroughtToBackground(this);
    }

    public boolean isLandscapeMode() {
        return getWindowManager().getDefaultDisplay().getOrientation() == 1;
    }

    public boolean isLaunching() {
        return !this.wasInterrupted && this.wasCreated;
    }

    public boolean isPortraitMode() {
        return !isLandscapeMode();
    }

    public boolean isRestoring() {
        return this.wasInterrupted;
    }

    public boolean isResuming() {
        return !this.wasCreated;
    }

    public AlertDialog newAlertDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301543);
    }

    public AlertDialog newErrorHandlerDialog(int i, Exception exc) {
        return BetterActivityHelper.newErrorHandlerDialog(this, getString(i), exc);
    }

    public AlertDialog newErrorHandlerDialog(Exception exc) {
        return newErrorHandlerDialog(getResources().getIdentifier(BetterActivityHelper.ERROR_DIALOG_TITLE_RESOURCE, "string", getPackageName()), exc);
    }

    public AlertDialog newInfoDialog(int i, int i2) {
        return BetterActivityHelper.newMessageDialog(this, getString(i), getString(i2), 17301659);
    }

    public <T> Dialog newListDialog(String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z) {
        return BetterActivityHelper.newListDialog(this, str, list, dialogClickListener, z);
    }

    public AlertDialog newYesNoDialog(int i, int i2, OnClickListener onClickListener) {
        return BetterActivityHelper.newYesNoDialog(this, getString(i), getString(i2), 17301659, onClickListener);
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.wasCreated = true;
        this.currentIntent = getIntent();
        Application application = getApplication();
        if (application instanceof DroidFuApplication) {
            ((DroidFuApplication) application).setActiveContext(getClass().getCanonicalName(), this);
        }
    }

    /* access modifiers changed from: protected */
    public Dialog onCreateDialog(int i) {
        return BetterActivityHelper.createProgressDialog(this, this.progressDialogTitleId, this.progressDialogMsgId);
    }

    /* access modifiers changed from: protected */
    public void onDestroy() {
        super.onDestroy();
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        BetterActivityHelper.handleApplicationClosing(this, i);
        return super.onKeyDown(i, keyEvent);
    }

    /* access modifiers changed from: protected */
    public void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        this.currentIntent = intent;
    }

    /* access modifiers changed from: protected */
    public void onPause() {
        super.onPause();
        this.wasInterrupted = false;
        this.wasCreated = false;
    }

    /* access modifiers changed from: protected */
    public void onRestoreInstanceState(Bundle bundle) {
        super.onRestoreInstanceState(bundle);
        this.wasInterrupted = true;
    }

    public void setProgressDialogMsgId(int i) {
        this.progressDialogMsgId = i;
    }

    public void setProgressDialogTitleId(int i) {
        this.progressDialogTitleId = i;
    }
}
