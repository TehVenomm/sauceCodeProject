.class public final Lcom/fasterxml/jackson/databind/util/ContainerBuilder;
.super Ljava/lang/Object;
.source "ContainerBuilder.java"


# static fields
.field private static final MAX_BUF:I = 0x3e8


# instance fields
.field private b:[Ljava/lang/Object;

.field private list:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private map:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private start:I

.field private tail:I


# direct methods
.method public constructor <init>(I)V
    .locals 0

    .line 46
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    and-int/lit8 p1, p1, -0x2

    .line 47
    new-array p1, p1, [Ljava/lang/Object;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    return-void
.end method

.method private _buildList(Z)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z)",
            "Ljava/util/List<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 186
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    sub-int/2addr v0, v1

    const/16 v1, 0x14

    const/4 v2, 0x2

    if-eqz p1, :cond_0

    if-ge v0, v2, :cond_3

    const/4 v0, 0x2

    goto :goto_0

    :cond_0
    if-ge v0, v1, :cond_1

    const/16 v0, 0x14

    goto :goto_0

    :cond_1
    const/16 p1, 0x3e8

    if-ge v0, p1, :cond_2

    shr-int/lit8 p1, v0, 0x1

    add-int/2addr v0, p1

    goto :goto_0

    :cond_2
    shr-int/lit8 p1, v0, 0x2

    add-int/2addr v0, p1

    .line 200
    :cond_3
    :goto_0
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1, v0}, Ljava/util/ArrayList;-><init>(I)V

    .line 201
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    :goto_1
    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    if-ge v0, v1, :cond_4

    .line 202
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    aget-object v1, v1, v0

    invoke-interface {p1, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 204
    :cond_4
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    iput v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    return-object p1
.end method

.method private _buildMap(Z)Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 221
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    sub-int/2addr v0, v1

    shr-int/lit8 v0, v0, 0x1

    if-eqz p1, :cond_2

    const/4 p1, 0x3

    if-gt v0, p1, :cond_0

    const/4 p1, 0x4

    goto :goto_0

    :cond_0
    const/16 p1, 0x28

    if-gt v0, p1, :cond_1

    shr-int/lit8 p1, v0, 0x1

    add-int/2addr p1, v0

    goto :goto_0

    :cond_1
    shr-int/lit8 p1, v0, 0x2

    shr-int/lit8 v1, v0, 0x4

    add-int/2addr p1, v1

    add-int/2addr p1, v0

    goto :goto_0

    :cond_2
    const/16 p1, 0xa

    if-ge v0, p1, :cond_3

    const/16 p1, 0x10

    goto :goto_0

    :cond_3
    const/16 p1, 0x3e8

    if-ge v0, p1, :cond_4

    shr-int/lit8 p1, v0, 0x1

    add-int/2addr p1, v0

    goto :goto_0

    .line 236
    :cond_4
    div-int/lit8 p1, v0, 0x3

    add-int/2addr p1, v0

    .line 239
    :goto_0
    new-instance v0, Ljava/util/LinkedHashMap;

    const v1, 0x3f4ccccd    # 0.8f

    invoke-direct {v0, p1, v1}, Ljava/util/LinkedHashMap;-><init>(IF)V

    .line 240
    iget p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    :goto_1
    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    if-ge p1, v1, :cond_5

    .line 241
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    aget-object v1, v1, p1

    check-cast v1, Ljava/lang/String;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    add-int/lit8 v3, p1, 0x1

    aget-object v2, v2, v3

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 p1, p1, 0x2

    goto :goto_1

    .line 243
    :cond_5
    iget p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    iput p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    return-object v0
.end method

.method private _expandList(Ljava/lang/Object;)V
    .locals 3

    .line 175
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v0, v0

    const/16 v1, 0x3e8

    if-ge v0, v1, :cond_0

    .line 176
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v1, v1

    shl-int/lit8 v1, v1, 0x1

    invoke-static {v0, v1}, Ljava/util/Arrays;->copyOf([Ljava/lang/Object;I)[Ljava/lang/Object;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    .line 177
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p1, v0, v1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 179
    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_buildList(Z)Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    .line 180
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :goto_0
    return-void
.end method

.method private _expandMap(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 3

    .line 209
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v0, v0

    const/16 v1, 0x3e8

    if-ge v0, v1, :cond_0

    .line 210
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v1, v1

    shl-int/lit8 v1, v1, 0x1

    invoke-static {v0, v1}, Ljava/util/Arrays;->copyOf([Ljava/lang/Object;I)[Ljava/lang/Object;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    .line 211
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p1, v0, v1

    .line 212
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p2, p1, v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 214
    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_buildMap(Z)Ljava/util/Map;

    move-result-object v0

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    .line 215
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    return-void
.end method


# virtual methods
.method public add(Ljava/lang/Object;)V
    .locals 3

    .line 94
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 95
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 96
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v1, v1

    if-lt v0, v1, :cond_1

    .line 97
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_expandList(Ljava/lang/Object;)V

    goto :goto_0

    .line 99
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p1, v0, v1

    :goto_0
    return-void
.end method

.method public bufferLength()I
    .locals 1

    .line 55
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v0, v0

    return v0
.end method

.method public canReuse()Z
    .locals 1

    .line 51
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public finishArray(I)[Ljava/lang/Object;
    .locals 3

    .line 129
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    .line 130
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    iget v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    invoke-static {v0, v1, v2}, Ljava/util/Arrays;->copyOfRange([Ljava/lang/Object;II)[Ljava/lang/Object;

    move-result-object v0

    goto :goto_0

    .line 132
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iget v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    sub-int/2addr v1, v2

    new-array v1, v1, [Ljava/lang/Object;

    invoke-interface {v0, v1}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    const/4 v1, 0x0

    .line 133
    iput-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    .line 135
    :goto_0
    iput p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    return-object v0
.end method

.method public finishArray(ILjava/lang/Class;)[Ljava/lang/Object;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(I",
            "Ljava/lang/Class<",
            "TT;>;)[",
            "Ljava/lang/Object;"
        }
    .end annotation

    .line 141
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    sub-int/2addr v0, v1

    .line 143
    invoke-static {p2, v0}, Ljava/lang/reflect/Array;->newInstance(Ljava/lang/Class;I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, [Ljava/lang/Object;

    .line 145
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v1, :cond_0

    .line 146
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    const/4 v3, 0x0

    invoke-static {v1, v2, p2, v3, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    goto :goto_0

    .line 148
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    invoke-interface {v0, p2}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p2

    const/4 v0, 0x0

    .line 149
    iput-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    .line 151
    :goto_0
    iput p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    return-object p2
.end method

.method public finishList(I)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/List<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 116
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 118
    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_buildList(Z)Ljava/util/List;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 120
    iput-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    .line 122
    :goto_0
    iput p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    return-object v0
.end method

.method public finishMap(I)Ljava/util/Map;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 157
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 160
    invoke-direct {p0, v0}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_buildMap(Z)Ljava/util/Map;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 162
    iput-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    .line 164
    :goto_0
    iput p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    return-object v0
.end method

.method public put(Ljava/lang/String;Ljava/lang/Object;)V
    .locals 3

    .line 104
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-eqz v0, :cond_0

    .line 105
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 106
    :cond_0
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v0, v0, 0x2

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    array-length v1, v1

    if-le v0, v1, :cond_1

    .line 107
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->_expandMap(Ljava/lang/String;Ljava/lang/Object;)V

    goto :goto_0

    .line 109
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p1, v0, v1

    .line 110
    iget-object p1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->b:[Ljava/lang/Object;

    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    add-int/lit8 v1, v0, 0x1

    iput v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    aput-object p2, p1, v0

    :goto_0
    return-void
.end method

.method public start()I
    .locals 2

    .line 65
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-nez v0, :cond_0

    .line 68
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    .line 69
    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iput v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    return v0

    .line 66
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0
.end method

.method public startList(Ljava/lang/Object;)I
    .locals 2

    .line 74
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-nez v0, :cond_0

    .line 77
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    .line 78
    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iput v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    .line 79
    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->add(Ljava/lang/Object;)V

    return v0

    .line 75
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1
.end method

.method public startMap(Ljava/lang/String;Ljava/lang/Object;)I
    .locals 2

    .line 84
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->list:Ljava/util/List;

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->map:Ljava/util/Map;

    if-nez v0, :cond_0

    .line 87
    iget v0, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    .line 88
    iget v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->tail:I

    iput v1, p0, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->start:I

    .line 89
    invoke-virtual {p0, p1, p2}, Lcom/fasterxml/jackson/databind/util/ContainerBuilder;->put(Ljava/lang/String;Ljava/lang/Object;)V

    return v0

    .line 85
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1
.end method
