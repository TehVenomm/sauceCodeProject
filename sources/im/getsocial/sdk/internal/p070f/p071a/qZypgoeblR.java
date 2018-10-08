package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.qZypgoeblR */
public enum qZypgoeblR {
    Facebook(0),
    Google(1),
    DeepLink(2),
    API(3);
    
    public final int value;

    private qZypgoeblR(int i) {
        this.value = i;
    }

    public static qZypgoeblR findByValue(int i) {
        switch (i) {
            case 0:
                return Facebook;
            case 1:
                return Google;
            case 2:
                return DeepLink;
            case 3:
                return API;
            default:
                return null;
        }
    }
}
