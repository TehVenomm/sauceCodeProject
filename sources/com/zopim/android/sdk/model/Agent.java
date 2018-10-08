package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Agent {
    @JsonProperty("avatar_path$string")
    private String avatarUri;
    @JsonProperty("display_name$string")
    private String displayName;
    @JsonProperty("typing$bool")
    private Boolean isTyping;

    @Nullable
    public String getAvatarUri() {
        return this.avatarUri;
    }

    @Nullable
    public String getDisplayName() {
        return this.displayName;
    }

    @Nullable
    public Boolean isTyping() {
        return this.isTyping;
    }

    public String toString() {
        return this.displayName + ", typing: " + this.isTyping;
    }
}
