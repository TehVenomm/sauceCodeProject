package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.zopim.android.sdk.model.Account;
import java.io.IOException;

public class LivechatAccountPath extends Path<Account> {
    private static final LivechatAccountPath INSTANCE = new LivechatAccountPath();
    private static final String TAG = LivechatAccountPath.class.getSimpleName();
    private final Object mLock = new Object();

    private LivechatAccountPath() {
    }

    public static synchronized LivechatAccountPath getInstance() {
        LivechatAccountPath livechatAccountPath;
        synchronized (LivechatAccountPath.class) {
            livechatAccountPath = INSTANCE;
        }
        return livechatAccountPath;
    }

    /* access modifiers changed from: 0000 */
    public void clear() {
        this.mData = null;
    }

    public Account getData() {
        return (Account) this.mData;
    }

    /* access modifiers changed from: 0000 */
    public void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            synchronized (this.mLock) {
                if (this.mData == null) {
                    this.mData = this.PARSER.parse(str, (TypeReference<T>) new C1233b<T>(this));
                } else {
                    try {
                        this.mData = (Account) this.PARSER.getMapper().readerForUpdating(this.mData).readValue(str);
                    } catch (JsonProcessingException e) {
                        Log.w(TAG, "Failed to process json. Account could not be updated.", e);
                    } catch (IOException e2) {
                        Log.w(TAG, "IO error. Account could not be updated.", e2);
                    }
                }
                broadcast(getData());
            }
        }
    }
}
