package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.FvojpKUsoc */
public enum FvojpKUsoc {
    NATIVE(0),
    UNITY(1),
    MARMALADE(2),
    CORDOVA(3);
    
    public final int value;

    private FvojpKUsoc(int i) {
        this.value = i;
    }

    public static FvojpKUsoc findByValue(int i) {
        switch (i) {
            case 0:
                return NATIVE;
            case 1:
                return UNITY;
            case 2:
                return MARMALADE;
            case 3:
                return CORDOVA;
            default:
                return null;
        }
    }
}
