package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Profile {
    @JsonProperty("department_id$int")
    private String departmentId;
    @JsonProperty("display_name$string")
    private String displayName;
    @JsonProperty("email$string")
    private String email;
    @JsonProperty("mid$string")
    private String machineId;
    @JsonProperty("phone$string")
    private String phoneNumber;

    @Nullable
    public String getDepartmentId() {
        return this.departmentId;
    }

    @Nullable
    public String getDisplayName() {
        return this.displayName;
    }

    @Nullable
    public String getEmail() {
        return this.email;
    }

    @Nullable
    public String getMachineId() {
        return this.machineId;
    }

    @Nullable
    public String getPhoneNumber() {
        return this.phoneNumber;
    }

    public String toString() {
        return " mid:" + this.machineId + " email:" + this.email + " name:" + this.displayName + " depId:" + this.departmentId;
    }
}
