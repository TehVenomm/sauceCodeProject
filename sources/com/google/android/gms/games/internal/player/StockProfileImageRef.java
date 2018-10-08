package com.google.android.gms.games.internal.player;

import android.net.Uri;
import android.os.Parcel;
import com.google.android.gms.common.data.zzc;

public class StockProfileImageRef extends zzc implements StockProfileImage {
    public int describeContents() {
        throw new NoSuchMethodError();
    }

    public /* synthetic */ Object freeze() {
        throw new NoSuchMethodError();
    }

    public String getImageUrl() {
        return getString("image_url");
    }

    public void writeToParcel(Parcel parcel, int i) {
        throw new NoSuchMethodError();
    }

    public final Uri zzaro() {
        throw new NoSuchMethodError();
    }
}
