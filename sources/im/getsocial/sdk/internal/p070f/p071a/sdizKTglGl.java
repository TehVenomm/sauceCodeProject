package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.sdizKTglGl */
public enum sdizKTglGl {
    OPEN(0),
    CLOSED(1);
    
    public final int value;

    private sdizKTglGl(int i) {
        this.value = i;
    }

    public static sdizKTglGl findByValue(int i) {
        switch (i) {
            case 0:
                return OPEN;
            case 1:
                return CLOSED;
            default:
                return null;
        }
    }
}
