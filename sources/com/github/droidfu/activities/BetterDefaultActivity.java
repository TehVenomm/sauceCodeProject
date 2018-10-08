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

    public AlertDialog newErrorHandlerDialog(int i, Exception exception) {
        return BetterActivityHelper.newErrorHandlerDialog(this, getString(i), exception);
    }

    public AlertDialog newErrorHandlerDialog(Exception exception) {
        return newErrorHandlerDialog(getResources().getIdentifier(BetterActivityHelper.ERROR_DIALOG_TITLE_RESOURCE, "string", getPackageName()), exception);
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

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.wasCreated = true;
        this.currentIntent = getIntent();
        Application application = getApplication();
        if (application instanceof DroidFuApplication) {
            ((DroidFuApplication) application).setActiveContext(getClass().getCanonicalName(), this);
        }
    }

    protected Dialog onCreateDialog(int i) {
        return BetterActivityHelper.createProgressDialog(this, this.progressDialogTitleId, this.progressDialogMsgId);
    }

    protected void onDestroy() {
        super.onDestroy();
    }

    public boolean onKeyDown(int i, KeyEvent keyEvent) {
        BetterActivityHelper.handleApplicationClosing(this, i);
        return super.onKeyDown(i, keyEvent);
    }

    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        this.currentIntent = intent;
    }

    protected void onPause() {
        super.onPause();
        this.wasInterrupted = false;
        this.wasCreated = false;
    }

    protected void onRestoreInstanceState(Bundle bundle) {
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
