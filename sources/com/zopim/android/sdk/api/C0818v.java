package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import com.zopim.android.sdk.model.ChatLog.Rating;
import java.io.File;

/* renamed from: com.zopim.android.sdk.api.v */
final class C0818v implements Chat {
    C0818v() {
    }

    public boolean emailTranscript(String str) {
        return false;
    }

    public void endChat() {
    }

    public ChatConfig getConfig() {
        return new C0819w(this);
    }

    public boolean hasEnded() {
        return true;
    }

    public void resend(String str) {
    }

    public void resetTimeout() {
    }

    public void send(File file) {
    }

    public void send(String str) {
    }

    public void sendChatComment(@NonNull String str) {
    }

    public void sendChatRating(@NonNull Rating rating) {
    }

    public boolean sendOfflineMessage(String str, String str2, String str3) {
        return false;
    }

    public void setDepartment(String str) {
    }

    public void setEmail(String str) {
    }

    public void setName(String str) {
    }

    public void setPhoneNumber(String str) {
    }
}
