package com.facebook.internal;

import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.FragmentActivity;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.internal.WebDialog.Builder;
import com.facebook.internal.WebDialog.OnCompleteListener;

public class FacebookDialogFragment extends DialogFragment {
    public static final String TAG = "FacebookDialogFragment";
    private Dialog dialog;

    /* renamed from: com.facebook.internal.FacebookDialogFragment$1 */
    class C04021 implements OnCompleteListener {
        C04021() {
        }

        public void onComplete(Bundle bundle, FacebookException facebookException) {
            FacebookDialogFragment.this.onCompleteWebDialog(bundle, facebookException);
        }
    }

    /* renamed from: com.facebook.internal.FacebookDialogFragment$2 */
    class C04032 implements OnCompleteListener {
        C04032() {
        }

        public void onComplete(Bundle bundle, FacebookException facebookException) {
            FacebookDialogFragment.this.onCompleteWebFallbackDialog(bundle);
        }
    }

    private void onCompleteWebDialog(Bundle bundle, FacebookException facebookException) {
        FragmentActivity activity = getActivity();
        activity.setResult(facebookException == null ? -1 : 0, NativeProtocol.createProtocolResultIntent(activity.getIntent(), bundle, facebookException));
        activity.finish();
    }

    private void onCompleteWebFallbackDialog(Bundle bundle) {
        FragmentActivity activity = getActivity();
        Intent intent = new Intent();
        if (bundle == null) {
            bundle = new Bundle();
        }
        intent.putExtras(bundle);
        activity.setResult(-1, intent);
        activity.finish();
    }

    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        if (this.dialog instanceof WebDialog) {
            ((WebDialog) this.dialog).resize();
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (this.dialog == null) {
            Dialog facebookWebFallbackDialog;
            Context activity = getActivity();
            Bundle methodArgumentsFromIntent = NativeProtocol.getMethodArgumentsFromIntent(activity.getIntent());
            String string;
            if (methodArgumentsFromIntent.getBoolean(NativeProtocol.WEB_DIALOG_IS_FALLBACK, false)) {
                string = methodArgumentsFromIntent.getString("url");
                if (Utility.isNullOrEmpty(string)) {
                    Utility.logd(TAG, "Cannot start a fallback WebDialog with an empty/missing 'url'");
                    activity.finish();
                    return;
                }
                facebookWebFallbackDialog = new FacebookWebFallbackDialog(activity, string, String.format("fb%s://bridge/", new Object[]{FacebookSdk.getApplicationId()}));
                facebookWebFallbackDialog.setOnCompleteListener(new C04032());
            } else {
                string = methodArgumentsFromIntent.getString(NativeProtocol.WEB_DIALOG_ACTION);
                methodArgumentsFromIntent = methodArgumentsFromIntent.getBundle(NativeProtocol.WEB_DIALOG_PARAMS);
                if (Utility.isNullOrEmpty(string)) {
                    Utility.logd(TAG, "Cannot start a WebDialog with an empty/missing 'actionName'");
                    activity.finish();
                    return;
                }
                facebookWebFallbackDialog = new Builder(activity, string, methodArgumentsFromIntent).setOnCompleteListener(new C04021()).build();
            }
            this.dialog = facebookWebFallbackDialog;
        }
    }

    @NonNull
    public Dialog onCreateDialog(Bundle bundle) {
        if (this.dialog == null) {
            onCompleteWebDialog(null, null);
            setShowsDialog(false);
        }
        return this.dialog;
    }

    public void onDestroyView() {
        if (getDialog() != null && getRetainInstance()) {
            getDialog().setDismissMessage(null);
        }
        super.onDestroyView();
    }

    public void setDialog(Dialog dialog) {
        this.dialog = dialog;
    }
}
