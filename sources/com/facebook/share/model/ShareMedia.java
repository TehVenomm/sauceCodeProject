package com.facebook.share.model;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;
import java.util.List;

public abstract class ShareMedia implements ShareModel {
    private final Bundle params;

    public static abstract class Builder<M extends ShareMedia, B extends Builder> implements ShareModelBuilder<M, B> {
        /* access modifiers changed from: private */
        public Bundle params = new Bundle();

        static List<ShareMedia> readListFrom(Parcel parcel) {
            Parcelable[] readParcelableArray = parcel.readParcelableArray(ShareMedia.class.getClassLoader());
            ArrayList arrayList = new ArrayList(readParcelableArray.length);
            for (Parcelable parcelable : readParcelableArray) {
                arrayList.add((ShareMedia) parcelable);
            }
            return arrayList;
        }

        static void writeListTo(Parcel parcel, int i, List<ShareMedia> list) {
            parcel.writeParcelableArray((ShareMedia[]) list.toArray(), i);
        }

        public B readFrom(M m) {
            return m == null ? this : setParameters(m.getParameters());
        }

        @Deprecated
        public B setParameter(String str, String str2) {
            this.params.putString(str, str2);
            return this;
        }

        @Deprecated
        public B setParameters(Bundle bundle) {
            this.params.putAll(bundle);
            return this;
        }
    }

    public enum Type {
        PHOTO,
        VIDEO
    }

    ShareMedia(Parcel parcel) {
        this.params = parcel.readBundle();
    }

    protected ShareMedia(Builder builder) {
        this.params = new Bundle(builder.params);
    }

    public int describeContents() {
        return 0;
    }

    public abstract Type getMediaType();

    @Deprecated
    public Bundle getParameters() {
        return new Bundle(this.params);
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeBundle(this.params);
    }
}
