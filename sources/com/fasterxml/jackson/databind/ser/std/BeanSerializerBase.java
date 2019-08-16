package com.fasterxml.jackson.databind.ser.std;

import com.fasterxml.jackson.annotation.JsonFormat.Shape;
import com.fasterxml.jackson.annotation.JsonFormat.Value;
import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanProperty;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonMappingException.Reference;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitable;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonObjectFormatVisitor;
import com.fasterxml.jackson.databind.jsonschema.JsonSerializableSchema;
import com.fasterxml.jackson.databind.jsonschema.SchemaAware;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.node.ObjectNode;
import com.fasterxml.jackson.databind.ser.AnyGetterWriter;
import com.fasterxml.jackson.databind.ser.BeanPropertyWriter;
import com.fasterxml.jackson.databind.ser.BeanSerializerBuilder;
import com.fasterxml.jackson.databind.ser.ContainerSerializer;
import com.fasterxml.jackson.databind.ser.ContextualSerializer;
import com.fasterxml.jackson.databind.ser.PropertyFilter;
import com.fasterxml.jackson.databind.ser.PropertyWriter;
import com.fasterxml.jackson.databind.ser.ResolvableSerializer;
import com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter;
import com.fasterxml.jackson.databind.ser.impl.WritableObjectId;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import com.fasterxml.jackson.databind.util.Converter;
import com.fasterxml.jackson.databind.util.NameTransformer;
import java.io.Closeable;
import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Iterator;

public abstract class BeanSerializerBase extends StdSerializer<Object> implements ContextualSerializer, ResolvableSerializer, JsonFormatVisitable, SchemaAware {
    protected static final PropertyName NAME_FOR_OBJECT_REF = new PropertyName("#object-ref");
    protected static final BeanPropertyWriter[] NO_PROPS = new BeanPropertyWriter[0];
    protected final AnyGetterWriter _anyGetterWriter;
    protected final BeanPropertyWriter[] _filteredProps;
    protected final ObjectIdWriter _objectIdWriter;
    protected final Object _propertyFilterId;
    protected final BeanPropertyWriter[] _props;
    protected final Shape _serializationShape;
    protected final AnnotatedMember _typeId;

    /* access modifiers changed from: protected */
    public abstract BeanSerializerBase asArraySerializer();

