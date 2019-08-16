package com.crashlytics.android.answers;

import com.crashlytics.android.answers.PredefinedEvent;
import java.util.Map;

public abstract class PredefinedEvent<T extends PredefinedEvent> extends AnswersEvent<T> {
    final AnswersAttributes predefinedAttributes = new AnswersAttributes(this.validator);

    /* access modifiers changed from: 0000 */
    public Map<String, Object> getPredefinedAttributes() {
        return this.predefinedAttributes.attributes;
    }

    /* access modifiers changed from: 0000 */
    public abstract String getPredefinedType();

    public String toString() {
        return "{type:\"" + getPredefinedType() + '\"' + ", predefinedAttributes:" + this.predefinedAttributes + ", customAttributes:" + this.customAttributes + "}";
    }
}
