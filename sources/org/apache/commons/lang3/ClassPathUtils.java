package org.apache.commons.lang3;

import com.appsflyer.share.Constants;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public class ClassPathUtils {
    public static String toFullyQualifiedName(Class<?> cls, String str) {
        Validate.notNull(cls, "Parameter '%s' must not be null!", "context");
        Validate.notNull(str, "Parameter '%s' must not be null!", "resourceName");
        return toFullyQualifiedName(cls.getPackage(), str);
    }

    public static String toFullyQualifiedName(Package packageR, String str) {
        Validate.notNull(packageR, "Parameter '%s' must not be null!", "context");
        Validate.notNull(str, "Parameter '%s' must not be null!", "resourceName");
        StringBuilder sb = new StringBuilder();
        sb.append(packageR.getName());
        sb.append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        sb.append(str);
        return sb.toString();
    }

    public static String toFullyQualifiedPath(Class<?> cls, String str) {
        Validate.notNull(cls, "Parameter '%s' must not be null!", "context");
        Validate.notNull(str, "Parameter '%s' must not be null!", "resourceName");
        return toFullyQualifiedPath(cls.getPackage(), str);
    }

    public static String toFullyQualifiedPath(Package packageR, String str) {
        Validate.notNull(packageR, "Parameter '%s' must not be null!", "context");
        Validate.notNull(str, "Parameter '%s' must not be null!", "resourceName");
        StringBuilder sb = new StringBuilder();
        sb.append(packageR.getName().replace(ClassUtils.PACKAGE_SEPARATOR_CHAR, '/'));
        sb.append(Constants.URL_PATH_DELIMITER);
        sb.append(str);
        return sb.toString();
    }
}
