package com.facebook.share.internal;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import com.facebook.FacebookCallback;
import com.facebook.internal.AppCall;
import com.facebook.internal.CallbackManagerImpl;
import com.facebook.internal.CallbackManagerImpl.Callback;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.internal.DialogFeature;
import com.facebook.internal.DialogPresenter;
import com.facebook.internal.DialogPresenter.ParameterProvider;
import com.facebook.internal.FacebookDialogBase;
import com.facebook.internal.FragmentWrapper;
import java.util.ArrayList;
import java.util.List;

@Deprecated
public class LikeDialog extends FacebookDialogBase<LikeContent, Result> {
    private static final int DEFAULT_REQUEST_CODE = RequestCodeOffset.Like.toRequestCode();
    private static final String TAG = "LikeDialog";

    private class NativeHandler extends ModeHandler {
        private NativeHandler() {
            super();
        }

        public boolean canShow(LikeContent likeContent, boolean z) {
            return false;
        }

        public AppCall createAppCall(final LikeContent likeContent) {
            AppCall createBaseAppCall = LikeDialog.this.createBaseAppCall();
            DialogPresenter.setupAppCallForNativeDialog(createBaseAppCall, new ParameterProvider() {
                public Bundle getLegacyParameters() {
                    Log.e(LikeDialog.TAG, "Attempting to present the Like Dialog with an outdated Facebook app on the device");
                    return new Bundle();
                }

                public Bundle getParameters() {
                    return LikeDialog.createParameters(likeContent);
                }
            }, LikeDialog.getFeature());
            return createBaseAppCall;
        }
    }

    @Deprecated
    public static final class Result {
        private final Bundle bundle;

        public Result(Bundle bundle2) {
            this.bundle = bundle2;
        }

        public Bundle getData() {
            return this.bundle;
        }
    }

    private class WebFallbackHandler extends ModeHandler {
        private WebFallbackHandler() {
            super();
        }

        public boolean canShow(LikeContent likeContent, boolean z) {
            return false;
        }

        public AppCall createAppCall(LikeContent likeContent) {
            AppCall createBaseAppCall = LikeDialog.this.createBaseAppCall();
            DialogPresenter.setupAppCallForWebFallbackDialog(createBaseAppCall, LikeDialog.createParameters(likeContent), LikeDialog.getFeature());
            return createBaseAppCall;
        }
    }

    @Deprecated
    public LikeDialog(Activity activity) {
        super(activity, DEFAULT_REQUEST_CODE);
    }

    @Deprecated
    public LikeDialog(Fragment fragment) {
        this(new FragmentWrapper(fragment));
    }

    @Deprecated
    public LikeDialog(android.support.p000v4.app.Fragment fragment) {
        this(new FragmentWrapper(fragment));
    }

    @Deprecated
    public LikeDialog(FragmentWrapper fragmentWrapper) {
        super(fragmentWrapper, DEFAULT_REQUEST_CODE);
    }

    @Deprecated
    public static boolean canShowNativeDialog() {
        return false;
    }

    @Deprecated
    public static boolean canShowWebFallback() {
        return false;
    }

    /* access modifiers changed from: private */
    public static Bundle createParameters(LikeContent likeContent) {
        Bundle bundle = new Bundle();
        bundle.putString("object_id", likeContent.getObjectId());
        bundle.putString("object_type", likeContent.getObjectType());
        return bundle;
    }

    /* access modifiers changed from: private */
    public static DialogFeature getFeature() {
        return LikeDialogFeature.LIKE_DIALOG;
    }

    /* access modifiers changed from: protected */
    public AppCall createBaseAppCall() {
        return new AppCall(getRequestCode());
    }

    /* access modifiers changed from: protected */
    public List<ModeHandler> getOrderedModeHandlers() {
        ArrayList arrayList = new ArrayList();
        arrayList.add(new NativeHandler());
        arrayList.add(new WebFallbackHandler());
        return arrayList;
    }

    /* access modifiers changed from: protected */
    public void registerCallbackImpl(CallbackManagerImpl callbackManagerImpl, final FacebookCallback<Result> facebookCallback) {
        final C07831 r0 = facebookCallback == null ? null : new ResultProcessor(facebookCallback) {
            public void onSuccess(AppCall appCall, Bundle bundle) {
                facebookCallback.onSuccess(new Result(bundle));
            }
        };
        callbackManagerImpl.registerCallback(getRequestCode(), new Callback() {
            public boolean onActivityResult(int i, Intent intent) {
                return ShareInternalUtility.handleActivityResult(LikeDialog.this.getRequestCode(), i, intent, r0);
            }
        });
    }

    @Deprecated
    public void show(LikeContent likeContent) {
    }
}
