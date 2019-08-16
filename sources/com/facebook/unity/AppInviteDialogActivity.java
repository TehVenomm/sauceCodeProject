package com.facebook.unity;

import android.app.Activity;
import android.os.Bundle;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.internal.NativeProtocol;
import com.facebook.share.model.AppInviteContent.Builder;
import com.facebook.share.widget.AppInviteDialog;
import com.facebook.share.widget.AppInviteDialog.Result;

public class AppInviteDialogActivity extends BaseActivity {
    public static final String DIALOG_PARAMS = "dialog_params";

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        final UnityMessage unityMessage = new UnityMessage("OnAppInviteComplete");
        Bundle bundleExtra = getIntent().getBundleExtra(DIALOG_PARAMS);
        Builder builder = new Builder();
        if (bundleExtra.containsKey(Constants.CALLBACK_ID_KEY)) {
            unityMessage.put(Constants.CALLBACK_ID_KEY, bundleExtra.getString(Constants.CALLBACK_ID_KEY));
        }
        if (bundleExtra.containsKey("appLinkUrl")) {
            builder.setApplinkUrl(bundleExtra.getString("appLinkUrl"));
        }
        if (bundleExtra.containsKey("previewImageUrl")) {
            builder.setPreviewImageUrl(bundleExtra.getString("previewImageUrl"));
        }
        AppInviteDialog appInviteDialog = new AppInviteDialog((Activity) this);
        appInviteDialog.registerCallback(this.mCallbackManager, new FacebookCallback<Result>() {
            public void onCancel() {
                unityMessage.putCancelled();
                unityMessage.send();
            }

            public void onError(FacebookException facebookException) {
                unityMessage.sendError(facebookException.getMessage());
            }

            public void onSuccess(Result result) {
                unityMessage.put(NativeProtocol.RESULT_ARGS_DIALOG_COMPLETE_KEY, Boolean.valueOf(true));
                unityMessage.send();
            }
        });
        appInviteDialog.show(builder.build());
    }
}
