package im.getsocial.sdk.internal.p070f.p071a;

/* renamed from: im.getsocial.sdk.internal.f.a.ruWsnwUPKh */
public enum ruWsnwUPKh {
    ExtraSubject(0),
    ExtraText(1),
    ExtraStream(2),
    ExtraGif(3),
    ExtraVideo(4);
    
    public final int value;

    private ruWsnwUPKh(int i) {
        this.value = i;
    }

    public static ruWsnwUPKh findByValue(int i) {
        switch (i) {
            case 0:
                return ExtraSubject;
            case 1:
                return ExtraText;
            case 2:
                return ExtraStream;
            case 3:
                return ExtraGif;
            case 4:
                return ExtraVideo;
            default:
                return null;
        }
    }
}
