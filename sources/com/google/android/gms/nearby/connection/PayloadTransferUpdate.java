package com.google.android.gms.nearby.connection;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

@Class(creator = "PayloadTransferUpdateCreator")
@Reserved({1000})
public final class PayloadTransferUpdate extends AbstractSafeParcelable {
    public static final Creator<PayloadTransferUpdate> CREATOR = new zzi();
    /* access modifiers changed from: private */
    @Field(getter = "getStatus", mo13990id = 2)
    public int status;
    /* access modifiers changed from: private */
    @Field(getter = "getPayloadId", mo13990id = 1)
    public long zzaf;
    /* access modifiers changed from: private */
    @Field(getter = "getTotalBytes", mo13990id = 3)
    public long zzag;
    /* access modifiers changed from: private */
    @Field(getter = "getBytesTransferred", mo13990id = 4)
    public long zzah;

    @Deprecated
    public static final class Builder {
        private final PayloadTransferUpdate zzai = new PayloadTransferUpdate();

        public Builder() {
        }

        public Builder(PayloadTransferUpdate payloadTransferUpdate) {
            this.zzai.zzaf = payloadTransferUpdate.zzaf;
            this.zzai.status = payloadTransferUpdate.status;
            this.zzai.zzag = payloadTransferUpdate.zzag;
            this.zzai.zzah = payloadTransferUpdate.zzah;
        }

        public final PayloadTransferUpdate build() {
            return this.zzai;
        }

        public final Builder setBytesTransferred(long j) {
            this.zzai.zzah = j;
            return this;
        }

        public final Builder setPayloadId(long j) {
            this.zzai.zzaf = j;
            return this;
        }

        public final Builder setStatus(int i) {
            this.zzai.status = i;
            return this;
        }

        public final Builder setTotalBytes(long j) {
            this.zzai.zzag = j;
            return this;
        }
    }

    @Retention(RetentionPolicy.SOURCE)
    public @interface Status {
        public static final int CANCELED = 4;
        public static final int FAILURE = 2;
        public static final int IN_PROGRESS = 3;
        public static final int SUCCESS = 1;
    }

    private PayloadTransferUpdate() {
    }

    @Constructor
    PayloadTransferUpdate(@Param(mo13993id = 1) long j, @Param(mo13993id = 2) int i, @Param(mo13993id = 3) long j2, @Param(mo13993id = 4) long j3) {
        this.zzaf = j;
        this.status = i;
        this.zzag = j2;
        this.zzah = j3;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof PayloadTransferUpdate)) {
            return false;
        }
        PayloadTransferUpdate payloadTransferUpdate = (PayloadTransferUpdate) obj;
        return Objects.equal(Long.valueOf(this.zzaf), Long.valueOf(payloadTransferUpdate.zzaf)) && Objects.equal(Integer.valueOf(this.status), Integer.valueOf(payloadTransferUpdate.status)) && Objects.equal(Long.valueOf(this.zzag), Long.valueOf(payloadTransferUpdate.zzag)) && Objects.equal(Long.valueOf(this.zzah), Long.valueOf(payloadTransferUpdate.zzah));
    }

    public final long getBytesTransferred() {
        return this.zzah;
    }

    public final long getPayloadId() {
        return this.zzaf;
    }

    public final int getStatus() {
        return this.status;
    }

    public final long getTotalBytes() {
        return this.zzag;
    }

    public final int hashCode() {
        return Objects.hashCode(Long.valueOf(this.zzaf), Integer.valueOf(this.status), Long.valueOf(this.zzag), Long.valueOf(this.zzah));
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeLong(parcel, 1, getPayloadId());
        SafeParcelWriter.writeInt(parcel, 2, getStatus());
        SafeParcelWriter.writeLong(parcel, 3, getTotalBytes());
        SafeParcelWriter.writeLong(parcel, 4, getBytesTransferred());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
