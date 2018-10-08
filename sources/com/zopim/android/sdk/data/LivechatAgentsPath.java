package com.zopim.android.sdk.data;

import android.util.Log;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.zopim.android.sdk.model.Agent;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Map.Entry;

public class LivechatAgentsPath extends Path<LinkedHashMap<String, Agent>> {
    private static final LivechatAgentsPath INSTANCE = new LivechatAgentsPath();
    private static final String TAG = LivechatAgentsPath.class.getSimpleName();
    private final Object mLock;

    private LivechatAgentsPath() {
        this.mLock = new Object();
        this.mData = new LinkedHashMap();
    }

    public static synchronized LivechatAgentsPath getInstance() {
        LivechatAgentsPath livechatAgentsPath;
        synchronized (LivechatAgentsPath.class) {
            livechatAgentsPath = INSTANCE;
        }
        return livechatAgentsPath;
    }

    private void updateInternal(LinkedHashMap<String, Agent> linkedHashMap) {
        if (linkedHashMap == null) {
            Log.i(TAG, "Passed parameter must not be null. Aborting update.");
            return;
        }
        synchronized (this.mLock) {
            for (Entry entry : linkedHashMap.entrySet()) {
                String str = (String) entry.getKey();
                Agent agent = (Agent) entry.getValue();
                if (((LinkedHashMap) this.mData).containsKey(str)) {
                    if (agent != null) {
                        ObjectMapper mapper = this.PARSER.getMapper();
                        JsonNode valueToTree = mapper.valueToTree(agent);
                        agent = (Agent) ((LinkedHashMap) this.mData).get(str);
                        if (agent == null) {
                            ((LinkedHashMap) this.mData).remove(str);
                        } else {
                            try {
                                ((LinkedHashMap) this.mData).put(str, (Agent) mapper.readerForUpdating(agent).readValue(valueToTree));
                            } catch (Throwable e) {
                                Log.w(TAG, "Failed to process json. Agent could not be updated.", e);
                            } catch (Throwable e2) {
                                Log.w(TAG, "IO error. Agent could not be updated.", e2);
                            }
                        }
                    } else {
                        continue;
                    }
                } else if (agent != null) {
                    try {
                        ((LinkedHashMap) this.mData).put(str, agent);
                    } catch (Throwable e22) {
                        Log.w(TAG, "Failed to process json. Agent could not be created.", e22);
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

    public LinkedHashMap<String, Agent> getData() {
        return this.mData != null ? new LinkedHashMap((Map) this.mData) : new LinkedHashMap();
    }

    void update(String str) {
        if (isClearRequired(str)) {
            clear();
        } else if (!str.isEmpty()) {
            updateInternal((LinkedHashMap) this.PARSER.parse(str, new C0865c(this)));
        }
    }
}
