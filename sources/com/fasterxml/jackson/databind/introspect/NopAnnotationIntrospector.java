package com.fasterxml.jackson.databind.introspect;

import com.fasterxml.jackson.core.Version;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.cfg.PackageVersion;
import java.io.Serializable;

public abstract class NopAnnotationIntrospector extends AnnotationIntrospector implements Serializable {
    public static final NopAnnotationIntrospector instance = new C05591();
    private static final long serialVersionUID = 1;

    /* renamed from: com.fasterxml.jackson.databind.introspect.NopAnnotationIntrospector$1 */
    static class C05591 extends NopAnnotationIntrospector {
        private static final long serialVersionUID = 1;

        C05591() {
        }

        public Version version() {
            return PackageVersion.VERSION;
        }
    }

    public Version version() {
        return Version.unknownVersion();
    }
}
