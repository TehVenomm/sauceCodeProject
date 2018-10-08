package im.getsocial.sdk.invites;

public interface InviteCallback {
    void onCancel();

    void onComplete();

    void onError(Throwable th);
}
