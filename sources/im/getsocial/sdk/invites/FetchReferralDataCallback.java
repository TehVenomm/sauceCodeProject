package im.getsocial.sdk.invites;

import im.getsocial.sdk.GetSocialException;

public interface FetchReferralDataCallback {
    void onFailure(GetSocialException getSocialException);

    void onSuccess(ReferralData referralData);
}
