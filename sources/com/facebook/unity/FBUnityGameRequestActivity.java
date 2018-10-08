package com.facebook.unity;

import android.app.Activity;
import android.os.Bundle;
import android.text.TextUtils;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.share.internal.ShareConstants;
import com.facebook.share.model.GameRequestContent;
import com.facebook.share.model.GameRequestContent.ActionType;
import com.facebook.share.model.GameRequestContent.Builder;
import com.facebook.share.model.GameRequestContent.Filters;
import com.facebook.share.widget.GameRequestDialog;
import com.facebook.share.widget.GameRequestDialog.Result;
import java.util.Arrays;
import java.util.Locale;

public class FBUnityGameRequestActivity extends BaseActivity {
    public static final String GAME_REQUEST_PARAMS = "game_request_params";

    protected void onCreate(Bundle bundle) {
        String string;
        super.onCreate(bundle);
        Bundle bundleExtra = getIntent().getBundleExtra(GAME_REQUEST_PARAMS);
        final UnityMessage unityMessage = new UnityMessage("OnAppRequestsComplete");
        if (bundleExtra.containsKey(Constants.CALLBACK_ID_KEY)) {
            unityMessage.put(Constants.CALLBACK_ID_KEY, bundleExtra.getString(Constants.CALLBACK_ID_KEY));
        }
        Builder builder = new Builder();
        if (bundleExtra.containsKey("message")) {
            builder.setMessage(bundleExtra.getString("message"));
        }
        if (bundleExtra.containsKey(ShareConstants.WEB_DIALOG_PARAM_ACTION_TYPE)) {
            string = bundleExtra.getString(ShareConstants.WEB_DIALOG_PARAM_ACTION_TYPE);
            try {
                builder.setActionType(ActionType.valueOf(string));
            } catch (IllegalArgumentException e) {
                unityMessage.sendError("Unknown action type: " + string);
                finish();
                return;
            }
        }
        if (bundleExtra.containsKey("object_id")) {
            builder.setObjectId(bundleExtra.getString("object_id"));
        }
        if (bundleExtra.containsKey("to")) {
            builder.setRecipients(Arrays.asList(bundleExtra.getString("to").split(",")));
        }
        if (bundleExtra.containsKey(ShareConstants.WEB_DIALOG_PARAM_FILTERS)) {
            string = bundleExtra.getString(ShareConstants.WEB_DIALOG_PARAM_FILTERS).toUpperCase(Locale.ROOT);
            try {
                builder.setFilters(Filters.valueOf(string));
            } catch (IllegalArgumentException e2) {
                unityMessage.sendError("Unsupported filter type: " + string);
                finish();
                return;
            }
        }
        if (bundleExtra.containsKey(ShareConstants.WEB_DIALOG_PARAM_DATA)) {
            builder.setData(bundleExtra.getString(ShareConstants.WEB_DIALOG_PARAM_DATA));
        }
        if (bundleExtra.containsKey("title")) {
            builder.setTitle(bundleExtra.getString("title"));
        }
        GameRequestContent build = builder.build();
        GameRequestDialog gameRequestDialog = new GameRequestDialog((Activity) this);
        gameRequestDialog.registerCallback(this.mCallbackManager, new FacebookCallback<Result>() {
            public void onCancel() {
                unityMessage.putCancelled();
                unityMessage.send();
            }

            public void onError(FacebookException facebookException) {
                unityMessage.sendError(facebookException.getMessage());
            }

            public void onSuccess(Result result) {
                unityMessage.put(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID, result.getRequestId());
                unityMessage.put("to", TextUtils.join(",", result.getRequestRecipients()));
                unityMessage.send();
            }
        });
        try {
            gameRequestDialog.show(build);
        } catch (IllegalArgumentException e3) {
            unityMessage.sendError("Unexpected exception encountered: " + e3.toString());
            finish();
        }
    }
}
