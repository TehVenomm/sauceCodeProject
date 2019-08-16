package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.zopim.android.sdk.model.Profile;
import java.io.IOException;

public class LivechatProfilePath extends Path<Profile> {
    private static final LivechatProfilePath INSTANCE = new LivechatProfilePath();
    private static final String TAG = LivechatProfilePath.class.getSimpleName();
    private final Object mLock = new Object();

    private LivechatProfilePath() {
    }

    public static synchronized LivechatProfilePath getInstance() {
        LivechatProfilePath livechatProfilePath;
        synchronized (LivechatProfilePath.class) {
            livechatProfilePath = INSTANCE;
        }
        return livechatProfilePath;
    }

    /* access modifiers changed from: 0000 */
    public void clear() {
        this.mData = null;
    }

    public Profile getData() {
        return (Profile) this.mData;
    }

    /* access modifiers changed from: 0000 */
    public void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            synchronized (this.mLock) {
                if (this.mData == null) {
                    this.mData = this.PARSER.parse(str, (TypeReference<T>) new C1238g<T>(this));
                } else {
                    try {
                        this.mData = (Profile) this.PARSER.getMapper().readerForUpdating(this.mData).readValue(str);
                    } catch (JsonProcessingException e) {
                        Log.w(TAG, "Failed to process json. Profile could not be updated.", e);
                    } catch (IOException e2) {
                        Log.w(TAG, "IO error. Profile could not be updated.", e2);
                    }
                }
                broadcast(getData());
            }
        }
    }
}
