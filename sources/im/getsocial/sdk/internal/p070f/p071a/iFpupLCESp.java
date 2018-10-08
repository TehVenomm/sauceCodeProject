package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.iFpupLCESp */
public enum iFpupLCESp {
    ANDROID(0),
    IOS(1),
    API(2),
    WEB_ANDROID(3),
    WEB_IOS(4),
    WEB_DESKTOP(5),
    OTHER(6);
    
    public final int value;

    private iFpupLCESp(int i) {
        this.value = i;
    }

    public static iFpupLCESp findByValue(int i) {
        switch (i) {
            case 0:
                return ANDROID;
            case 1:
                return IOS;
            case 2:
                return API;
            case 3:
                return WEB_ANDROID;
            case 4:
                return WEB_IOS;
            case 5:
                return WEB_DESKTOP;
            case 6:
                return OTHER;
            default:
                return null;
        }
    }
}
