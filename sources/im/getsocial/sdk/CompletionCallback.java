package im.getsocial.sdk;

public interface CompletionCallback {
    void onFailure(GetSocialException getSocialException);

    void onSuccess();
}
