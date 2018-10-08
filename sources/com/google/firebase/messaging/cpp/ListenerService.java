package com.google.firebase.messaging.cpp;

import android.content.Context;
import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import com.google.firebase.messaging.RemoteMessage.Notification;
import com.google.flatbuffers.FlatBufferBuilder;
import java.io.FileOutputStream;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.channels.FileLock;
import java.util.Map;
import java.util.Map.Entry;

public class ListenerService extends FirebaseMessagingService {
    static final String LOCK_FILE = "FIREBASE_CLOUD_MESSAGING_LOCKFILE";
    public static final String MESSAGE_TYPE_DELETED = "deleted_messages";
    public static final String MESSAGE_TYPE_SEND_ERROR = "send_error";
    public static final String MESSAGE_TYPE_SEND_EVENT = "send_event";
    static final String STORAGE_FILE = "FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE";
    private static final String TAG = "FIREBASE_LISTENER";

    private static String emptyIfNull(String str) {
        return str != null ? str : "";
    }

    private static byte[] generateMessageByteBuffer(String str, String str2, String str3, String str4, Map<String, String> map, Notification notification, boolean z) {
        int i;
        int createDataVector;
        FlatBufferBuilder flatBufferBuilder = new FlatBufferBuilder(0);
        int createString = flatBufferBuilder.createString(emptyIfNull(str));
        int createString2 = flatBufferBuilder.createString(emptyIfNull(str2));
        int createString3 = flatBufferBuilder.createString(emptyIfNull(str3));
        int createString4 = flatBufferBuilder.createString(emptyIfNull(str4));
        if (map != null) {
            int[] iArr = new int[map.size()];
            i = 0;
            for (Entry entry : map.entrySet()) {
                iArr[i] = DataPair.createDataPair(flatBufferBuilder, flatBufferBuilder.createString((CharSequence) entry.getKey()), flatBufferBuilder.createString((CharSequence) entry.getValue()));
                i++;
            }
            createDataVector = SerializedMessage.createDataVector(flatBufferBuilder, iArr);
        } else {
            createDataVector = 0;
        }
        int i2 = 0;
        if (notification != null) {
            int i3;
            int createString5 = flatBufferBuilder.createString(emptyIfNull(notification.getTitle()));
            int createString6 = flatBufferBuilder.createString(emptyIfNull(notification.getBody()));
            int createString7 = flatBufferBuilder.createString(emptyIfNull(notification.getIcon()));
            int createString8 = flatBufferBuilder.createString(emptyIfNull(notification.getSound()));
            int createString9 = flatBufferBuilder.createString((CharSequence) "");
            int createString10 = flatBufferBuilder.createString(emptyIfNull(notification.getTag()));
            int createString11 = flatBufferBuilder.createString(emptyIfNull(notification.getColor()));
            int createString12 = flatBufferBuilder.createString(emptyIfNull(notification.getClickAction()));
            int createString13 = flatBufferBuilder.createString(emptyIfNull(notification.getBodyLocalizationKey()));
            i2 = 0;
            String[] bodyLocalizationArgs = notification.getBodyLocalizationArgs();
            if (bodyLocalizationArgs != null) {
                int[] iArr2 = new int[bodyLocalizationArgs.length];
                int length = bodyLocalizationArgs.length;
                i3 = 0;
                i2 = 0;
                while (i3 < length) {
                    iArr2[i2] = flatBufferBuilder.createString((CharSequence) bodyLocalizationArgs[i3]);
                    i3++;
                    i2++;
                }
                i2 = SerializedNotification.createBodyLocArgsVector(flatBufferBuilder, iArr2);
            }
            int createString14 = flatBufferBuilder.createString(emptyIfNull(notification.getTitleLocalizationKey()));
            i3 = 0;
            String[] titleLocalizationArgs = notification.getTitleLocalizationArgs();
            if (titleLocalizationArgs != null) {
                int[] iArr3 = new int[titleLocalizationArgs.length];
                int length2 = titleLocalizationArgs.length;
                i3 = 0;
                i = 0;
                while (i3 < length2) {
                    iArr3[i] = flatBufferBuilder.createString((CharSequence) titleLocalizationArgs[i3]);
                    i3++;
                    i++;
                }
                i3 = SerializedNotification.createTitleLocArgsVector(flatBufferBuilder, iArr3);
            }
            SerializedNotification.startSerializedNotification(flatBufferBuilder);
            SerializedNotification.addTitle(flatBufferBuilder, createString5);
            SerializedNotification.addBody(flatBufferBuilder, createString6);
            SerializedNotification.addIcon(flatBufferBuilder, createString7);
            SerializedNotification.addSound(flatBufferBuilder, createString8);
            SerializedNotification.addBadge(flatBufferBuilder, createString9);
            SerializedNotification.addTag(flatBufferBuilder, createString10);
            SerializedNotification.addColor(flatBufferBuilder, createString11);
            SerializedNotification.addClickAction(flatBufferBuilder, createString12);
            SerializedNotification.addBodyLocKey(flatBufferBuilder, createString13);
            SerializedNotification.addBodyLocArgs(flatBufferBuilder, i2);
            SerializedNotification.addTitleLocKey(flatBufferBuilder, createString14);
            SerializedNotification.addTitleLocArgs(flatBufferBuilder, i3);
            i2 = SerializedNotification.endSerializedNotification(flatBufferBuilder);
        }
        SerializedMessage.startSerializedMessage(flatBufferBuilder);
        SerializedMessage.addFrom(flatBufferBuilder, createString);
        SerializedMessage.addMessageId(flatBufferBuilder, createString2);
        SerializedMessage.addMessageType(flatBufferBuilder, createString3);
        SerializedMessage.addError(flatBufferBuilder, createString4);
        if (map != null) {
            SerializedMessage.addData(flatBufferBuilder, createDataVector);
        }
        if (notification != null) {
            SerializedMessage.addNotification(flatBufferBuilder, i2);
        }
        SerializedMessage.addNotificationOpened(flatBufferBuilder, z);
        i2 = SerializedMessage.endSerializedMessage(flatBufferBuilder);
        SerializedEvent.startSerializedEvent(flatBufferBuilder);
        SerializedEvent.addEventType(flatBufferBuilder, (byte) 1);
        SerializedEvent.addEvent(flatBufferBuilder, i2);
        flatBufferBuilder.finish(SerializedEvent.endSerializedEvent(flatBufferBuilder));
        return flatBufferBuilder.sizedByteArray();
    }

