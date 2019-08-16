package com.fasterxml.jackson.databind.introspect;

import com.fasterxml.jackson.annotation.JsonInclude.Value;
import com.fasterxml.jackson.annotation.JsonProperty.Access;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.AnnotationIntrospector.ReferenceProperty;
import com.fasterxml.jackson.databind.PropertyMetadata;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.cfg.MapperConfig;
import com.fasterxml.jackson.databind.util.ClassUtil;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.NoSuchElementException;
import java.util.Set;

public class POJOPropertyBuilder extends BeanPropertyDefinition implements Comparable<POJOPropertyBuilder> {
    protected final AnnotationIntrospector _annotationIntrospector;
    protected final MapperConfig<?> _config;
    protected Linked<AnnotatedParameter> _ctorParameters;
    protected Linked<AnnotatedField> _fields;
    protected final boolean _forSerialization;
    protected Linked<AnnotatedMethod> _getters;
    protected final PropertyName _internalName;
    protected final PropertyName _name;
    protected Linked<AnnotatedMethod> _setters;

    protected static final class Linked<T> {
        public final boolean isMarkedIgnored;
        public final boolean isNameExplicit;
        public final boolean isVisible;
        public final PropertyName name;
        public final Linked<T> next;
        public final T value;

        public Linked(T t, Linked<T> linked, PropertyName propertyName, boolean z, boolean z2, boolean z3) {
            PropertyName propertyName2;
            this.value = t;
            this.next = linked;
            if (propertyName == null || propertyName.isEmpty()) {
                propertyName2 = null;
            } else {
                propertyName2 = propertyName;
            }
            this.name = propertyName2;
            if (z) {
                if (this.name == null) {
                    throw new IllegalArgumentException("Can not pass true for 'explName' if name is null/empty");
                } else if (!propertyName.hasSimpleName()) {
                    z = false;
                }
            }
            this.isNameExplicit = z;
            this.isVisible = z2;
            this.isMarkedIgnored = z3;
        }

        public Linked<T> withoutNext() {
            return this.next == null ? this : new Linked(this.value, null, this.name, this.isNameExplicit, this.isVisible, this.isMarkedIgnored);
        }

        public Linked<T> withValue(T t) {
            if (t == this.value) {
                return this;
            }
            return new Linked(t, this.next, this.name, this.isNameExplicit, this.isVisible, this.isMarkedIgnored);
        }

        public Linked<T> withNext(Linked<T> linked) {
            if (linked == this.next) {
                return this;
            }
            return new Linked(this.value, linked, this.name, this.isNameExplicit, this.isVisible, this.isMarkedIgnored);
        }

        public Linked<T> withoutIgnored() {
            if (!this.isMarkedIgnored) {
                if (this.next != null) {
                    Linked<T> withoutIgnored = this.next.withoutIgnored();
                    if (withoutIgnored != this.next) {
                        return withNext(withoutIgnored);
                    }
                }
                return this;
            } else if (this.next == null) {
                return null;
            } else {
                return this.next.withoutIgnored();
            }
        }

        public Linked<T> withoutNonVisible() {
            Linked<T> withoutNonVisible = this.next == null ? null : this.next.withoutNonVisible();
            return this.isVisible ? withNext(withoutNonVisible) : withoutNonVisible;
        }

        /* access modifiers changed from: protected */
        public Linked<T> append(Linked<T> linked) {
            if (this.next == null) {
                return withNext(linked);
            }
            return withNext(this.next.append(linked));
        }

        public Linked<T> trimByVisibility() {
            if (this.next == null) {
                return this;
            }
            Linked<T> trimByVisibility = this.next.trimByVisibility();
            if (this.name != null) {
                if (trimByVisibility.name == null) {
                    return withNext(null);
                }
                return withNext(trimByVisibility);
            } else if (trimByVisibility.name != null) {
                return trimByVisibility;
            } else {
                if (this.isVisible == trimByVisibility.isVisible) {
                    return withNext(trimByVisibility);
                }
                return this.isVisible ? withNext(null) : trimByVisibility;
            }
        }

        public String toString() {
            String str = this.value.toString() + "[visible=" + this.isVisible + ",ignore=" + this.isMarkedIgnored + ",explicitName=" + this.isNameExplicit + "]";
            if (this.next != null) {
                return str + ", " + this.next.toString();
            }
            return str;
        }
    }

