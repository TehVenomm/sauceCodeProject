package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.zopim.android.sdk.model.Forms;
import java.io.IOException;

public class LivechatFormsPath extends Path<Forms> {
    private static final LivechatFormsPath INSTANCE = new LivechatFormsPath();
    private static final String TAG = LivechatFormsPath.class.getSimpleName();
    private final Object mLock = new Object();

    private LivechatFormsPath() {
    }

    public static synchronized LivechatFormsPath getInstance() {
        LivechatFormsPath livechatFormsPath;
        synchronized (LivechatFormsPath.class) {
            livechatFormsPath = INSTANCE;
        }
        return livechatFormsPath;
    }

    /* access modifiers changed from: 0000 */
    public void clear() {
        this.mData = null;
    }

    public Forms getData() {
        return (Forms) this.mData;
    }

    /* access modifiers changed from: 0000 */
    public void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            synchronized (this.mLock) {
                if (this.mData == null) {
                    this.mData = this.PARSER.parse(str, (TypeReference<T>) new C1237f<T>(this));
                } else {
                    try {
                        this.mData = (Forms) this.PARSER.getMapper().readerForUpdating(this.mData).readValue(str);
                    } catch (JsonProcessingException e) {
                        Log.w(TAG, "Failed to process json. Forms could not be updated.", e);
                    } catch (IOException e2) {
                        Log.w(TAG, "IO error. Forms could not be updated.", e2);
                    }
                }
                broadcast(getData());
            }
        }
    }
}
