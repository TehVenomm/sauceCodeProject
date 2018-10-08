package com.facebook.appevents;

import android.content.Context;
import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.internal.Utility;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.Closeable;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.ObjectStreamClass;
import java.util.Iterator;

class AppEventStore {
    private static final String PERSISTED_EVENTS_FILENAME = "AppEventsLogger.persistedevents";
    private static final String TAG = AppEventStore.class.getName();

    private static class MovedClassObjectInputStream extends ObjectInputStream {
        private static final String ACCESS_TOKEN_APP_ID_PAIR_SERIALIZATION_PROXY_V1_CLASS_NAME = "com.facebook.appevents.AppEventsLogger$AccessTokenAppIdPair$SerializationProxyV1";
        private static final String APP_EVENT_SERIALIZATION_PROXY_V1_CLASS_NAME = "com.facebook.appevents.AppEventsLogger$AppEvent$SerializationProxyV1";

        public MovedClassObjectInputStream(InputStream inputStream) throws IOException {
            super(inputStream);
        }

        protected ObjectStreamClass readClassDescriptor() throws IOException, ClassNotFoundException {
            ObjectStreamClass readClassDescriptor = super.readClassDescriptor();
            return readClassDescriptor.getName().equals(ACCESS_TOKEN_APP_ID_PAIR_SERIALIZATION_PROXY_V1_CLASS_NAME) ? ObjectStreamClass.lookup(SerializationProxyV1.class) : readClassDescriptor.getName().equals(APP_EVENT_SERIALIZATION_PROXY_V1_CLASS_NAME) ? ObjectStreamClass.lookup(SerializationProxyV1.class) : readClassDescriptor;
        }
    }

    AppEventStore() {
    }

    private static void assertIsNotMainThread() {
    }

    public static void persistEvents(AccessTokenAppIdPair accessTokenAppIdPair, SessionEventsState sessionEventsState) {
        synchronized (AppEventStore.class) {
            try {
                assertIsNotMainThread();
                Object readAndClearStore = readAndClearStore();
                if (readAndClearStore.containsKey(accessTokenAppIdPair)) {
                    readAndClearStore.get(accessTokenAppIdPair).addAll(sessionEventsState.getEventsToPersist());
                } else {
                    readAndClearStore.addEvents(accessTokenAppIdPair, sessionEventsState.getEventsToPersist());
                }
                saveEventsToDisk(readAndClearStore);
            } finally {
                Class cls = AppEventStore.class;
            }
        }
    }

    public static void persistEvents(AppEventCollection appEventCollection) {
        synchronized (AppEventStore.class) {
            try {
                assertIsNotMainThread();
                PersistedEvents readAndClearStore = readAndClearStore();
                Iterator it = appEventCollection.keySet().iterator();
                while (true) {
                    Object hasNext = it.hasNext();
                    if (hasNext == null) {
                        break;
                    }
                    AccessTokenAppIdPair accessTokenAppIdPair = (AccessTokenAppIdPair) it.next();
                    readAndClearStore.addEvents(accessTokenAppIdPair, appEventCollection.get(accessTokenAppIdPair).getEventsToPersist());
                }
                saveEventsToDisk(readAndClearStore);
            } finally {
                Class cls = AppEventStore.class;
            }
        }
    }

