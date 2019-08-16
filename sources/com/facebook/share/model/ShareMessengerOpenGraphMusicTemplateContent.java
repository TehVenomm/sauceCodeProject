package com.facebook.share.model;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;

public final class ShareMessengerOpenGraphMusicTemplateContent extends ShareContent<ShareMessengerOpenGraphMusicTemplateContent, Builder> {
    public static final Creator<ShareMessengerOpenGraphMusicTemplateContent> CREATOR = new Creator<ShareMessengerOpenGraphMusicTemplateContent>() {
        public ShareMessengerOpenGraphMusicTemplateContent createFromParcel(Parcel parcel) {
            return new ShareMessengerOpenGraphMusicTemplateContent(parcel);
        }

        public ShareMessengerOpenGraphMusicTemplateContent[] newArray(int i) {
            return new ShareMessengerOpenGraphMusicTemplateContent[i];
        }
    };
    private final ShareMessengerActionButton button;
    private final Uri url;

    public static class Builder extends com.facebook.share.model.ShareContent.Builder<ShareMessengerOpenGraphMusicTemplateContent, Builder> {
        /* access modifiers changed from: private */
        public ShareMessengerActionButton button;
        /* access modifiers changed from: private */
        public Uri url;

        public ShareMessengerOpenGraphMusicTemplateContent build() {
            return new ShareMessengerOpenGraphMusicTemplateContent(this);
        }

        public Builder readFrom(ShareMessengerOpenGraphMusicTemplateContent shareMessengerOpenGraphMusicTemplateContent) {
            return shareMessengerOpenGraphMusicTemplateContent == null ? this : ((Builder) super.readFrom(shareMessengerOpenGraphMusicTemplateContent)).setUrl(shareMessengerOpenGraphMusicTemplateContent.getUrl()).setButton(shareMessengerOpenGraphMusicTemplateContent.getButton());
        }

        public Builder setButton(ShareMessengerActionButton shareMessengerActionButton) {
            this.button = shareMessengerActionButton;
            return this;
        }

        public Builder setUrl(Uri uri) {
            this.url = uri;
            return this;
        }
    }

    ShareMessengerOpenGraphMusicTemplateContent(Parcel parcel) {
        super(parcel);
        this.url = (Uri) parcel.readParcelable(Uri.class.getClassLoader());
        this.button = (ShareMessengerActionButton) parcel.readParcelable(ShareMessengerActionButton.class.getClassLoader());
    }

    private ShareMessengerOpenGraphMusicTemplateContent(Builder builder) {
        super((com.facebook.share.model.ShareContent.Builder) builder);
        this.url = builder.url;
        this.button = builder.button;
    }

    public int describeContents() {
        return 0;
    }

    public ShareMessengerActionButton getButton() {
        return this.button;
    }

    public Uri getUrl() {
        return this.url;
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeParcelable(this.url, i);
        parcel.writeParcelable(this.button, i);
    }
}