    public abstract void serialize(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException;

    public abstract BeanSerializerBase withFilterId(Object obj);

    /* access modifiers changed from: protected */
    public abstract BeanSerializerBase withIgnorals(String[] strArr);

    public abstract BeanSerializerBase withObjectIdWriter(ObjectIdWriter objectIdWriter);

    protected BeanSerializerBase(JavaType javaType, BeanSerializerBuilder beanSerializerBuilder, BeanPropertyWriter[] beanPropertyWriterArr, BeanPropertyWriter[] beanPropertyWriterArr2) {
        Shape shape = null;
        super(javaType);
        this._props = beanPropertyWriterArr;
        this._filteredProps = beanPropertyWriterArr2;
        if (beanSerializerBuilder == null) {
            this._typeId = null;
            this._anyGetterWriter = null;
            this._propertyFilterId = null;
            this._objectIdWriter = null;
            this._serializationShape = null;
            return;
        }
        this._typeId = beanSerializerBuilder.getTypeId();
        this._anyGetterWriter = beanSerializerBuilder.getAnyGetter();
        this._propertyFilterId = beanSerializerBuilder.getFilterId();
        this._objectIdWriter = beanSerializerBuilder.getObjectIdWriter();
        Value findExpectedFormat = beanSerializerBuilder.getBeanDescription().findExpectedFormat(null);
        if (findExpectedFormat != null) {
            shape = findExpectedFormat.getShape();
        }
        this._serializationShape = shape;
    }

    public BeanSerializerBase(BeanSerializerBase beanSerializerBase, BeanPropertyWriter[] beanPropertyWriterArr, BeanPropertyWriter[] beanPropertyWriterArr2) {
        super(beanSerializerBase._handledType);
        this._props = beanPropertyWriterArr;
        this._filteredProps = beanPropertyWriterArr2;
        this._typeId = beanSerializerBase._typeId;
        this._anyGetterWriter = beanSerializerBase._anyGetterWriter;
        this._objectIdWriter = beanSerializerBase._objectIdWriter;
        this._propertyFilterId = beanSerializerBase._propertyFilterId;
        this._serializationShape = beanSerializerBase._serializationShape;
    }

    protected BeanSerializerBase(BeanSerializerBase beanSerializerBase, ObjectIdWriter objectIdWriter) {
        this(beanSerializerBase, objectIdWriter, beanSerializerBase._propertyFilterId);
    }

    protected BeanSerializerBase(BeanSerializerBase beanSerializerBase, ObjectIdWriter objectIdWriter, Object obj) {
        super(beanSerializerBase._handledType);
        this._props = beanSerializerBase._props;
        this._filteredProps = beanSerializerBase._filteredProps;
        this._typeId = beanSerializerBase._typeId;
        this._anyGetterWriter = beanSerializerBase._anyGetterWriter;
        this._objectIdWriter = objectIdWriter;
        this._propertyFilterId = obj;
        this._serializationShape = beanSerializerBase._serializationShape;
    }

    protected BeanSerializerBase(BeanSerializerBase beanSerializerBase, String[] strArr) {
        BeanPropertyWriter[] beanPropertyWriterArr = null;
        super(beanSerializerBase._handledType);
        HashSet arrayToSet = ArrayBuilders.arrayToSet(strArr);
        BeanPropertyWriter[] beanPropertyWriterArr2 = beanSerializerBase._props;
        BeanPropertyWriter[] beanPropertyWriterArr3 = beanSerializerBase._filteredProps;
        int length = beanPropertyWriterArr2.length;
        ArrayList arrayList = new ArrayList(length);
        ArrayList arrayList2 = beanPropertyWriterArr3 == null ? null : new ArrayList(length);
        for (int i = 0; i < length; i++) {
            BeanPropertyWriter beanPropertyWriter = beanPropertyWriterArr2[i];
            if (!arrayToSet.contains(beanPropertyWriter.getName())) {
                arrayList.add(beanPropertyWriter);
                if (beanPropertyWriterArr3 != null) {
                    arrayList2.add(beanPropertyWriterArr3[i]);
                }
            }
        }
        this._props = (BeanPropertyWriter[]) arrayList.toArray(new BeanPropertyWriter[arrayList.size()]);
        if (arrayList2 != null) {
            beanPropertyWriterArr = (BeanPropertyWriter[]) arrayList2.toArray(new BeanPropertyWriter[arrayList2.size()]);
        }
        this._filteredProps = beanPropertyWriterArr;
        this._typeId = beanSerializerBase._typeId;
        this._anyGetterWriter = beanSerializerBase._anyGetterWriter;
        this._objectIdWriter = beanSerializerBase._objectIdWriter;
        this._propertyFilterId = beanSerializerBase._propertyFilterId;
        this._serializationShape = beanSerializerBase._serializationShape;
    }

    protected BeanSerializerBase(BeanSerializerBase beanSerializerBase) {
        this(beanSerializerBase, beanSerializerBase._props, beanSerializerBase._filteredProps);
    }

    protected BeanSerializerBase(BeanSerializerBase beanSerializerBase, NameTransformer nameTransformer) {
        this(beanSerializerBase, rename(beanSerializerBase._props, nameTransformer), rename(beanSerializerBase._filteredProps, nameTransformer));
    }

    private static final BeanPropertyWriter[] rename(BeanPropertyWriter[] beanPropertyWriterArr, NameTransformer nameTransformer) {
        if (beanPropertyWriterArr == null || beanPropertyWriterArr.length == 0 || nameTransformer == null || nameTransformer == NameTransformer.NOP) {
            return beanPropertyWriterArr;
        }
        int length = beanPropertyWriterArr.length;
        BeanPropertyWriter[] beanPropertyWriterArr2 = new BeanPropertyWriter[length];
        for (int i = 0; i < length; i++) {
            BeanPropertyWriter beanPropertyWriter = beanPropertyWriterArr[i];
            if (beanPropertyWriter != null) {
                beanPropertyWriterArr2[i] = beanPropertyWriter.rename(nameTransformer);
            }
        }
        return beanPropertyWriterArr2;
    }

    public void resolve(SerializerProvider serializerProvider) throws JsonMappingException {
        int length;
        if (this._filteredProps == null) {
            length = 0;
        } else {
            length = this._filteredProps.length;
        }
        int length2 = this._props.length;
        for (int i = 0; i < length2; i++) {
            BeanPropertyWriter beanPropertyWriter = this._props[i];
            if (!beanPropertyWriter.willSuppressNulls() && !beanPropertyWriter.hasNullSerializer()) {
                JsonSerializer findNullValueSerializer = serializerProvider.findNullValueSerializer(beanPropertyWriter);
                if (findNullValueSerializer != null) {
                    beanPropertyWriter.assignNullSerializer(findNullValueSerializer);
                    if (i < length) {
                        BeanPropertyWriter beanPropertyWriter2 = this._filteredProps[i];
                        if (beanPropertyWriter2 != null) {
                            beanPropertyWriter2.assignNullSerializer(findNullValueSerializer);
                        }
                    }
                }
            }
            if (!beanPropertyWriter.hasSerializer()) {
                JsonSerializer findConvertingSerializer = findConvertingSerializer(serializerProvider, beanPropertyWriter);
                if (findConvertingSerializer == null) {
                    JavaType serializationType = beanPropertyWriter.getSerializationType();
                    if (serializationType == null) {
                        serializationType = beanPropertyWriter.getType();
                        if (!serializationType.isFinal()) {
                            if (serializationType.isContainerType() || serializationType.containedTypeCount() > 0) {
                                beanPropertyWriter.setNonTrivialBaseType(serializationType);
                            }
                        }
                    }
                    findConvertingSerializer = serializerProvider.findValueSerializer(serializationType, (BeanProperty) beanPropertyWriter);
                    if (serializationType.isContainerType()) {
                        TypeSerializer typeSerializer = (TypeSerializer) serializationType.getContentType().getTypeHandler();
                        if (typeSerializer != null && (findConvertingSerializer instanceof ContainerSerializer)) {
                            findConvertingSerializer = ((ContainerSerializer) findConvertingSerializer).withValueTypeSerializer(typeSerializer);
                        }
                    }
                }
                beanPropertyWriter.assignSerializer(findConvertingSerializer);
                if (i < length) {
                    BeanPropertyWriter beanPropertyWriter3 = this._filteredProps[i];
                    if (beanPropertyWriter3 != null) {
                        beanPropertyWriter3.assignSerializer(findConvertingSerializer);
                    }
                }
            }
        }
        if (this._anyGetterWriter != null) {
            this._anyGetterWriter.resolve(serializerProvider);
        }
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<Object> findConvertingSerializer(SerializerProvider serializerProvider, BeanPropertyWriter beanPropertyWriter) throws JsonMappingException {
        JsonSerializer jsonSerializer = null;
        AnnotationIntrospector annotationIntrospector = serializerProvider.getAnnotationIntrospector();
        if (annotationIntrospector == null) {
            return null;
        }
        AnnotatedMember member = beanPropertyWriter.getMember();
        if (member == null) {
            return null;
        }
        Object findSerializationConverter = annotationIntrospector.findSerializationConverter(member);
        if (findSerializationConverter == null) {
            return null;
        }
        Converter converterInstance = serializerProvider.converterInstance(beanPropertyWriter.getMember(), findSerializationConverter);
        JavaType outputType = converterInstance.getOutputType(serializerProvider.getTypeFactory());
        if (!outputType.isJavaLangObject()) {
            jsonSerializer = serializerProvider.findValueSerializer(outputType, (BeanProperty) beanPropertyWriter);
        }
        return new StdDelegatingSerializer(converterInstance, outputType, jsonSerializer);
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x0038  */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x006d  */
    /* JADX WARNING: Removed duplicated region for block: B:36:0x008a  */
    /* JADX WARNING: Removed duplicated region for block: B:38:0x0090  */
    /* JADX WARNING: Removed duplicated region for block: B:41:0x0096  */
    /* JADX WARNING: Removed duplicated region for block: B:61:0x016f  */
    /* JADX WARNING: Removed duplicated region for block: B:66:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonSerializer<?> createContextual(com.fasterxml.jackson.databind.SerializerProvider r14, com.fasterxml.jackson.databind.BeanProperty r15) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r13 = this;
            r12 = 1
            r5 = 0
            r1 = 0
            com.fasterxml.jackson.databind.AnnotationIntrospector r6 = r14.getAnnotationIntrospector()
            if (r15 == 0) goto L_0x000b
            if (r6 != 0) goto L_0x009b
        L_0x000b:
            r2 = r1
        L_0x000c:
            com.fasterxml.jackson.databind.SerializationConfig r3 = r14.getConfig()
            if (r2 == 0) goto L_0x0173
            com.fasterxml.jackson.annotation.JsonFormat$Value r4 = r6.findFormat(r2)
            if (r4 == 0) goto L_0x0173
            com.fasterxml.jackson.annotation.JsonFormat$Shape r0 = r4.getShape()
            com.fasterxml.jackson.annotation.JsonFormat$Shape r7 = r13._serializationShape
            if (r0 == r7) goto L_0x0033
            java.lang.Class r7 = r13._handledType
            boolean r7 = r7.isEnum()
            if (r7 == 0) goto L_0x0033
            int[] r7 = com.fasterxml.jackson.databind.ser.std.BeanSerializerBase.C08901.$SwitchMap$com$fasterxml$jackson$annotation$JsonFormat$Shape
            int r8 = r0.ordinal()
            r7 = r7[r8]
            switch(r7) {
                case 1: goto L_0x00a2;
                case 2: goto L_0x00a2;
                case 3: goto L_0x00a2;
                default: goto L_0x0033;
            }
        L_0x0033:
            r3 = r0
        L_0x0034:
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r0 = r13._objectIdWriter
            if (r2 == 0) goto L_0x016f
            java.lang.String[] r4 = r6.findPropertiesToIgnore(r2, r12)
            com.fasterxml.jackson.databind.introspect.ObjectIdInfo r7 = r6.findObjectIdInfo(r2)
            if (r7 != 0) goto L_0x00b7
            if (r0 == 0) goto L_0x0059
            com.fasterxml.jackson.databind.introspect.ObjectIdInfo r0 = new com.fasterxml.jackson.databind.introspect.ObjectIdInfo
            com.fasterxml.jackson.databind.PropertyName r5 = NAME_FOR_OBJECT_REF
            r0.<init>(r5, r1, r1, r1)
            com.fasterxml.jackson.databind.introspect.ObjectIdInfo r0 = r6.findObjectReferenceInfo(r2, r0)
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r5 = r13._objectIdWriter
            boolean r0 = r0.getAlwaysAsId()
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r0 = r5.withAlwaysAsId(r0)
        L_0x0059:
            java.lang.Object r2 = r6.findFilterId(r2)
            if (r2 == 0) goto L_0x016c
            java.lang.Object r5 = r13._propertyFilterId
            if (r5 == 0) goto L_0x006b
            java.lang.Object r5 = r13._propertyFilterId
            boolean r5 = r2.equals(r5)
            if (r5 != 0) goto L_0x016c
        L_0x006b:
            if (r0 == 0) goto L_0x0169
            com.fasterxml.jackson.databind.JavaType r1 = r0.idType
            com.fasterxml.jackson.databind.JsonSerializer r1 = r14.findValueSerializer(r1, r15)
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r0 = r0.withSerializer(r1)
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r1 = r13._objectIdWriter
            if (r0 == r1) goto L_0x0169
            com.fasterxml.jackson.databind.ser.std.BeanSerializerBase r0 = r13.withObjectIdWriter(r0)
        L_0x007f:
            if (r4 == 0) goto L_0x0088
            int r1 = r4.length
            if (r1 == 0) goto L_0x0088
            com.fasterxml.jackson.databind.ser.std.BeanSerializerBase r0 = r0.withIgnorals(r4)
        L_0x0088:
            if (r2 == 0) goto L_0x008e
            com.fasterxml.jackson.databind.ser.std.BeanSerializerBase r0 = r0.withFilterId(r2)
        L_0x008e:
            if (r3 != 0) goto L_0x0092
            com.fasterxml.jackson.annotation.JsonFormat$Shape r3 = r13._serializationShape
        L_0x0092:
            com.fasterxml.jackson.annotation.JsonFormat$Shape r1 = com.fasterxml.jackson.annotation.JsonFormat.Shape.ARRAY
            if (r3 != r1) goto L_0x009a
            com.fasterxml.jackson.databind.ser.std.BeanSerializerBase r0 = r0.asArraySerializer()
        L_0x009a:
            return r0
        L_0x009b:
            com.fasterxml.jackson.databind.introspect.AnnotatedMember r0 = r15.getMember()
            r2 = r0
            goto L_0x000c
        L_0x00a2:
            java.lang.Class r0 = r13._handledType
            com.fasterxml.jackson.databind.BeanDescription r0 = r3.introspectClassAnnotations(r0)
            java.lang.Class r1 = r13._handledType
            com.fasterxml.jackson.databind.SerializationConfig r2 = r14.getConfig()
            com.fasterxml.jackson.databind.ser.std.EnumSerializer r0 = com.fasterxml.jackson.databind.ser.std.EnumSerializer.construct(r1, r2, r0, r4)
            com.fasterxml.jackson.databind.JsonSerializer r0 = r14.handlePrimaryContextualization(r0, r15)
            goto L_0x009a
        L_0x00b7:
            com.fasterxml.jackson.databind.introspect.ObjectIdInfo r7 = r6.findObjectReferenceInfo(r2, r7)
            java.lang.Class r0 = r7.getGeneratorType()
            com.fasterxml.jackson.databind.JavaType r8 = r14.constructType(r0)
            com.fasterxml.jackson.databind.type.TypeFactory r9 = r14.getTypeFactory()
            java.lang.Class<com.fasterxml.jackson.annotation.ObjectIdGenerator> r10 = com.fasterxml.jackson.annotation.ObjectIdGenerator.class
            com.fasterxml.jackson.databind.JavaType[] r8 = r9.findTypeParameters(r8, r10)
            r8 = r8[r5]
            java.lang.Class<com.fasterxml.jackson.annotation.ObjectIdGenerators$PropertyGenerator> r9 = com.fasterxml.jackson.annotation.ObjectIdGenerators.PropertyGenerator.class
            if (r0 != r9) goto L_0x0157
            com.fasterxml.jackson.databind.PropertyName r0 = r7.getPropertyName()
            java.lang.String r8 = r0.getSimpleName()
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r0 = r13._props
            int r9 = r0.length
            r0 = r5
        L_0x00df:
            if (r0 != r9) goto L_0x0110
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Invalid Object Id definition for "
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.Class r2 = r13._handledType
            java.lang.String r2 = r2.getName()
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = ": can not find property with name '"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r8)
            java.lang.String r2 = "'"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0110:
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r10 = r13._props
            r10 = r10[r0]
            java.lang.String r11 = r10.getName()
            boolean r11 = r8.equals(r11)
            if (r11 == 0) goto L_0x0154
            if (r0 <= 0) goto L_0x013e
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r8 = r13._props
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r9 = r13._props
            java.lang.System.arraycopy(r8, r5, r9, r12, r0)
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r8 = r13._props
            r8[r5] = r10
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r8 = r13._filteredProps
            if (r8 == 0) goto L_0x013e
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r8 = r13._filteredProps
            r8 = r8[r0]
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r9 = r13._filteredProps
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r11 = r13._filteredProps
            java.lang.System.arraycopy(r9, r5, r11, r12, r0)
            com.fasterxml.jackson.databind.ser.BeanPropertyWriter[] r0 = r13._filteredProps
            r0[r5] = r8
        L_0x013e:
            com.fasterxml.jackson.databind.JavaType r5 = r10.getType()
            com.fasterxml.jackson.databind.ser.impl.PropertyBasedObjectIdGenerator r8 = new com.fasterxml.jackson.databind.ser.impl.PropertyBasedObjectIdGenerator
            r8.<init>(r7, r10)
            r0 = r1
            com.fasterxml.jackson.databind.PropertyName r0 = (com.fasterxml.jackson.databind.PropertyName) r0
            boolean r7 = r7.getAlwaysAsId()
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r0 = com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter.construct(r5, r0, r8, r7)
            goto L_0x0059
        L_0x0154:
            int r0 = r0 + 1
            goto L_0x00df
        L_0x0157:
            com.fasterxml.jackson.annotation.ObjectIdGenerator r0 = r14.objectIdGeneratorInstance(r2, r7)
            com.fasterxml.jackson.databind.PropertyName r5 = r7.getPropertyName()
            boolean r7 = r7.getAlwaysAsId()
            com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter r0 = com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter.construct(r8, r5, r0, r7)
            goto L_0x0059
        L_0x0169:
            r0 = r13
            goto L_0x007f
        L_0x016c:
            r2 = r1
            goto L_0x006b
        L_0x016f:
            r2 = r1
            r4 = r1
            goto L_0x006b
        L_0x0173:
            r3 = r1
            goto L_0x0034
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ser.std.BeanSerializerBase.createContextual(com.fasterxml.jackson.databind.SerializerProvider, com.fasterxml.jackson.databind.BeanProperty):com.fasterxml.jackson.databind.JsonSerializer");
    }

    public Iterator<PropertyWriter> properties() {
        return Arrays.asList(this._props).iterator();
    }

    public boolean usesObjectId() {
        return this._objectIdWriter != null;
    }

    public void serializeWithType(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        if (this._objectIdWriter != null) {
            jsonGenerator.setCurrentValue(obj);
            _serializeWithObjectId(obj, jsonGenerator, serializerProvider, typeSerializer);
            return;
        }
        String _customTypeId = this._typeId == null ? null : _customTypeId(obj);
        if (_customTypeId == null) {
            typeSerializer.writeTypePrefixForObject(obj, jsonGenerator);
        } else {
            typeSerializer.writeCustomTypePrefixForObject(obj, jsonGenerator, _customTypeId);
        }
        jsonGenerator.setCurrentValue(obj);
        if (this._propertyFilterId != null) {
            serializeFieldsFiltered(obj, jsonGenerator, serializerProvider);
        } else {
            serializeFields(obj, jsonGenerator, serializerProvider);
        }
        if (_customTypeId == null) {
            typeSerializer.writeTypeSuffixForObject(obj, jsonGenerator);
        } else {
            typeSerializer.writeCustomTypeSuffixForObject(obj, jsonGenerator, _customTypeId);
        }
    }

    /* access modifiers changed from: protected */
    public final void _serializeWithObjectId(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, boolean z) throws IOException {
        ObjectIdWriter objectIdWriter = this._objectIdWriter;
        WritableObjectId findObjectId = serializerProvider.findObjectId(obj, objectIdWriter.generator);
        if (!findObjectId.writeAsId(jsonGenerator, serializerProvider, objectIdWriter)) {
            Object generateId = findObjectId.generateId(obj);
            if (objectIdWriter.alwaysAsId) {
                objectIdWriter.serializer.serialize(generateId, jsonGenerator, serializerProvider);
                return;
            }
            if (z) {
                jsonGenerator.writeStartObject();
            }
            findObjectId.writeAsField(jsonGenerator, serializerProvider, objectIdWriter);
            if (this._propertyFilterId != null) {
                serializeFieldsFiltered(obj, jsonGenerator, serializerProvider);
            } else {
                serializeFields(obj, jsonGenerator, serializerProvider);
            }
            if (z) {
                jsonGenerator.writeEndObject();
            }
        }
    }

    /* access modifiers changed from: protected */
    public final void _serializeWithObjectId(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer) throws IOException {
        ObjectIdWriter objectIdWriter = this._objectIdWriter;
        WritableObjectId findObjectId = serializerProvider.findObjectId(obj, objectIdWriter.generator);
        if (!findObjectId.writeAsId(jsonGenerator, serializerProvider, objectIdWriter)) {
            Object generateId = findObjectId.generateId(obj);
            if (objectIdWriter.alwaysAsId) {
                objectIdWriter.serializer.serialize(generateId, jsonGenerator, serializerProvider);
            } else {
                _serializeObjectId(obj, jsonGenerator, serializerProvider, typeSerializer, findObjectId);
            }
        }
    }

    /* access modifiers changed from: protected */
    public void _serializeObjectId(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider, TypeSerializer typeSerializer, WritableObjectId writableObjectId) throws IOException {
        ObjectIdWriter objectIdWriter = this._objectIdWriter;
        String _customTypeId = this._typeId == null ? null : _customTypeId(obj);
        if (_customTypeId == null) {
            typeSerializer.writeTypePrefixForObject(obj, jsonGenerator);
        } else {
            typeSerializer.writeCustomTypePrefixForObject(obj, jsonGenerator, _customTypeId);
        }
        writableObjectId.writeAsField(jsonGenerator, serializerProvider, objectIdWriter);
        if (this._propertyFilterId != null) {
            serializeFieldsFiltered(obj, jsonGenerator, serializerProvider);
        } else {
            serializeFields(obj, jsonGenerator, serializerProvider);
        }
        if (_customTypeId == null) {
            typeSerializer.writeTypeSuffixForObject(obj, jsonGenerator);
        } else {
            typeSerializer.writeCustomTypeSuffixForObject(obj, jsonGenerator, _customTypeId);
        }
    }

    /* access modifiers changed from: protected */
    public final String _customTypeId(Object obj) {
        Object value = this._typeId.getValue(obj);
        if (value == null) {
            return "";
        }
        return value instanceof String ? (String) value : value.toString();
    }

    /* access modifiers changed from: protected */
    public void serializeFields(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException {
        BeanPropertyWriter[] beanPropertyWriterArr;
        if (this._filteredProps == null || serializerProvider.getActiveView() == null) {
            beanPropertyWriterArr = this._props;
        } else {
            beanPropertyWriterArr = this._filteredProps;
        }
        int i = 0;
        try {
            int length = beanPropertyWriterArr.length;
            while (i < length) {
                BeanPropertyWriter beanPropertyWriter = beanPropertyWriterArr[i];
                if (beanPropertyWriter != null) {
                    beanPropertyWriter.serializeAsField(obj, jsonGenerator, serializerProvider);
                }
                i++;
            }
            if (this._anyGetterWriter != null) {
                this._anyGetterWriter.getAndSerialize(obj, jsonGenerator, serializerProvider);
            }
        } catch (Exception e) {
            wrapAndThrow(serializerProvider, (Throwable) e, obj, i == beanPropertyWriterArr.length ? "[anySetter]" : beanPropertyWriterArr[i].getName());
        } catch (StackOverflowError e2) {
            JsonMappingException jsonMappingException = new JsonMappingException((Closeable) jsonGenerator, "Infinite recursion (StackOverflowError)", (Throwable) e2);
            jsonMappingException.prependPath(new Reference(obj, i == beanPropertyWriterArr.length ? "[anySetter]" : beanPropertyWriterArr[i].getName()));
            throw jsonMappingException;
        }
    }

    /* access modifiers changed from: protected */
    public void serializeFieldsFiltered(Object obj, JsonGenerator jsonGenerator, SerializerProvider serializerProvider) throws IOException, JsonGenerationException {
        BeanPropertyWriter[] beanPropertyWriterArr;
        if (this._filteredProps == null || serializerProvider.getActiveView() == null) {
            beanPropertyWriterArr = this._props;
        } else {
            beanPropertyWriterArr = this._filteredProps;
        }
        PropertyFilter findPropertyFilter = findPropertyFilter(serializerProvider, this._propertyFilterId, obj);
        if (findPropertyFilter == null) {
            serializeFields(obj, jsonGenerator, serializerProvider);
            return;
        }
        try {
            for (BeanPropertyWriter beanPropertyWriter : beanPropertyWriterArr) {
                if (beanPropertyWriter != null) {
                    findPropertyFilter.serializeAsField(obj, jsonGenerator, serializerProvider, beanPropertyWriter);
                }
            }
            if (this._anyGetterWriter != null) {
                this._anyGetterWriter.getAndFilter(obj, jsonGenerator, serializerProvider, findPropertyFilter);
            }
        } catch (Exception e) {
            wrapAndThrow(serializerProvider, (Throwable) e, obj, 0 == beanPropertyWriterArr.length ? "[anySetter]" : beanPropertyWriterArr[0].getName());
        } catch (StackOverflowError e2) {
            JsonMappingException jsonMappingException = new JsonMappingException((Closeable) jsonGenerator, "Infinite recursion (StackOverflowError)", (Throwable) e2);
            jsonMappingException.prependPath(new Reference(obj, 0 == beanPropertyWriterArr.length ? "[anySetter]" : beanPropertyWriterArr[0].getName()));
            throw jsonMappingException;
        }
    }

    @Deprecated
    public JsonNode getSchema(SerializerProvider serializerProvider, Type type) throws JsonMappingException {
        PropertyFilter propertyFilter;
        ObjectNode createSchemaNode = createSchemaNode("object", true);
        JsonSerializableSchema jsonSerializableSchema = (JsonSerializableSchema) this._handledType.getAnnotation(JsonSerializableSchema.class);
        if (jsonSerializableSchema != null) {
            String id = jsonSerializableSchema.mo11568id();
            if (id != null && id.length() > 0) {
                createSchemaNode.put("id", id);
            }
        }
        ObjectNode objectNode = createSchemaNode.objectNode();
        if (this._propertyFilterId != null) {
            propertyFilter = findPropertyFilter(serializerProvider, this._propertyFilterId, null);
        } else {
            propertyFilter = null;
        }
        for (BeanPropertyWriter beanPropertyWriter : this._props) {
            if (propertyFilter == null) {
                beanPropertyWriter.depositSchemaProperty(objectNode, serializerProvider);
            } else {
                propertyFilter.depositSchemaProperty((PropertyWriter) beanPropertyWriter, objectNode, serializerProvider);
            }
        }
        createSchemaNode.set("properties", objectNode);
        return createSchemaNode;
    }

    public void acceptJsonFormatVisitor(JsonFormatVisitorWrapper jsonFormatVisitorWrapper, JavaType javaType) throws JsonMappingException {
        BeanPropertyWriter[] beanPropertyWriterArr;
        Class cls = null;
        int i = 0;
        if (jsonFormatVisitorWrapper != null) {
            JsonObjectFormatVisitor expectObjectFormat = jsonFormatVisitorWrapper.expectObjectFormat(javaType);
            if (expectObjectFormat != null) {
                SerializerProvider provider = jsonFormatVisitorWrapper.getProvider();
                if (this._propertyFilterId != null) {
                    PropertyFilter findPropertyFilter = findPropertyFilter(jsonFormatVisitorWrapper.getProvider(), this._propertyFilterId, null);
                    int length = this._props.length;
                    while (i < length) {
                        findPropertyFilter.depositSchemaProperty((PropertyWriter) this._props[i], expectObjectFormat, provider);
                        i++;
                    }
                    return;
                }
                if (!(this._filteredProps == null || provider == null)) {
                    cls = provider.getActiveView();
                }
                if (cls != null) {
                    beanPropertyWriterArr = this._filteredProps;
                } else {
                    beanPropertyWriterArr = this._props;
                }
                int length2 = beanPropertyWriterArr.length;
                while (i < length2) {
                    BeanPropertyWriter beanPropertyWriter = beanPropertyWriterArr[i];
                    if (beanPropertyWriter != null) {
                        beanPropertyWriter.depositSchemaProperty(expectObjectFormat, provider);
                    }
                    i++;
                }
            }
        }
    }
}
