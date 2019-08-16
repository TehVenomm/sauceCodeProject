package com.facebook.share.internal;

import android.content.Context;
import android.util.AttributeSet;
import com.facebook.FacebookButtonBase;
import com.facebook.common.C0618R;
import com.facebook.internal.AnalyticsEvents;

@Deprecated
public class LikeButton extends FacebookButtonBase {
    @Deprecated
    public LikeButton(Context context, boolean z) {
        super(context, null, 0, 0, AnalyticsEvents.EVENT_LIKE_BUTTON_CREATE, AnalyticsEvents.EVENT_LIKE_BUTTON_DID_TAP);
        setSelected(z);
    }

    private void updateForLikeStatus() {
        if (isSelected()) {
            setCompoundDrawablesWithIntrinsicBounds(C0618R.C0620drawable.com_facebook_button_like_icon_selected, 0, 0, 0);
            setText(getResources().getString(C0618R.string.com_facebook_like_button_liked));
            return;
        }
        setCompoundDrawablesWithIntrinsicBounds(C0618R.C0620drawable.com_facebook_button_icon, 0, 0, 0);
        setText(getResources().getString(C0618R.string.com_facebook_like_button_not_liked));
    }

    /* access modifiers changed from: protected */
    public void configureButton(Context context, AttributeSet attributeSet, int i, int i2) {
        super.configureButton(context, attributeSet, i, i2);
        updateForLikeStatus();
    }

    /* access modifiers changed from: protected */
    public int getDefaultRequestCode() {
        return 0;
    }

    /* access modifiers changed from: protected */
    public int getDefaultStyleResource() {
        return C0618R.style.com_facebook_button_like;
    }

    @Deprecated
    public void setSelected(boolean z) {
        super.setSelected(z);
        updateForLikeStatus();
    }
}
