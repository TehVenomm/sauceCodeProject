package com.zopim.android.sdk.store;

import com.zopim.android.sdk.model.VisitorInfo;

public interface VisitorInfoStorage extends BaseStorage {
    VisitorInfo getVisitorInfo();

    void setVisitorInfo(VisitorInfo visitorInfo);
}
