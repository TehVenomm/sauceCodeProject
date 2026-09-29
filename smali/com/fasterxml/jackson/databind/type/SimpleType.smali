.class public final Lcom/fasterxml/jackson/databind/type/SimpleType;
.super Lcom/fasterxml/jackson/databind/type/TypeBase;
.source "SimpleType.java"


# static fields
.field private static final serialVersionUID:J = -0xb1b80aa96a43468L


# instance fields
.field protected final _typeNames:[Ljava/lang/String;

.field protected final _typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

.field protected final _typeParametersFor:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field


# direct methods
.method protected constructor <init>(Ljava/lang/Class;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)V"
        }
    .end annotation

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    move-object v0, p0

    move-object v1, p1

    .line 44
    invoke-direct/range {v0 .. v7}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-void
.end method

.method protected constructor <init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;Z)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;[",
            "Ljava/lang/String;",
            "[",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            "Z)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v7, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move-object v5, p5

    move v6, p6

    .line 54
    invoke-direct/range {v0 .. v7}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-void
.end method

.method protected constructor <init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;[",
            "Ljava/lang/String;",
            "[",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            "Z",
            "Ljava/lang/Class<",
            "*>;)V"
        }
    .end annotation

    const/4 v2, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v3, p4

    move-object v4, p5

    move v5, p6

    .line 68
    invoke-direct/range {v0 .. v5}, Lcom/fasterxml/jackson/databind/type/TypeBase;-><init>(Ljava/lang/Class;ILjava/lang/Object;Ljava/lang/Object;Z)V

    if-eqz p2, :cond_1

    .line 69
    array-length p1, p2

    if-nez p1, :cond_0

    goto :goto_0

    .line 73
    :cond_0
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    .line 74
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    .line 70
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    .line 71
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    .line 76
    :goto_1
    iput-object p7, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    return-void
.end method

.method public static construct(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/type/SimpleType;"
        }
    .end annotation

    .line 116
    const-class v0, Ljava/util/Map;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 119
    const-class v0, Ljava/util/Collection;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 123
    invoke-virtual {p0}, Ljava/lang/Class;->isArray()Z

    move-result v0

    if-nez v0, :cond_0

    .line 126
    new-instance v0, Lcom/fasterxml/jackson/databind/type/SimpleType;

    invoke-direct {v0, p0}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;)V

    return-object v0

    .line 124
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct SimpleType for an array (class: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ")"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 120
    :cond_1
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct SimpleType for a Collection (class: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ")"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 117
    :cond_2
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can not construct SimpleType for a Map (class: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ")"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static constructUnsafe(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/type/SimpleType;"
        }
    .end annotation

    .line 86
    new-instance v8, Lcom/fasterxml/jackson/databind/type/SimpleType;

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    move-object v0, v8

    move-object v1, p0

    invoke-direct/range {v0 .. v7}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-object v8
.end method


# virtual methods
.method protected _narrow(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JavaType;"
        }
    .end annotation

    .line 93
    new-instance v8, Lcom/fasterxml/jackson/databind/type/SimpleType;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_valueHandler:Ljava/lang/Object;

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeHandler:Ljava/lang/Object;

    iget-boolean v6, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_asStatic:Z

    iget-object v7, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    move-object v0, v8

    move-object v1, p1

    invoke-direct/range {v0 .. v7}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-object v8
.end method

.method protected buildCanonicalName()Ljava/lang/String;
    .locals 8

    .line 164
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 165
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 166
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    array-length v1, v1

    if-lez v1, :cond_2

    const/16 v1, 0x3c

    .line 167
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 169
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    array-length v2, v1

    const/4 v3, 0x0

    const/4 v4, 0x1

    const/4 v4, 0x0

    const/4 v5, 0x1

    :goto_0
    if-ge v4, v2, :cond_1

    aget-object v6, v1, v4

    if-eqz v5, :cond_0

    const/4 v5, 0x0

    goto :goto_1

    :cond_0
    const/16 v7, 0x2c

    .line 173
    invoke-virtual {v0, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 175
    :goto_1
    invoke-virtual {v6}, Lcom/fasterxml/jackson/databind/JavaType;->toCanonical()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v0, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_1
    const/16 v1, 0x3e

    .line 177
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 179
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public bridge synthetic containedType(I)Lcom/fasterxml/jackson/core/type/ResolvedType;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->containedType(I)Lcom/fasterxml/jackson/databind/JavaType;

    move-result-object p1

    return-object p1
.end method

.method public containedType(I)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    if-ltz p1, :cond_1

    .line 199
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    array-length v0, v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 202
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    aget-object p1, v0, p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public containedTypeCount()I
    .locals 1

    .line 193
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    array-length v0, v0

    :goto_0
    return v0
.end method

.method public containedTypeName(I)Ljava/lang/String;
    .locals 1

    if-ltz p1, :cond_1

    .line 208
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    array-length v0, v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 211
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    aget-object p1, v0, p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 7

    const/4 v0, 0x1

    if-ne p1, p0, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-nez p1, :cond_1

    return v1

    .line 258
    :cond_1
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_2

    return v1

    .line 260
    :cond_2
    check-cast p1, Lcom/fasterxml/jackson/databind/type/SimpleType;

    .line 263
    iget-object v2, p1, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    if-eq v2, v3, :cond_3

    return v1

    .line 266
    :cond_3
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    .line 267
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    if-nez v2, :cond_6

    if-eqz p1, :cond_5

    .line 269
    array-length p1, p1

    if-nez p1, :cond_4

    goto :goto_0

    :cond_4
    const/4 v0, 0x0

    :cond_5
    :goto_0
    return v0

    :cond_6
    if-nez p1, :cond_7

    return v1

    .line 273
    :cond_7
    array-length v3, v2

    array-length v4, p1

    if-eq v3, v4, :cond_8

    return v1

    .line 274
    :cond_8
    array-length v3, v2

    const/4 v4, 0x0

    :goto_1
    if-ge v4, v3, :cond_a

    .line 275
    aget-object v5, v2, v4

    aget-object v6, p1, v4

    invoke-virtual {v5, v6}, Lcom/fasterxml/jackson/databind/JavaType;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-nez v5, :cond_9

    return v1

    :cond_9
    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    :cond_a
    return v0
.end method

.method public getErasedSignature(Ljava/lang/StringBuilder;)Ljava/lang/StringBuilder;
    .locals 2

    .line 221
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    const/4 v1, 0x1

    invoke-static {v0, p1, v1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->_classSignature(Ljava/lang/Class;Ljava/lang/StringBuilder;Z)Ljava/lang/StringBuilder;

    move-result-object p1

    return-object p1
.end method

.method public getGenericSignature(Ljava/lang/StringBuilder;)Ljava/lang/StringBuilder;
    .locals 4

    .line 227
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    const/4 v1, 0x0

    invoke-static {v0, p1, v1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->_classSignature(Ljava/lang/Class;Ljava/lang/StringBuilder;Z)Ljava/lang/StringBuilder;

    .line 228
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    if-eqz v0, :cond_1

    const/16 v0, 0x3c

    .line 229
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 230
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    array-length v2, v0

    :goto_0
    if-ge v1, v2, :cond_0

    aget-object v3, v0, v1

    .line 231
    invoke-virtual {v3, p1}, Lcom/fasterxml/jackson/databind/JavaType;->getGenericSignature(Ljava/lang/StringBuilder;)Ljava/lang/StringBuilder;

    move-result-object p1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    const/16 v0, 0x3e

    .line 233
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_1
    const/16 v0, 0x3b

    .line 235
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    return-object p1
.end method

.method public getParameterSource()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 216
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    return-object v0
.end method

.method public isContainerType()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public narrowContentsBy(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JavaType;"
        }
    .end annotation

    .line 101
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Internal error: SimpleType.narrowContentsBy() should never be called"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    .line 248
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v1, 0x28

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    const-string v1, "[simple type, class "

    .line 249
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/type/SimpleType;->buildCanonicalName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x5d

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    .line 250
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public widenContentsBy(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Lcom/fasterxml/jackson/databind/JavaType;"
        }
    .end annotation

    .line 108
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Internal error: SimpleType.widenContentsBy() should never be called"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public withContentTypeHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 138
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Simple types have no content types; can not call withContenTypeHandler()"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public bridge synthetic withContentValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->withContentValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;

    move-result-object p1

    return-object p1
.end method

.method public withContentValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 1

    .line 152
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Simple types have no content types; can not call withContenValueHandler()"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public bridge synthetic withStaticTyping()Lcom/fasterxml/jackson/databind/JavaType;
    .locals 1

    .line 13
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/type/SimpleType;->withStaticTyping()Lcom/fasterxml/jackson/databind/type/SimpleType;

    move-result-object v0

    return-object v0
.end method

.method public withStaticTyping()Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 9

    .line 157
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_asStatic:Z

    if-eqz v0, :cond_0

    move-object v0, p0

    goto :goto_0

    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/type/SimpleType;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    iget-object v5, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_valueHandler:Ljava/lang/Object;

    iget-object v6, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeHandler:Ljava/lang/Object;

    iget-boolean v7, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_asStatic:Z

    iget-object v8, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    move-object v1, v0

    invoke-direct/range {v1 .. v8}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    :goto_0
    return-object v0
.end method

.method public bridge synthetic withTypeHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->withTypeHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;

    move-result-object p1

    return-object p1
.end method

.method public withTypeHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 9

    .line 132
    new-instance v8, Lcom/fasterxml/jackson/databind/type/SimpleType;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_valueHandler:Ljava/lang/Object;

    iget-boolean v6, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_asStatic:Z

    iget-object v7, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    move-object v0, v8

    move-object v5, p1

    invoke-direct/range {v0 .. v7}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-object v8
.end method

.method public bridge synthetic withValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JavaType;
    .locals 0

    .line 13
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/type/SimpleType;->withValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;

    move-result-object p1

    return-object p1
.end method

.method public withValueHandler(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/type/SimpleType;
    .locals 9

    .line 143
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_valueHandler:Ljava/lang/Object;

    if-ne p1, v0, :cond_0

    return-object p0

    .line 146
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/type/SimpleType;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_class:Ljava/lang/Class;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeNames:[Ljava/lang/String;

    iget-object v4, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParameters:[Lcom/fasterxml/jackson/databind/JavaType;

    iget-object v6, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeHandler:Ljava/lang/Object;

    iget-boolean v7, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_asStatic:Z

    iget-object v8, p0, Lcom/fasterxml/jackson/databind/type/SimpleType;->_typeParametersFor:Ljava/lang/Class;

    move-object v1, v0

    move-object v5, p1

    invoke-direct/range {v1 .. v8}, Lcom/fasterxml/jackson/databind/type/SimpleType;-><init>(Ljava/lang/Class;[Ljava/lang/String;[Lcom/fasterxml/jackson/databind/JavaType;Ljava/lang/Object;Ljava/lang/Object;ZLjava/lang/Class;)V

    return-object v0
.end method
