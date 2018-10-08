package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.QhisXzMgay */
public enum QhisXzMgay {
    SMART_INVITE(1),
    MARKETING_CAMPAIGN(2);
    
    public final int value;

    private QhisXzMgay(int i) {
        this.value = i;
    }

    public static QhisXzMgay findByValue(int i) {
        switch (i) {
            case 1:
                return SMART_INVITE;
            case 2:
                return MARKETING_CAMPAIGN;
            default:
                return null;
        }
    }
}
