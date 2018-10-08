package im.getsocial.sdk.usermanagement;

import im.getsocial.sdk.GetSocialException;

public interface AddAuthIdentityCallback {
    void onComplete();

    void onConflict(ConflictUser conflictUser);

    void onFailure(GetSocialException getSocialException);
}
