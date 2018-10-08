package com.google.firebase.messaging.cpp;

import android.content.Intent;
import com.google.firebase.iid.FirebaseInstanceIdService;

public class FcmInstanceIDListenerService extends FirebaseInstanceIdService {
    public void onTokenRefresh() {
        startService(new Intent(this, RegistrationIntentService.class));
    }
}