    protected static class MemberIterator<T extends AnnotatedMember> implements Iterator<T> {
        private Linked<T> next;

        public MemberIterator(Linked<T> linked) {
            this.next = linked;
        }

        public boolean hasNext() {
            return this.next != null;
        }

        public T next() {
            if (this.next == null) {
                throw new NoSuchElementException();
            }
            T t = (AnnotatedMember) this.next.value;
            this.next = this.next.next;
            return t;
        }

        public void remove() {
            throw new UnsupportedOperationException();
        }
    }

    private interface WithMember<T> {
        T withMember(AnnotatedMember annotatedMember);
    }

    public POJOPropertyBuilder(MapperConfig<?> mapperConfig, AnnotationIntrospector annotationIntrospector, boolean z, PropertyName propertyName) {
        this(mapperConfig, annotationIntrospector, z, propertyName, propertyName);
    }

    protected POJOPropertyBuilder(MapperConfig<?> mapperConfig, AnnotationIntrospector annotationIntrospector, boolean z, PropertyName propertyName, PropertyName propertyName2) {
        this._config = mapperConfig;
        this._annotationIntrospector = annotationIntrospector;
        this._internalName = propertyName;
        this._name = propertyName2;
        this._forSerialization = z;
    }

    public POJOPropertyBuilder(POJOPropertyBuilder pOJOPropertyBuilder, PropertyName propertyName) {
        this._config = pOJOPropertyBuilder._config;
        this._annotationIntrospector = pOJOPropertyBuilder._annotationIntrospector;
        this._internalName = pOJOPropertyBuilder._internalName;
        this._name = propertyName;
        this._fields = pOJOPropertyBuilder._fields;
        this._ctorParameters = pOJOPropertyBuilder._ctorParameters;
        this._getters = pOJOPropertyBuilder._getters;
        this._setters = pOJOPropertyBuilder._setters;
        this._forSerialization = pOJOPropertyBuilder._forSerialization;
    }

    public POJOPropertyBuilder withName(PropertyName propertyName) {
        return new POJOPropertyBuilder(this, propertyName);
    }

    public POJOPropertyBuilder withSimpleName(String str) {
        PropertyName withSimpleName = this._name.withSimpleName(str);
        return withSimpleName == this._name ? this : new POJOPropertyBuilder(this, withSimpleName);
    }

    public int compareTo(POJOPropertyBuilder pOJOPropertyBuilder) {
        if (this._ctorParameters != null) {
            if (pOJOPropertyBuilder._ctorParameters == null) {
                return -1;
            }
        } else if (pOJOPropertyBuilder._ctorParameters != null) {
            return 1;
        }
        return getName().compareTo(pOJOPropertyBuilder.getName());
    }

    public String getName() {
        if (this._name == null) {
            return null;
        }
        return this._name.getSimpleName();
    }

    public PropertyName getFullName() {
        return this._name;
    }

    public boolean hasName(PropertyName propertyName) {
        return this._name.equals(propertyName);
    }

    public String getInternalName() {
        return this._internalName.getSimpleName();
    }

    public PropertyName getWrapperName() {
        AnnotatedMember primaryMember = getPrimaryMember();
        if (primaryMember == null || this._annotationIntrospector == null) {
            return null;
        }
        return this._annotationIntrospector.findWrapperName(primaryMember);
    }

    public boolean isExplicitlyIncluded() {
        return _anyExplicits(this._fields) || _anyExplicits(this._getters) || _anyExplicits(this._setters) || _anyExplicits(this._ctorParameters);
    }

    public boolean isExplicitlyNamed() {
        return _anyExplicitNames(this._fields) || _anyExplicitNames(this._getters) || _anyExplicitNames(this._setters) || _anyExplicitNames(this._ctorParameters);
    }

    public boolean hasGetter() {
        return this._getters != null;
    }

    public boolean hasSetter() {
        return this._setters != null;
    }

    public boolean hasField() {
        return this._fields != null;
    }

    public boolean hasConstructorParameter() {
        return this._ctorParameters != null;
    }

    public boolean couldDeserialize() {
        return (this._ctorParameters == null && this._setters == null && this._fields == null) ? false : true;
    }

