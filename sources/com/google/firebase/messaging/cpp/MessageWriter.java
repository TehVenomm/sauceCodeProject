package com.google.firebase.messaging.cpp;

import android.content.Context;
import android.net.Uri;
import com.google.firebase.messaging.RemoteMessage;
import com.google.firebase.messaging.RemoteMessage.Notification;
import com.google.flatbuffers.FlatBufferBuilder;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;

public class MessageWriter {
    private static final MessageWriter DEFAULT_INSTANCE = new MessageWriter();
    static final String LOCK_FILE = "FIREBASE_CLOUD_MESSAGING_LOCKFILE";
    static final String STORAGE_FILE = "FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE";
    private static final String TAG = "FIREBASE_MESSAGE_WRITER";

    public static MessageWriter defaultInstance() {
        return DEFAULT_INSTANCE;
    }

    private static String emptyIfNull(String str) {
        return str != null ? str : "";
    }

    private static byte[] generateMessageByteBuffer(String str, String str2, String str3, String str4, String str5, Map<String, String> map, Notification notification, boolean z, String str6, String str7, int i, int i2, long j, int i3) {
        int i4;
        FlatBufferBuilder flatBufferBuilder = new FlatBufferBuilder(0);
        int createString = flatBufferBuilder.createString((CharSequence) emptyIfNull(str));
        int createString2 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str2));
        int createString3 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str3));
        int createString4 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str4));
        int createString5 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str5));
        int createString6 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str6));
        int createString7 = flatBufferBuilder.createString((CharSequence) emptyIfNull(str7));
        int createString8 = flatBufferBuilder.createString((CharSequence) priorityToString(i));
        int createString9 = flatBufferBuilder.createString((CharSequence) priorityToString(i2));
        if (map != null) {
            int[] iArr = new int[map.size()];
            int i5 = 0;
            Iterator it = map.entrySet().iterator();
            while (true) {
                int i6 = i5;
                if (!it.hasNext()) {
                    break;
                }
                Entry entry = (Entry) it.next();
                iArr[i6] = DataPair.createDataPair(flatBufferBuilder, flatBufferBuilder.createString((CharSequence) entry.getKey()), flatBufferBuilder.createString((CharSequence) entry.getValue()));
                i5 = i6 + 1;
            }
            i4 = SerializedMessage.createDataVector(flatBufferBuilder, iArr);
        } else {
            i4 = 0;
        }
        int i7 = 0;
        if (notification != null) {
            int createString10 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getTitle()));
            int createString11 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getBody()));
            int createString12 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getIcon()));
            int createString13 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getSound()));
            int createString14 = flatBufferBuilder.createString((CharSequence) "");
            int createString15 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getTag()));
            int createString16 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getColor()));
            int createString17 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getClickAction()));
            int createString18 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getBodyLocalizationKey()));
            int i8 = 0;
            String[] bodyLocalizationArgs = notification.getBodyLocalizationArgs();
            if (bodyLocalizationArgs != null) {
                int[] iArr2 = new int[bodyLocalizationArgs.length];
                int length = bodyLocalizationArgs.length;
                int i9 = 0;
                int i10 = 0;
                while (i9 < length) {
                    iArr2[i10] = flatBufferBuilder.createString((CharSequence) bodyLocalizationArgs[i9]);
                    i9++;
                    i10++;
                }
                i8 = SerializedNotification.createBodyLocArgsVector(flatBufferBuilder, iArr2);
            }
            int createString19 = flatBufferBuilder.createString((CharSequence) emptyIfNull(notification.getTitleLocalizationKey()));
            int i11 = 0;
            String[] titleLocalizationArgs = notification.getTitleLocalizationArgs();
            if (titleLocalizationArgs != null) {
                int[] iArr3 = new int[titleLocalizationArgs.length];
                int length2 = titleLocalizationArgs.length;
                int i12 = 0;
                int i13 = 0;
                while (i12 < length2) {
                    iArr3[i13] = flatBufferBuilder.createString((CharSequence) titleLocalizationArgs[i12]);
                    i12++;
                    i13++;
                }
                i11 = SerializedNotification.createTitleLocArgsVector(flatBufferBuilder, iArr3);
            }
            SerializedNotification.startSerializedNotification(flatBufferBuilder);
            SerializedNotification.addTitle(flatBufferBuilder, createString10);
            SerializedNotification.addBody(flatBufferBuilder, createString11);
            SerializedNotification.addIcon(flatBufferBuilder, createString12);
            SerializedNotification.addSound(flatBufferBuilder, createString13);
            SerializedNotification.addBadge(flatBufferBuilder, createString14);
            SerializedNotification.addTag(flatBufferBuilder, createString15);
            SerializedNotification.addColor(flatBufferBuilder, createString16);
            SerializedNotification.addClickAction(flatBufferBuilder, createString17);
            SerializedNotification.addBodyLocKey(flatBufferBuilder, createString18);
            SerializedNotification.addBodyLocArgs(flatBufferBuilder, i8);
            SerializedNotification.addTitleLocKey(flatBufferBuilder, createString19);
            SerializedNotification.addTitleLocArgs(flatBufferBuilder, i11);
            i7 = SerializedNotification.endSerializedNotification(flatBufferBuilder);
        }
        SerializedMessage.startSerializedMessage(flatBufferBuilder);
        SerializedMessage.addFrom(flatBufferBuilder, createString);
        SerializedMessage.addTo(flatBufferBuilder, createString2);
        SerializedMessage.addMessageId(flatBufferBuilder, createString3);
        SerializedMessage.addMessageType(flatBufferBuilder, createString4);
        SerializedMessage.addPriority(flatBufferBuilder, createString8);
        SerializedMessage.addOriginalPriority(flatBufferBuilder, createString9);
        SerializedMessage.addSentTime(flatBufferBuilder, j);
        SerializedMessage.addTimeToLive(flatBufferBuilder, i3);
        SerializedMessage.addError(flatBufferBuilder, createString5);
        SerializedMessage.addCollapseKey(flatBufferBuilder, createString7);
        if (map != null) {
            SerializedMessage.addData(flatBufferBuilder, i4);
        }
        if (notification != null) {
            SerializedMessage.addNotification(flatBufferBuilder, i7);
        }
        SerializedMessage.addNotificationOpened(flatBufferBuilder, z);
        SerializedMessage.addLink(flatBufferBuilder, createString6);
        int endSerializedMessage = SerializedMessage.endSerializedMessage(flatBufferBuilder);
        SerializedEvent.startSerializedEvent(flatBufferBuilder);
        SerializedEvent.addEventType(flatBufferBuilder, 1);
        SerializedEvent.addEvent(flatBufferBuilder, endSerializedMessage);
        flatBufferBuilder.finish(SerializedEvent.endSerializedEvent(flatBufferBuilder));
        return flatBufferBuilder.sizedByteArray();
    }

    private static String priorityToString(int i) {
        switch (i) {
            case 1:
                return "high";
            case 2:
                return "normal";
            default:
                return "";
        }
    }

    public void writeMessage(Context context, RemoteMessage remoteMessage, boolean z, Uri uri) {
        String from = remoteMessage.getFrom();
        String to = remoteMessage.getTo();
        String messageId = remoteMessage.getMessageId();
        String messageType = remoteMessage.getMessageType();
        Map data = remoteMessage.getData();
        Notification notification = remoteMessage.getNotification();
        String collapseKey = remoteMessage.getCollapseKey();
        int priority = remoteMessage.getPriority();
        int originalPriority = remoteMessage.getOriginalPriority();
        long sentTime = remoteMessage.getSentTime();
        int ttl = remoteMessage.getTtl();
        if (uri == null && notification != null) {
            uri = notification.getLink();
        }
        String str = uri != null ? uri.toString() : null;
        DebugLogging.log(TAG, String.format("onMessageReceived from=%s message_id=%s, data=%s, notification=%s", new Object[]{from, messageId, data == null ? "(null)" : data.toString(), notification == null ? "(null)" : notification.toString()}));
        writeMessageToInternalStorage(context, from, to, messageId, messageType, null, data, notification, z, str, collapseKey, priority, originalPriority, sentTime, ttl);
    }

    /* access modifiers changed from: 0000 */
    public void writeMessageEventToInternalStorage(Context context, String str, String str2, String str3) {
        writeMessageToInternalStorage(context, null, null, str, str2, null, null, null, false, null, null, 0, 0, 0, 0);
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x0044, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:13:?, code lost:
        r0.printStackTrace();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x0048, code lost:
        if (r1 != null) goto L_0x004a;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:?, code lost:
        r1.release();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x004e, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x004f, code lost:
        r0.printStackTrace();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:?, code lost:
        r1.release();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:25:0x005b, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x005c, code lost:
        r1.printStackTrace();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:27:0x0060, code lost:
        r0 = th;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:31:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:32:?, code lost:
        return;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:?, code lost:
        return;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:11:0x0044 A[ExcHandler: Exception (r0v3 'e' java.lang.Exception A[CUSTOM_DECLARE]), Splitter:B:1:0x0017] */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x0057 A[SYNTHETIC, Splitter:B:22:0x0057] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void writeMessageToInternalStorage(android.content.Context r7, java.lang.String r8, java.lang.String r9, java.lang.String r10, java.lang.String r11, java.lang.String r12, java.util.Map<java.lang.String, java.lang.String> r13, com.google.firebase.messaging.RemoteMessage.Notification r14, boolean r15, java.lang.String r16, java.lang.String r17, int r18, int r19, long r20, int r22) {
        /*
            r6 = this;
            byte[] r0 = generateMessageByteBuffer(r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18, r19, r20, r22)
            r1 = 4
            java.nio.ByteBuffer r3 = java.nio.ByteBuffer.allocate(r1)
            java.nio.ByteOrder r1 = java.nio.ByteOrder.LITTLE_ENDIAN
            r3.order(r1)
            int r1 = r0.length
            r3.putInt(r1)
            r2 = 0
            r1 = 0
            java.lang.String r4 = "FIREBASE_CLOUD_MESSAGING_LOCKFILE"
            r5 = 0
            java.io.FileOutputStream r4 = r7.openFileOutput(r4, r5)     // Catch:{ Exception -> 0x0044, all -> 0x0053 }
            java.nio.channels.FileChannel r4 = r4.getChannel()     // Catch:{ Exception -> 0x0044, all -> 0x0053 }
            java.nio.channels.FileLock r1 = r4.lock()     // Catch:{ Exception -> 0x0044, all -> 0x0053 }
            java.lang.String r2 = "FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE"
            r4 = 32768(0x8000, float:4.5918E-41)
            java.io.FileOutputStream r2 = r7.openFileOutput(r2, r4)     // Catch:{ Exception -> 0x0044 }
            byte[] r3 = r3.array()     // Catch:{ Exception -> 0x0044 }
            r2.write(r3)     // Catch:{ Exception -> 0x0044 }
            r2.write(r0)     // Catch:{ Exception -> 0x0044 }
            r2.close()     // Catch:{ Exception -> 0x0044 }
            if (r1 == 0) goto L_0x003e
            r1.release()     // Catch:{ Exception -> 0x003f }
        L_0x003e:
            return
        L_0x003f:
            r0 = move-exception
            r0.printStackTrace()
            goto L_0x003e
        L_0x0044:
            r0 = move-exception
            r0.printStackTrace()     // Catch:{ all -> 0x0060 }
            if (r1 == 0) goto L_0x003e
            r1.release()     // Catch:{ Exception -> 0x004e }
            goto L_0x003e
        L_0x004e:
            r0 = move-exception
            r0.printStackTrace()
            goto L_0x003e
        L_0x0053:
            r0 = move-exception
            r1 = r2
        L_0x0055:
            if (r1 == 0) goto L_0x005a
            r1.release()     // Catch:{ Exception -> 0x005b }
        L_0x005a:
            throw r0
        L_0x005b:
            r1 = move-exception
            r1.printStackTrace()
            goto L_0x005a
        L_0x0060:
            r0 = move-exception
            goto L_0x0055
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.messaging.cpp.MessageWriter.writeMessageToInternalStorage(android.content.Context, java.lang.String, java.lang.String, java.lang.String, java.lang.String, java.lang.String, java.util.Map, com.google.firebase.messaging.RemoteMessage$Notification, boolean, java.lang.String, java.lang.String, int, int, long, int):void");
    }
}
