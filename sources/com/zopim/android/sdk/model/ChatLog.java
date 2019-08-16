package com.zopim.android.sdk.model;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import com.appsflyer.share.Constants;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;
import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;

@JsonIgnoreProperties(ignoreUnknown = true)
public class ChatLog implements Comparable<ChatLog> {
    /* access modifiers changed from: private */
    @JsonIgnore
    public static final String LOG_TAG = ChatLog.class.getSimpleName();
    @JsonProperty("attachment")
    private Attachment attachment;
    @JsonProperty("new_comment$string")
    private String comment;
    @JsonProperty("display_name$string")
    private String displayName;
    @JsonProperty("error$string")
    private String error;
    @JsonProperty("failed$bool")
    private Boolean failed;
    @JsonIgnore
    private File file;
    @JsonProperty("file_name$string")
    private String fileName;
    @JsonProperty("file_size$int")
    private Integer fileSize;
    @JsonProperty("file_type$string")
    private String fileType;
    @JsonProperty("msg$string")
    private String message;
    @JsonProperty("nick$string")
    private String nick;
    @JsonIgnore
    private Option[] options;
    @JsonProperty("new_rating$string")
    private String rating;
    @JsonProperty("timestamp$int")
    private Long timestamp;
    @JsonProperty("type$string")
    private String type;
    @JsonProperty("unverified$bool")
    private Boolean unverified;
    @JsonIgnore
    private int uploadProgress;
    @JsonProperty("post_url$string")
    private String uploadUrl;
    @JsonProperty("visitor_queue$int")
    private Integer visitorQueue;

    public enum Error {
        UPLOAD_SIZE_ERROR("size"),
        UPLOAD_FILE_EXTENSION_ERROR("type"),
        UPLOAD_FAILED_ERROR("upload_request_failed"),
        UNKNOWN("unknown");
        
        final String error;

        private Error(String str) {
            this.error = str;
        }

        @NonNull
        public static Error getType(String str) {
            Error[] values;
            for (Error error2 : values()) {
                if (error2.getValue().equals(str)) {
                    return error2;
                }
            }
            return UNKNOWN;
        }

        public String getValue() {
            return this.error;
        }
    }

    public static class Option {
        private String label;
        private boolean selected;

        private Option() {
        }

        public Option(String str) {
            if (str == null) {
                Log.w(ChatLog.LOG_TAG, "Option label not assigned");
                this.label = "";
            }
            this.label = str;
            this.selected = false;
        }

        @NonNull
        public String getLabel() {
            return this.label;
        }

        @NonNull
        public boolean isSelected() {
            return this.selected;
        }

        public void select() {
            this.selected = true;
        }
    }

    public enum Rating {
        GOOD("good"),
        BAD("bad"),
        UNRATED("unrated"),
        UNKNOWN("unknown");
        
        final String rating;

        private Rating(String str) {
            this.rating = str;
        }

        @NonNull
        public static Rating getRating(String str) {
            Rating[] values;
            for (Rating rating2 : values()) {
                if (rating2.getValue().equals(str)) {
                    return rating2;
                }
            }
            return UNKNOWN;
        }

        public String getValue() {
            return this.rating;
        }
    }

    public enum Type {
        CHAT_MSG_AGENT,
        CHAT_MSG_VISITOR,
        CHAT_MSG_TRIGGER,
        CHAT_MSG_SYSTEM,
        MEMBER_LEAVE,
        MEMBER_JOIN,
        SYSTEM_OFFLINE,
        ATTACHMENT_UPLOAD,
        CHAT_RATING,
        UNKNOWN
    }

    /* renamed from: com.zopim.android.sdk.model.ChatLog$a */
    private enum C1244a {
        AGENT_SYSTEM("agent:system"),
        AGENT_TRIGGER("agent:trigger"),
        AGENT_MSG("agent"),
        VISITOR_MSG("visitor"),
        UNKNOWN("unknown");
        

        /* renamed from: f */
        final String f925f;

