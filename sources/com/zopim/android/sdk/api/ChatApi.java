package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import com.zopim.android.sdk.model.ChatLog.Rating;
import java.io.File;

public interface ChatApi {
    boolean emailTranscript(String str);

    void endChat();

    void resend(String str);

    void send(@NonNull File file);

    void send(String str);

    void sendChatComment(@NonNull String str);

    void sendChatRating(@NonNull Rating rating);

    boolean sendOfflineMessage(String str, String str2, String str3);

    void setDepartment(String str);

    void setEmail(String str);

    void setName(String str);

    void setPhoneNumber(String str);
}
