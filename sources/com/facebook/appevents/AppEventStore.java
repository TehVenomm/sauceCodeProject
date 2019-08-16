package com.facebook.appevents;

import android.content.Context;
import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.appevents.internal.AppEventUtility;
import com.facebook.internal.Utility;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.ObjectStreamClass;

class AppEventStore {
    private static final String PERSISTED_EVENTS_FILENAME = "AppEventsLogger.persistedevents";
    private static final String TAG = AppEventStore.class.getName();

    private static class MovedClassObjectInputStream extends ObjectInputStream {
        private static final String ACCESS_TOKEN_APP_ID_PAIR_SERIALIZATION_PROXY_V1_CLASS_NAME = "com.facebook.appevents.AppEventsLogger$AccessTokenAppIdPair$SerializationProxyV1";
        private static final String APP_EVENT_SERIALIZATION_PROXY_V1_CLASS_NAME = "com.facebook.appevents.AppEventsLogger$AppEvent$SerializationProxyV1";

        public MovedClassObjectInputStream(InputStream inputStream) throws IOException {
            super(inputStream);
        }

        /* access modifiers changed from: protected */
        public ObjectStreamClass readClassDescriptor() throws IOException, ClassNotFoundException {
            ObjectStreamClass readClassDescriptor = super.readClassDescriptor();
            return readClassDescriptor.getName().equals(ACCESS_TOKEN_APP_ID_PAIR_SERIALIZATION_PROXY_V1_CLASS_NAME) ? ObjectStreamClass.lookup(SerializationProxyV1.class) : readClassDescriptor.getName().equals(APP_EVENT_SERIALIZATION_PROXY_V1_CLASS_NAME) ? ObjectStreamClass.lookup(SerializationProxyV1.class) : readClassDescriptor;
        }
    }

    AppEventStore() {
    }

    public static void persistEvents(AccessTokenAppIdPair accessTokenAppIdPair, SessionEventsState sessionEventsState) {
        synchronized (AppEventStore.class) {
            try {
                AppEventUtility.assertIsNotMainThread();
                PersistedEvents readAndClearStore = readAndClearStore();
                if (readAndClearStore.containsKey(accessTokenAppIdPair)) {
                    readAndClearStore.get(accessTokenAppIdPair).addAll(sessionEventsState.getEventsToPersist());
                } else {
                    readAndClearStore.addEvents(accessTokenAppIdPair, sessionEventsState.getEventsToPersist());
                }
                saveEventsToDisk(readAndClearStore);
            } finally {
                Class<AppEventStore> cls = AppEventStore.class;
            }
        }
    }

