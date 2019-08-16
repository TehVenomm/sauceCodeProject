package com.facebook.share.model;

import android.os.Parcel;
import android.os.Parcelable.Creator;

public class ShareHashtag implements ShareModel {
    public static final Creator<ShareHashtag> CREATOR = new Creator<ShareHashtag>() {
        public ShareHashtag createFromParcel(Parcel parcel) {
            return new ShareHashtag(parcel);
        }

        public ShareHashtag[] newArray(int i) {
            return new ShareHashtag[i];
        }
    };
    private final String hashtag;

    public static class Builder implements ShareModelBuilder<ShareHashtag, Builder> {
        /* access modifiers changed from: private */
        public String hashtag;

        public ShareHashtag build() {
            return new ShareHashtag(this);
        }

        public String getHashtag() {
            return this.hashtag;
        }

        /* access modifiers changed from: 0000 */
        public Builder readFrom(Parcel parcel) {
            return readFrom((ShareHashtag) parcel.readParcelable(ShareHashtag.class.getClassLoader()));
        }

        public Builder readFrom(ShareHashtag shareHashtag) {
            return shareHashtag == null ? this : setHashtag(shareHashtag.getHashtag());
        }

        public Builder setHashtag(String str) {
            this.hashtag = str;
            return this;
        }
    }

    ShareHashtag(Parcel parcel) {
        this.hashtag = parcel.readString();
    }

    private ShareHashtag(Builder builder) {
        this.hashtag = builder.hashtag;
    }

    public int describeContents() {
        return 0;
    }

    public String getHashtag() {
        return this.hashtag;
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.hashtag);
    }
}
