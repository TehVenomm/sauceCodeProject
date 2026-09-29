.class public final Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;
.super Ljava/lang/Object;
.source "SerializerCache.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/databind/ser/SerializerCache;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "TypeKey"
.end annotation


# instance fields
.field protected _class:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field

.field protected _hashCode:I

.field protected _isTyped:Z

.field protected _type:Lcom/fasterxml/jackson/databind/JavaType;


# direct methods
.method public constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;Z)V
    .locals 1

    .line 216
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 217
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    const/4 v0, 0x0

    .line 218
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    .line 219
    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 220
    invoke-static {p1, p2}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Lcom/fasterxml/jackson/databind/JavaType;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method public constructor <init>(Ljava/lang/Class;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;Z)V"
        }
    .end annotation

    .line 209
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 210
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    const/4 v0, 0x0

    .line 211
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    .line 212
    iput-boolean p2, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 213
    invoke-static {p1, p2}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Ljava/lang/Class;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method private static final hash(Lcom/fasterxml/jackson/databind/JavaType;Z)I
    .locals 0

    .line 232
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/JavaType;->hashCode()I

    move-result p0

    add-int/lit8 p0, p0, -0x1

    if-eqz p1, :cond_0

    add-int/lit8 p0, p0, -0x1

    :cond_0
    return p0
.end method

.method private static final hash(Ljava/lang/Class;Z)I
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;Z)I"
        }
    .end annotation

    .line 224
    invoke-virtual {p0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/String;->hashCode()I

    move-result p0

    if-eqz p1, :cond_0

    add-int/lit8 p0, p0, 0x1

    :cond_0
    return p0
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x1

    if-ne p1, p0, :cond_1

    return v1

    .line 281
    :cond_1
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_2

    return v0

    .line 284
    :cond_2
    check-cast p1, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;

    .line 285
    iget-boolean v2, p1, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    iget-boolean v3, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    if-ne v2, v3, :cond_5

    .line 286
    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    if-eqz v2, :cond_4

    .line 287
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    if-ne p1, v2, :cond_3

    const/4 v0, 0x1

    :cond_3
    return v0

    .line 289
    :cond_4
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    iget-object p1, p1, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/JavaType;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_5
    return v0
.end method

.method public final hashCode()I
    .locals 1

    .line 267
    iget v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return v0
.end method

.method public resetTyped(Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 1

    .line 254
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    const/4 v0, 0x0

    .line 255
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    const/4 v0, 0x1

    .line 256
    iput-boolean v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 257
    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Lcom/fasterxml/jackson/databind/JavaType;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method public resetTyped(Ljava/lang/Class;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 240
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    .line 241
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    const/4 v0, 0x1

    .line 242
    iput-boolean v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 243
    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Ljava/lang/Class;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method public resetUntyped(Lcom/fasterxml/jackson/databind/JavaType;)V
    .locals 1

    .line 261
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    const/4 v0, 0x0

    .line 262
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    const/4 v0, 0x0

    .line 263
    iput-boolean v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 264
    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Lcom/fasterxml/jackson/databind/JavaType;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method public resetUntyped(Ljava/lang/Class;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 247
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    .line 248
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    const/4 v0, 0x0

    .line 249
    iput-boolean v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    .line 250
    invoke-static {p1, v0}, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->hash(Ljava/lang/Class;Z)I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_hashCode:I

    return-void
.end method

.method public final toString()Ljava/lang/String;
    .locals 2

    .line 270
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    if-eqz v0, :cond_0

    .line 271
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "{class: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_class:Ljava/lang/Class;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", typed? "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, "}"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0

    .line 273
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "{type: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", typed? "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/fasterxml/jackson/databind/ser/SerializerCache$TypeKey;->_isTyped:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, "}"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