    public static PersistedEvents readAndClearStore() {
        Closeable movedClassObjectInputStream;
        PersistedEvents persistedEvents;
        Throwable e;
        Throwable th;
        Closeable closeable = null;
        synchronized (AppEventStore.class) {
            try {
                assertIsNotMainThread();
                Context applicationContext = FacebookSdk.getApplicationContext();
                try {
                    movedClassObjectInputStream = new MovedClassObjectInputStream(new BufferedInputStream(applicationContext.openFileInput(PERSISTED_EVENTS_FILENAME)));
                    try {
                        persistedEvents = (PersistedEvents) movedClassObjectInputStream.readObject();
                        Utility.closeQuietly(movedClassObjectInputStream);
                        applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    } catch (FileNotFoundException e2) {
                        Utility.closeQuietly(movedClassObjectInputStream);
                        try {
                            applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                            persistedEvents = null;
                        } catch (Throwable e3) {
                            Log.w(TAG, "Got unexpected exception when removing events file: ", e3);
                            persistedEvents = null;
                        }
                        if (persistedEvents == null) {
                            persistedEvents = new PersistedEvents();
                        }
                        return persistedEvents;
                    } catch (Exception e4) {
                        e3 = e4;
                        try {
                            Log.w(TAG, "Got unexpected exception while reading events: ", e3);
                            Utility.closeQuietly(movedClassObjectInputStream);
                            try {
                                applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                                persistedEvents = null;
                            } catch (Throwable e32) {
                                Log.w(TAG, "Got unexpected exception when removing events file: ", e32);
                                persistedEvents = null;
                            }
                            if (persistedEvents == null) {
                                persistedEvents = new PersistedEvents();
                            }
                            return persistedEvents;
                        } catch (Throwable th2) {
                            e32 = th2;
                            closeable = movedClassObjectInputStream;
                            movedClassObjectInputStream = closeable;
                            th = e32;
                            Utility.closeQuietly(movedClassObjectInputStream);
                            try {
                                applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                            } catch (Throwable e322) {
                                Log.w(TAG, "Got unexpected exception when removing events file: ", e322);
                            }
                            throw th;
                        }
                    } catch (Throwable e3222) {
                        th = e3222;
                        Utility.closeQuietly(movedClassObjectInputStream);
                        applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                        throw th;
                    }
                } catch (FileNotFoundException e5) {
                    movedClassObjectInputStream = null;
                    Utility.closeQuietly(movedClassObjectInputStream);
                    applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    persistedEvents = null;
                    if (persistedEvents == null) {
                        persistedEvents = new PersistedEvents();
                    }
                    return persistedEvents;
                } catch (Exception e6) {
                    e3222 = e6;
                    movedClassObjectInputStream = null;
                    Log.w(TAG, "Got unexpected exception while reading events: ", e3222);
                    Utility.closeQuietly(movedClassObjectInputStream);
                    applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    persistedEvents = null;
                    if (persistedEvents == null) {
                        persistedEvents = new PersistedEvents();
                    }
                    return persistedEvents;
                } catch (Throwable th3) {
                    e3222 = th3;
                    movedClassObjectInputStream = closeable;
                    th = e3222;
                    Utility.closeQuietly(movedClassObjectInputStream);
                    applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    throw th;
                }
            } catch (Throwable e7) {
                Log.w(TAG, "Got unexpected exception when removing events file: ", e7);
            } catch (Throwable th4) {
                Class cls = AppEventStore.class;
            }
            if (persistedEvents == null) {
                persistedEvents = new PersistedEvents();
            }
        }
        return persistedEvents;
    }

    private static void saveEventsToDisk(PersistedEvents persistedEvents) {
        Closeable objectOutputStream;
        Throwable th;
        Throwable e;
        Throwable th2;
        Closeable closeable = null;
        Context applicationContext = FacebookSdk.getApplicationContext();
        try {
            objectOutputStream = new ObjectOutputStream(new BufferedOutputStream(applicationContext.openFileOutput(PERSISTED_EVENTS_FILENAME, 0)));
            try {
                objectOutputStream.writeObject(persistedEvents);
                Utility.closeQuietly(objectOutputStream);
            } catch (Throwable e2) {
                th = e2;
                closeable = objectOutputStream;
                th2 = th;
                try {
                    Log.w(TAG, "Got unexpected exception while persisting events: ", th2);
                    try {
                        applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
                    } catch (Exception e3) {
                    }
                    Utility.closeQuietly(closeable);
                } catch (Throwable th22) {
                    th = th22;
                    objectOutputStream = closeable;
                    e2 = th;
                    Utility.closeQuietly(objectOutputStream);
                    throw e2;
                }
            } catch (Throwable th3) {
                e2 = th3;
                Utility.closeQuietly(objectOutputStream);
                throw e2;
            }
        } catch (Exception e4) {
            th22 = e4;
            Log.w(TAG, "Got unexpected exception while persisting events: ", th22);
            applicationContext.getFileStreamPath(PERSISTED_EVENTS_FILENAME).delete();
            Utility.closeQuietly(closeable);
        } catch (Throwable th222) {
            th = th222;
            objectOutputStream = null;
            e2 = th;
            Utility.closeQuietly(objectOutputStream);
            throw e2;
        }
    }
}
