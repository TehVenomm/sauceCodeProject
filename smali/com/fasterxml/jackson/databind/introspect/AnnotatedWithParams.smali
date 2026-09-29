.class public abstract Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;
.super Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;
.source "AnnotatedWithParams.java"


# static fields
.field private static final serialVersionUID:J = 0x1L


# instance fields
.field protected final _paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;


# direct methods
.method protected constructor <init>(Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;)V
    .locals 0

    .line 35
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedMember;-><init>(Lcom/fasterxml/jackson/databind/introspect/AnnotatedClass;Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;)V

    .line 36
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    return-void
.end method


# virtual methods
.method public final addOrOverrideParam(ILjava/lang/annotation/Annotation;)V
    .locals 2

    .line 47
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    aget-object v0, v0, p1

    if-nez v0, :cond_0

    .line 49
    new-instance v0, Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    invoke-direct {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;-><init>()V

    .line 50
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    aput-object v0, v1, p1

    .line 52
    :cond_0
    invoke-virtual {v0, p2}, Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;->add(Ljava/lang/annotation/Annotation;)Z

    return-void
.end method

.method public abstract call()Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract call([Ljava/lang/Object;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public abstract call1(Ljava/lang/Object;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation
.end method

.method public final getAnnotation(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<A::",
            "Ljava/lang/annotation/Annotation;",
            ">(",
            "Ljava/lang/Class<",
            "TA;>;)TA;"
        }
    .end annotation

    .line 98
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_annotations:Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;->get(Ljava/lang/Class;)Ljava/lang/annotation/Annotation;

    move-result-object p1

    return-object p1
.end method

.method public final getAnnotationCount()I
    .locals 1

    .line 136
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_annotations:Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;->size()I

    move-result v0

    return v0
.end method

.method public abstract getGenericParameterType(I)Ljava/lang/reflect/Type;
.end method

.method public final getParameter(I)Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;
    .locals 3

    .line 118
    new-instance v0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getGenericParameterType(I)Ljava/lang/reflect/Type;

    move-result-object v1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getParameterAnnotations(I)Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    move-result-object v2

    invoke-direct {v0, p0, v1, v2, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;-><init>(Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;Ljava/lang/reflect/Type;Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;I)V

    return-object v0
.end method

.method public final getParameterAnnotations(I)Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;
    .locals 1

    .line 109
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    if-eqz v0, :cond_0

    if-ltz p1, :cond_0

    .line 110
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    array-length v0, v0

    if-ge p1, v0, :cond_0

    .line 111
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    aget-object p1, v0, p1

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public abstract getParameterCount()I
.end method

.method public abstract getRawParameterType(I)Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end method

.method protected getType(Lcom/fasterxml/jackson/databind/type/TypeBindings;[Ljava/lang/reflect/TypeVariable;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/type/TypeBindings;",
            "[",
            "Ljava/lang/reflect/TypeVariable<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JavaType;"
        }
    .end annotation

    if-eqz p2, :cond_1

    .line 74
    array-length v0, p2

    if-lez v0, :cond_1

    .line 75
    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->childInstance()Lcom/fasterxml/jackson/databind/type/TypeBindings;

    move-result-object p1

    .line 76
    array-length v0, p2

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_1

    aget-object v3, p2, v2

    .line 77
    invoke-interface {v3}, Ljava/lang/reflect/TypeVariable;->getName()Ljava/lang/String;

    move-result-object v4

    .line 79
    invoke-virtual {p1, v4}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->_addPlaceholder(Ljava/lang/String;)V

    .line 81
    invoke-interface {v3}, Ljava/lang/reflect/TypeVariable;->getBounds()[Ljava/lang/reflect/Type;

    move-result-object v4

    aget-object v4, v4, v1

    if-nez v4, :cond_0

    .line 82
    invoke-static {}, Lcom/fasterxml/jackson/databind/type/TypeFactory;->unknownType()Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v4

    goto :goto_1

    :cond_0
    invoke-virtual {p1, v4}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->resolveType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object v4

    .line 84
    :goto_1
    invoke-interface {v3}, Ljava/lang/reflect/TypeVariable;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p1, v3, v4}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->addBinding(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JavaType;)V

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 87
    :cond_1
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getGenericType()Ljava/lang/reflect/Type;

    move-result-object p2

    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->resolveType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p1

    return-object p1
.end method

.method protected replaceParameterAnnotations(ILcom/fasterxml/jackson/databind/introspect/AnnotationMap;)Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;
    .locals 1

    .line 61
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->_paramAnnotations:[Lcom/fasterxml/jackson/databind/introspect/AnnotationMap;

    aput-object p2, v0, p1

    .line 62
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getParameter(I)Lcom/fasterxml/jackson/databind/introspect/AnnotatedParameter;

    move-result-object p1

    return-object p1
.end method

.method public final resolveParameterType(ILcom/fasterxml/jackson/databind/type/TypeBindings;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 0

    .line 133
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/introspect/AnnotatedWithParams;->getGenericParameterType(I)Ljava/lang/reflect/Type;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/fasterxml/jackson/databind/type/TypeBindings;->resolveType(Ljava/lang/reflect/Type;)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p1

    return-object p1
.end method
