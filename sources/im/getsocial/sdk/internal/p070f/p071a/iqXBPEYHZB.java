package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.iqXBPEYHZB */
public enum iqXBPEYHZB {
    SPAM(0),
    INAPPROPRIATE_CONTENT(1);
    
    public final int value;

    private iqXBPEYHZB(int i) {
        this.value = i;
    }

    public static iqXBPEYHZB findByValue(int i) {
        switch (i) {
            case 0:
                return SPAM;
            case 1:
                return INAPPROPRIATE_CONTENT;
            default:
                return null;
        }
    }
}
