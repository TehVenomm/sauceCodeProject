package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.RbduBRVrSj */
public enum RbduBRVrSj {
    ACTIVITY_FEED(0),
    CHAT(1),
    LANDING_PAGE(2),
    THUMB_LANDING_PAGE(3),
    INVITE_IMAGE(4),
    APP_ICON(5),
    CUSTOM_INVITE_IMAGE(6),
    STICKY_ACTIVITY(7),
    USER_AVATAR(8),
    CUSTOM_LANDING_PAGE_IMAGE(9);
    
    public final int value;

    private RbduBRVrSj(int i) {
        this.value = i;
    }

    public static RbduBRVrSj findByValue(int i) {
        switch (i) {
            case 0:
                return ACTIVITY_FEED;
            case 1:
                return CHAT;
            case 2:
                return LANDING_PAGE;
            case 3:
                return THUMB_LANDING_PAGE;
            case 4:
                return INVITE_IMAGE;
            case 5:
                return APP_ICON;
            case 6:
                return CUSTOM_INVITE_IMAGE;
            case 7:
                return STICKY_ACTIVITY;
            case 8:
                return USER_AVATAR;
            case 9:
                return CUSTOM_LANDING_PAGE_IMAGE;
            default:
                return null;
        }
    }
}
