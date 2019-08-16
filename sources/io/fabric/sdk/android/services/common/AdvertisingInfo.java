package p017io.fabric.sdk.android.services.common;

/* renamed from: io.fabric.sdk.android.services.common.AdvertisingInfo */
class AdvertisingInfo {
    public final String advertisingId;
    public final boolean limitAdTrackingEnabled;

    AdvertisingInfo(String str, boolean z) {
        this.advertisingId = str;
        this.limitAdTrackingEnabled = z;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            AdvertisingInfo advertisingInfo = (AdvertisingInfo) obj;
            if (this.limitAdTrackingEnabled != advertisingInfo.limitAdTrackingEnabled) {
                return false;
            }
            if (this.advertisingId != null) {
                if (!this.advertisingId.equals(advertisingInfo.advertisingId)) {
                    return false;
                }
            } else if (advertisingInfo.advertisingId != null) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        int i = 0;
        int i2 = this.advertisingId != null ? this.advertisingId.hashCode() : 0;
        if (this.limitAdTrackingEnabled) {
            i = 1;
        }
        return (i2 * 31) + i;
    }
}
