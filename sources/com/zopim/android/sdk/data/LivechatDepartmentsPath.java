package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.zopim.android.sdk.model.Department;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Map.Entry;

public class LivechatDepartmentsPath extends Path<LinkedHashMap<String, Department>> {
    private static final LivechatDepartmentsPath INSTANCE = new LivechatDepartmentsPath();
    private static final String TAG = LivechatDepartmentsPath.class.getSimpleName();
    private final Object mLock;

    private LivechatDepartmentsPath() {
        this.mLock = new Object();
        this.mData = new LinkedHashMap();
    }

    public static synchronized LivechatDepartmentsPath getInstance() {
        LivechatDepartmentsPath livechatDepartmentsPath;
        synchronized (LivechatDepartmentsPath.class) {
            livechatDepartmentsPath = INSTANCE;
        }
        return livechatDepartmentsPath;
    }

    private void updateInternal(LinkedHashMap<String, Department> linkedHashMap) {
        if (linkedHashMap == null) {
            Log.i(TAG, "Passed parameter must not be null. Aborting update.");
            return;
        }
        synchronized (this.mLock) {
            for (Entry entry : linkedHashMap.entrySet()) {
                String str = (String) entry.getKey();
                Department department = (Department) entry.getValue();
                if (((LinkedHashMap) this.mData).containsKey(str)) {
                    if (department == null) {
                        ((LinkedHashMap) this.mData).remove(str);
                    } else {
                        ObjectMapper mapper = this.PARSER.getMapper();
                        JsonNode valueToTree = mapper.valueToTree(department);
                        department = (Department) ((LinkedHashMap) this.mData).get(str);
                        if (department == null) {
                            ((LinkedHashMap) this.mData).remove(str);
                        } else {
                            try {
                                ((LinkedHashMap) this.mData).put(str, (Department) mapper.readerForUpdating(department).readValue(valueToTree));
                            } catch (Throwable e) {
                                Log.w(TAG, "Failed to process json. Department could not be updated.", e);
                            } catch (Throwable e2) {
                                Log.w(TAG, "IO error. Department could not be updated.", e2);
                            }
                        }
                    }
                } else if (department != null) {
                    try {
                        ((LinkedHashMap) this.mData).put(str, department);
                    } catch (Throwable e22) {
                        Log.w(TAG, "Failed to process json. Department could not be created.", e22);
                    }
                } else {
                    continue;
                }
            }
            broadcast(getData());
        }
    }

    void clear() {
        if (this.mData != null) {
            ((LinkedHashMap) this.mData).clear();
        }
    }

    public LinkedHashMap<String, Department> getData() {
        return this.mData != null ? new LinkedHashMap((Map) this.mData) : new LinkedHashMap();
    }

    void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            updateInternal((LinkedHashMap) this.PARSER.parse(str, new C0866e(this)));
        }
    }
}
