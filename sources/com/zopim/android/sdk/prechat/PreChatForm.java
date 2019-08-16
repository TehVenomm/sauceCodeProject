package com.zopim.android.sdk.prechat;

import java.io.Serializable;

public class PreChatForm implements Serializable {
    private static final long serialVersionUID = 9006657233172922772L;
    private final Field department;
    private final Field email;
    private final Field message;
    private final Field name;
    private final Field phoneNumber;

    public static class Builder {
        /* access modifiers changed from: private */
        public Field department = Field.NOT_REQUIRED;
        /* access modifiers changed from: private */
        public Field email = Field.NOT_REQUIRED;
        /* access modifiers changed from: private */
        public Field message = Field.NOT_REQUIRED;
        /* access modifiers changed from: private */
        public Field name = Field.NOT_REQUIRED;
        /* access modifiers changed from: private */
        public Field phoneNumber = Field.NOT_REQUIRED;

        public PreChatForm build() {
            return new PreChatForm(this);
        }

        public Builder department(Field field) {
            this.department = field;
            return this;
        }

        public Builder email(Field field) {
            this.email = field;
            return this;
        }

        public Builder message(Field field) {
            this.message = field;
            return this;
        }

        public Builder name(Field field) {
            this.name = field;
            return this;
        }

        public Builder phoneNumber(Field field) {
            this.phoneNumber = field;
            return this;
        }
    }

    public enum Field {
        NOT_REQUIRED,
        OPTIONAL,
        REQUIRED,
        OPTIONAL_EDITABLE,
        REQUIRED_EDITABLE
    }

    private PreChatForm() {
        throw new UnsupportedOperationException("This constructor is not supported, use parametrized constructor");
    }

    private PreChatForm(Builder builder) {
        this.name = builder.name;
        this.email = builder.email;
        this.phoneNumber = builder.phoneNumber;
        this.department = builder.department;
        this.message = builder.message;
    }

    public Field getDepartment() {
        return this.department;
    }

    public Field getEmail() {
        return this.email;
    }

    public Field getMessage() {
        return this.message;
    }

    public Field getName() {
        return this.name;
    }

    public Field getPhoneNumber() {
        return this.phoneNumber;
    }
}
