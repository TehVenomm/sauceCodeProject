package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Department {
    @JsonProperty("name$string")
    String name;
    @JsonProperty("status$string")
    String status;

    @Nullable
    public String getName() {
        return this.name;
    }

    @Nullable
    public String getStatus() {
        return this.status;
    }

    public String toString() {
        return this.name + " (" + this.status + ")";
    }
}
