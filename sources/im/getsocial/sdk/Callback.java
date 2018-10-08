package im.getsocial.sdk;

public interface Callback<T> {
    void onFailure(GetSocialException getSocialException);

    void onSuccess(T t);
}
