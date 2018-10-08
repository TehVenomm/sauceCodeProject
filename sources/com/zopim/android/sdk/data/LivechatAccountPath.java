package com.zopim.android.sdk.data;

import android.util.Log;
import com.zopim.android.sdk.model.Account;

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

    void clear() {
        this.mData = null;
    }

    public Account getData() {
        return (Account) this.mData;
    }

    void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            synchronized (this.mLock) {
                if (this.mData == null) {
                    this.mData = this.PARSER.parse(str, new C0863b(this));
                } else {
                    try {
                        this.mData = (Account) this.PARSER.getMapper().readerForUpdating(this.mData).readValue(str);
                    } catch (Throwable e) {
                        Log.w(TAG, "Failed to process json. Account could not be updated.", e);
                    } catch (Throwable e2) {
                        Log.w(TAG, "IO error. Account could not be updated.", e2);
                    }
                }
                broadcast(getData());
            }
        }
    }
}
