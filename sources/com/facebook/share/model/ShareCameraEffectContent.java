package com.facebook.share.model;

import android.os.Parcel;
import android.os.Parcelable.Creator;

public class ShareCameraEffectContent extends ShareContent<ShareCameraEffectContent, Builder> {
    public static final Creator<ShareCameraEffectContent> CREATOR = new Creator<ShareCameraEffectContent>() {
        public ShareCameraEffectContent createFromParcel(Parcel parcel) {
            return new ShareCameraEffectContent(parcel);
        }

        public ShareCameraEffectContent[] newArray(int i) {
            return new ShareCameraEffectContent[i];
        }
    };
    private CameraEffectArguments arguments;
    private String effectId;
    private CameraEffectTextures textures;

    public static final class Builder extends com.facebook.share.model.ShareContent.Builder<ShareCameraEffectContent, Builder> {
        /* access modifiers changed from: private */
        public CameraEffectArguments arguments;
        /* access modifiers changed from: private */
        public String effectId;
        /* access modifiers changed from: private */
        public CameraEffectTextures textures;

        public ShareCameraEffectContent build() {
            return new ShareCameraEffectContent(this);
        }

        public Builder readFrom(ShareCameraEffectContent shareCameraEffectContent) {
            return shareCameraEffectContent == null ? this : ((Builder) super.readFrom(shareCameraEffectContent)).setEffectId(this.effectId).setArguments(this.arguments);
        }

        public Builder setArguments(CameraEffectArguments cameraEffectArguments) {
            this.arguments = cameraEffectArguments;
            return this;
        }

        public Builder setEffectId(String str) {
            this.effectId = str;
            return this;
        }

        public Builder setTextures(CameraEffectTextures cameraEffectTextures) {
            this.textures = cameraEffectTextures;
            return this;
        }
    }

    ShareCameraEffectContent(Parcel parcel) {
        super(parcel);
        this.effectId = parcel.readString();
        this.arguments = new com.facebook.share.model.CameraEffectArguments.Builder().readFrom(parcel).build();
        this.textures = new com.facebook.share.model.CameraEffectTextures.Builder().readFrom(parcel).build();
    }

    private ShareCameraEffectContent(Builder builder) {
        super((com.facebook.share.model.ShareContent.Builder) builder);
        this.effectId = builder.effectId;
        this.arguments = builder.arguments;
        this.textures = builder.textures;
    }

    public CameraEffectArguments getArguments() {
        return this.arguments;
    }

    public String getEffectId() {
        return this.effectId;
    }

    public CameraEffectTextures getTextures() {
        return this.textures;
    }

    public void writeToParcel(Parcel parcel, int i) {
        super.writeToParcel(parcel, i);
        parcel.writeString(this.effectId);
        parcel.writeParcelable(this.arguments, 0);
        parcel.writeParcelable(this.textures, 0);
    }
}
