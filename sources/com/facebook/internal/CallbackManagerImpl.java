package com.facebook.internal;

import android.content.Intent;
import com.facebook.CallbackManager;
import com.facebook.FacebookSdk;
import java.util.HashMap;
import java.util.Map;

public final class CallbackManagerImpl implements CallbackManager {
    private static Map<Integer, Callback> staticCallbacks = new HashMap();
    private Map<Integer, Callback> callbacks = new HashMap();

    public interface Callback {
        boolean onActivityResult(int i, Intent intent);
    }

    public enum RequestCodeOffset {
        Login(0),
        Share(1),
        Message(2),
        Like(3),
        GameRequest(4),
        AppGroupCreate(5),
        AppGroupJoin(6),
        AppInvite(7),
        DeviceShare(8);
        
        private final int offset;

        private RequestCodeOffset(int i) {
            this.offset = i;
        }

        public int toRequestCode() {
            return FacebookSdk.getCallbackRequestCodeOffset() + this.offset;
        }
    }

    private static Callback getStaticCallback(Integer num) {
        synchronized (CallbackManagerImpl.class) {
            try {
                Callback callback = (Callback) staticCallbacks.get(num);
                return callback;
            } finally {
                Object obj = CallbackManagerImpl.class;
            }
        }
    }

    public static void registerStaticCallback(int i, Callback callback) {
        synchronized (CallbackManagerImpl.class) {
            try {
                Validate.notNull(callback, "callback");
                if (!staticCallbacks.containsKey(Integer.valueOf(i))) {
                    staticCallbacks.put(Integer.valueOf(i), callback);
                }
            } catch (Throwable th) {
                Class cls = CallbackManagerImpl.class;
            }
        }
    }

    private static boolean runStaticCallback(int i, int i2, Intent intent) {
        Callback staticCallback = getStaticCallback(Integer.valueOf(i));
        return staticCallback != null ? staticCallback.onActivityResult(i2, intent) : false;
    }

    public boolean onActivityResult(int i, int i2, Intent intent) {
        Callback callback = (Callback) this.callbacks.get(Integer.valueOf(i));
        return callback != null ? callback.onActivityResult(i2, intent) : runStaticCallback(i, i2, intent);
    }

    public void registerCallback(int i, Callback callback) {
        Validate.notNull(callback, "callback");
        this.callbacks.put(Integer.valueOf(i), callback);
    }
}
