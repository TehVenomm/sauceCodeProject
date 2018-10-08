package com.zopim.android.sdk.model;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Connection {
    @JsonProperty("status$string")
    private String status;

    public enum Status {
        NO_CONNECTION("noConnection"),
        CLOSED("closed"),
        DISCONNECTED("disconnected"),
        CONNECTING("connecting"),
        CONNECTED("connected"),
        UNKNOWN("unknown");
        
        final String value;

        private Status(String str) {
            this.value = str;
        }

        @NonNull
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

    public Connection(Status status) {
        this.status = status.getValue();
    }

    @Nullable
    public Status getStatus() {
        return Status.getStatus(this.status);
    }

    public String toString() {
        return this.status;
    }
}
