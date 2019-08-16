package com.facebook.share.model;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;

public final class ShareOpenGraphAction extends ShareOpenGraphValueContainer<ShareOpenGraphAction, Builder> {
    public static final Creator<ShareOpenGraphAction> CREATOR = new Creator<ShareOpenGraphAction>() {
        public ShareOpenGraphAction createFromParcel(Parcel parcel) {
            return new ShareOpenGraphAction(parcel);
        }

        public ShareOpenGraphAction[] newArray(int i) {
            return new ShareOpenGraphAction[i];
        }
    };

    public static final class Builder extends com.facebook.share.model.ShareOpenGraphValueContainer.Builder<ShareOpenGraphAction, Builder> {
        private static final String ACTION_TYPE_KEY = "og:type";

        public ShareOpenGraphAction build() {
            return new ShareOpenGraphAction(this);
        }

        /* access modifiers changed from: 0000 */
        public Builder readFrom(Parcel parcel) {
            return readFrom((ShareOpenGraphAction) parcel.readParcelable(ShareOpenGraphAction.class.getClassLoader()));
        }

        public Builder readFrom(ShareOpenGraphAction shareOpenGraphAction) {
            return shareOpenGraphAction == null ? this : ((Builder) super.readFrom(shareOpenGraphAction)).setActionType(shareOpenGraphAction.getActionType());
        }

        public Builder setActionType(String str) {
            putString(ACTION_TYPE_KEY, str);
            return this;
        }
    }

    ShareOpenGraphAction(Parcel parcel) {
        super(parcel);
    }

    private ShareOpenGraphAction(Builder builder) {
        super((com.facebook.share.model.ShareOpenGraphValueContainer.Builder<P, E>) builder);
    }

    @Nullable
    public String getActionType() {
        return getString("og:type");
    }
}
