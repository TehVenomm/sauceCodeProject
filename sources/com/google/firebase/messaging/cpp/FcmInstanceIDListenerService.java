package com.google.firebase.messaging.cpp;

import com.google.firebase.iid.FirebaseInstanceIdService;

public class FcmInstanceIDListenerService extends FirebaseInstanceIdService {
    /* JADX WARNING: type inference failed for: r0v0, types: [android.content.Context, com.google.firebase.messaging.cpp.FcmInstanceIDListenerService] */
    public void onTokenRefresh() {
        RegistrationIntentService.refreshToken(this);
    }
}
