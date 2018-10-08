package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.HptYHntaqF */
public enum HptYHntaqF {
    Url(0),
    InstallBeginTimestamp(1),
    ReferrerClickTimestamp(2);
    
    public final int value;

    private HptYHntaqF(int i) {
        this.value = i;
    }

    public static HptYHntaqF findByValue(int i) {
        switch (i) {
            case 0:
                return Url;
            case 1:
                return InstallBeginTimestamp;
            case 2:
                return ReferrerClickTimestamp;
            default:
                return null;
        }
    }
}
