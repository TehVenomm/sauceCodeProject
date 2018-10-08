package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Account {
    @JsonProperty("status$string")
    private String status;

    public enum Status {
        OFFLINE("offline"),
        ONLINE("online"),
        UNKNOWN("unknown");
        
        final String value;

        private Status(String str) {
            this.value = str;
        }

        public static Status getStatus(String str) {
            for (Status status : values()) {
                if (status.getValue().equals(str)) {
                    return status;
                }
            }
            return UNKNOWN;
        }

        public String getValue() {
            return this.value;
        }
    }

    @Nullable
    public Status getStatus() {
        return Status.getStatus(this.status);
    }
}
