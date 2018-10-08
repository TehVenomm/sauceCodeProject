package im.getsocial.sdk.ui.invites;

public interface InviteUiCallback {
    void onCancel(String str);

    void onComplete(String str);

    void onError(String str, Throwable th);
}
