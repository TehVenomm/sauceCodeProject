package com.facebook.share.model;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;

public final class ShareOpenGraphContent extends ShareContent<ShareOpenGraphContent, Builder> {
    public static final Creator<ShareOpenGraphContent> CREATOR = new Creator<ShareOpenGraphContent>() {
        public ShareOpenGraphContent createFromParcel(Parcel parcel) {
            return new ShareOpenGraphContent(parcel);
        }

        public ShareOpenGraphContent[] newArray(int i) {
            return new ShareOpenGraphContent[i];
        }
    };
    private final ShareOpenGraphAction action;
    private final String previewPropertyName;

    public static final class Builder extends com.facebook.share.model.ShareContent.Builder<ShareOpenGraphContent, Builder> {
        /* access modifiers changed from: private */
        public ShareOpenGraphAction action;
        /* access modifiers changed from: private */
        public String previewPropertyName;

        public ShareOpenGraphContent build() {
            return new ShareOpenGraphContent(this);
        }

        public Builder readFrom(ShareOpenGraphContent shareOpenGraphContent) {
            return shareOpenGraphContent == null ? this : ((Builder) super.readFrom(shareOpenGraphContent)).setAction(shareOpenGraphContent.getAction()).setPreviewPropertyName(shareOpenGraphContent.getPreviewPropertyName());
        }

        public Builder setAction(@Nullable ShareOpenGraphAction shareOpenGraphAction) {
            this.action = shareOpenGraphAction == null ? null : new com.facebook.share.model.ShareOpenGraphAction.Builder().readFrom(shareOpenGraphAction).build();
            return this;
        }

        public Builder setPreviewPropertyName(@Nullable String str) {
            this.previewPropertyName = str;
            return this;
        }
    }

    ShareOpenGraphContent(Parcel parcel) {
        super(parcel);
        this.action = new com.facebook.share.model.ShareOpenGraphAction.Builder().readFrom(parcel).build();
        this.previewPropertyName = parcel.readString();
    }

    private ShareOpenGraphContent(Builder builder) {
        super((com.facebook.share.model.ShareContent.Builder) builder);
        this.action = builder.action;
        this.previewPropertyName = builder.previewPropertyName;
    }

    public int describeContents() {
        return 0;
    }

    @Nullable
    public ShareOpenGraphAction getAction() {
        return this.action;
    }

    @Nullable
    public String getPreviewPropertyName() {
        return this.previewPropertyName;
    }

    public void writeToParcel(Parcel parcel, int i) {
        super.writeToParcel(parcel, i);
        parcel.writeParcelable(this.action, 0);
        parcel.writeString(this.previewPropertyName);
    }
}
