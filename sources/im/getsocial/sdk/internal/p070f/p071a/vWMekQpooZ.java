package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.vWMekQpooZ */
public enum vWMekQpooZ {
    IMAGE(0),
    VIDEO(1);
    
    public final int value;

    private vWMekQpooZ(int i) {
        this.value = i;
    }

    public static vWMekQpooZ findByValue(int i) {
        switch (i) {
            case 0:
                return IMAGE;
            case 1:
                return VIDEO;
            default:
                return null;
        }
    }
}