    public static void onMessageReceived(Context context, RemoteMessage remoteMessage, boolean z) {
        Object obj;
        String from = remoteMessage.getFrom();
        Map data = remoteMessage.getData();
        Notification notification = remoteMessage.getNotification();
        if (data == null) {
            obj = "(null)";
        } else {
            String obj2 = data.toString();
        }
        String obj3 = notification == null ? "(null)" : notification.toString();
        DebugLogging.log(TAG, String.format("onMessageReceived from=%s data=%s, notification=%s", new Object[]{from, obj, obj3}));
        writeMessageToInternalStorage(context, from, null, remoteMessage.getMessageType(), null, data, notification, z);
    }

    private static void writeMessageToInternalStorage(Context context, String str, String str2, String str3, String str4, Map<String, String> map, Notification notification, boolean z) {
        FileLock fileLock = null;
        byte[] generateMessageByteBuffer = generateMessageByteBuffer(str, str2, str3, str4, map, notification, z);
        ByteBuffer allocate = ByteBuffer.allocate(4);
        allocate.order(ByteOrder.LITTLE_ENDIAN);
        allocate.putInt(generateMessageByteBuffer.length);
        try {
            fileLock = context.openFileOutput(LOCK_FILE, 0).getChannel().lock();
            FileOutputStream openFileOutput = context.openFileOutput(STORAGE_FILE, 32768);
            openFileOutput.write(allocate.array());
            openFileOutput.write(generateMessageByteBuffer);
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

    public void onDeletedMessages() {
        DebugLogging.log(TAG, "onDeletedMessages");
        writeMessageToInternalStorage(this, null, null, "deleted_messages", null, null, null, false);
    }

    public void onMessageReceived(RemoteMessage remoteMessage) {
        onMessageReceived(this, remoteMessage, false);
    }

    public void onMessageSent(String str) {
        DebugLogging.log(TAG, String.format("onMessageSent messageId=%s", new Object[]{str}));
        writeMessageToInternalStorage(this, null, str, "send_event", null, null, null, false);
    }

    public void onSendError(String str, Exception exception) {
        DebugLogging.log(TAG, String.format("onSendError messageId=%s exception=%s", new Object[]{str, exception.toString()}));
        writeMessageToInternalStorage(this, null, str, "send_error", exception.toString(), null, null, false);
    }
}
