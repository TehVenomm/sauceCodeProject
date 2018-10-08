package com.google.android.gms.nearby.messages.audio;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.messages.Message;
import java.util.Arrays;

public final class AudioBytes {
    public static final int MAX_SIZE = 10;
    private final byte[] zzjex;

    public AudioBytes(byte[] bArr) {
        boolean z = true;
        zzbp.zzu(bArr);
        zzbp.zzb(bArr.length <= 10, (Object) "Given byte array longer than 10 bytes, given by AudioBytes.MAX_SIZE.");
        if (bArr.length <= 0) {
            z = false;
        }
        zzbp.zzb(z, (Object) "Given byte array is of zero length.");
        this.zzjex = bArr;
    }

    public static AudioBytes from(Message message) {
        zzbp.zzu(message);
        boolean zzkj = message.zzkj(Message.MESSAGE_TYPE_AUDIO_BYTES);
        String type = message.getType();
        zzbp.zzb(zzkj, new StringBuilder(String.valueOf(type).length() + 56).append("Message type '").append(type).append("' is not Message.MESSAGE_TYPE_AUDIO_BYTES.").toString());
        return new AudioBytes(message.getContent());
    }

    public final byte[] getBytes() {
        return this.zzjex;
    }

    public final Message toMessage() {
        return new Message(this.zzjex, Message.MESSAGE_NAMESPACE_RESERVED, Message.MESSAGE_TYPE_AUDIO_BYTES);
    }

    public final String toString() {
        String arrays = Arrays.toString(this.zzjex);
        return new StringBuilder(String.valueOf(arrays).length() + 14).append("AudioBytes [").append(arrays).append(" ]").toString();
    }
}