        private C1244a(String str) {
            this.f925f = str;
        }

        @NonNull
        /* renamed from: a */
        public static C1244a m718a(String str) {
            return (str == null || str.isEmpty()) ? UNKNOWN : "agent:system".equals(str) ? AGENT_SYSTEM : "agent:trigger".equals(str) ? AGENT_TRIGGER : str.contains("agent") ? AGENT_MSG : str.contains("visitor") ? VISITOR_MSG : UNKNOWN;
        }

        /* renamed from: a */
        public String mo20878a() {
            return this.f925f;
        }
    }

    /* renamed from: com.zopim.android.sdk.model.ChatLog$b */
    private enum C1245b {
        CHAT_MSG("chat.msg"),
        MEMBER_JOIN("chat.memberjoin"),
        MEMBER_LEAVE("chat.memberleave"),
        CHAT_EVENT("chat.event"),
        SYSTEM_OFFLINE("system.offline"),
        FILE_UPLOAD("chat.file.upload"),
        CHAT_RATING_REQUEST("chat.request.rating"),
        CHAT_RATING("chat.rating"),
        CHAT_COMMENT("chat.comment"),
        UNKNOWN("unknown");
        

        /* renamed from: k */
        final String f937k;

        private C1245b(String str) {
            this.f937k = str;
        }

        @NonNull
        /* renamed from: a */
        public static C1245b m720a(String str) {
            C1245b[] values;
            for (C1245b bVar : values()) {
                if (bVar.mo20879a().equals(str)) {
                    return bVar;
                }
            }
            return UNKNOWN;
        }

        /* renamed from: a */
        public String mo20879a() {
            return this.f937k;
        }
    }

    public ChatLog() {
        this.uploadProgress = 0;
    }

    public ChatLog(String str, Type type2, String str2) {
        this.uploadProgress = 0;
        this.timestamp = Long.valueOf(System.currentTimeMillis());
        this.displayName = str;
        this.message = str2;
        this.unverified = Boolean.valueOf(true);
        this.failed = Boolean.valueOf(false);
        switch (C1246a.f938a[type2.ordinal()]) {
            case 1:
                this.type = C1245b.CHAT_MSG.mo20879a();
                this.nick = C1244a.VISITOR_MSG.mo20878a();
                return;
            case 2:
                this.type = C1245b.CHAT_MSG.mo20879a();
                this.nick = C1244a.AGENT_MSG.mo20878a();
                return;
            case 3:
                this.type = C1245b.CHAT_EVENT.mo20879a();
                this.nick = C1244a.AGENT_SYSTEM.mo20878a();
                return;
            case 4:
                this.type = C1245b.CHAT_MSG.mo20879a();
                this.nick = C1244a.AGENT_TRIGGER.mo20878a();
                return;
            case 5:
                this.type = C1245b.MEMBER_JOIN.mo20879a();
                return;
            case 6:
                this.type = C1245b.MEMBER_LEAVE.mo20879a();
                return;
            case 7:
                this.type = C1245b.SYSTEM_OFFLINE.mo20879a();
                return;
            default:
                return;
        }
    }