    public static void persistEvents(AppEventCollection appEventCollection) {
        synchronized (AppEventStore.class) {
            try {
                AppEventUtility.assertIsNotMainThread();
                PersistedEvents readAndClearStore = readAndClearStore();
                for (AccessTokenAppIdPair accessTokenAppIdPair : appEventCollection.keySet()) {
                    readAndClearStore.addEvents(accessTokenAppIdPair, appEventCollection.get(accessTokenAppIdPair).getEventsToPersist());
                }
                saveEventsToDisk(readAndClearStore);
            } finally {
                Class<AppEventStore> cls = AppEventStore.class;
            }
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:15:0x002f A[SYNTHETIC, Splitter:B:15:0x002f] */
    /* JADX WARNING: Unknown top exception splitter block from list: {B:58:0x0090=Splitter:B:58:0x0090, B:29:0x0048=Splitter:B:29:0x0048} */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.facebook.appevents.PersistedEvents readAndClearStore() {
        /*
            r2 = 0
            java.lang.Class<com.facebook.appevents.AppEventStore> r0 = com.facebook.appevents.AppEventStore.class
            monitor-enter(r0)
            com.facebook.appevents.internal.AppEventUtility.assertIsNotMainThread()     // Catch:{ all -> 0x0041 }
            android.content.Context r4 = com.facebook.FacebookSdk.getApplicationContext()     // Catch:{ all -> 0x0041 }
            java.lang.String r0 = "AppEventsLogger.persistedevents"
            java.io.FileInputStream r0 = r4.openFileInput(r0)     // Catch:{ FileNotFoundException -> 0x0046, Exception -> 0x0060, all -> 0x0081 }
            com.facebook.appevents.AppEventStore$MovedClassObjectInputStream r1 = new com.facebook.appevents.AppEventStore$MovedClassObjectInputStream     // Catch:{ FileNotFoundException -> 0x0046, Exception -> 0x0060, all -> 0x0081 }
            java.io.BufferedInputStream r3 = new java.io.BufferedInputStream     // Catch:{ FileNotFoundException -> 0x0046, Exception -> 0x0060, all -> 0x0081 }
            r3.<init>(r0)     // Catch:{ FileNotFoundException -> 0x0046, Exception -> 0x0060, all -> 0x0081 }
            r1.<init>(r3)     // Catch:{ FileNotFoundException -> 0x0046, Exception -> 0x0060, all -> 0x0081 }
            java.lang.Object r0 = r1.readObject()     // Catch:{ FileNotFoundException -> 0x009d, Exception -> 0x009f, all -> 0x009a }
            com.facebook.appevents.PersistedEvents r0 = (com.facebook.appevents.PersistedEvents) r0     // Catch:{ FileNotFoundException -> 0x009d, Exception -> 0x009f, all -> 0x009a }
            com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0041 }
            java.lang.String r1 = "AppEventsLogger.persistedevents"
            java.io.File r1 = r4.getFileStreamPath(r1)     // Catch:{ Exception -> 0x0038 }
            r1.delete()     // Catch:{ Exception -> 0x0038 }
        L_0x002d:
            if (r0 != 0) goto L_0x0034
            com.facebook.appevents.PersistedEvents r0 = new com.facebook.appevents.PersistedEvents     // Catch:{ all -> 0x0041 }
            r0.<init>()     // Catch:{ all -> 0x0041 }
        L_0x0034:
            java.lang.Class<com.facebook.appevents.AppEventStore> r1 = com.facebook.appevents.AppEventStore.class
            monitor-exit(r1)
            return r0
        L_0x0038:
            r1 = move-exception
            java.lang.String r2 = TAG     // Catch:{ all -> 0x0041 }
            java.lang.String r3 = "Got unexpected exception when removing events file: "
            android.util.Log.w(r2, r3, r1)     // Catch:{ all -> 0x0041 }
            goto L_0x002d
        L_0x0041:
            r0 = move-exception
            java.lang.Class<com.facebook.appevents.AppEventStore> r1 = com.facebook.appevents.AppEventStore.class
            monitor-exit(r1)
            throw r0
        L_0x0046:
            r0 = move-exception
            r1 = r2
        L_0x0048:
            com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0041 }
            java.lang.String r0 = "AppEventsLogger.persistedevents"
            java.io.File r0 = r4.getFileStreamPath(r0)     // Catch:{ Exception -> 0x0056 }
            r0.delete()     // Catch:{ Exception -> 0x0056 }
            r0 = r2
            goto L_0x002d
        L_0x0056:
            r0 = move-exception
            java.lang.String r1 = TAG     // Catch:{ all -> 0x0041 }
            java.lang.String r3 = "Got unexpected exception when removing events file: "
            android.util.Log.w(r1, r3, r0)     // Catch:{ all -> 0x0041 }
            r0 = r2
            goto L_0x002d
        L_0x0060:
            r0 = move-exception
            r1 = r2
        L_0x0062:
            java.lang.String r3 = TAG     // Catch:{ all -> 0x00a1 }
            java.lang.String r5 = "Got unexpected exception while reading events: "
            android.util.Log.w(r3, r5, r0)     // Catch:{ all -> 0x00a1 }
            com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0041 }
            java.lang.String r0 = "AppEventsLogger.persistedevents"
            java.io.File r0 = r4.getFileStreamPath(r0)     // Catch:{ Exception -> 0x0077 }
            r0.delete()     // Catch:{ Exception -> 0x0077 }
            r0 = r2
            goto L_0x002d
        L_0x0077:
            r0 = move-exception
            java.lang.String r1 = TAG     // Catch:{ all -> 0x0041 }
            java.lang.String r3 = "Got unexpected exception when removing events file: "
            android.util.Log.w(r1, r3, r0)     // Catch:{ all -> 0x0041 }
            r0 = r2
            goto L_0x002d
        L_0x0081:
            r0 = move-exception
        L_0x0082:
            r1 = r2
            r3 = r0
        L_0x0084:
            com.facebook.internal.Utility.closeQuietly(r1)     // Catch:{ all -> 0x0041 }
            java.lang.String r0 = "AppEventsLogger.persistedevents"
            java.io.File r0 = r4.getFileStreamPath(r0)     // Catch:{ Exception -> 0x0091 }
            r0.delete()     // Catch:{ Exception -> 0x0091 }
        L_0x0090:
            throw r3     // Catch:{ all -> 0x0041 }
        L_0x0091:
            r0 = move-exception
            java.lang.String r1 = TAG     // Catch:{ all -> 0x0041 }
            java.lang.String r2 = "Got unexpected exception when removing events file: "
            android.util.Log.w(r1, r2, r0)     // Catch:{ all -> 0x0041 }
            goto L_0x0090
        L_0x009a:
            r0 = move-exception
            r3 = r0
            goto L_0x0084
        L_0x009d:
            r0 = move-exception
            goto L_0x0048
        L_0x009f:
            r0 = move-exception
            goto L_0x0062
        L_0x00a1:
            r0 = move-exception
            r2 = r1
            goto L_0x0082
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.appevents.AppEventStore.readAndClearStore():com.facebook.appevents.PersistedEvents");
    }

    private static void saveEventsToDisk(PersistedEvents persistedEvents) {
        ObjectOutputStream objectOutputStream;
        Exception e;
        ObjectOutputStream objectOutputStream2 = null;
        Context applicationContext = FacebookSdk.getApplicationContext();
        try {
            objectOutputStream = new ObjectOutputStream(new BufferedOutputStream(applicationContext.openFileOutput(PERSISTED_EVENTS_FILENAME, 0)));
            try {
                objectOutputStream.writeObject(persistedEvents);
                Utility.closeQuietly(objectOutputStream);
            } catch (Exception e2) {
                e = e2;
                objectOutputStream2 = objectOutputStream;
                try {
                    Log.w(TAG, "Got unexpected exception while persisting events: ", e);
                    try {
                        applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    } catch (Exception e3) {
                    }
                    Utility.closeQuietly(objectOutputStream2);
                } catch (Throwable th) {
                    th = th;
                    objectOutputStream = objectOutputStream2;
                    Utility.closeQuietly(objectOutputStream);
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
                Utility.closeQuietly(objectOutputStream);
                throw th;
            }
        } catch (Exception e4) {
            e = e4;
            Log.w(TAG, "Got unexpected exception while persisting events: ", e);
            applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
            Utility.closeQuietly(objectOutputStream2);
        } catch (Throwable th3) {
            th = th3;
            objectOutputStream = null;
            Utility.closeQuietly(objectOutputStream);
            throw th;
        }
    }
}
