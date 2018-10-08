package com.zopim.android.sdk.store;

public interface MachineIdStorage extends BaseStorage {
    String getMachineId();

    void setMachineId(String str);
}