    @JsonProperty("options$string")
    private void setOptions(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(LOG_TAG, "Can not set options with empty string");
            return;
        }
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        this.options = new Option[split.length];
        for (int i = 0; i < split.length; i++) {
            this.options[i] = new Option(split[i]);
        }
    }

    @JsonProperty("rating$string")
    private void setUnrated(String str) {
        if (Rating.getRating(str) != Rating.UNKNOWN) {
            if (getRating() == Rating.UNKNOWN || Rating.getRating(str) == getRating()) {
                this.rating = Rating.UNRATED.getValue();
            }
        }
    }

    public int compareTo(@Nullable ChatLog chatLog) {
        int i = 0;
        if (chatLog == null) {
            Log.w(LOG_TAG, "Passed parameter must not be null. Can not compare. Declaring them as same.");
            return i;
        } else if (this.timestamp == null || chatLog.getTimestamp() == null) {
            Log.w(LOG_TAG, "Error comparing chat logs. Timestamp was null. Declaring them as same.");
            return i;
        } else {
            try {
                return this.timestamp.compareTo(chatLog.getTimestamp());
            } catch (NullPointerException e) {
                Log.w(LOG_TAG, "Error comparing chat logs. Timestamp was not initialized. Declaring them as same.", e);
                return i;
            }
        }
    }

    @Nullable
    public Attachment getAttachment() {
        return this.attachment;
    }

    public String getComment() {
        return this.comment;
    }

    @Nullable
    public String getDisplayName() {
        return this.displayName;
    }

    @NonNull
    public Error getError() {
        return Error.getType(this.error);
    }

    @Nullable
    public File getFile() {
        return this.file;
    }

    @Nullable
    public String getFileName() {
        return this.fileName;
    }

    @Nullable
    public String getMessage() {
        return this.message;
    }

    @Nullable
    public String getNick() {
        return this.nick;
    }

    @NonNull
    public Option[] getOptions() {
        return this.options == null ? new Option[0] : this.options;
    }

    @NonNull
    public int getProgress() {
        return this.uploadProgress;
    }

    public Rating getRating() {
        return Rating.getRating(this.rating);
    }

    @Nullable
    public Long getTimestamp() {
        return this.timestamp;
    }

    @NonNull
    public Type getType() {
        switch (C1246a.f940c[C1245b.m720a(this.type).ordinal()]) {
            case 1:
                switch (C1246a.f939b[C1244a.m718a(this.nick).ordinal()]) {
                    case 1:
                        return Type.CHAT_MSG_SYSTEM;
                    case 2:
                        return Type.CHAT_MSG_TRIGGER;
                    case 3:
                        return Type.CHAT_MSG_AGENT;
                    case 4:
                        return Type.CHAT_MSG_VISITOR;
                    default:
                        return Type.UNKNOWN;
                }
            case 2:
                return Type.CHAT_MSG_SYSTEM;
            case 3:
                return Type.MEMBER_JOIN;
            case 4:
                return Type.MEMBER_LEAVE;
            case 5:
                return Type.SYSTEM_OFFLINE;
            case 6:
                return Type.ATTACHMENT_UPLOAD;
            case 7:
            case 8:
            case 9:
                return Type.CHAT_RATING;
            default:
                return Type.UNKNOWN;
        }
    }

    @Nullable
    public URL getUploadUrl() {
        if (this.uploadUrl == null) {
            return null;
        }
        try {
            return new URL(this.uploadUrl);
        } catch (MalformedURLException e) {
            Log.w(LOG_TAG, "Can not retrieve url. ", e);
            return null;
        }
    }

    @Nullable
    public Integer getVisitorQueue() {
        return this.visitorQueue;
    }

    @Nullable
    public Boolean isFailed() {
        return this.failed;
    }

    @Nullable
    public Boolean isUnverified() {
        return this.unverified;
    }

    public void setComment(String str) {
        this.comment = str;
    }

    public void setError(Error error2) {
        this.error = error2.getValue();
    }

    public void setFailed(boolean z) {
        this.failed = Boolean.valueOf(z);
    }

    public void setFile(File file2) {
        this.file = file2;
    }

    public void setProgress(int i) {
        if (i < 0 || i > 100) {
            Log.i(LOG_TAG, "Supplied progress must be in range 0 - 100. Progress will not be updated.");
        } else if (i < this.uploadProgress) {
            Log.i(LOG_TAG, "Supplied progress must not be less then current progress. Progress will not be updated.");
        } else {
            this.uploadProgress = i;
        }
    }

    public void setRating(Rating rating2) {
        this.rating = rating2.getValue();
    }

    public String toString() {
        return "type:" + this.type + ", name:" + this.displayName + ", msg:" + this.message + ", time:" + this.timestamp + ", url:" + this.uploadUrl;
    }
}