    public boolean couldSerialize() {
        return (this._getters == null && this._fields == null) ? false : true;
    }

    public AnnotatedMethod getGetter() {
        Linked linked;
        Linked linked2 = this._getters;
        if (linked2 == null) {
            return null;
        }
        Linked<T> linked3 = linked2.next;
        if (linked3 == null) {
            return (AnnotatedMethod) linked2.value;
        }
        while (linked3 != null) {
            Class declaringClass = ((AnnotatedMethod) linked2.value).getDeclaringClass();
            Class declaringClass2 = ((AnnotatedMethod) linked3.value).getDeclaringClass();
            if (declaringClass != declaringClass2) {
                if (declaringClass.isAssignableFrom(declaringClass2)) {
                    linked = linked3;
                } else if (declaringClass2.isAssignableFrom(declaringClass)) {
                    linked = linked2;
                }
                linked3 = linked3.next;
                linked2 = linked;
            }
            int _getterPriority = _getterPriority((AnnotatedMethod) linked3.value);
            int _getterPriority2 = _getterPriority((AnnotatedMethod) linked2.value);
            if (_getterPriority != _getterPriority2) {
                if (_getterPriority < _getterPriority2) {
                    linked = linked3;
                } else {
                    linked = linked2;
                }
                linked3 = linked3.next;
                linked2 = linked;
            } else {
                throw new IllegalArgumentException("Conflicting getter definitions for property \"" + getName() + "\": " + ((AnnotatedMethod) linked2.value).getFullName() + " vs " + ((AnnotatedMethod) linked3.value).getFullName());
            }
        }
        this._getters = linked2.withoutNext();
        return (AnnotatedMethod) linked2.value;
    }

    public AnnotatedMethod getSetter() {
        Linked linked;
        Linked linked2 = this._setters;
        if (linked2 == null) {
            return null;
        }
        Linked<T> linked3 = linked2.next;
        if (linked3 == null) {
            return (AnnotatedMethod) linked2.value;
        }
        while (linked3 != null) {
            Class declaringClass = ((AnnotatedMethod) linked2.value).getDeclaringClass();
            Class declaringClass2 = ((AnnotatedMethod) linked3.value).getDeclaringClass();
            if (declaringClass != declaringClass2) {
                if (declaringClass.isAssignableFrom(declaringClass2)) {
                    linked = linked3;
                } else if (declaringClass2.isAssignableFrom(declaringClass)) {
                    linked = linked2;
                }
                linked3 = linked3.next;
                linked2 = linked;
            }
            AnnotatedMethod annotatedMethod = (AnnotatedMethod) linked3.value;
            AnnotatedMethod annotatedMethod2 = (AnnotatedMethod) linked2.value;
            int _setterPriority = _setterPriority(annotatedMethod);
            int _setterPriority2 = _setterPriority(annotatedMethod2);
            if (_setterPriority == _setterPriority2) {
                if (this._annotationIntrospector != null) {
                    AnnotatedMethod resolveSetterConflict = this._annotationIntrospector.resolveSetterConflict(this._config, annotatedMethod2, annotatedMethod);
                    if (resolveSetterConflict == annotatedMethod2) {
                        linked = linked2;
                    } else if (resolveSetterConflict == annotatedMethod) {
                        linked = linked3;
                    }
                }
                throw new IllegalArgumentException("Conflicting setter definitions for property \"" + getName() + "\": " + ((AnnotatedMethod) linked2.value).getFullName() + " vs " + ((AnnotatedMethod) linked3.value).getFullName());
            } else if (_setterPriority < _setterPriority2) {
                linked = linked3;
            } else {
                linked = linked2;
            }
            linked3 = linked3.next;
            linked2 = linked;
        }
        this._setters = linked2.withoutNext();
        return (AnnotatedMethod) linked2.value;
    }

