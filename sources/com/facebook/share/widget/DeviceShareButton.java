package com.facebook.share.widget;

import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.OnClickListener;
import com.facebook.CallbackManager;
import com.facebook.FacebookButtonBase;
import com.facebook.FacebookCallback;
import com.facebook.FacebookSdk;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.share.C0741R;
import com.facebook.share.DeviceShareDialog;
import com.facebook.share.DeviceShareDialog.Result;
import com.facebook.share.model.ShareContent;

public final class DeviceShareButton extends FacebookButtonBase {
    private DeviceShareDialog dialog;
    private boolean enabledExplicitlySet;
    private int requestCode;
    private ShareContent shareContent;

    public DeviceShareButton(Context context) {
        this(context, null, 0);
    }

    public DeviceShareButton(Context context, AttributeSet attributeSet) {
        this(context, attributeSet, 0);
    }

    private DeviceShareButton(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i, 0, AnalyticsEvents.EVENT_DEVICE_SHARE_BUTTON_CREATE, AnalyticsEvents.EVENT_DEVICE_SHARE_BUTTON_DID_TAP);
        this.requestCode = 0;
        this.enabledExplicitlySet = false;
        this.dialog = null;
        this.requestCode = isInEditMode() ? 0 : getDefaultRequestCode();
        internalSetEnabled(false);
    }

    private boolean canShare() {
        return new DeviceShareDialog(getActivity()).canShow(getShareContent());
    }

    /* access modifiers changed from: private */
    public DeviceShareDialog getDialog() {
        if (this.dialog != null) {
            return this.dialog;
        }
        if (getFragment() != null) {
            this.dialog = new DeviceShareDialog(getFragment());
        } else if (getNativeFragment() != null) {
            this.dialog = new DeviceShareDialog(getNativeFragment());
        } else {
            this.dialog = new DeviceShareDialog(getActivity());
        }
        return this.dialog;
    }

    private void internalSetEnabled(boolean z) {
        setEnabled(z);
        this.enabledExplicitlySet = false;
    }

    private void setRequestCode(int i) {
        if (FacebookSdk.isFacebookRequestCode(i)) {
            throw new IllegalArgumentException("Request code " + i + " cannot be within the range reserved by the Facebook SDK.");
        }
        this.requestCode = i;
    }

    /* access modifiers changed from: protected */
    public void configureButton(Context context, AttributeSet attributeSet, int i, int i2) {
        super.configureButton(context, attributeSet, i, i2);
        setInternalOnClickListener(getShareOnClickListener());
    }

    /* access modifiers changed from: protected */
    public int getDefaultRequestCode() {
        return RequestCodeOffset.Share.toRequestCode();
    }

    /* access modifiers changed from: protected */
    public int getDefaultStyleResource() {
        return C0741R.style.com_facebook_button_share;
    }

    public int getRequestCode() {
        return this.requestCode;
    }

    public ShareContent getShareContent() {
        return this.shareContent;
    }

    /* access modifiers changed from: protected */
    public OnClickListener getShareOnClickListener() {
        return new OnClickListener() {
            public void onClick(View view) {
                DeviceShareButton.this.callExternalOnClickListener(view);
                DeviceShareButton.this.getDialog().show(DeviceShareButton.this.getShareContent());
            }
        };
    }

    public void registerCallback(CallbackManager callbackManager, FacebookCallback<Result> facebookCallback) {
        getDialog().registerCallback(callbackManager, facebookCallback);
    }

    public void registerCallback(CallbackManager callbackManager, FacebookCallback<Result> facebookCallback, int i) {
        setRequestCode(i);
        getDialog().registerCallback(callbackManager, facebookCallback, i);
    }

    public void setEnabled(boolean z) {
        super.setEnabled(z);
        this.enabledExplicitlySet = true;
    }

    public void setShareContent(ShareContent shareContent2) {
        this.shareContent = shareContent2;
        if (!this.enabledExplicitlySet) {
            internalSetEnabled(canShare());
        }
    }
}
