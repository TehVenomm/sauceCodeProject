package com.google.firebase.messaging.cpp;

import android.app.IntentService;
import android.content.Intent;
import com.google.firebase.iid.FirebaseInstanceId;
import com.google.flatbuffers.FlatBufferBuilder;
import java.io.FileOutputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.channels.FileLock;

public class RegistrationIntentService extends IntentService {
    private static final String TAG = "FirebaseRegService";

    public RegistrationIntentService() {
        super(TAG);
    }

    private byte[] generateTokenByteBuffer(String str) {
        CharSequence charSequence;
        FlatBufferBuilder flatBufferBuilder = new FlatBufferBuilder(0);
        if (str == null) {
            charSequence = "";
        }
        int createString = flatBufferBuilder.createString(charSequence);
        SerializedTokenReceived.startSerializedTokenReceived(flatBufferBuilder);
        SerializedTokenReceived.addToken(flatBufferBuilder, createString);
        createString = SerializedTokenReceived.endSerializedTokenReceived(flatBufferBuilder);
        SerializedEvent.startSerializedEvent(flatBufferBuilder);
        SerializedEvent.addEventType(flatBufferBuilder, (byte) 2);
        SerializedEvent.addEvent(flatBufferBuilder, createString);
        flatBufferBuilder.finish(SerializedEvent.endSerializedEvent(flatBufferBuilder));
        return flatBufferBuilder.sizedByteArray();
    }

    private void writeTokenToInternalStorage(String str) {
        FileLock fileLock = null;
        byte[] generateTokenByteBuffer = generateTokenByteBuffer(str);
        ByteBuffer allocate = ByteBuffer.allocate(4);
        allocate.order(ByteOrder.LITTLE_ENDIAN);
        allocate.putInt(generateTokenByteBuffer.length);
        try {
            fileLock = openFileOutput("FIREBASE_CLOUD_MESSAGING_LOCKFILE", 0).getChannel().lock();
            FileOutputStream openFileOutput = openFileOutput("FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE", 32768);
            openFileOutput.write(allocate.array());
            openFileOutput.write(generateTokenByteBuffer);
            openFileOutput.close();
            if (fileLock != null) {
                try {
                    fileLock.release();
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        } catch (Exception e2) {
            e2.printStackTrace();
            if (fileLock != null) {
                try {
                    fileLock.release();
                } catch (Exception e22) {
                    e22.printStackTrace();
                }
            }
        } catch (Throwable th) {
            if (fileLock != null) {
                try {
                    fileLock.release();
                } catch (Exception e3) {
                    e3.printStackTrace();
                }
            }
        }
    }

    protected void onHandleIntent(Intent intent) {
        DebugLogging.log(TAG, String.format("onHandleIntent token=%s", new Object[]{FirebaseInstanceId.getInstance().getToken()}));
        String token = FirebaseInstanceId.getInstance().getToken();
        if (token != null) {
            writeTokenToInternalStorage(token);
        }
    }
}