    public AnnotatedField getField() {
        if (this._fields == null) {
            return null;
        }
        AnnotatedField annotatedField = (AnnotatedField) this._fields.value;
        Linked<T> linked = this._fields.next;
        AnnotatedField annotatedField2 = annotatedField;
        while (linked != null) {
            AnnotatedField annotatedField3 = (AnnotatedField) linked.value;
            Class declaringClass = annotatedField2.getDeclaringClass();
            Class declaringClass2 = annotatedField3.getDeclaringClass();
            if (declaringClass != declaringClass2) {
                if (!declaringClass.isAssignableFrom(declaringClass2)) {
                    if (declaringClass2.isAssignableFrom(declaringClass)) {
                        annotatedField3 = annotatedField2;
                    }
                }
                linked = linked.next;
                annotatedField2 = annotatedField3;
            }
            throw new IllegalArgumentException("Multiple fields representing property \"" + getName() + "\": " + annotatedField2.getFullName() + " vs " + annotatedField3.getFullName());
        }
        return annotatedField2;
    }

    public AnnotatedParameter getConstructorParameter() {
        if (this._ctorParameters == null) {
            return null;
        }
        Linked linked = this._ctorParameters;
        while (true) {
            Linked linked2 = linked;
            if (((AnnotatedParameter) linked2.value).getOwner() instanceof AnnotatedConstructor) {
                return (AnnotatedParameter) linked2.value;
            }
            linked = linked2.next;
            if (linked == null) {
                return (AnnotatedParameter) this._ctorParameters.value;
            }
        }
    }

    public Iterator<AnnotatedParameter> getConstructorParameters() {
        if (this._ctorParameters == null) {
            return ClassUtil.emptyIterator();
        }
        return new MemberIterator(this._ctorParameters);
    }

    public AnnotatedMember getAccessor() {
        AnnotatedMethod getter = getGetter();
        if (getter == null) {
            return getField();
        }
        return getter;
    }

    public AnnotatedMember getMutator() {
        AnnotatedParameter constructorParameter = getConstructorParameter();
        if (constructorParameter != null) {
            return constructorParameter;
        }
        AnnotatedMethod setter = getSetter();
        if (setter == null) {
            return getField();
        }
        return setter;
    }

    public AnnotatedMember getNonConstructorMutator() {
        AnnotatedMethod setter = getSetter();
        if (setter == null) {
            return getField();
        }
        return setter;
    }

    public AnnotatedMember getPrimaryMember() {
        if (this._forSerialization) {
            return getAccessor();
        }
        return getMutator();
    }

    /* access modifiers changed from: protected */
    public int _getterPriority(AnnotatedMethod annotatedMethod) {
        String name = annotatedMethod.getName();
        if (name.startsWith("get") && name.length() > 3) {
            return 1;
        }
        if (!name.startsWith("is") || name.length() <= 2) {
            return 3;
        }
        return 2;
    }

    /* access modifiers changed from: protected */
    public int _setterPriority(AnnotatedMethod annotatedMethod) {
        String name = annotatedMethod.getName();
        if (!name.startsWith("set") || name.length() <= 3) {
            return 2;
        }
        return 1;
    }

