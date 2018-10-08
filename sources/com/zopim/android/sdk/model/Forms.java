package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Forms {
    @JsonProperty("offline_form")
    OfflineForm offlineForm;

    @JsonIgnoreProperties(ignoreUnknown = true)
    public static class FormSubmitted {
    }

    @JsonIgnoreProperties(ignoreUnknown = true)
    public static class OfflineForm {
        @JsonProperty("form_submitted")
        FormSubmitted formSubmitted;

        @Nullable
        public FormSubmitted getFormSubmitted() {
            return this.formSubmitted;
        }
    }

    public OfflineForm getOfflineForm() {
        return this.offlineForm;
    }
}
