package com.github.droidfu.activities;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import com.github.droidfu.dialogs.DialogClickListener;
import java.util.List;

public interface BetterActivity {
    Intent getCurrentIntent();

    int getWindowFeatures();

    boolean isApplicationBroughtToBackground();

    boolean isLandscapeMode();

    boolean isLaunching();

    boolean isPortraitMode();

    boolean isRestoring();

    boolean isResuming();

    AlertDialog newAlertDialog(int i, int i2);

    AlertDialog newErrorHandlerDialog(int i, Exception exception);

    AlertDialog newErrorHandlerDialog(Exception exception);

    AlertDialog newInfoDialog(int i, int i2);

    <T> Dialog newListDialog(String str, List<T> list, DialogClickListener<T> dialogClickListener, boolean z);

    AlertDialog newYesNoDialog(int i, int i2, OnClickListener onClickListener);

    void setProgressDialogMsgId(int i);

    void setProgressDialogTitleId(int i);
}