    public Class<?>[] findViews() {
        return (Class[]) fromMemberAnnotations(new WithMember<Class<?>[]>() {
            public Class<?>[] withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findViews(annotatedMember);
            }
        });
    }

    public ReferenceProperty findReferenceType() {
        return (ReferenceProperty) fromMemberAnnotations(new WithMember<ReferenceProperty>() {
            public ReferenceProperty withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findReferenceType(annotatedMember);
            }
        });
    }

    public boolean isTypeId() {
        Boolean bool = (Boolean) fromMemberAnnotations(new WithMember<Boolean>() {
            public Boolean withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.isTypeId(annotatedMember);
            }
        });
        return bool != null && bool.booleanValue();
    }

    public PropertyMetadata getMetadata() {
        Boolean _findRequired = _findRequired();
        String _findDescription = _findDescription();
        Integer _findIndex = _findIndex();
        String _findDefaultValue = _findDefaultValue();
        if (_findRequired == null && _findIndex == null && _findDefaultValue == null) {
            return _findDescription == null ? PropertyMetadata.STD_REQUIRED_OR_OPTIONAL : PropertyMetadata.STD_REQUIRED_OR_OPTIONAL.withDescription(_findDescription);
        }
        return PropertyMetadata.construct(_findRequired.booleanValue(), _findDescription, _findIndex, _findDefaultValue);
    }

    /* access modifiers changed from: protected */
    public Boolean _findRequired() {
        return (Boolean) fromMemberAnnotations(new WithMember<Boolean>() {
            public Boolean withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.hasRequiredMarker(annotatedMember);
            }
        });
    }

    /* access modifiers changed from: protected */
    public String _findDescription() {
        return (String) fromMemberAnnotations(new WithMember<String>() {
            public String withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findPropertyDescription(annotatedMember);
            }
        });
    }

    /* access modifiers changed from: protected */
    public Integer _findIndex() {
        return (Integer) fromMemberAnnotations(new WithMember<Integer>() {
            public Integer withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findPropertyIndex(annotatedMember);
            }
        });
    }

    /* access modifiers changed from: protected */
    public String _findDefaultValue() {
        return (String) fromMemberAnnotations(new WithMember<String>() {
            public String withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findPropertyDefaultValue(annotatedMember);
            }
        });
    }

    public ObjectIdInfo findObjectIdInfo() {
        return (ObjectIdInfo) fromMemberAnnotations(new WithMember<ObjectIdInfo>() {
            public ObjectIdInfo withMember(AnnotatedMember annotatedMember) {
                ObjectIdInfo findObjectIdInfo = POJOPropertyBuilder.this._annotationIntrospector.findObjectIdInfo(annotatedMember);
                if (findObjectIdInfo != null) {
                    return POJOPropertyBuilder.this._annotationIntrospector.findObjectReferenceInfo(annotatedMember, findObjectIdInfo);
                }
                return findObjectIdInfo;
            }
        });
    }

    public Value findInclusion() {
        if (this._annotationIntrospector != null) {
            Value findPropertyInclusion = this._annotationIntrospector.findPropertyInclusion(getAccessor());
            if (findPropertyInclusion != null) {
                return findPropertyInclusion;
            }
        }
        return Value.empty();
    }

    public Access findAccess() {
        return (Access) fromMemberAnnotationsExcept(new WithMember<Access>() {
            public Access withMember(AnnotatedMember annotatedMember) {
                return POJOPropertyBuilder.this._annotationIntrospector.findPropertyAccess(annotatedMember);
            }
        }, Access.AUTO);
    }

    public void addField(AnnotatedField annotatedField, PropertyName propertyName, boolean z, boolean z2, boolean z3) {
        this._fields = new Linked<>(annotatedField, this._fields, propertyName, z, z2, z3);
    }

    public void addCtor(AnnotatedParameter annotatedParameter, PropertyName propertyName, boolean z, boolean z2, boolean z3) {
        this._ctorParameters = new Linked<>(annotatedParameter, this._ctorParameters, propertyName, z, z2, z3);
    }

    public void addGetter(AnnotatedMethod annotatedMethod, PropertyName propertyName, boolean z, boolean z2, boolean z3) {
        this._getters = new Linked<>(annotatedMethod, this._getters, propertyName, z, z2, z3);
    }

    public void addSetter(AnnotatedMethod annotatedMethod, PropertyName propertyName, boolean z, boolean z2, boolean z3) {
        this._setters = new Linked<>(annotatedMethod, this._setters, propertyName, z, z2, z3);
    }

    public void addAll(POJOPropertyBuilder pOJOPropertyBuilder) {
        this._fields = merge(this._fields, pOJOPropertyBuilder._fields);
        this._ctorParameters = merge(this._ctorParameters, pOJOPropertyBuilder._ctorParameters);
        this._getters = merge(this._getters, pOJOPropertyBuilder._getters);
        this._setters = merge(this._setters, pOJOPropertyBuilder._setters);
    }

    private static <T> Linked<T> merge(Linked<T> linked, Linked<T> linked2) {
        if (linked == null) {
            return linked2;
        }
        if (linked2 == null) {
            return linked;
        }
        return linked.append(linked2);
    }

    public void removeIgnored() {
        this._fields = _removeIgnored(this._fields);
        this._getters = _removeIgnored(this._getters);
        this._setters = _removeIgnored(this._setters);
        this._ctorParameters = _removeIgnored(this._ctorParameters);
    }

    public void removeNonVisible(boolean z) {
        Access findAccess = findAccess();
        if (findAccess == null) {
            findAccess = Access.AUTO;
        }
        switch (findAccess) {
            case READ_ONLY:
                this._setters = null;
                this._ctorParameters = null;
                if (!this._forSerialization) {
                    this._fields = null;
                    return;
                }
                return;
            case READ_WRITE:
                return;
            case WRITE_ONLY:
                this._getters = null;
                if (this._forSerialization) {
                    this._fields = null;
                    return;
                }
                return;
            default:
                this._getters = _removeNonVisible(this._getters);
                this._ctorParameters = _removeNonVisible(this._ctorParameters);
                if (!z || this._getters == null) {
                    this._fields = _removeNonVisible(this._fields);
                    this._setters = _removeNonVisible(this._setters);
                    return;
                }
                return;
        }
    }

    public void removeConstructors() {
        this._ctorParameters = null;
    }

    public void trimByVisibility() {
        this._fields = _trimByVisibility(this._fields);
        this._getters = _trimByVisibility(this._getters);
        this._setters = _trimByVisibility(this._setters);
        this._ctorParameters = _trimByVisibility(this._ctorParameters);
    }

    public void mergeAnnotations(boolean z) {
        if (z) {
            if (this._getters != null) {
                this._getters = _applyAnnotations(this._getters, _mergeAnnotations(0, this._getters, this._fields, this._ctorParameters, this._setters));
            } else if (this._fields != null) {
                this._fields = _applyAnnotations(this._fields, _mergeAnnotations(0, this._fields, this._ctorParameters, this._setters));
            }
        } else if (this._ctorParameters != null) {
            this._ctorParameters = _applyAnnotations(this._ctorParameters, _mergeAnnotations(0, this._ctorParameters, this._setters, this._fields, this._getters));
        } else if (this._setters != null) {
            this._setters = _applyAnnotations(this._setters, _mergeAnnotations(0, this._setters, this._fields, this._getters));
        } else if (this._fields != null) {
            this._fields = _applyAnnotations(this._fields, _mergeAnnotations(0, this._fields, this._getters));
        }
    }

    private AnnotationMap _mergeAnnotations(int i, Linked<? extends AnnotatedMember>... linkedArr) {
        AnnotationMap _getAllAnnotations = _getAllAnnotations(linkedArr[i]);
        do {
            i++;
            if (i >= linkedArr.length) {
                return _getAllAnnotations;
            }
        } while (linkedArr[i] == null);
        return AnnotationMap.merge(_getAllAnnotations, _mergeAnnotations(i, linkedArr));
    }

    private <T extends AnnotatedMember> AnnotationMap _getAllAnnotations(Linked<T> linked) {
        AnnotationMap allAnnotations = ((AnnotatedMember) linked.value).getAllAnnotations();
        if (linked.next != null) {
            return AnnotationMap.merge(allAnnotations, _getAllAnnotations(linked.next));
        }
        return allAnnotations;
    }

    private <T extends AnnotatedMember> Linked<T> _applyAnnotations(Linked<T> linked, AnnotationMap annotationMap) {
        AnnotatedMember annotatedMember = (AnnotatedMember) ((AnnotatedMember) linked.value).withAnnotations(annotationMap);
        if (linked.next != null) {
            linked = linked.withNext(_applyAnnotations(linked.next, annotationMap));
        }
        return linked.withValue(annotatedMember);
    }

    private <T> Linked<T> _removeIgnored(Linked<T> linked) {
        return linked == null ? linked : linked.withoutIgnored();
    }

    private <T> Linked<T> _removeNonVisible(Linked<T> linked) {
        return linked == null ? linked : linked.withoutNonVisible();
    }

    private <T> Linked<T> _trimByVisibility(Linked<T> linked) {
        return linked == null ? linked : linked.trimByVisibility();
    }

    private <T> boolean _anyExplicits(Linked<T> linked) {
        while (linked != null) {
            if (linked.name != null && linked.name.hasSimpleName()) {
                return true;
            }
            linked = linked.next;
        }
        return false;
    }

    private <T> boolean _anyExplicitNames(Linked<T> linked) {
        while (linked != null) {
            if (linked.name != null && linked.isNameExplicit) {
                return true;
            }
            linked = linked.next;
        }
        return false;
    }

    public boolean anyVisible() {
        return _anyVisible(this._fields) || _anyVisible(this._getters) || _anyVisible(this._setters) || _anyVisible(this._ctorParameters);
    }

    private <T> boolean _anyVisible(Linked<T> linked) {
        while (linked != null) {
            if (linked.isVisible) {
                return true;
            }
            linked = linked.next;
        }
        return false;
    }

    public boolean anyIgnorals() {
        return _anyIgnorals(this._fields) || _anyIgnorals(this._getters) || _anyIgnorals(this._setters) || _anyIgnorals(this._ctorParameters);
    }

    private <T> boolean _anyIgnorals(Linked<T> linked) {
        while (linked != null) {
            if (linked.isMarkedIgnored) {
                return true;
            }
            linked = linked.next;
        }
        return false;
    }

    public Set<PropertyName> findExplicitNames() {
        Set<PropertyName> _findExplicitNames = _findExplicitNames(this._ctorParameters, _findExplicitNames(this._setters, _findExplicitNames(this._getters, _findExplicitNames(this._fields, null))));
        if (_findExplicitNames == null) {
            return Collections.emptySet();
        }
        return _findExplicitNames;
    }

    public Collection<POJOPropertyBuilder> explode(Collection<PropertyName> collection) {
        HashMap hashMap = new HashMap();
        _explode(collection, hashMap, this._fields);
        _explode(collection, hashMap, this._getters);
        _explode(collection, hashMap, this._setters);
        _explode(collection, hashMap, this._ctorParameters);
        return hashMap.values();
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<?>, code=com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked, for r10v0, types: [com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<?>, com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void _explode(java.util.Collection<com.fasterxml.jackson.databind.PropertyName> r8, java.util.Map<com.fasterxml.jackson.databind.PropertyName, com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder> r9, com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder.Linked r10) {
        /*
            r7 = this;
            r6 = r10
        L_0x0001:
            if (r6 == 0) goto L_0x00a7
            com.fasterxml.jackson.databind.PropertyName r5 = r6.name
            boolean r0 = r6.isNameExplicit
            if (r0 == 0) goto L_0x000b
            if (r5 != 0) goto L_0x0042
        L_0x000b:
            boolean r0 = r6.isVisible
            if (r0 != 0) goto L_0x0013
        L_0x000f:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<T> r0 = r6.next
            r6 = r0
            goto L_0x0001
        L_0x0013:
            java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Conflicting/ambiguous property name definitions (implicit name '"
            java.lang.StringBuilder r1 = r1.append(r2)
            com.fasterxml.jackson.databind.PropertyName r2 = r7._name
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = "'): found multiple explicit names: "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r8)
            java.lang.String r2 = ", but also implicit accessor: "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r6)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0042:
            java.lang.Object r0 = r9.get(r5)
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder r0 = (com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder) r0
            if (r0 != 0) goto L_0x005a
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder r0 = new com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder
            com.fasterxml.jackson.databind.cfg.MapperConfig<?> r1 = r7._config
            com.fasterxml.jackson.databind.AnnotationIntrospector r2 = r7._annotationIntrospector
            boolean r3 = r7._forSerialization
            com.fasterxml.jackson.databind.PropertyName r4 = r7._internalName
            r0.<init>(r1, r2, r3, r4, r5)
            r9.put(r5, r0)
        L_0x005a:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedField> r1 = r7._fields
            if (r10 != r1) goto L_0x0067
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedField> r1 = r0._fields
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked r1 = r6.withNext(r1)
            r0._fields = r1
            goto L_0x000f
        L_0x0067:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedMethod> r1 = r7._getters
            if (r10 != r1) goto L_0x0074
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedMethod> r1 = r0._getters
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked r1 = r6.withNext(r1)
            r0._getters = r1
            goto L_0x000f
        L_0x0074:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedMethod> r1 = r7._setters
            if (r10 != r1) goto L_0x0081
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedMethod> r1 = r0._setters
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked r1 = r6.withNext(r1)
            r0._setters = r1
            goto L_0x000f
        L_0x0081:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedParameter> r1 = r7._ctorParameters
            if (r10 != r1) goto L_0x008e
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<com.fasterxml.jackson.databind.introspect.AnnotatedParameter> r1 = r0._ctorParameters
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked r1 = r6.withNext(r1)
            r0._ctorParameters = r1
            goto L_0x000f
        L_0x008e:
            java.lang.IllegalStateException r0 = new java.lang.IllegalStateException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Internal error: mismatched accessors, property: "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r7)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x00a7:
            return
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder._explode(java.util.Collection, java.util.Map, com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked):void");
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<? extends com.fasterxml.jackson.databind.introspect.AnnotatedMember>, code=com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked, for r3v0, types: [com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<? extends com.fasterxml.jackson.databind.introspect.AnnotatedMember>, com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private java.util.Set<com.fasterxml.jackson.databind.PropertyName> _findExplicitNames(com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder.Linked r3, java.util.Set<com.fasterxml.jackson.databind.PropertyName> r4) {
        /*
            r2 = this;
            r0 = r4
        L_0x0001:
            if (r3 == 0) goto L_0x001b
            boolean r1 = r3.isNameExplicit
            if (r1 == 0) goto L_0x000b
            com.fasterxml.jackson.databind.PropertyName r1 = r3.name
            if (r1 != 0) goto L_0x000e
        L_0x000b:
            com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked<T> r3 = r3.next
            goto L_0x0001
        L_0x000e:
            if (r0 != 0) goto L_0x0015
            java.util.HashSet r0 = new java.util.HashSet
            r0.<init>()
        L_0x0015:
            com.fasterxml.jackson.databind.PropertyName r1 = r3.name
            r0.add(r1)
            goto L_0x000b
        L_0x001b:
            return r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder._findExplicitNames(com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder$Linked, java.util.Set):java.util.Set");
    }

    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("[Property '").append(this._name).append("'; ctors: ").append(this._ctorParameters).append(", field(s): ").append(this._fields).append(", getter(s): ").append(this._getters).append(", setter(s): ").append(this._setters);
        sb.append("]");
        return sb.toString();
    }

    /* access modifiers changed from: protected */
    public <T> T fromMemberAnnotations(WithMember<T> withMember) {
        T t = null;
        if (this._annotationIntrospector == null) {
            return null;
        }
        if (!this._forSerialization) {
            if (this._ctorParameters != null) {
                t = withMember.withMember((AnnotatedMember) this._ctorParameters.value);
            }
            if (t == null && this._setters != null) {
                t = withMember.withMember((AnnotatedMember) this._setters.value);
            }
        } else if (this._getters != null) {
            t = withMember.withMember((AnnotatedMember) this._getters.value);
        }
        if (t != null || this._fields == null) {
            return t;
        }
        return withMember.withMember((AnnotatedMember) this._fields.value);
    }

    /* access modifiers changed from: protected */
    public <T> T fromMemberAnnotationsExcept(WithMember<T> withMember, T t) {
        if (this._annotationIntrospector == null) {
            return null;
        }
        if (this._forSerialization) {
            if (this._getters != null) {
                T withMember2 = withMember.withMember((AnnotatedMember) this._getters.value);
                if (!(withMember2 == null || withMember2 == t)) {
                    return withMember2;
                }
            }
            if (this._fields != null) {
                T withMember3 = withMember.withMember((AnnotatedMember) this._fields.value);
                if (!(withMember3 == null || withMember3 == t)) {
                    return withMember3;
                }
            }
            if (this._ctorParameters != null) {
                T withMember4 = withMember.withMember((AnnotatedMember) this._ctorParameters.value);
                if (!(withMember4 == null || withMember4 == t)) {
                    return withMember4;
                }
            }
            if (this._setters != null) {
                T withMember5 = withMember.withMember((AnnotatedMember) this._setters.value);
                if (!(withMember5 == null || withMember5 == t)) {
                    return withMember5;
                }
            }
            return null;
        }
        if (this._ctorParameters != null) {
            T withMember6 = withMember.withMember((AnnotatedMember) this._ctorParameters.value);
            if (!(withMember6 == null || withMember6 == t)) {
                return withMember6;
            }
        }
        if (this._setters != null) {
            T withMember7 = withMember.withMember((AnnotatedMember) this._setters.value);
            if (!(withMember7 == null || withMember7 == t)) {
                return withMember7;
            }
        }
        if (this._fields != null) {
            T withMember8 = withMember.withMember((AnnotatedMember) this._fields.value);
            if (!(withMember8 == null || withMember8 == t)) {
                return withMember8;
            }
        }
        if (this._getters != null) {
            T withMember9 = withMember.withMember((AnnotatedMember) this._getters.value);
            if (!(withMember9 == null || withMember9 == t)) {
                return withMember9;
            }
        }
        return null;
    }
}
