package p018jp.colopl.libs;

/* renamed from: jp.colopl.libs.GuestCheckinInfo */
public class GuestCheckinInfo {
    private String mAppId;
    private String mGuestUserId;
    private String mHash;
    private String mTime;

    public GuestCheckinInfo(String str, String str2, String str3, String str4) {
        this.mAppId = str;
        this.mGuestUserId = str2;
        this.mTime = str3;
        this.mHash = str4;
    }

    public String getAppId() {
        return this.mAppId;
    }

    public String getGuestUserId() {
        return this.mGuestUserId;
    }

    public String getHash() {
        return this.mHash;
    }

    public String getTime() {
        return this.mTime;
    }
}
